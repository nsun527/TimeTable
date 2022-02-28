﻿#nullable enable

using System.Globalization;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Background;
using Microsoft.Toolkit.Uwp.Notifications;
using static System.DayOfWeek;

namespace TimeTableUWP.Pages;
public sealed partial class TimeTablePage : Page
{
    private const int refreshTerm = 600;
    private async Task InitializeUIAsync()
    {
        // Set Color
        foreach (Border border in DayBorders)
            border.Background = new SolidColorBrush(Info.Settings.ColorType);

        foreach (ComboBox combo in ComboBoxes)
            combo.BorderBrush = new SolidColorBrush(Info.Settings.ColorType);

        SetSubText();

        if (Info.User.Status is LoadStatus.NewlyInstalled)
            await ShowMessageAsync(Messages.Welcome, Datas.GTTWithVer, Info.Settings.Theme, XamlRoot);

        else if (Info.User.Status is LoadStatus.Updated)
            await ShowMessageAsync(Messages.Updated, "New version installed", xamlRoot: XamlRoot);

        // TimeTablePage를 다시 띄웠을 때 똑같은 메시지가 뜨지 않게
        Info.User.Status = LoadStatus.Normal;
    }

    /// <summary>
    /// Sets the subtitle for TimeTablePage
    /// </summary>
    private void SetSubText() => mainText2.Text = Info.User.ActivationLevel switch
    {
        ActivationLevel.Developer => SubTitles.Developer,
        ActivationLevel.Azure => SubTitles.Azure,
        ActivationLevel.Bisque => SubTitles.Bisque,
        ActivationLevel.Coral => SubTitles.Coral,
        _ => string.Empty
    };

    private void DrawTimeTable()
    {
        TimeTable.ResetClass(Info.User.Class);
        AssignButtonsByTable(TimeTable.Table!);
    }

    private void AssignButtonsByTable(TimeTable subjectTable)
    {
        IEnumerable<Subject>? subjects = subjectTable.Data.Cast<Subject>();
        IEnumerable<(Button btn, Subject subject)>? lists = Buttons.Zip(subjects, (Button btn, Subject subject) => (btn, subject));
        foreach ((Button btn, Subject subject) in lists)
            btn.Content = subject.Name;
    }

    private async Task LoopTimeAsync()
    {
        (int day, int time) pos;
        // bool invoke = true;
        while (true)
        {
            try
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, SetClock);
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, RefreshColor);

                if (DateTime.Now.DayOfWeek is Sunday or Saturday || DateTime.Now.Hour is >= 17 or < 9 or 13)
                {
                    await Task.Delay(refreshTerm); // 500ms 마다 반복하기
                    continue;
                }

                pos.day = (int)DateTime.Now.DayOfWeek;
                pos.time = DateTime.Now.Hour switch
                {
                    9 or 10 or 11 or 12 => DateTime.Now.Hour - 8,
                    14 or 15 or 16 => DateTime.Now.Hour - 9,
                    _ => throw new DataAccessException($"Hour is not in 9, 10, 11, 12, 14, 15, 16. given {DateTime.Now.Hour}.")
                };
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => ChangeCellColor(pos));
                await Task.Delay(refreshTerm); // 500ms 마다 반복하기

#if TOAST_ENABLED

                // GTT 4부터 모든 사용자에게 3분전 알림 개방
                // if (SaveData.IsNotDeveloperOrInsider) continue;

                // 동아리(2)나 홈커밍일 때는 토스트 알림 없음.
                if (pos is ((int)Friday, 7) or ((int)Friday, 6)) continue;

                // 4시에는 실행하면 안 된다!
                if (DateTime.Now.Hour is 16) continue;

                if (DateTime.Now.Minute is 57 && invoke)
                {
                    invoke = false;
                    _ = SendToast(pos); // 여기까진 알고리즘 완벽.
                }
                if (DateTime.Now.Minute is 58 && invoke is false)
                {
                    invoke = true; // 알람 폭탄 방지
                }
#endif
            }
            catch (Exception e)
            {
                await TimeTableException.HandleException(e);
            }
        }
    }

