using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Asimov.API.Activities.Resources;
using Asimov.API.Courses.Resources;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SpecFlow.Internal.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace Asimov.API.Tests.ActivityTests
{
    [Binding]
    public class ActivityServiceStepsDefinition
    {
        private readonly WebApplicationFactory<Startup> _factory;

        private HttpClient Client { get; set; }
        private Uri BaseUri { get; set; }
        private CourseResource Course { get; set; }
        private ActivityResource Activity { get; set; }
        private Task<HttpResponseMessage> Response { get; set; }

        public ActivityServiceStepsDefinition(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Given(@"the Endpoint https://localhost:(.*)/api/v(.*)/activities is available")]
        public void GivenTheEndpointHttpsLocalhostApiVActivitiesIsAvailable(int port, int version)
        {
            BaseUri = new Uri($"https://localhost:{port}/api/v{version}/activities");
            Client = _factory.CreateClient(new WebApplicationFactoryClientOptions {BaseAddress = BaseUri});
        }
        
        [Given(@"A Course already exist")]
        public async void GivenACourseAlreadyExist(Table existingCourseResource)
        {
            var courseUri = new Uri("https://localhost:5001/api/v1/courses");
            var resource = existingCourseResource.CreateSet<SaveCourseResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var courseResponse = Client.PostAsync(courseUri, content);
            var courseResponseData = await courseResponse.Result.Content.ReadAsStringAsync();
            var existingCourse = JsonConvert.DeserializeObject<CourseResource>(courseResponseData);
            Course = existingCourse;
        }
        
        [Given(@"A second Course already exist")]
        public async void GivenASecondCourseAlreadyExist(Table existingCourseResource)
        {
            var courseUri = new Uri("https://localhost:5001/api/v1/courses");
            var resource = existingCourseResource.CreateSet<SaveCourseResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var courseResponse = Client.PostAsync(courseUri, content);
            var courseResponseData = await courseResponse.Result.Content.ReadAsStringAsync();
            var existingCourse = JsonConvert.DeserializeObject<CourseResource>(courseResponseData);
            Course = existingCourse;
        }
        
        [Given(@"A Activity already exist")]
        public async void GivenAActivityAlreadyExist(Table existingActivityResource)
        {
            var activityUri = new Uri("https://localhost:5001/api/v1/activities");
            var resource = existingActivityResource.CreateSet<SaveActivityResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var activityResponse = Client.PostAsync(activityUri, content);
            var activityResponseData = await activityResponse.Result.Content.ReadAsStringAsync();
            var existingActivity = JsonConvert.DeserializeObject<ActivityResource>(activityResponseData);
            Activity = existingActivity;
        }
        
        [When(@"A Post Activity request is sent")]
        public void WhenAPostActivityRequestIsSent(Table saveActivityResource)
        {
            var resource = saveActivityResource.CreateSet<SaveActivityResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            Response = Client.PostAsync(BaseUri, content);
        }
        
        [Then(@"A response with status (.*) is received")]
        public void ThenAResponseWithStatusIsReceived(int expectedStatus)
        {
            var expectedStatusCode = ((HttpStatusCode) expectedStatus).ToString();
            var actualStatusCode = Response.Result.StatusCode.ToString();
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }
        
        
        [Then(@"A Activity resource is included in response body")]
        public async void ThenAActivityResourceIsIncludedInResponseBody(Table expectedActivityResource)
        {
            var expectedResource = expectedActivityResource.CreateSet<ActivityResource>().First();
            var responseData = await Response.Result.Content.ReadAsStringAsync();
            var resource = JsonConvert.DeserializeObject<ActivityResource>(responseData);
            expectedResource.Id = resource.Id;
            var jsonExpectedResource = expectedResource.ToJson();
            var jsonActualResource = resource.ToJson();
            Assert.Equal(jsonExpectedResource, jsonActualResource);
        }

        [Then(@"A message of ""(.*)"" is included in response body")]
        public async void ThenAMessageOfIsIncludedInResponseBody(string expectedMessage)
        {
            var jsonActualMessage = await Response.Result.Content.ReadAsStringAsync();
            var jsonExpectedMessage = expectedMessage.ToJson();
            var temp = jsonActualMessage.Remove(0, 11);
            var actualMessage = temp.Trim(new Char[] { '}'} );
            Assert.Equal(jsonExpectedMessage, actualMessage);
        }
    }
}