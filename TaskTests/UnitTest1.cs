using System;
using System.Linq;
using Xunit;
using MyAppTodo.Database;
using MyAppTodo.Entities;

namespace TaskTests
{
    public class TodoRepositoryTests
    {
        private readonly TodoRepository _repository;

        public TodoRepositoryTests()
        {
            var database = new Database();
            database.InitializeDatabase();
            _repository = new TodoRepository();
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
                Date_Debut = DateTime.Now,
                Date_Fin = DateTime.Now.AddDays(1),
                Statut = "Not Started",
                Priorite = 1
            };

            _repository.Add(newTodo);

            var todos = _repository.GetAll();
            Assert.Contains(todos, todo => todo.Name == "Test Todo");
        }

        [Fact]
        public void UpdateTodo_ShouldUpdateTodoInRepository()
        {
            var newTodo = new Todo
            {
                Name = "Test Todo",
                Date_Debut = DateTime.Now,
                Date_Fin = DateTime.Now.AddDays(1),
                Statut = "Not Started",
                Priorite = 1
            };
            _repository.Add(newTodo);

            var updatedTodo = new Todo
            {
                Id = newTodo.Id,
                Name = "Updated Todo",
                Date_Debut = DateTime.Now.AddDays(2),
                Date_Fin = DateTime.Now.AddDays(3),
                Statut = "In Progress",
                Priorite = 2
            };

            _repository.Update(updatedTodo);

            var todos = _repository.GetAll();
            var updatedTodoFromRepo = todos.FirstOrDefault(todo => todo.Id == newTodo.Id);
            Assert.NotNull(updatedTodoFromRepo);
            Assert.Equal("Updated Todo", updatedTodoFromRepo.Name);
            Assert.Equal("In Progress", updatedTodoFromRepo.Statut);
            Assert.Equal(2, updatedTodoFromRepo.Priorite);
        }

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

            _repository.Add(newTodo);
            _repository.Delete(newTodo.Id);

            var deletedTodo = _repository.GetById(newTodo.Id);
            Assert.Null(deletedTodo);
        }
    }
}