#if TOAST_ENABLED
    private async Task SendToast((int day, int time) pos)
    {
        int hour = DateTime.Now.Hour + 1; // 현재의 다음 시간이니까 +1

        // TODO: 이때는 뭘 하지 버튼 없는 토스트?
        if (pos is ((int)Monday, 6) or ((int)Monday, 7) or ((int)Friday, 5))
            return;

        // TimeTable 표에서 현재 날짜와 시간을 통해 과목 꺼내기. time은 하나 +1 시켜야 함.
        string? subject = TimeTable.Table?.AtPos(pos.day - 1, pos.time).ToString();
        if (subject is null)
            return;

        ToastContentBuilder toast = new ToastContentBuilder()
            .AddAppLogoOverride(new Uri("ms-appx:///Assets/SecurityAndMaintenance.png")) // 동그라미 i 표시
            .AddText("다음 수업이 3분 이내에 시작됩니다.", hintMaxLines: 1); // 안내문

        // 무음모드가 아닐 때만 알람음 설정
        toast.AddAudio(new Uri("ms-appx:///Assets/Alarm01.wav"), false, Info.Settings.SilentMode);

        if (GetClassZoomLink().TryGetValue(subject, out OnlineLink? online) is false || (online is null))
        {
            toast.AddText($"[{subject}]")
                 .AddText(hour > 12 ?
                $"{hour - 12}:00 PM - {hour - 12}:50 PM" :
                $"{hour}:00 AM - {hour}:50 AM").Show();
            return;
        }

        toast.AddText($"[{subject}] - {online.Teacher} 선생님") // 과목 및 선생님
            .AddText(hour > 12 ?
                $"{hour - 12}:00 PM - {hour - 12}:50 PM" :
                $"{hour}:00 AM - {hour}:50 AM");

        if (online.Zoom is not null)
            toast.AddButton(new ToastButton()
                .SetContent("ZOOM 열기")
                .AddArgument("action", "zoom")
                .AddArgument("zoomUrl", online.Zoom)
                .SetBackgroundActivation());

        if (online.Classroom is not null)
            toast.AddButton(new ToastButton()
                .SetContent("클래스룸 열기")
                .AddArgument("action", "classRoom")
                .AddArgument("classRoomUrl", online.Classroom)
                .SetBackgroundActivation());

        toast.Show();

        string taskName = $"ToastZoomOpen-{pos.day}{pos.time}";

        if (BackgroundTaskRegistration.AllTasks.Any(i => i.Value.Name.Equals(taskName)))
            return;

        BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();
        BackgroundTaskBuilder builder = new() { Name = taskName };
        builder.SetTrigger(new ToastNotificationActionTrigger());
        BackgroundTaskRegistration registration = builder.Register();
    }
#endif
    private void ChangeCellColor((int day, int time) pos)
    {
        SolidColorBrush brush = new(Info.Settings.ColorType);
        Buttons.ElementAt((7 * (pos.day - 1)) + (pos.time - 1)).Background = brush;
        if (pos.time <= 6)
            Buttons.ElementAt((7 * (pos.day - 1)) + pos.time).Foreground = brush;
    }

    private void RefreshColor()
    {
        foreach (var item in Buttons)
        {
            item.RequestedTheme = Info.Settings.IsDarkMode ? ElementTheme.Dark : ElementTheme.Light;
            item.Background = new SolidColorBrush(Info.Settings.IsDarkMode
                ? Color.FromArgb(0xEE, 0x34, 0x34, 0x34)
                : Color.FromArgb(0xEE, 0xF4, 0xF4, 0xF4));
            item.Foreground = new SolidColorBrush(Info.Settings.IsDarkMode ? Colors.White : Colors.Black);
        }
    }

    private void SetClock()
    {
        clock.Text = DateTime.Now.ToString(Info.Settings.Use24Hour ? "HH:mm" : "hh:mm");
        amorpmBox.Text = Info.Settings.Use24Hour
            ? string.Empty : DateTime.Now.ToString("tt", CultureInfo.InvariantCulture);
        dateBlock.Text = DateTime.Now.ToString(Info.Settings.DateFormat switch
        {
            DateType.MMDDYYYY => "MM/dd/yyyy",
            DateType.YYYYMMDD => "yyyy/MM/dd",
            DateType.YYYYMMDD2 => "yyyy-MM-dd",
            _ => throw new NotImplementedException()
        });
        dayBlock.Text = DateTime.Now.ToString("ddd", CultureInfo.CreateSpecificCulture("en-US"));
    }
}
