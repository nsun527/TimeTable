﻿#nullable enable

using Windows.UI.Text;
using Windows.UI.Xaml.Controls.Primitives;

namespace TimeTableUWP.Todo;
public class TaskButton : Button
{
    private const int ButtonWidth = 2560;
    private const int ButtonHeight = 93;

    public TodoTask TodoTask { get; private set; }

    public TaskButton(in TodoTask task, RoutedEventHandler TaskButton_Click)
    {
        TodoTask = task;
        Click += TaskButton_Click;
        RightTapped += TaskButton_RightTapped;
        Height = ButtonHeight;
        Margin = new(0, 0, 0, 5);
        CornerRadius = new(10);
        VerticalAlignment = VerticalAlignment.Top;

        CreateGrid(out Grid inner, out Grid dday, out Grid outter);
        CreateDdayTextBlock(out TextBlock tb1, out TextBlock tb2);
        dday.Children.Add(tb1);
        dday.Children.Add(tb2);
        inner.Children.Add(dday);

        CreateTaskTextBlock(out TextBlock tb3, out TextBlock tb4);
        inner.Children.Add(tb3);
        inner.Children.Add(tb4);

        CreateArrowTextBlock(out TextBlock arrow);
        outter.Children.Add(inner);
        outter.Children.Add(arrow);

        if (TodoTask.DueDate.Date == DateTime.Now.Date)
        {
            BorderThickness = new(2.6);
            BorderBrush = new SolidColorBrush(Info.Settings.ColorType with { A = 200 });
        }
        Content = outter;
    }

    private void TaskButton_RightTapped(object sender, RightTappedRoutedEventArgs e)
    {
        if (sender is UIElement uiElem)
        {
            MenuFlyout btnFlyOut = new();
            MenuFlyoutItem edit = new() { Text = "Edit", Icon = new SymbolIcon(Symbol.Edit) };
            MenuFlyoutItem delete = new() { Text = "Delete", Icon = new SymbolIcon(Symbol.Delete) };

            edit.Click += (_, e) => {
                AddPage.Task = TodoTask;
                if (Window.Current.Content is Frame rootFrame)
                    rootFrame.Navigate(typeof(AddPage), null, new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
            };
            delete.Click += async (_, e) =>
            {
                if (await TaskList.DeleteTask(TodoTask.Title, TodoTask) is false)
                    return;
                await Task.Delay(100);
                if (Window.Current.Content is Frame rootFrame)
                {
                    // TODO: 이걸 그냥 MainPage의 Reload Task..?
                    // TODO: 이거 그냥 TodoPage로 하면 NavigationView 날아간다. 수정좀.
                    MainPage.IsGoingToTodoPage = true;
                    rootFrame.Navigate(typeof(MainPage), null, new Windows.UI.Xaml.Media.Animation.SuppressNavigationTransitionInfo());
                }
            };

            btnFlyOut.Items.Add(edit);
            btnFlyOut.Items.Add(delete);
            btnFlyOut.Placement = FlyoutPlacementMode.Bottom;
            btnFlyOut.ShowAt(uiElem, e.GetPosition(uiElem));
        }
    }

    private void CreateArrowTextBlock(out TextBlock arrow)
    {
        arrow = new()
        {
            Text = "\xE971", // E9B9 
            FontFamily = new("../Assets/Segoe#Segoe Fluent Icons"),
            FontSize = 17,
            Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x72, 0x72, 0x72)),
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Right,
            Margin = new(0, 0, 15, 0)
        };
    }

    private void CreateGrid(out Grid inner, out Grid dday, out Grid outter)
    {
        inner = new()
        {
            Height = 80,
            Width = 2560,
            Margin = new(-12, 0, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Left
        };
        dday = new()
        {
            Width = 65,
            Margin = new(10, 0, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Left
        };
        outter = new();
    }

    private void CreateDdayTextBlock(out TextBlock tb1, out TextBlock tb2)
    {
        tb1 = new()
        {
            FontSize = 19,
            Text = TodoTask.DueDate.ToString("MM/dd"),
            Margin = new(0, 10, 0, 46),
            HorizontalAlignment = HorizontalAlignment.Center,
            FontFamily = new("Segoe"),
            FontWeight = FontWeights.Bold
        };

        DateTime now = DateTime.Now;
        int days = (new DateTime(now.Year, now.Month, now.Day) - TodoTask.DueDate).Days;
        string text = "D" + days switch
        {
            0 => "-Day",
            _ => days.ToString("+0;-0"),
        };

        tb2 = new()
        {
            FontSize = 15,
            Text = text,
            Margin = new(0, 44, 0, 12),
            HorizontalAlignment = HorizontalAlignment.Center,
            FontFamily = new("Consolas"),
            FontWeight = FontWeights.Bold
        };
    }

    private void CreateTaskTextBlock(out TextBlock tb3, out TextBlock tb4)
    {
        tb3 = new()
        {
            FontSize = 17,
            Text = TodoTask.Subject,
            Margin = new(80, 12, 0, 44),
            Width = ButtonWidth
        };
        tb4 = new()
        {
            FontSize = 15,
            Text = TodoTask.Title,
            Margin = new(80, 43, 0, 13),
            HorizontalAlignment = HorizontalAlignment.Left,
            Width = ButtonWidth,
            Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xA0, 0xA0, 0xA0))
        };
    }
}