# To Do List Manager

> [!IMPORTANT]
> This project is a learning piece for GraphQL and Specflow. It is not intended for any other use.

## Table of Contents
- [x] [Project Information](#user-story)
- [x] [Setup](#setup)
- [x] [Queries](#queries)
- [ ] [Mutations](#Mutations)

## User Story:
- Users can interact with the application through a web interface (MVC).
- Users can create, modify, and delete tasks.
- Tasks can be filtered by priority, completion status, and due date.
- Users can mark tasks as completed.
- Authentication is required for users to access their tasks.

## Core Components:
- GraphQL API: To expose task management functionalities (create, update, delete, query tasks).
- EF Core: To persist tasks in a database.
- ASP.NET Core: To host the GraphQL server and serve the API.
- SpecFlow: For behavior-driven development (BDD) testing of the GraphQL API.

## Tech stack:
- C# .NET 8: Core language and framework.
- GraphQL (HotChocolate): For API operations.
- EF Core: For database interaction.
- SQL Server: As a database.
- SpecFlow: To run tests.

<hr>

# Usage

## Setup

## First a JWT token will need generating using the preset userId 

> [!NOTE]
> There is no functionality to add a user. There is a preset user in the database as this is a learning piece only.

```
query TodosByUserId {
    generateJwtToken(userId: "dff66c34-bd9b-4355-80ba-e7c7cd02d83f")
}
```
> [!NOTE]
> After the token has been retrieved and added to the header as an authorization piece then the below queries/mutations can be posted.

## Queries

#### Get list of Todos for User
```
query TodosByUserId {
    todosByUserId {
        id
        title
        description
        dueDateT
        isCompleted
        priority
        userId
    }
}
```

#### Get Todo by Id (only users can see their own Todos)
```
query TodoByTodoId {
    todoByTodoId(id: 2) {
        id
        title
        description
        dueDateT
        isCompleted
        priority
        userId
    }
}
```

#### Get Todos by priority (only users can see their own Todos)
```
query TodosByPriority {
    todosByPriority(priority: MEDIUM) {
        id
        title
        description
        dueDateT
        isCompleted
        priority
        userId
    }
}
```
## Mutations





