using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace LW_4
{
    public partial class ErrorWindow : Window
    {
        private Button retryButton;
        public ErrorWindow()
        {
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            retryButton = this.FindControl<Button>("RetryButton");
        }

        public void OnRetryButtonClick(object sender, RoutedEventArgs args)
        {
            this.Close();
        }
    }
}
