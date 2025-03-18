using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class TodoRepositoryTests
{
    private readonly TodoRepository _repository;

    // Constructor - on crée une nouvelle instance de TodoRepository
    public TodoRepositoryTests()
    {
        _repository = new TodoRepository(); // Utilisation de la connection par défaut
        CleanupDatabase(); // Nettoyer la base de données avant chaque test
    }

    // Nettoyer la base de données
    private void CleanupDatabase()
    {
        var todos = _repository.GetAll();
        foreach (var todo in todos)
        {
            _repository.Delete(todo.Id);
        }
    }

    // Teste l'ajout d'un Todo à la base de données
    [Fact]
    public void AddTodo_ShouldAddTodoToDatabase()
    {
        // Arrange - Créer un nouveau Todo
        var newTodo = new Todo
        {
            Nom = "Test Todo",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Status = "Not Started",
            Priority = 1
        };

        // Act - Ajouter le Todo à la base de données
        _repository.Add(newTodo);

        // Assert - Vérifie que le Todo a bien été ajouté
        var todos = _repository.GetAll();
        Assert.Contains(todos, todo => todo.Nom == "Test Todo");
    }

    // Teste la mise à jour d'un Todo
    [Fact]
    public void UpdateTodo_ShouldUpdateTodoInDatabase()
    {
        // Arrange - Créer un Todo à ajouter
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
            Id = newTodo.Id, // L'ID du Todo à mettre à jour
            Nom = "Updated Todo",
            StartDate = DateTime.Now.AddDays(2),
            EndDate = DateTime.Now.AddDays(3),
            Status = "In Progress",
            Priority = 2
        };

        // Act - Mettre à jour le Todo dans la base de données
        _repository.Update(updatedTodo);

        // Assert - Vérifie que le Todo a bien été mis à jour
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


    // Teste la récupération de tous les Todos
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

        // Act - Récupérer tous les Todos de la base de données
        var todos = _repository.GetAll();

        // Assert - Vérifie que tous les Todos sont présents dans la liste
        Assert.Contains(todos, todo => todo.Nom == "Todo 1");
        Assert.Contains(todos, todo => todo.Nom == "Todo 2");
    }

    // Teste la gestion d'une exception lors de la suppression d'un Todo inexistant
    [Fact]
    public void DeleteTodo_ShouldThrowExceptionWhenTodoNotFound()
    {
        // Arrange - Créer un Todo avec un ID non existant
        var nonExistentId = 999;

        // Act & Assert - Vérifie que la méthode Delete lance une exception pour un Todo inexistant
        var exception = Assert.Throws<InvalidOperationException>(() => _repository.Delete(nonExistentId));
        Assert.Equal("Todo not found", exception.Message);
    }
}
