namespace Medinova.ViewModels.Blog
{
    public class BlogCreateVM
    {

        public string Tittle { get; set; } = string.Empty;
        public IFormFile Image { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public int DoctorId { get; set; } 
    }
}
