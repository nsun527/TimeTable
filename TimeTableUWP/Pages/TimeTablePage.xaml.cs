#nullable enable

using TimeTableCore.Grade3.Semester2;
using TimeTableUWP.Helpers;

namespace TimeTableUWP.Pages;

public sealed partial class TimeTablePage : Page
{
    private TimeTables TimeTable { get; } = new();
    private OnlineLinks Online { get; } = new();
    private OnlineLinksDac Links { get; } = new();
    private bool NotInvoked { get; set; } = true;

    public TimeTablePage()
    {
        InitializeComponent();
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        _ = LoopTimeAsync().ConfigureAwait(false); // detach.

        Task getLinks = Task.CompletedTask;
        if (Info.Settings.HotReload)
        {
            getLinks = Links.LoadLinksAsync();
        }
        if (Info.User.Status is not LoadStatus.NewlyInstalled)
        {
            SelectionChangedEventHandler handler = new(ComboBox_SelectionChanged);
            classComboBox.SelectionChanged -= handler;
            classComboBox.SelectedIndex = Info.User.Class - 1;
            classComboBox.SelectionChanged += handler;

            if (Social.Selected != Social.Default)
                socialComboBox.Select(Subjects.Social);

            if (ttc::Language.Selected != ttc::Language.Default)
                langComboBox.Select(Subjects.Language);

            if (Global1.Selected != Global1.Default)
                global1ComboBox.Select(Subjects.Global1);

            if (Global2.Selected != Global2.Default)
                global2ComboBox.Select(Subjects.Global2);
            // 공통 콤보박스 제한
            DisableComboBoxBySubjects();
        }

        await InitializeUIAsync();
        if (Info.User.Class != 0)
            DrawTimeTable();

        await getLinks;
    }

    #region ENEMERATORS
    private IEnumerable<Border> DayBorders
    {
        get
        {
            yield return monBorder;
            yield return tueBorder;
            yield return wedBorder;
            yield return thuBorder;
            yield return friBorder;
        }
    }

    private IEnumerable<ComboBox> ComboBoxes
    {
        get
        {
            yield return classComboBox;
            yield return socialComboBox;
            yield return langComboBox;
            yield return global1ComboBox;
            yield return global2ComboBox;
        }
    }

    // Buttons enumerator. Mon1 -> Mon2 -> ...
    private IEnumerable<Button> Buttons
    {
        get
        {
            yield return mon1Button;
            yield return mon2Button;
            yield return mon3Button;
            yield return mon4Button;
            yield return mon5Button;
            yield return mon6Button;
            yield return mon7Button;

            yield return tue1Button;
            yield return tue2Button;
            yield return tue3Button;
            yield return tue4Button;
            yield return tue5Button;
            yield return tue6Button;
            yield return tue7Button;

            yield return wed1Button;
            yield return wed2Button;
            yield return wed3Button;
            yield return wed4Button;
            yield return wed5Button;
            yield return wed6Button;
            yield return wed7Button;

            yield return thu1Button;
            yield return thu2Button;
            yield return thu3Button;
            yield return thu4Button;
            yield return thu5Button;
            yield return thu6Button;
            yield return thu7Button;

            yield return fri1Button;
            yield return fri2Button;
            yield return fri3Button;
            yield return fri4Button;
            yield return fri5Button;
            yield return fri6Button;
            yield return fri7Button;
        }
    }

    #endregion

    private void EnableAllCombobox()
    => Enable(socialComboBox, langComboBox, global1ComboBox, global2ComboBox);

    // TODO: need ref?
    private void DisableComboBoxByClass(ComboBox cb, Subject subject)
    {
        cb.Select(subject);
        Disable(cb);
    }

