using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace ListViewExample.Model
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Type ClassType { get; set; }
        public Category category { get; set; }
    }
    public class ListManager
    {
        public static ObservableCollection<MenuItem> GetList()
        {
            var listitems = new List<MenuItem>();

            listitems.Add(new MenuItem { Id = 0, Title = "New Task", ClassType = typeof(Page1) });
            listitems.Add(new MenuItem { Id = 1, Title = "Calendar", ClassType = typeof(Page2) });
            listitems.Add(new MenuItem { Id = 2, Title = "Today's", ClassType = typeof(Page3) });
            listitems.Add(new MenuItem { Id = 3, Title = "Tomorrow's", ClassType = typeof(Page3) });
            listitems.Add(new MenuItem { Id = 4, Title = "Upcoming Week", ClassType = typeof(Page3) });
            listitems.Add(new MenuItem { Id = 5, Title = "Completed Tasks", ClassType = typeof(Page3) });
            listitems.Add(new MenuItem { Id = 6, Title = "All Tasks", ClassType = typeof(Page3) });
            List<Category> cats;
            using (var db = DBConnection.DbConnection)
            {
                cats = (from p in db.Table<Category>()
                                       select p).ToList();
            }
            foreach (Category c in cats) { listitems.Add(new MenuItem { Id = c.id + 6, Title = c.name, category = c, ClassType = typeof(Page3) }); }

            return new ObservableCollection<MenuItem>(listitems);
        }
    }

}

