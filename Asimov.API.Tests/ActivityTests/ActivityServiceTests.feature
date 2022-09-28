Feature: ActivityServiceTests
As a Director
I want to add a new Activity through API
So that It can be available for applications.

    Background:
        Given the Endpoint https://localhost:5001/api/v1/activities is available
        And A Course already exist
          | Id | Name    | Description          | Grade | State |
          | 1  | Algebra | This is a new course | 2do   | false |

        And A second Course already exist
          | Id | Name   | Description                   | Grade | State |
          | 2  | French | This is a new language course | 3do   | false |

        And A Activity already exist
          | Id | Name                       | Value                                             | State | CourseId |
          | 1  | Algebra exercises for home | The student must solve the following exercises... | false | 1        |

    @activity-adding
    Scenario: Add a new Activity for a course
      When A Post Activity request is sent
        | Name                      | Value                                              | State | CourseId |
        | French questions for home | The student must answer the following questions... | false | 2        |
      Then A response with status 200 is received
      And A Activity resource is included in response body
        | Name                      | Value                                              | State | CourseId |
        | French questions for home | The student must answer the following questions... | false | 2        |

    @activity-adding
    Scenario: Add the same Activity from one course to another
        When A Post Activity request is sent
          | Name                       | Value                                             | State | CourseId |
          | Algebra exercises for home | The student must solve the following exercises... | false | 2        |
        Then A response with status 400 is received
        And A message of "This activity is already assigned to another course" is included in response body