using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class TodoRepositoryTests
{
    private readonly TodoRepository _repository;

    // Constructor - on cr�e une nouvelle instance de TodoRepository
    public TodoRepositoryTests()
    {
        _repository = new TodoRepository(); // Utilisation de la connection par d�faut
        CleanupDatabase(); // Nettoyer la base de donn�es avant chaque test
    }

    // Nettoyer la base de donn�es
    private void CleanupDatabase()
    {
        var todos = _repository.GetAll();
        foreach (var todo in todos)
        {
            _repository.Delete(todo.Id);
        }
    }

    // Teste l'ajout d'un Todo � la base de donn�es
    [Fact]
    public void AddTodo_ShouldAddTodoToDatabase()
    {
        // Arrange - Cr�er un nouveau Todo
        var newTodo = new Todo
        {
            Nom = "Test Todo",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Status = "Not Started",
            Priority = 1
        };

        // Act - Ajouter le Todo � la base de donn�es
        _repository.Add(newTodo);

        // Assert - V�rifie que le Todo a bien �t� ajout�
        var todos = _repository.GetAll();
        Assert.Contains(todos, todo => todo.Nom == "Test Todo");
    }

    // Teste la mise � jour d'un Todo
    [Fact]
    public void UpdateTodo_ShouldUpdateTodoInDatabase()
    {
        // Arrange - Cr�er un Todo � ajouter
        var newTodo = new Todo
        {
            Nom = "Test Todo",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Status = "Not Started",
            Priority = 1
        };
        _repository.Add(newTodo);

        // Modifier le Todo
        var updatedTodo = new Todo
        {
            Id = newTodo.Id, // L'ID du Todo � mettre � jour
            Nom = "Updated Todo",
            StartDate = DateTime.Now.AddDays(2),
            EndDate = DateTime.Now.AddDays(3),
            Status = "In Progress",
            Priority = 2
        };

        // Act - Mettre � jour le Todo dans la base de donn�es
        _repository.Update(updatedTodo);

        // Assert - V�rifie que le Todo a bien �t� mis � jour
        var todos = _repository.GetAll();
        var updatedTodoFromDb = todos.Find(todo => todo.Id == newTodo.Id);
        Assert.NotNull(updatedTodoFromDb);
        Assert.Equal("Updated Todo", updatedTodoFromDb.Nom);
        Assert.Equal("In Progress", updatedTodoFromDb.Status);
    }

    // Teste la suppression d'un Todo
    [Fact]
   
    public void DeleteTodo_ShouldRemoveTodoFromDatabase()
    {
        // Arrange
        var newTodo = new Todo
        {
            Nom = "Test Todo",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Status = "Not Started",
            Priority = 1
        };
        _repository.Add(newTodo); // Add the Todo to the database

        // Get the ID of the newly added Todo
        var todoId = newTodo.Id;

        // Act
        _repository.Delete(todoId); // Attempt to delete the Todo

        // Assert - Verify that the Todo is no longer in the database
        var deletedTodo = _repository.GetById(todoId);
        Assert.Null(deletedTodo);  // The Todo should be null if it was deleted
    }


    // Teste la r�cup�ration de tous les Todos
    [Fact]
    public void GetAllTodos_ShouldReturnAllTodosFromDatabase()
    {
        // Arrange - Ajouter plusieurs Todos
        var todo1 = new Todo
        {
            Nom = "Todo 1",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Status = "Not Started",
            Priority = 1
        };
        var todo2 = new Todo
        {
            Nom = "Todo 2",
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(2),
            Status = "In Progress",
            Priority = 2
        };
        _repository.Add(todo1);
        _repository.Add(todo2);

        // Act - R�cup�rer tous les Todos de la base de donn�es
        var todos = _repository.GetAll();

        // Assert - V�rifie que tous les Todos sont pr�sents dans la liste
        Assert.Contains(todos, todo => todo.Nom == "Todo 1");
        Assert.Contains(todos, todo => todo.Nom == "Todo 2");
    }

    // Teste la gestion d'une exception lors de la suppression d'un Todo inexistant
    [Fact]
    public void DeleteTodo_ShouldThrowExceptionWhenTodoNotFound()
    {
        // Arrange - Cr�er un Todo avec un ID non existant
        var nonExistentId = 999;

        // Act & Assert - V�rifie que la m�thode Delete lance une exception pour un Todo inexistant
        var exception = Assert.Throws<InvalidOperationException>(() => _repository.Delete(nonExistentId));
        Assert.Equal("Todo not found", exception.Message);
    }
}
