@query
Feature: Todo Queries

    Scenario: Retrieve Todos by UserId
        Given the user with UserId "user1" exists
        When I request todos by UserId
        Then the todos for the user should be returned

    Scenario: Retrieve Todo by TodoId
        Given the todo with id 1 exists for user "user1"
        When I request the todo by id
        Then the todo with id 1 should be returned

    Scenario: Retrieve Todos by Priority
        Given the todos with priority "High" exist for user "user1"
        When I request the todos by priority
        Then the todos with priority "High" should be returned