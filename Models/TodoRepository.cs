using GenericList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Models
{
        public class TodoRepository : ITodoRepository
        {
            /// <summary >
            /// Repository does not fetch todoItems from the actual database ,
            /// it uses in memory storage for this excersise .
            /// </ summary >
            private readonly IGenericList<TodoItem> _inMemoryTodoDatabase;
            public TodoRepository(IGenericList<TodoItem> initialDbState = null)
            {
                _inMemoryTodoDatabase = initialDbState ?? new GenericList<TodoItem>();
            }

            public void Add(TodoItem todoItem) 
            {
                if (todoItem == null) throw new ArgumentNullException();
                if (Get(todoItem.Id) == todoItem) throw new DuplicateTodoIdemException();
                _inMemoryTodoDatabase.Add(todoItem);
               
             
            }

            public TodoItem Get(Guid todoId)
            {
                TodoItem item = _inMemoryTodoDatabase.FirstOrDefault(i => i.Id == todoId);
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

       
}


