using System;
using System.Globalization;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace STC
{
    public sealed partial class ThreadAdd : Page
    {
        AppContext db = null;
        User thisUser = null;

        public ThreadAdd()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //date.Text = DateTime.UtcNow.ToString();
            date.Text = DateTime.Now.ToString(new CultureInfo("pl-PL"));
            db = (e.Parameter as PayLoadData).thisContext;
            thisUser = (e.Parameter as PayLoadData).thisUser;
            base.OnNavigatedTo(e);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string textContent = "";
                content.Document.GetText(Windows.UI.Text.TextGetOptions.None, out textContent);
                db.ThreadSet.Add(new Thread { title = title.Text, content = textContent, date = date.Text, user = thisUser });
                db.SaveChanges();
            }

            catch (Exception exc)
            {
                MessageDialog alert = new MessageDialog(exc.Message, "Exception");
                alert.ShowAsync();
            }

            Frame.GoBack();
        }
    }
}