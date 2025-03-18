using System;
using Xunit;

public class TodoRepositoryTests
{
    private readonly TodoRepository _repository;

    // Constructor - on crée une nouvelle instance de TodoRepository
    public TodoRepositoryTests()
    {
        _repository = new TodoRepository(); // Utilisation de la connection par défaut
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

    // D'autres tests peuvent être ajoutés ici, comme Update, Delete, etc.
}
