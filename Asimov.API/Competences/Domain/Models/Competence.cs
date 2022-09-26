using System.Collections.Generic;
using Asimov.API.Courses.Domain.Models;

namespace Asimov.API.Competences.Domain.Models
{
    public class Competence
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}