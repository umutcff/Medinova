namespace Medinova.ViewModels.Doctor
{
    public class DoctorUpdateVM
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; } = null!;
        public string FullName { get; set; } = string.Empty;
    }
}
