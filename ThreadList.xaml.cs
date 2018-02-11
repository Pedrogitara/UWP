using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace STC
{
    public sealed partial class ThreadList : Page
    {
        AppContext db = null;
        User thisUser = null;

        public ThreadList()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            db = (e.Parameter as PayLoadData).thisContext;
            thisUser = (e.Parameter as PayLoadData).thisUser;
            
            try
            {
                dataList.ItemsSource = db.ThreadSet.Include(item => item.posts).Include(item => item.user).ToList();

                if (thisUser.accountType != 1)
                {
                    button2.IsEnabled = false;
                    users.IsEnabled = false;
                }
            }

            catch (Exception exc)
            {
                MessageDialog alert = new MessageDialog(exc.Message, "Exception");
                alert.ShowAsync();
            }

            base.OnNavigatedTo(e);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            // Add Thread
            PayLoadData data = new PayLoadData();
            data.thisContext = db;
            data.thisUser = thisUser;

            this.Frame.Navigate(typeof(ThreadAdd), data);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Refresh
            dataList.ItemsSource = db.ThreadSet.Include(item => item.posts).Include(item => item.user).ToList();
        }

        private void dataList_DoubleClick(object sender, RoutedEventArgs e)
        {
            // Open Thread
            try
            {
                Thread thisThread = (Thread)dataList.SelectedItem;

                PayLoadData data = new PayLoadData();
                data.thisContext = db;
                data.thisThread = thisThread;
                data.thisUser = thisUser;

                Frame.Navigate(typeof(ThreadWindow), data);

            }

            catch (Exception exc)
            {
                MessageDialog alert = new MessageDialog(exc.Message, "Exception");
                alert.ShowAsync();
            }

        }
        
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // Delete
            Thread thisThread = (Thread)dataList.SelectedItem;

            try
            {
                var record = db.ThreadSet.Include(item => item.posts).Include(item => item.user).SingleOrDefault(item => item.id == thisThread.id);
                db.ThreadSet.Remove(record);
                db.SaveChanges();

                dataList.ItemsSource = db.ThreadSet.Include(item => item.posts).Include(item => item.user).ToList();
            }

            catch (Exception exc)
            {
                MessageDialog alert = new MessageDialog(exc.Message, "Exception");
                alert.ShowAsync();
            }
        }

        private void filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataList.ItemsSource = db.ThreadSet.Include(item => item.posts).Include(item => item.user).Where(item => item.title.Contains(filter.Text) == true).ToList();
        }

        private void users_Click(object sender, RoutedEventArgs e)
        {
            PayLoadData data = new PayLoadData();
            data.thisContext = db;

            this.Frame.Navigate(typeof(UserList), data);
        }

    }
}