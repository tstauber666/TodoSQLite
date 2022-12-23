using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoSQLite.Data;

namespace TodoSQLite.Models
{

    public class SimpleDataGridPageViewModel : BindableObject
    {
        static Random random = new();
       
        
        //public ObservableCollection<TodoItem> TodoItems { get; set; } = new();
        //public SimpleDataGridPageViewModel(TodoItemDatabase todoItemDatabase)
        //{
            
        //    database = todoItemDatabase;
        //    BindingContext = this;

        //    Task<List<TodoItem>> Items = GetItemsAsync(database);
        //}


        public ObservableCollection<TodoItem> Items { get; } = new();
        public SimpleDataGridPageViewModel()
        {

            TodoItemDatabase database = new TodoItemDatabase();

            
           GetItemsAsync(database);

           
        }
        protected  async void GetItemsAsync(TodoItemDatabase database)
        {
            var items = await database.GetItemsAsync();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if(Items.Count > 0)   Items.Clear();
                foreach (var item in items)
                    Items.Add(item);

            });
        }
        public class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }

}
