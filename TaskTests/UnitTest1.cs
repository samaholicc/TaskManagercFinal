using System;
using Xunit;

public class TodoRepositoryTests
{
    private readonly TodoRepository _repository;

    // Constructor - on cr�e une nouvelle instance de TodoRepository
    public TodoRepositoryTests()
    {
        _repository = new TodoRepository(); // Utilisation de la connection par d�faut
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

    // D'autres tests peuvent �tre ajout�s ici, comme Update, Delete, etc.
}
