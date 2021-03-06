#nullable enable

namespace TimeTableUWP;

using RollingRess.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.UI.Popups;
using wux = Windows.UI.Xaml;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
internal sealed class ErrorCodeAttribute : Attribute
{
    public string PositionalString { get; }

    // This is a positional argument
    public ErrorCodeAttribute(string positionalString)
    {
        PositionalString = positionalString;
    }
}

[Serializable, ErrorCode("1xxx")]
public class TimeTableException : Exception
{
    public int ErrorCode { get; protected set; } = -1;
    public TimeTableException() { }
    public TimeTableException(string message) : base(message) { }
    public TimeTableException(string message, int errorCode) : this(message) { ErrorCode = errorCode; }
    public TimeTableException(string message, Exception inner) : base(message, inner) { }
    protected TimeTableException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public static async void HandleException(object _, wux::UnhandledExceptionEventArgs e)
    {
        // 여기에 try~catch 걸까
        e.Handled = true;
        await HandleException(e.Exception);
    }

    public static async Task HandleException(Exception exception)
    {
        int? code = (exception is TimeTableException te) ? te.ErrorCode : null;

        string result = "에러가 발생했습니다.";
        result += Info.User.ActivationLevel is not ActivationLevel.Developer
            ? "다른 사용자들에게서 발생한 오류이므로 속히 해결 부탁드립니다.\n"
            : "디버그 중 발생한 오류입니다.\n";

        if (code is not null)
        {
            result += $"\nError code: {code}";
        }
        result += $"\n{exception}";

        var smtp = FeedbackDialog.PrepareSendMail(result,
            $"GGHS Time Table EXCEPTION OCCURED in V{Info.Version}", out var msg);

        if (Connection.IsInternetAvailable)
        {
            Task mail = smtp.SendAsync(msg);

            if (Info.User.ActivationLevel is not ActivationLevel.Developer)
            {
                SqlConnection sql = new(ChatMessageDac.ConnectionString);
                ChatMessageDac chat = new(sql);
                await sql.OpenAsync();
                await chat.InsertAsync((byte)ChatMessageDac.Sender.GttBot, string.Format(Messages.ErrorChat, exception.GetType().Name));
                sql.Close();
            }

            await mail;
        }
        else
        {
            MessageDialog messageDialog = new("카루에게 아래 정보를 제공해주세요.\n" + result, "에러가 발생했습니다.");
            await messageDialog.ShowAsync();
        }
    }
}

[Serializable, ErrorCode("2xxx")]
public class TableCellException : TimeTableException
{
    public TableCellException() { }
    public TableCellException(string message, int errorCode = 2000) : base(message, errorCode) { }
    public TableCellException(string message, Exception inner) : base(message, inner) { }
    protected TableCellException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

[Serializable, ErrorCode("3xxx")]
public class DataAccessException : TimeTableException
{
    public DataAccessException() { }
    public DataAccessException(string message, int errorCode = 3000) : base(message, errorCode) { }
    public DataAccessException(string message, Exception inner) : base(message, inner) { }
    protected DataAccessException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

[Serializable, ErrorCode("4xxx")]
public class ComboBoxDataException : TimeTableException
{
    public ComboBoxDataException() { }
    public ComboBoxDataException(string message, int errorCode = 4000) : base(message, errorCode) { }
    public ComboBoxDataException(string message, Exception inner) : base(message, inner) { }
    protected ComboBoxDataException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
