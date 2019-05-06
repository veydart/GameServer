namespace GameServer.Utils
{
    public class ErrorModel
    {
        public bool Completed => false;
        public string Message { get; set; }
        public string Raw { get; set; }

        public ErrorModel()
        {
        }

        public ErrorModel(string message, string raw)
        {
            Message = message;
            Raw = raw;
        }
    }
}