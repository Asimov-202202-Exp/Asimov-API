Feature: CompetenceServiceTests
    As a Developer
    I want to add new Competence through API
    So that It can be available for applications.
    
    Background:
        Given the Endpoint https://localhost:5001/api/v1/competences is available
        And A Course is already stored
        | Id | Name              | Description                         | Grade | State |
        | 2  | Universal history | The term World History gathering... | 2do   | false |
        
@competence-adding
Scenario: Add Competence
    When A Post Request to Competence is sent
        | Title              | Description                  | CourseId |
        | Digital Competence | The student is capable of... | 1        |
    Then A Response with Status 200 is received in Competence
    And A Competence Resource is included in Response body
        | Title              | Description                  | CourseId |
        | Digital Competence | The student is capable of... | 1        |
        
@competence-adding
Scenario: Add Competence that belongs to another Course
    Given A Competence is already assigned to a Course
      | Title                    | Description                  | CourseId |
      | Communication Competence | The student is capable of... | 2        |
    When A Post Request to Competence is sent
      | Title                    | Description                  | CourseId |
      | Communication Competence | The student is capable of... | 2        |
    Then A Response with Status 400 is received in Competence
    And A message of "This competence belongs to another Course" is included in Response body