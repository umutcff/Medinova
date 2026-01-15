namespace Medinova.ViewModels.Blog
{
    public class BlogUpdateVM
    {
        public int Id { get; set; }
        public string Tittle { get; set; } = string.Empty;
        public IFormFile Image { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public int DoctorId { get; set; }
    }
}
