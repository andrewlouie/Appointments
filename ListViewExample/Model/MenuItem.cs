using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListViewExample.Model
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Type ClassType { get; set; }
    }

    public class ListManager
    {
        public static List<MenuItem> GetList()
        {
            var listitems = new List<MenuItem>();

            listitems.Add(new MenuItem { Id = 1, Title = "New Task", ClassType = typeof(Page1) });
            listitems.Add(new MenuItem { Id = 2, Title = "Calendar", ClassType = typeof(Page2) });
            listitems.Add(new MenuItem { Id = 3, Title = "Today's", ClassType = typeof(Page3) });
            listitems.Add(new MenuItem { Id = 4, Title = "Tomorrow's", ClassType = typeof(Page3) });
            listitems.Add(new MenuItem { Id = 5, Title = "Upcoming Week", ClassType = typeof(Page3) });
            listitems.Add(new MenuItem { Id = 6, Title = "All Tasks", ClassType = typeof(Page3) });

            return listitems;
        }
    }
}

