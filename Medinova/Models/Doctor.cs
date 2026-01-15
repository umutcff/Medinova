namespace Medinova.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public ICollection<Blog> Blogs {get;set;}
    }
}
