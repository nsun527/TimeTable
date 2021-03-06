#nullable enable

namespace TimeTableUWP.Todo;

public class TodoTask
{
    public DateTime DueDate { get; set; }
    public string Subject { get; set; }
    public string Title { get; set; }
    public string? Body { get; set; }

    public TodoTask() : this(DateTime.Now, "", "", default) { }
    public TodoTask(in DateTime date, string subject, string title, string? body)
    {
        DueDate = date;
        Subject = subject;
        Title = title;
        Body = body;
    }
}