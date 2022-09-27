using Asimov.API.Courses.Resources;

namespace Asimov.API.Units.Resources
{
    public class UnitResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }
        public CourseResource Course;
    }
}