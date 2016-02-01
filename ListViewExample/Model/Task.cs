using System;
using SQLite;
using System.IO;
using Windows.Storage;

namespace ListViewExample.Model
{
    public class DBConnection
    {
        public static SQLiteConnection DbConnection
        {
            get
            {
                return new SQLiteConnection(Path.Combine(ApplicationData.Current.LocalFolder.Path, "Storage.sqlite"),false);
            }
        }
    }
    public enum Priority
    {
        Low,
        Medium,
        High,
        Urgent
    }
    public class Task
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [MaxLength(64)]
        public string title { get; set; }
        public DateTime datetime { get; set; }
        public string notes { get; set; }
        public int categoryid { get; set; }
        public int parentid { get; set; }
        public Priority priority { get; set; }
        public DateTime reminder { get; set; }
        public bool livetile { get; set; }
        public bool completed { get; set; } 
        public string datetimedisplay
        {
            get
            {
                string s;
                if (datetime.Date == DateTime.Today) s = "Today at " + datetime.TimeOfDay.ToString("hh\\:mm");
                else if (datetime.Date == DateTime.Today.AddDays(1)) s = "Tomorrow at " + datetime.TimeOfDay.ToString("hh\\:mm");
                else s = datetime.Date.ToString("MM/dd/yyyy") + " at " + datetime.TimeOfDay.ToString("hh\\:mm");
                if (parentid >= 0) return Indent(9) + s;
                return s;
            }
        }
        public string titledisplay { get
            {
                if (parentid >= 0) return Indent(9) + title;
                return title;
            }
        }
        public Category getCategory()
        {
            using (var db = DBConnection.DbConnection)
            {
                Category m = (from p in db.Table<Category>()
                              where p.id == categoryid
                              select p).FirstOrDefault();
                return m;
            }
        }
        public Task getParent()
        {
            using (var db = DBConnection.DbConnection)
            {
                Task m = (from p in db.Table<Task>()
                              where p.id == parentid
                              select p).FirstOrDefault();
                return m;
            }
        }
        public static string Indent(int count)
        {
            return "".PadLeft(count);
        }


    }
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [MaxLength(64)]
        public string name { get; set; }
    }

}
