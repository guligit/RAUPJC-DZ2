using GenericList;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }

        public TodoItem(string text)
        {
            Id = Guid.NewGuid(); // Generates new unique identifier
            Text = text;
            IsCompleted = false;
            DateCreated = DateTime.Now; // Set creation date as current time
        }

    }

    namespace Interfaces
    {
        public interface ITodoRepository
        {
            /// <summary >
            /// Gets TodoItem for a given id
            /// </ summary >
            /// <returns > TodoItem if found , null otherwise </ returns >
            TodoItem Get(Guid todoId);

            /// <summary >
            /// Adds new TodoItem object in database .
            /// If object with the same id already exists ,
            /// method should throw DuplicateTodoItemException with the message" duplicate id: {id }".
            /// </ summary >
            void Add(TodoItem todoItem);

            /// <summary >
            /// Tries to remove a TodoItem with given id from the database .
            /// </ summary >
            /// <returns > True if success , false otherwise </ returns >
            bool Remove(Guid todoId);

            /// <summary >
            /// Updates given TodoItem in database .
            /// If TodoItem does not exist , method will add one .
            /// </ summary >
            void Update(TodoItem todoItem);

            /// <summary >
            /// Tries to mark a TodoItem as completed in database .
            /// </ summary >
            /// <returns > True if success , false otherwise </ returns >
            bool MarkAsCompleted(Guid todoId);

            /// <summary >
            /// Gets all TodoItem objects in database , sorted by date created( descending )
            /// </ summary >
            List<TodoItem> GetAll();

            /// <summary >
            /// Gets all incomplete TodoItem objects in database
            /// </ summary >
            List<TodoItem> GetActive();

            /// <summary >
            /// Gets all completed TodoItem objects in database
            /// </ summary >
            List<TodoItem> GetCompleted();

            /// <summary >
            /// Gets all TodoItem objects in database that apply to the filter
            /// </ summary >
            List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction);
        }
    }

    namespace Repositories
    {
        /// <summary >
        /// Class that encapsulates all the logic for accessing TodoTtems .
        /// </ summary >
        public class TodoRepository : ITodoRepository
        {
            /// <summary >
            /// Repository does not fetch todoItems from the actual database ,
            /// it uses in memory storage for this excersise .
            /// </ summary >
            private readonly IGenericList<TodoItem> _inMemoryTodoDatabase;
            public TodoRepository(IGenericList<TodoItem> initialDbState = null)
            {
                if (initialDbState != null)
                    _inMemoryTodoDatabase = initialDbState ?? new GenericList<TodoItem>();
            }

            public void Add(TodoItem todoItem)
            {
                TodoItem id = Get(todoItem.Id);
                if (id != null)
                    throw new DuplicateTodoItemException("duplicate id: {0}", id);
                else
                    _inMemoryTodoDatabase.Add(todoItem);
            }

            public TodoItem Get(Guid todoId)
            {
                TodoItem item = _inMemoryTodoDatabase.Where(i => i.Id == todoId)
                                                                  .FirstOrDefault();
                return item;
            }

            public List<TodoItem> GetActive()
            {
                List<TodoItem> active = _inMemoryTodoDatabase.Where(i => i.IsCompleted == false).ToList();
                return active;
            }

            public List<TodoItem> GetAll()
            {
                List<TodoItem> items = _inMemoryTodoDatabase.OrderByDescending(i => i.DateCreated).ToList();
                return items;
            }

            public List<TodoItem> GetCompleted()
            {
                List<TodoItem> completed = _inMemoryTodoDatabase.Where(i => i.IsCompleted == true).ToList();
                return completed;
            }

            public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
            {
                List<TodoItem> filtered = _inMemoryTodoDatabase.Where(i => filterFunction(i) == true).ToList();
                return filtered;
            }

            public bool MarkAsCompleted(Guid todoId)
            {
                TodoItem item = Get(todoId);
                if (item.IsCompleted)
                    return false;
                else
                {
                    item.IsCompleted = true;
                    return true;
                }
            }

            public bool Remove(Guid todoId)
            {
                TodoItem item = Get(todoId);
                if (item == null)
                    return false;
                else
                {
                    _inMemoryTodoDatabase.Remove(item);
                    return true;
                }
            }

            public void Update(TodoItem todoItem)
            {
                Guid id = todoItem.Id;
                TodoItem item = Get(id);

                if (item == null)
                    Add(item);
                else
                    item = todoItem;
            }
        }

        [Serializable]
        internal class DuplicateTodoItemException : Exception
        {
            private TodoItem id;
            private string v;

            public DuplicateTodoItemException()
            {
            }

            public DuplicateTodoItemException(string message) : base(message)
            {
            }

            public DuplicateTodoItemException(string message, Exception innerException) : base(message, innerException)
            {
            }

            public DuplicateTodoItemException(string v, TodoItem id)
            {
                this.v = v;
                this.id = id;
            }

            protected DuplicateTodoItemException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}