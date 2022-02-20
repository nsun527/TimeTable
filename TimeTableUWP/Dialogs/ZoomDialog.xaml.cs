﻿#nullable enable

using System;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

using RollingRess;

using TimeTableCore;
using TimeTableCore.Grade3.Semester1;

namespace TimeTableUWP
{
    public partial class ZoomDialog : ContentDialog
    {
        private const string None = "(None)";
        protected readonly OnlineLink online;
        protected TextBlock TextBlock;
        public ZoomDialog(int @class, string subject, OnlineLink zoomInfo)
        {
            InitializeComponent();
            RequestedTheme = Info.Settings.Theme;

            Title = $"Class {@class} {subject} Links";
            this.online = zoomInfo;

            TextBlock = new();
            SetContent();
        }

        protected virtual void SetContent()
        {
            TextBlock.AddText("Zoom: ");
            if (online.Zoom is not null)
            {
                Hyperlink hyperlink = new();
                hyperlink.NavigateUri = new(online.Zoom);
                hyperlink.Inlines.Add(new Run() { Text = "Click here to open ZOOM meetings" });
                TextBlock.Inlines.Add(hyperlink);
                TextBlock.AddText("\n");
                TextBlock.AddTextLine($"ID: {online.Id ?? None}");
                TextBlock.AddTextLine($"PW: {online.Password ?? None}");
            }
            else
            {
                TextBlock.AddTextLine("Not available");
                TextBlock.AddTextLine("카루에게 줌 링크 추가를 요청해보세요.");
                IsPrimaryButtonEnabled = false;
                DefaultButton = ContentDialogButton.Secondary;
            }

            TextBlock.AddText("\nClassroom: ");
            if (online.Classroom is not null)
            {
                Hyperlink classroom = new();
                classroom.NavigateUri = new(online.Classroom);
                classroom.Inlines.Add(new Run() { Text = "Click here to open classroom" });
                TextBlock.Inlines.Add(classroom);
            }
            else
            {
                TextBlock.AddTextLine("Not available");
                TextBlock.AddTextLine("카루에게 클래스룸 링크 추가를 요청해보세요.");
                IsSecondaryButtonEnabled = false;
            }
            TextBlock.AddTextLine($"\nTeacher: {online.Teacher} 선생님\n");
            TextBlock.AddText(@"Click the buttons below or the links above to navigate.");

            Content = TextBlock;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog _, ContentDialogButtonClickEventArgs args) 
            => await Launcher.LaunchUriAsync(new(online.Zoom));

        private async void ContentDialog_SecondaryButtonClick(ContentDialog _, ContentDialogButtonClickEventArgs args) 
            => await Launcher.LaunchUriAsync(new(online.Classroom));
    }
}
