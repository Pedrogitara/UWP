using System;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace STC
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PostAdd : Page
    {
        AppContext context = null;
        Thread thisThread = null;
        User thisUser = null;

        public PostAdd()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.context = (e.Parameter as PayLoadData).thisContext;
            this.thisThread = (e.Parameter as PayLoadData).thisThread;
            this.thisUser = (e.Parameter as PayLoadData).thisUser;

            //date.Text = DateTime.UtcNow.ToString();
            date.Text = DateTime.Now.ToString(new CultureInfo("pl-PL"));

            base.OnNavigatedTo(e);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Post newPost = new Post { content = textBox.Text, date = date.Text, user = thisUser };
                thisThread.posts.Add(newPost);
                context.SaveChanges();
            }

            catch (Exception exc)
            {
                // Exception
            }

            Frame.GoBack();
        }
    }
}