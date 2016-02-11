using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using NotificationsExtensions.Toasts;
using ListViewExample.Model;

namespace ListViewExample.Tile
{
    class deletingToast
    {
        public static void eatingToast(int id)
        {
            using (var db = DBConnection.DbConnection)
            {

                List<Model.Task> list = (from p in db.Table<Model.Task>() select p).ToList();
                int count = list.Count();
                foreach (Model.Task i in list)
                {
                 var schedual = ToastNotificationManager.CreateToastNotifier().GetScheduledToastNotifications();
                        foreach ( ScheduledToastNotification note in schedual)
                        {
                            string idString = id.ToString();
                            if (note.Tag == idString) {

                                ToastNotificationManager.CreateToastNotifier().RemoveFromSchedule(note);
                                    }
                        }
                       
                }
            }
        }
        
    }
}

