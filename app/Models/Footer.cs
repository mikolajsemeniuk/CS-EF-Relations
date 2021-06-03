namespace app.Models
{
    public class Footer
    {
        public int FooterId { get; set; }
        public string Reference { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}