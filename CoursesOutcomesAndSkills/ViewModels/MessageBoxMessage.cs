namespace CoursesOutcomesAndSkills.ViewModels
{
    internal class MessageBoxMessage
    {

        public MessageBoxMessage(string title, string message)
        {
            Title = title;
            Message = message;
        }

        public string Title { get; }
        public string Message { get; }
    }
}