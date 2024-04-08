namespace RealStateApp.Core.Application.Helpers
{
    public class Singleton
    {
        private static Singleton instance;
        public string Message { get; set; } = "";

        public Singleton(string Message)
        {
            this.Message = Message;
        }

        public static Singleton GetInstance(string Message)
        {
            return instance == null ? instance = new Singleton(Message) : instance;
        }

        public static void SetString(string newMessage)
        {
            instance.Message = newMessage;
        }
    }
}
