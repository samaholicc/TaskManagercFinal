using System;
using System.Linq;
using Xunit;

public class TodoRepositoryTests
{
    private readonly TodoRepository _repository;

    public TodoRepositoryTests()
    {
        _repository = new TodoRepository(); // Create a new instance of the repository
        CleanupDatabase(); // Clean the database before each test
    }

    // Clean the database before each test
    private void CleanupDatabase()
    {
        var todos = _repository.GetAll();
        foreach (var todo in todos)
        {
            _repository.Delete(todo.Id);
        }
    }

    // Test adding a new Todo
    [Fact]
    public void AddTodo_ShouldAddTodoToRepository()
    {
        // Arrange - Create a new Todo
        var newTodo = new Todo
        {
            Name = "Test Todo",
            Date_Debut = DateTime.Now,
            Date_Fin = DateTime.Now.AddDays(1),
            Statut = "Not Started",
            Priorite = 1
        };

        // Act - Add the Todo to the repository
        _repository.Add(newTodo);

        // Assert - Verify that the Todo was added
        var todos = _repository.GetAll();
        Assert.Contains(todos, todo => todo.Name == "Test Todo");
    }

    // Test updating an existing Todo
    [Fact]
    public void UpdateTodo_ShouldUpdateTodoInRepository()
    {
        // Arrange - Create and add a new Todo
        var newTodo = new Todo
        {
            Name = "Test Todo",
            Date_Debut = DateTime.Now,
            Date_Fin = DateTime.Now.AddDays(1),
            Statut = "Not Started",
            Priorite = 1
        };
        _repository.Add(newTodo);

        var newTodoId = newTodo.Id; // Get the Id of the newly added Todo

        // Create the updated Todo object
        var updatedTodo = new Todo
        {
            Id = newTodoId, // Use the same Id
            Name = "Updated Todo",
            Date_Debut = DateTime.Now.AddDays(2),
            Date_Fin = DateTime.Now.AddDays(3),
            Statut = "In Progress",
            Priorite = 2
        };

        // Act - Update the Todo in the repository
        _repository.Update(updatedTodo);

        // Assert - Verify that the Todo was updated
        var todos = _repository.GetAll();
        var updatedTodoFromRepo = todos.FirstOrDefault(todo => todo.Id == newTodoId);
        Assert.NotNull(updatedTodoFromRepo); // Ensure the Todo exists after the update
        Assert.Equal("Updated Todo", updatedTodoFromRepo.Name);
        Assert.Equal("In Progress", updatedTodoFromRepo.Statut);
        Assert.Equal(2, updatedTodoFromRepo.Priorite);
    }

    // Test deleting a Todo
    [Fact]

  
    public void DeleteTodo_ShouldRemoveTodoFromRepository()
    {
        var newTodo = new Todo
        {
            Name = "Test Todo",
            Date_Debut = DateTime.Now,
            Date_Fin = DateTime.Now.AddDays(1),
            Statut = "Not Started",
            Priorite = 1
        };

        // Log ID before adding
        Console.WriteLine($"Attempting to add Todo with Name: {newTodo.Name}");

        _repository.Add(newTodo);
        var newTodoId = newTodo.Id;

        // Log ID before deletion
        Console.WriteLine($"Attempting to delete Todo with ID: {newTodoId}");

        _repository.Delete(newTodoId);

        var deletedTodo = _repository.GetById(newTodoId);
        Assert.Null(deletedTodo);
    }




}
