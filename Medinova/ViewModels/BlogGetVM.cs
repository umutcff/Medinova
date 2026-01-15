namespace Medinova.ViewModels
{
    public class BlogGetVM
    {
        public int Id { get; set; }
        public string Tittle { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DoctorName { get; set; }
    }
}