    // 값 넣고 -> Disable(comboBox) -> Empty(comboBox)
    private void DisableComboBoxBySubjects()
    {
        if (TimeTable.Table is null)
            return; // Class is not yet selected, or a bug.

        EnableAllCombobox();
        var common = TimeTable.Table.CommonSubject;
        if (common.HasFlag(Common.Social))
            DisableComboBoxByClass(socialComboBox, Social.Selected);

        if (common.HasFlag(Common.Language))
            DisableComboBoxByClass(langComboBox, ttc::Language.Selected);

        if (common.HasFlag(Common.Global1))
            DisableComboBoxByClass(global1ComboBox, Global1.Selected);

        if (common.HasFlag(Common.Global2))
            DisableComboBoxByClass(global2ComboBox, Global2.Selected);
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs _)
    {
        if (sender is not ComboBox comboBox || comboBox.SelectedItem is not string selected)
            return;

        switch (comboBox.Name)
        {
            case "classComboBox":
                EnableAllCombobox();
                Subjects.ResetSelectiveSubjects();
                Empty(socialComboBox, langComboBox, global1ComboBox, global2ComboBox);

                Info.User.Class = comboBox.SelectedIndex + 1;
                TimeTable.ResetClass(Info.User.Class);
                DisableComboBoxBySubjects();
                break;

            case "socialComboBox": 
                Subjects.Social.SetAs(selected);
                break;

            case "langComboBox":
                Subjects.Language.SetAs(selected);
                break;

            case "global1ComboBox":
                Subjects.Global1.SetAs(selected);
                break;

            case "global2ComboBox":
                Subjects.Global2.SetAs(selected);
                break;
        }
        DrawTimeTable();
    }

    private async Task ShowSubjectZoom(string subjectCellName)
    {
        if (Info.User.ActivationLevel is ActivationLevel.None)
        {
            // 인증을 하지 않았다면 return
            if (await User.ActivateAsync() is false)
                return;

            SetSubText();
        }

        if (NotInvoked && Info.Settings.HotReload && Links.Dictionary.Count is 0)
        {
            NotInvoked = false;
            return;
        }

        if (GetOnlineLink(subjectCellName, out var online))
        {
            ZoomDialog contentDialog = new(Info.User.Class, subjectCellName, online!);
            await contentDialog.ShowAsync();
        }
        else
        {
            await ShowMessageAsync(string.Format(Messages.Dialog.NotAvailable, subjectCellName, Info.Settings.Theme),
            "No data", Info.Settings.Theme);
        }
    }

    private bool GetOnlineLink(string cell, out OnlineLink? online)
    {
        if (Info.Settings.HotReload 
            && Links.GetLinks(Info.User.Class).TryGetValue(cell, out online)
            && online is not null)
        {
            return true;
        }
        else if (GetClassZoomLink().TryGetValue(cell, out online) && online is not null)
        {
            return true;
        }
        return false;
    }

    private Dictionary<string, OnlineLink?> GetClassZoomLink() => Info.User.Class switch
    {
        1 => Online.Class1,
        2 => Online.Class2,
        3 => Online.Class3,
        4 => Online.Class4,
        5 => Online.Class5,
        6 => Online.Class6,
        7 => Online.Class7,
        8 => Online.Class8,
        _ => throw new DataAccessException(
            $"GetClassZoomLink(): Class out of range: 1 to 8 expected, but given {Info.User.Class}")
    };

    private async void TableButtons_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button btn || btn.Content is null)   
            return;

        if (btn.Content is string cellName)
            await ShowSubjectZoom(cellName);

        else if (btn.Content is null)
            await ShowMessageAsync("Please select your class first.", "Error", Info.Settings.Theme);
    }

    private async void SpecialButtons_Click(object sender, RoutedEventArgs _)
    {
        if (sender is not Button btn)
            return;
        
        (string msg, string txt) = btn.Name switch
        {
            "fri5Button" or "fri6Button" => ("창의적 체험활동 시간입니다.", "창의적 체험활동"),
            "mon6Button" or "mon7Button" => ("창의적 체험활동 시간입니다.", "창의적 체험활동"),
            // "wed7Button" => ("자기주도학습 시간입니다.", "수업 없음"),
            "fri7Button" => ("즐거운 홈커밍 데이 :)", "Homecoming"),
            _ => throw new TableCellException($"SpecialButtons_Click(): No candidate to show for button '{btn.Name}'")
        };
        await ShowMessageAsync(msg, txt, Info.Settings.Theme);
    }

    private void mainGrid_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (mainGrid.ActualWidth < 1030)
        {
            foreach (var button in Buttons)
                button.FontSize = 19;
            
            clockGrid.Background = new SolidColorBrush(Colors.White);
        }
        else if (mainGrid.ActualWidth > 1920)
        {
            foreach (var button in Buttons)
                button.FontSize = 35;
            
            clockGrid.Background = null;
        }
        else
        {
            foreach (var button in Buttons)
                button.FontSize = 24;
            
            clockGrid.Background = null;
        }
    }
}