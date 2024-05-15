namespace ChatApps.Models
{
    public class ChatViewModel
    {
        public List<MessageViewModel> Messages { get; set; }
        public MessageViewModel CurrentMessage { get; set; }
    }
}