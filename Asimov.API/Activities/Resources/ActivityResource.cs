namespace Asimov.API.Activities.Resources
{
    public class ActivityResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool State { get; set; }
        
        public int CourseId { get; set; }
    }
}