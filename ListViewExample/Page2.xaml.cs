using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ListViewExample.Model;
using Windows.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ListViewExample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page2 : Page
    {
       IEnumerable<DateTime> allTasks;
        List<Task> TTasks;
        public Page2()
        {
            using (var db = DBConnection.DbConnection)
            {

                TTasks = (from p in db.Table<Task>() where p.completed == false select p).ToList();
                allTasks = from p in TTasks select p.datetime.Date;
                
            }
            this.InitializeComponent();
            myCal.CalendarViewDayItemChanging += reloadedItems;
        }

        private void CalendarView_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            var selectedDates = sender.SelectedDates.Select(p => p.DateTime.Date);

            var tDates = from p in allTasks where p == selectedDates.First() select p;
            if(tDates.Count() == 0)
            {
                Page1.CurrentItem = null;
                this.Frame.Navigate(typeof(Page1));
            }
            else if(tDates.Count() == 1){
                
                Page1.CurrentItem = (from a in TTasks where a.datetime.Date == selectedDates.First() select a).First();
                this.Frame.Navigate(typeof(Page1));

            }
            else
            {
                Page4.taskdate = selectedDates.First();
                this.Frame.Navigate(typeof(Page4));
            }
            
            
        }

        private void popitems()
        {
            
        }

        private void reloadedItems(CalendarView sender, CalendarViewDayItemChangingEventArgs e)
        {
            CalendarViewDayItem cvdi = e.Item;

            if (allTasks.Contains(cvdi.Date.Date))
            {
                cvdi.Background = new SolidColorBrush(Colors.Green);
                cvdi.BorderThickness = new Thickness(2, 2, 2, 2);
                var brush = new SolidColorBrush(Color.FromArgb(1, 59, 89, 152));
                cvdi.BorderBrush = new SolidColorBrush(Colors.Blue);

            }
        }

        private void CalendarView_Loaded(object sender, RoutedEventArgs e)
        {

            
        }

        private void myCal_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
           
        }
    }
    }
