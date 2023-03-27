using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Supabase.Interfaces;
using Supabase;
using System;
using System.Threading.Tasks;
using System.Linq;
using Supabase.Core.Extensions;
using static Postgrest.Constants;
using System.Collections.Generic;
using Supabase.Realtime.Socket;
using System.IO.IsolatedStorage;

namespace LW_4
{
    public partial class MainWindow : Window
    {
        private Supabase.Client supabaseClient;

        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button registerButton;
        private Button loginButton;
        public MainWindow()
        {
            InitializeComponent();
            InitializeClient();
        }

        public async void InitializeClient()
        {
            var url = "https://gxcjvqriexsdszuiwazv.supabase.co";
            var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Imd4Y2p2cXJpZXhzZHN6dWl3YXp2Iiwicm9sZSI6ImFub24iLCJpYXQiOjE2Nzg4ODE2MzgsImV4cCI6MTk5NDQ1NzYzOH0.oCIZwKxPY38IdhB3eRWMteFdFPEmTIID5INjfyUaWeU";

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            supabaseClient = new Supabase.Client(url, key, options);
            await supabaseClient.InitializeAsync();
        }

        public void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            usernameTextBox = this.FindControl<TextBox>("UsernameTextBox");
            passwordTextBox = this.FindControl<TextBox>("PasswordTextBox");
            registerButton = this.FindControl<Button>("RegisterButton");
            loginButton = this.FindControl<Button>("LoginButton");

            IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForAssembly();
        }

        public async void OnRegisterButtonClick(object sender, RoutedEventArgs args)
        {
            var email = usernameTextBox.Text;
            var password = passwordTextBox.Text;

            try
            {
                await supabaseClient.Auth.SignUp(email, password);
            }
            catch (Exception ex)
            {
                var error = new ErrorWindow();
                error.Show();
            }
        }

        public async void OnLoginButtonClick(object sender, RoutedEventArgs args)
        {
            var email = usernameTextBox.Text;
            var password = passwordTextBox.Text;

            try
            {
                await supabaseClient.Auth.SignInWithPassword(email, password);

                var chatWindow = new ChatWindow();
                chatWindow.Show();
                chatWindow.Session = supabaseClient.Auth.CurrentSession;

                Close();
            }
            catch (Exception ex) 
            {
                var error = new ErrorWindow();
                error.Show();
            }
        }
    }
}