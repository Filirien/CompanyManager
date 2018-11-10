using System.ComponentModel.DataAnnotations;

namespace CompanyManager.DAL.Entities
{
    public class ExperienceLevel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
