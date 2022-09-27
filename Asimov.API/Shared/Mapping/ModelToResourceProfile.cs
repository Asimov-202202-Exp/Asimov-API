using Asimov.API.Activities.Domain.Models;
using Asimov.API.Activities.Resources;
using Asimov.API.Announcements.Domain.Models;
using Asimov.API.Announcements.Resources;
using Asimov.API.Competences.Domain.Models;
using Asimov.API.Competences.Resources;
using Asimov.API.Courses.Domain.Models;
using Asimov.API.Courses.Resources;
using Asimov.API.Directors.Domain.Models;
using Asimov.API.Directors.Resources;
using Asimov.API.Security.Domain.Services.Communication;
using Asimov.API.Teachers.Domain.Models;
using Asimov.API.Teachers.Resources;
using Asimov.API.Units.Domain.Models;
using Asimov.API.Units.Resources;
using AutoMapper;

namespace Asimov.API.Shared.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Director, DirectorResource>();
            CreateMap<Announcement, AnnouncementResource>();
            CreateMap<Teacher, TeacherResource>();
            CreateMap<Course, CourseResource>();
            CreateMap<Activity, ActivityResource>();
            CreateMap<Competence, CompetenceResource>();
            CreateMap<Director, AuthenticateResponseDirector>();
            CreateMap<Teacher, AuthenticateResponseTeacher>();
            CreateMap<Unit, UnitResource>();
        }
    }
}