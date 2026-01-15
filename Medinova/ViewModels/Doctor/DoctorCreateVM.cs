namespace Medinova.ViewModels.Doctor
{
    public class DoctorCreateVM
    {
        public IFormFile Image { get; set; } = null!;
        public string FullName { get; set; } = string.Empty;
    }
}
