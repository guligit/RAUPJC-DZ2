using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace ModelsTests
{
    [TestClass]
    public class TodoRepositoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingNullToDatabaseThrowsException()
        {
            ITodoRepository repository = new TodoRepository();
            repository.Add(null);
        }

        [TestMethod]
        public void AddingItemWillAddToDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);

            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.IsTrue(repository.Get(todoItem.Id) != null);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateTodoIdemException))]
        public void AddingExistingItemWillThrowException()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");

            repository.Add(todoItem);
            repository.Add(todoItem);
        }

        [TestMethod]
        public void RemovingItemWillRemoveFromDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");

            repository.Add(todoItem);
            bool returnValue = repository.Remove(todoItem.Id);

            Assert.AreEqual(0, repository.GetAll().Count);
            Assert.AreEqual(true, returnValue);
            Assert.IsTrue(repository.Get(todoItem.Id) == null);
        }

        [TestMethod]
        public void GettingItemWillGetFromDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");

            repository.Add(todoItem);

            TodoItem item = repository.Get(todoItem.Id); 
            Assert.AreEqual(item, todoItem);
        }

        [TestMethod]
        public void RemovingNullFromDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem todoItem = new TodoItem("Groceries");

            TodoItem newItem = repository.Get(todoItem.Id);

            Assert.AreEqual(null, newItem); 

        }

        [TestMethod]
        public void GettingActiveItemsWillGetFromDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            var todoItem2 = new TodoItem("Cloth");
            todoItem2.MarkAsCompleted();

            repository.Add(todoItem);
            repository.Add(todoItem2);

            Assert.AreEqual(1, repository.GetActive().Count);
        }

        [TestMethod]
        public void GettingAllItemsWillGetFromDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            var todoItem2 = new TodoItem("Cloth");
            todoItem2.MarkAsCompleted();

            repository.Add(todoItem);
            repository.Add(todoItem2);

            Assert.AreEqual(2, repository.GetAll().Count);
        }

        [TestMethod]
        public void GettingCompletedItemsWillGetFromDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            var todoItem2 = new TodoItem("Cloth");
            todoItem2.MarkAsCompleted();

            repository.Add(todoItem);
            repository.Add(todoItem2);

            Assert.AreEqual(1, repository.GetCompleted().Count);
        }

    }
}