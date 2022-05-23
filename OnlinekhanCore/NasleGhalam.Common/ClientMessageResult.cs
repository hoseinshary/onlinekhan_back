namespace NasleGhalam.Common
{
    public class ClientMessageResult
    {
        public string Message { get; set; }

        public MessageType MessageType { get; set; }

        public int Id { get; set; }

        public object Obj { get; set; }

        public static ClientMessageResult NotFound() => new ClientMessageResult()
        {
            Message = "رکورد مورد نظر یافت نگردید.",
            MessageType = MessageType.NotFound
        };

        public static ClientMessageResult Unauthorized() => new ClientMessageResult()
        {
            Message = "عدم دسترسی.",
            MessageType = MessageType.NotFound
        };
    }
}
