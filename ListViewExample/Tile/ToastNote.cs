using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using NotificationsExtensions.Toasts;
using ListViewExample.Model;
using Microsoft.QueryStringDotNET;

namespace ListViewExample.Tile
{

    public class ToastNote
    {
        public static DateTime now = DateTime.UtcNow;
        public static DateTime current = DateTime.UtcNow;

        private static void show(XmlDocument content, DateTime deliveryTime, string id)
          
        {
            
             var scheduledToast = new ScheduledToastNotification(content, new DateTimeOffset(deliveryTime));
              scheduledToast.Tag = id;
            scheduledToast.Group = "toastgroup";
            ToastNotificationManager.CreateToastNotifier().AddToSchedule(scheduledToast);


        }

        public static void ToastNotes()

        {

            using (var db = DBConnection.DbConnection)
            {
               
                List<Model.Task> list = (from p in db.Table<Model.Task>() where p.completed == false select p).ToList();
                int count = list.Count();
                foreach (Model.Task i in list)
                {
                    if (count != 0 && i.reminder > DateTime.Now)
                    {
                        string title = null;
                        string note = null;
                        string id = null;
                        DateTime time = i.reminder;
                        DateTime due = i.datetime;
                        TimeSpan now1 = time.Subtract(now);
                        id += i.id;
                        note += i.notes;
                        title += i.title;
                        
                        MakeToast(title, time, due, id, note);
                    }

                }
                

            }
        }


         private static void MakeToast(string title, DateTime date, DateTime due, string id, string note)

          {
              string duedate = null; 
              duedate = due.ToString("MM/dd/yy");

              ToastContent content = new ToastContent()
              {
                  Scenario = ToastScenario.Reminder,

                  Visual = new ToastVisual()
                  {
                      TitleText = new ToastText() { Text = title },
                      BodyTextLine1 = new ToastText() { Text = note },
                      BodyTextLine2 = new ToastText() { Text = duedate }
                  },

                  Launch = "392914",

                  Actions = new ToastActionsCustom()
                  {
                      Inputs =
                      {
                          new ToastSelectionBox("snoozeAmount")
                          {
                              Title = "Remind me...",
                              Items =
                              {
                                  new ToastSelectionBoxItem("1", "Super soon (1 min)"),
                                  new ToastSelectionBoxItem("5", "In a few mins"),
                                  new ToastSelectionBoxItem("15", "When it starts"),
                                  new ToastSelectionBoxItem("60", "After it's done")
                              },
                              DefaultSelectionBoxItemId = "1"
                          }
                      },

                      Buttons =
                      {
                          new ToastButtonSnooze()
                          {
                              SelectionBoxId = "snoozeAmount"
                          },

                          new ToastButtonDismiss()
                      },
                  }
              };
            var toast = new ToastNotification(content.GetXml());
            toast.Tag = id;
            toast.Group = "ToastGroup";

            XmlDocument doc = content.GetXml();
              show(doc, date, id);
          }
          

    }
}
