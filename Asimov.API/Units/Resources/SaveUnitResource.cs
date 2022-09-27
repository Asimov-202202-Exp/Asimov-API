using System.ComponentModel.DataAnnotations;

namespace Asimov.API.Units.Resources
{
    public class SaveUnitResource
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        
        [Required]
        public int CourseId { get; set; }
    }
}