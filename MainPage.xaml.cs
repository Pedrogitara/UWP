using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Linq;
using Windows.ApplicationModel.Core;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.System.Profile;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace STC
{
    public sealed partial class MainPage : Page
    {
        AppContext db = null;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if ((e.Parameter as User) != null)
            {
                User thisUser = e.Parameter as User;

                if (thisUser.username.Length != 0 && thisUser.password.Length != 0 && thisUser.username != "admin")
                {
                    string password = passwordEncode(thisUser.password);

                    try
                    {
                        User adminUser = db.UserSet.Single(item => item.username == "admin");
                        db.UserSet.Remove(adminUser);
                        db.SaveChanges();
                    }

                    catch
                    {
                        // Exception
                    }

                    // User not exists
                    if (db.UserSet.Any(user => user.username == thisUser.username) == false)
                    {
                        try
                        {
                            db.UserSet.Add(new User { username = thisUser.username, password = password, accountType = 1 });
                            db.SaveChanges();
                        }
                        catch (Exception exc)
                        {
                            MessageDialog info = new MessageDialog("Ups! It is app error.", "Warning");
                            info.ShowAsync();
                        }
                    }

                }

                MessageDialog welcome = new MessageDialog(String.Format("Username: {0}\nPassword: {1}\n\nRemember it!", thisUser.username, thisUser.password), "Admin Account");
                welcome.ShowAsync();
            }

            this.Frame.Navigate(typeof(MainPage));

            base.OnNavigatedTo(e);  
        }

        public string passwordEncode(string password)
        {
            return CryptographicBuffer.EncodeToBase64String(HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha1).HashData(CryptographicBuffer.ConvertStringToBinary(password, BinaryStringEncoding.Utf8)));
        }

        public MainPage()
        {
            InitializeComponent();
            CoreApplication.Exiting += App_Closing;
            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;

            // Add back button on Windows 10 Desktop
            if(AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Desktop")
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            db = new AppContext();

            // Create example users if not exists
            if (!db.UserSet.Any(item => item.id >= 1))
            {
                db.UserSet.Add(new User { id = 1, username = "admin", password = passwordEncode("admin"), accountType = 1 });
                db.UserSet.Add(new User { id = 2, username = "user", password = passwordEncode("user"), accountType = 2 });

                db.SaveChanges();

                // Construct the visuals of the toast
                ToastVisual visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = "Information"
                            },

                            new AdaptiveText()
                            {
                                Text = "New database created! Type your credentials to create admin account."
                            },
                        }
                    }
                };

                // Construct the actions for the toast (inputs and buttons)
                ToastActionsCustom actions = new ToastActionsCustom()
                {
                    Inputs =
                    {
                        new ToastTextBox("login")
                        {
                            PlaceholderContent = "Login"
                        },

                        new ToastTextBox("password")
                        {
                            PlaceholderContent = "Password"
                        }
                    },

                    Buttons =
                    {
                        new ToastButton("Create", new QueryString()
                        {
                            { "action", "adminCreation" }

                        }.ToString())
                        {
                            ActivationType = ToastActivationType.Foreground,
                        }
                    }
                };

                // Now we can construct the final toast content
                ToastContent toastContent = new ToastContent()
                {
                    Visual = visual,
                    Actions = actions,

                    // Arguments when the user taps body of toast
                    Launch = new QueryString()
                    {
                        { "action", "adminDefault" },
                        { "login", "admin" },
                        { "password", "admin" }
                    }.ToString()
                };

                // And create the toast notification
                var toast = new ToastNotification(toastContent.GetXml());

                ToastNotificationManager.CreateToastNotifier().Show(toast);
            }

            // Write login and password from local storage
            ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
            if ((roamingSettings.Values["login"] != null) && (roamingSettings.Values["password"] != null))
            {
                username.Text = (string)roamingSettings.Values["login"];
                password.Password = (string)roamingSettings.Values["password"];
            }
        }

        private void App_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                return;
            }
                
            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            DoLogin();
        }

        private void DoLogin()
        {
            // Login to system
            if (username.Text.Length != 0 && password.Password.Length != 0)
            {
                string passHash = passwordEncode(password.Password);
                User thisUser = db.UserSet.SingleOrDefault(user => user.username == username.Text && user.password == passHash);

                if (thisUser != null)
                {
                    // Log OK
                    ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

                    roamingSettings.Values["login"] = username.Text;
                    roamingSettings.Values["password"] = password.Password;

                    PayLoadData data = new PayLoadData();
                    data.thisPage = this;
                    data.thisContext = db;
                    data.thisUser = thisUser;

                    Frame.Navigate(typeof(ThreadList), data);
                }

                else
                {
                    // Log WRONG
                    MessageDialog info = new MessageDialog("Password is wrong.", "Warning");
                    info.ShowAsync();
                }
            }

            else
            {
                MessageDialog info = new MessageDialog("Username or password is empty!", "Warning");
                info.ShowAsync();
            }
        }

        private void App_Closing(object sender, object e)
        {
            // Close database on exit
            db.Dispose();
        }

        private void MenuClose_Click(object sender, RoutedEventArgs e)
        {
            // Close app
            CoreApplication.Exit();
        }

        private void MenuRegister_Click(object sender, RoutedEventArgs e)
        {
            // Register
            Frame.Navigate(typeof(Register), db);
        }

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            // About
            MessageDialog info = new MessageDialog("App created by Piotr Sumionka.", "Information");
            info.ShowAsync();
        }

        private void password_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                DoLogin();
            }
        }
    }
}