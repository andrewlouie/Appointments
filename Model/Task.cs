using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListViewExample.Model
{
    public enum Priority
    {
        Low,
        Medium,
        High,
        Urgent
    }
    public class Task
    {
        public int id { get; set; } //
        public string title { get; set; } //
        public DateTime datetime { get; set; } //
        public string notes { get; set; } //
        public Category category { get; set; } //
        public Task parent { get; set; }
        public Priority priority { get; set; }
        public DateTime reminder { get; set; }
        public bool livetile { get; set; } //
        public bool completed { get; set; } //
    }
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
