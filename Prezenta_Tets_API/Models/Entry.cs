namespace Prezenta_API.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public int UserCode { get; set; }
        public DateTime ScanTime { get; set; }
        public int ScannerId { get; set; }
    }
}
