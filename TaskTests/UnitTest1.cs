using System;
using System.Linq;
using Xunit;

public class TodoRepositoryTests
{
    private readonly TodoRepository _repository;

    public TodoRepositoryTests()
    {
        _repository = new TodoRepository("Data Source=tasks.db;Version=3;"); // Use tasks.db
        CleanupDatabase();
    }

    private void CleanupDatabase()
    {
        var todos = _repository.GetAll();
        foreach (var todo in todos)
        {
            _repository.Delete(todo.Id);
        }
    }

    [Fact]
    public void AddTodo_ShouldAddTodoToRepository()
    {
        var newTodo = new Todo
        {
            Name = "Test Todo",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Status = "Not Started",
            Priority = 1
        };

        _repository.Add(newTodo);

        var todos = _repository.GetAll();
        Assert.Contains(todos, todo => todo.Name == "Test Todo");
        Assert.True(newTodo.Id > 0); // Verify ID was set
    }

    [Fact]
    public void UpdateTodo_ShouldUpdateTodoInRepository()
    {
        var newTodo = new Todo
        {
            Name = "Test Todo",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Status = "Not Started",
            Priority = 1
        };
        _repository.Add(newTodo);
        var newTodoId = newTodo.Id;

        var updatedTodo = new Todo
        {
            Id = newTodoId,
            Name = "Updated Todo",
            StartDate = DateTime.Now.AddDays(2),
            EndDate = DateTime.Now.AddDays(3),
            Status = "In Progress",
            Priority = 2
        };

        _repository.Update(updatedTodo);

        var todos = _repository.GetAll();
        var updatedTodoFromRepo = todos.FirstOrDefault(todo => todo.Id == newTodoId);
        Assert.NotNull(updatedTodoFromRepo);
        Assert.Equal("Updated Todo", updatedTodoFromRepo.Name);
        Assert.Equal("In Progress", updatedTodoFromRepo.Status);
        Assert.Equal(2, updatedTodoFromRepo.Priority);
    }

    [Fact]
    public void DeleteTodo_ShouldRemoveTodoFromRepository()
    {
        var newTodo = new Todo
        {
            Name = "Test Todo",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Status = "Not Started",
            Priority = 1
        };

        Console.WriteLine($"Attempting to add Todo with Name: {newTodo.Name}");
        _repository.Add(newTodo);
        var newTodoId = newTodo.Id;

        Console.WriteLine($"Attempting to delete Todo with ID: {newTodoId}");
        _repository.Delete(newTodoId);

        var deletedTodo = _repository.GetById(newTodoId);
        Assert.Null(deletedTodo);
    }
}