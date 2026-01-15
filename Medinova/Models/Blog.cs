namespace Medinova.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Tittle { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
    }
}
