namespace Medinova.ViewModels.Blog
{
    public class BlogGetVM
    {
        public int Id { get; set; }
        public string Tittle { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string DoctorImagePath{ get; set; } = string.Empty;
    }
}
