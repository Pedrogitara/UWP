using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace STC
{
    public sealed partial class ThreadWindow : Page
    {
        AppContext context = null;
        Thread thisThread = null;
        User thisUser = null;

        public ThreadWindow()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            context = (e.Parameter as PayLoadData).thisContext;
            thisThread = (e.Parameter as PayLoadData).thisThread;
            thisUser = (e.Parameter as PayLoadData).thisUser;

            textBlock.Text = thisThread.title;
            textBlock1_Copy.Text = "Thread date: " + thisThread.date;

            content.Document.SetText(TextSetOptions.None, thisThread.content);
            content.IsReadOnly = true;

            dataList.ItemsSource = thisThread.posts;

            if (thisUser.accountType != 1)
            {
                button2.IsEnabled = false;
                deletePost.IsEnabled = false;
            }

            base.OnNavigatedTo(e);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            // Add post
            PayLoadData data = new PayLoadData();
            data.thisContext = context;
            data.thisThread = thisThread;
            data.thisUser = thisUser;

            this.Frame.Navigate(typeof(PostAdd), data);
        }

        private async void dataList_DoubleClick(object sender, RoutedEventArgs e)
       {
          // Open post
          Post thisPost = (Post)dataList.SelectedItem;

          try
          {
                MediaElement mediaElement = new MediaElement();
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
                Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(thisPost.content);
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

                MessageDialog alert = new MessageDialog(thisPost.content, "Post content");
                alert.ShowAsync();
            }

            catch (Exception exc)
            {
                MessageDialog alert = new MessageDialog(exc.Message, "Exception");
                alert.ShowAsync();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Refresh
            dataList.ItemsSource = null;
            dataList.ItemsSource = thisThread.posts;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // Delete thread
            try
            {
                var record = context.ThreadSet.Include(item => item.posts).SingleOrDefault(item => item.id == thisThread.id);
                context.ThreadSet.Remove(record);
                context.SaveChanges();
            }

            catch (Exception exc)
            {
                MessageDialog alert = new MessageDialog(exc.Message, "Exception");
                alert.ShowAsync();
            }

            Frame.GoBack();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            // Delete post
            Post thisPost = (Post)dataList.SelectedItem;

            try
            {
                if (thisPost == null) throw new NullReferenceException("Please select an item first!");
                var record = context.PostSet.Include(item => item.user).SingleOrDefault(item => item.id == thisPost.id);
                context.PostSet.Remove(record);
                thisThread.posts.Remove(record);
                context.SaveChanges();

                dataList.ItemsSource = null;
                dataList.ItemsSource = thisThread.posts;
            }

            catch (NullReferenceException exc)
            {
                MessageDialog alert = new MessageDialog(exc.Message, "Warning");
                alert.ShowAsync();
            }

            catch (Exception exc)
            {
                MessageDialog alert = new MessageDialog(exc.Message, "Exception");
                alert.ShowAsync();
            }
        }
    }
}
