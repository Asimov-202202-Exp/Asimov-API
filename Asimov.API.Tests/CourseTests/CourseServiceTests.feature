Feature: CourseServiceTests
As a Developer
I want to add new Course through API
So that It can be available for applications.
	
    Background: 
        Given the Endpoint https://localhost:5001/api/v1/courses is available

    @course-adding
    Scenario: Add Course
        When A Post Request is sent to Course
          | Id | Name    | Description                | Grade | State |
          | 1  | Algebra | A branch of Mathematics... | 1ro   | false |
        Then A Response with Status 200 is received in Course
        And A Course Resource is included in Response Body
          | Id | Name    | Description                | Grade | State |
          | 1  | Algebra | A branch of Mathematics... | 1ro   | false |