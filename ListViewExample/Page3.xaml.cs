﻿using ListViewExample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using ListViewExample.Tile;
using Windows.UI;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ListViewExample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page3 : Page
    {
        private List<Task> TaskList;
        public static MenuItem SelectedMenuItem;

        public Page3()
        {
            using (var db = DBConnection.DbConnection)
            {
                TaskList = new List<Task>();
                List<Task> fulllist = (from p in db.Table<Task>() select p).ToList();
                List<Task> list;
                switch (SelectedMenuItem.Id)
                {
                    case 2: list = (from p in fulllist where p.datetime.Date == DateTime.Today && p.completed == false select p).ToList(); break;
                    case 3: list = (from p in fulllist where p.datetime.Date == DateTime.Today.AddDays(1) && p.completed == false select p).ToList(); break;
                    case 4: list = (from p in fulllist where p.datetime.Date < DateTime.Today.AddDays(7) && p.datetime.Date >= DateTime.Today && p.completed == false select p).ToList(); break;
                    case 5: list = (from p in fulllist where p.completed == true select p).ToList(); break;
                    case 6: list = (from p in fulllist select p).ToList(); break;
                    default: list = (from p in fulllist where p.categoryid == SelectedMenuItem.category.id && p.completed == false select p).ToList(); break;
                }
                if (list != null)
                {
                    foreach (Task a in list)
                    {
                        //add items without parents
                        if (a.parentid == -1 || list.GetItemId(a.parentid) == -1) TaskList.Add(a);
                    }
                    foreach (Task a in list)
                    {
                        //add items with parents after the parents
                        if (a.parentid >= 0) TaskList.Insert(TaskList.GetItemId(a.parentid) + 1, a);
                    }
                }
            }
            this.InitializeComponent();
            taskTitle.Text = SelectedMenuItem.Title;
        }

        private void TaskControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox scenarioListBox = sender as ListBox;
            Task s = scenarioListBox.SelectedItem as Task;
            if (scenarioListBox.SelectedItem != null)
            {
                this.Parent.SetValue(BackgroundProperty, Background = new SolidColorBrush( Colors.Green));
                Page1.CurrentItem = s;
                this.Frame.Navigate(typeof(Page1));
            }

        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var item = ((Button)sender).DataContext;
            var container = (ListBoxItem)TaskControl.ContainerFromItem(item);
            container.IsSelected = true;
            Task s = TaskControl.SelectedItem as Task;
            if (TaskControl.SelectedItem != null)
            {
                using (var db = DBConnection.DbConnection)
                {
                    var list = (from p in db.Table<Task>() where p.parentid == s.id select p).ToList();
                    foreach (Task stask in list) { stask.parentid = -1; }
                    int taskid = s.id;
                    deletingToast.eatingToast(taskid);
                    db.UpdateAll(list);
                    db.Delete(s);
                }
                this.Frame.Navigate(typeof(Page3));
            }
        }


        private void Button_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //complete button
            var item = ((Button)sender).DataContext;
            var container = (ListBoxItem)TaskControl.ContainerFromItem(item);
            container.IsSelected = true;
            Task s = TaskControl.SelectedItem as Task;
            if (TaskControl.SelectedItem != null)
            {
                using (var db = DBConnection.DbConnection)
                {
                    var list = (from p in db.Table<Task>() where p.parentid == s.id select p).ToList();
                    foreach (Task stask in list) { stask.completed = true; }
                    s.completed = true;
                    list.Add(s);
                    db.UpdateAll(list);

               }
                
                this.Frame.Navigate(typeof(Page3));
            }
        }
    }
}
