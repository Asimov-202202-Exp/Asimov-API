using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Asimov.API.Competences.Resources;
using Asimov.API.Courses.Resources;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SpecFlow.Internal.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace Asimov.API.Tests.CompetenceTests
{
    [Binding]
    public class CompetenceServiceStepsDefinition
    {
        private readonly WebApplicationFactory<Startup> _factory;

        private HttpClient Client { get; set; }
        private Uri BaseUri { get; set; }
        private CourseResource Course { get; set; }
        private CompetenceResource Competence { get; set; }
        private Task<HttpResponseMessage> Response { get; set; }

        public CompetenceServiceStepsDefinition(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Given(@"the Endpoint https://localhost:(.*)/api/v(.*)/competences is available")]
        public void GivenTheEndpointHttpsLocalhostApiVCompetencesIsAvailable(int port, int version)
        {
            BaseUri = new Uri($"https://localhost:{port}/api/v{version}/competences");
            Client = _factory.CreateClient(new WebApplicationFactoryClientOptions {BaseAddress = BaseUri});
        }
        
        [Given(@"A Course is already stored")]
        public async void GivenACourseIsAlreadyStored(Table existingCourseResource)
        {
            var courseUri = new Uri("https://localhost:5001/api/v1/courses");
            var resource = existingCourseResource.CreateSet<SaveCourseResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var courseResponse = Client.PostAsync(courseUri, content);
            var courseResponseData = await courseResponse.Result.Content.ReadAsStringAsync();
            var existingCourse = JsonConvert.DeserializeObject<CourseResource>(courseResponseData);
            Course = existingCourse;
        }
        
        [When(@"A Post Request to Competence is sent")]
        public void WhenAPostRequestToCompetenceIsSent(Table saveCompetenceResource)
        {
            var resource = saveCompetenceResource.CreateSet<SaveCompetenceResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            Response = Client.PostAsync(BaseUri, content);
        }
        
        [Then(@"A Response with Status (.*) is received in Competence")]
        public void ThenAResponseWithStatusIsReceivedInCompetence(int expectedStatus)
        {
            var expectedStatusCode = ((HttpStatusCode) expectedStatus).ToString();
            var actualStatusCode = Response.Result.StatusCode.ToString();
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }

        [Then(@"A Competence Resource is included in Response body")]
        public async void ThenACompetenceResourceIsIncludedInResponseBody(Table expectedCompetenceResource)
        {
            var expectedResource = expectedCompetenceResource.CreateSet<CompetenceResource>().First();
            var responseData = await Response.Result.Content.ReadAsStringAsync();
            var resource = JsonConvert.DeserializeObject<CompetenceResource>(responseData);
            expectedResource.Id = resource.Id;
            var jsonExpectedResource = expectedResource.ToJson();
            var jsonActualResource = resource.ToJson();
            Assert.Equal(jsonExpectedResource, jsonActualResource);
        }
        
        [Given(@"A Competence is already assigned to a Course")]
        public async void GivenACompetenceIsAlreadyAssignedToACourse(Table existingCompetenceResource)
        {
            var resource = existingCompetenceResource.CreateSet<SaveCompetenceResource>().First();
            var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var competenceResponse = Client.PostAsync(BaseUri, content);
            var competenceResponseData = await competenceResponse.Result.Content.ReadAsStringAsync();
            var existingCompetence = JsonConvert.DeserializeObject<CompetenceResource>(competenceResponseData);
            Competence = existingCompetence;
        }

        [Then(@"A message of ""(.*)"" is included in Response body")]
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