using System;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace STC
{
    public sealed partial class UserList : Page
    {
        AppContext db = null;

        public UserList()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Delete.IsEnabled = false;
            db = (e.Parameter as PayLoadData).thisContext;
            dataList.ItemsSource = db.UserSet.ToList();

            base.OnNavigatedTo(e);
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            // Delete User
            try
            {
                User selUser = (User)dataList.SelectedItem;
                var record = db.UserSet.SingleOrDefault(item => item.id == selUser.id);
                if(selUser.accountType == 1)
                {
                    throw new Exception("You can't delete admin account!");
                }
                else
                {
                    db.UserSet.Remove(record);
                    db.SaveChanges();

                    dataList.ItemsSource = null;
                    dataList.ItemsSource = db.UserSet.ToList();
                }


                Delete.IsEnabled = false;
            }

            catch (Exception exc)
            {
                MessageDialog alert = new MessageDialog(exc.Message, "Exception");
                alert.ShowAsync();
            }
        }

        private void dataList_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Delete.IsEnabled = true;
        }
    }
}