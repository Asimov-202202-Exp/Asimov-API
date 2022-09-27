using System.Collections.Generic;
using Asimov.API.Activities.Domain.Models;
using Asimov.API.Competences.Domain.Models;
using Asimov.API.Teachers.Domain.Models;

namespace Asimov.API.Courses.Domain.Models
{

    public class Course
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Grade { get; set; }
        public bool State { get; set; }
        

        public IList<Activity> Items { get; set; } = new List<Activity>();
        public IList<TeacherCourse> TeacherCourses { get; set; } = new List<TeacherCourse>();
        public IList<Competence> Competences { get; set; } = new List<Competence>();
    }
}