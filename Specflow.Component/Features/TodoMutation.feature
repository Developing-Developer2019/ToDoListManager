@mutation
Feature: Todo Mutations

    Scenario: Add a new Todo
        Given the user with UserId "user1" exists
        When I add a todo with title "New Task", description "This is a new task", dueDate "2023-12-31", and priority "High"
        Then the todo should be added successfully

    Scenario: Mark a Todo as completed
        Given the todo with id 1 exists for user "user1"
        When I mark the todo as completed
        Then the todo should be marked as completed

    Scenario: Delete a Todo
        Given the todo with id 1 exists for user "user1"
        When I delete the todo
        Then the todo should be deleted successfully