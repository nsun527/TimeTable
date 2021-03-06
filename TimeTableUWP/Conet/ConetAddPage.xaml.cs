#nullable enable
using Windows.UI.Xaml.Media.Animation;

namespace TimeTableUWP.Conet;

public sealed partial class ConetAddPage : Page
{
    public static ConetHelp? Conet { get; set; }
    public ConetAddPage()
    {
        InitializeComponent();
        idText.Text = $"{Info.User.Conet!.Id} {Info.User.Conet.Name}";

        if (Conet is not null) // Not creating, but modifying
        {
            idText.Text = $"{Conet.Uploader.Id} {Conet.Uploader.Name}";
            mainText.Text = "Conet Details";
            TitleTextBox.Text = Conet.Title;
            eggTextBox.Text = $"{Conet.Price?.Value}";
            BodyTextBox.Text = Conet.Body ?? string.Empty;

            // 다른 사람 글이라면
            if (Conet.Uploader.Id != Info.User.Conet.Id)
            {
                ReadOnly(TitleTextBox, eggTextBox, BodyTextBox);
                Disable(PostButton);
            }
            // 내가 쓴 글이라면
            else
            {
                Visible(DeleteButton);
            }
        }
    }

    private async void PostButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
        {
            await ShowMessageAsync("제목을 입력하세요.", "Error", Info.Settings.Theme);
            return;
        }

        bool eggExists = false;
        uint egg = 0;

        if (!eggTextBox.IsNullOrEmpty())
        {
            eggExists = true;
            if (!uint.TryParse(eggTextBox.Text, out egg))
            {
                await ShowMessageAsync("에그가 올바르게 입력되지 않았습니다.", "Error", Info.Settings.Theme);
                return;
            }
            if (Info.User.Conet!.Eggs.Value < egg)
            {
                await ShowMessageAsync("보유 에그보다 많은 금액을 입력하셨습니다.", "Error", Info.Settings.Theme);
                return;
            }
            if (Conet is not null)
            {
                // 기존 Egg 보충
                Info.User.Conet!.Eggs += Conet.Price!.Value;
            }
            Info.User.Conet!.Eggs -= new Egg(egg);

            using SqlConnection _sql = new(ChatMessageDac.ConnectionString);
            ConetUserDac user = new(_sql, Info.User.Conet);
            await _sql.OpenAsync();
            await user.UpdateEggAsync(Info.User.Conet!.Eggs);
        }

        ConetHelp conet = new(DateTime.Now, Info.User.Conet!, TitleTextBox.Text,
            BodyTextBox.IsNullOrWhiteSpace() ? "" : BodyTextBox.Text, eggExists ? new Egg(egg) : null);

        using SqlConnection sql = new(ChatMessageDac.ConnectionString);
        using ConetHelpDac con = new(sql);
        Disable(PostButton, TitleTextBox, BodyTextBox, eggTextBox);
       
        try
        {
            await sql.OpenAsync();
            if (Conet is null)
                await con.InsertAsync(conet);
            else
                await con.UpdateAsync(Conet.UploadDate, conet);
        }
        catch (Exception ex)
        {
            await Task.WhenAll(
                ShowMessageAsync("업로드에 실패했습니다.", "에러", Info.Settings.Theme),
                TimeTableException.HandleException(ex));
        }
        finally
        {
            Disable(PostButton, TitleTextBox, BodyTextBox, eggTextBox);
            Close();
        }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e) => Close();

    private void Close() { Conet = null; Frame.Navigate(typeof(ConetPage), null, new DrillInNavigationTransitionInfo()); }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (Modified)
        {
            await ShowMessageAsync("This task has been modified. Save or discard changes and try again.", "Couldn't delete", Info.Settings.Theme);
            return;
        }

        using SqlConnection sql = new(ChatMessageDac.ConnectionString);
        using ConetHelpDac con = new(sql);

        Disable(DeleteButton);
        try
        {
            await sql.OpenAsync();
            await con.DeleteAsync(Conet!.UploadDate);
        }
        catch (Exception ex)
        {
            await Task.WhenAll(ShowMessageAsync("업로드에 실패했습니다.", "에러", Info.Settings.Theme),
                TimeTableException.HandleException(ex));
        }
        finally
        {
            Close();
            Enable(DeleteButton);
        }
    }

    private bool Modified => Conet is not null && !(TitleTextBox.Text == Conet.Title
            && BodyTextBox.IsSameWith(Conet.Body)
            && eggTextBox.IsSameWith(Conet.Price?.Value.ToString()));
}