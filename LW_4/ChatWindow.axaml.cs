using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Supabase.Gotrue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LW_4
{
    public partial class ChatWindow : Window
    {
        private Supabase.Client supabaseClient;

        private ListBox messagesListBox;
        private TextBox messageTextBox;
        private Button sendButton;

        public Session Session;
        public delegate void Load();
        public ChatWindow()
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

            var meassageTabel = supabaseClient.From<Message>();

            Load load = LoadMessages;
            Dispatcher.UIThread.InvokeAsync(new Action(load));

            await meassageTabel.On(Supabase.Client.ChannelEventType.All, async (sender, ev) =>
            {
                Dispatcher.UIThread.InvokeAsync(new Action(load));
            });

        }

        public void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            messagesListBox = this.FindControl<ListBox>("MessagesListBox");
            messageTextBox = this.FindControl<TextBox>("MessageTextBox");
            sendButton = this.FindControl<Button>("SendButton");
        }

        public async void OnSendButtonClick(object sender, RoutedEventArgs args)
        {
            var messageText = messageTextBox.Text;

            var message = new Message { Email = Session.User.Email, Text = messageText };

            var result = await supabaseClient.From<Message>().Insert(message);

            messageTextBox.Clear();
        }

        private async void LoadMessages()
        {
            var result = await supabaseClient.From<Message>()
                .Select(x => new object[] { x.Email, x.Text })
                .Get();

            messagesListBox.Items = result.Models;
        }
    }
}
