using ListViewExample.Model;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ListViewExample.Tile;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ListViewExample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page1 : Page
    {
        public static Task CurrentItem;
        private List<Task> OpenTasks;
        private List<Category> Categories;

        public Page1()
        {
            using (var db = DBConnection.DbConnection)
            {
                Categories = (from p in db.Table<Category>() select p).ToList();
                Categories.Insert(0, new Category { name = "None", id = -1 });
                int currentitemid = CurrentItem != null ? CurrentItem.id : -1;
                OpenTasks = (from p in db.Table<Task>() where p.completed == false && p.id != currentitemid && (currentitemid == -1 || p.parentid != currentitemid) select p).ToList();
                OpenTasks.Insert(0, new Task { title = "None", id = -1 });
            }
            this.InitializeComponent();
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectTextBox.Text == "" || SubjectTextBox.Text == null)
            {
                SubjectTextBox.Focus(FocusState.Programmatic);
                MyScrollView.ScrollToElement(TitleLabel);
                return;
            }
            Task t = CurrentItem ?? new Task();
            if (CurrentItem != null) { t.id = CurrentItem.id; }
            else { t.id = -1; }
            t.completed = CompletedCheckBox.IsChecked == true;
            t.title = SubjectTextBox.Text;
            t.notes = DetailsTextBox.Text;
            t.livetile = AllDayCheckBox.IsChecked == true;
            t.priority = (Priority)SensitivityComboBox.SelectedIndex;
            t.categoryid = Int32.Parse(BusyStatusComboBox.SelectedValue.ToString());
            Debug.WriteLine(DurationComboBox.SelectedValue);
            t.parentid = Int32.Parse(DurationComboBox.SelectedValue.ToString());
            if (TimeCheckBox.IsChecked == null || TimeCheckBox.IsChecked == false)
            {
                String dateTimeString = "";
                DateTime dateTime = DateTime.MinValue;

                dateTimeString = DateTime.Parse(StartTimeDatePicker.Date.ToString()).ToString("MM/dd/yyyy") + " " +
                                 DateTime.Parse(StartTimeTimePicker.Time.ToString()).ToString("h:mm tt");
                dateTime = DateTime.Parse(dateTimeString);
                t.datetime = dateTime;
            }
            if (ReminderCheckBox.IsChecked != null && ReminderCheckBox.IsChecked == true)
            {
                String dateTimeString = "";
                DateTime dateTime = DateTime.MinValue;

                dateTimeString = DateTime.Parse(StartTimeDatePickerRem.Date.ToString()).ToString("MM/dd/yyyy") + " " +
                                 DateTime.Parse(StartTimeTimePickerRem.Time.ToString()).ToString("h:mm tt");
                dateTime = DateTime.Parse(dateTimeString);
                t.reminder = dateTime;
            }
            using (var db = DBConnection.DbConnection)
            {
                if (t.id == -1) db.Insert(t);
                else db.Update(t);
            }
            this.Frame.Navigate(typeof(Page2));

            ToastNote.ToastNotes();
        }

        private void ReminderCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            StartTimeDatePickerRem.Visibility = Windows.UI.Xaml.Visibility.Visible;
            StartTimeTimePickerRem.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void ReminderCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            StartTimeDatePickerRem.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            StartTimeTimePickerRem.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
        private void TimeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            StartTimeDatePicker.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            StartTimeTimePicker.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void TimeCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            StartTimeDatePicker.Visibility = Windows.UI.Xaml.Visibility.Visible;
            StartTimeTimePicker.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void ToDo_List_Loaded(object sender, RoutedEventArgs e)
        {
            if (CurrentItem != null) {
                SubjectTextBox.Text = CurrentItem.title;
                DetailsTextBox.Text = CurrentItem.notes;
                AllDayCheckBox.IsChecked = CurrentItem.livetile;
                CompletedCheckBox.IsChecked = CurrentItem.completed;
                SensitivityComboBox.SelectedIndex = (int)CurrentItem.priority;
                CreateAppointmentButton.Content = "Update Task";
                if (CurrentItem.datetime.Date != DateTime.MinValue)
                {
                    StartTimeDatePicker.Date = CurrentItem.datetime.Date;
                    StartTimeTimePicker.Time = CurrentItem.datetime.TimeOfDay;
                }
                if (CurrentItem.reminder.Date != DateTime.MinValue)
                {
                    StartTimeDatePickerRem.Date = CurrentItem.reminder.Date;
                    StartTimeTimePickerRem.Time = CurrentItem.reminder.TimeOfDay;
                }
                
                DurationComboBox.SelectedValue = CurrentItem.parentid;
                if (DurationComboBox.SelectedValue == null) DurationComboBox.SelectedIndex = 0;
                BusyStatusComboBox.SelectedValue = CurrentItem.categoryid;
                if (BusyStatusComboBox.SelectedValue == null) BusyStatusComboBox.SelectedIndex = 0;
                BackButton.Visibility = Visibility.Visible;
            }
            else
            {
                DurationComboBox.SelectedIndex = 0;
                BusyStatusComboBox.SelectedIndex = 0;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Page3));
        }
    }
}
