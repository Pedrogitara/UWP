using System;
using System.Linq;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace STC
{
    public sealed partial class Register : Page
    {
        AppContext context = null;

        public Register()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            context = e.Parameter as AppContext;
            base.OnNavigatedTo(e);
        }

        public string passwordEncode(string password)
        {
            return CryptographicBuffer.EncodeToBase64String(HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha1).HashData(CryptographicBuffer.ConvertStringToBinary(password, BinaryStringEncoding.Utf8)));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length != 0 && textBox1.Password.Length != 0)
            {
                string password = passwordEncode(textBox1.Password);

                // User not exists
                if (context.UserSet.Any(user => user.username == textBox.Text) == false)
                {
                    try
                    {
                        context.UserSet.Add(new User { username = textBox.Text, password = password, accountType = 2 });
                        context.SaveChanges();

                        Frame.Navigate(typeof(MainPage));
                    }
                    catch (Exception exc)
                    {
                        MessageDialog info = new MessageDialog("Ups! It is app error.", "Warning");
                        info.ShowAsync();
                    }
                }

                else
                {
                    MessageDialog info = new MessageDialog("User exists! Create new once.", "Warning");
                    info.ShowAsync();
                }
            }

            else
            {
                MessageDialog info = new MessageDialog("Username or password is empty!", "Warning");
                info.ShowAsync();
            }

        }
    }
}