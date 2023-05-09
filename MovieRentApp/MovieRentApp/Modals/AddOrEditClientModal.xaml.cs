using MovieRentApp.MVVM.Models;
using System.Windows;
using System.Windows.Input;

namespace MovieRentApp.Modals
{
    /// <summary>
    /// Interaction logic for AddOrEditClientModal.xaml
    /// </summary>
    public partial class AddOrEditClientModal : Window
    {
        public AddOrEditClientModal(Client client = null)
        {
            InitializeComponent();
            if (IsEditOperation(client))
                SetFormValues();
        }

        public Client Client { get; private set; }

        private bool IsEditOperation(Client client)
        {
            if (client is not null)
            {
                Client = (Client)client.Clone();
                return true;
            }
            return false;
        }

        private void SetFormValues()
        {
            txtFirstName.Text = Client.FirstName;
            txtLastName.Text = Client.LastName;
            txtAge.Text = Client.Age.ToString();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                var savedClient = new Client
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Age = int.Parse(txtAge.Text)
                };

                if (Client != null)
                {
                    savedClient.Id = Client.Id;
                }

                Client = (Client)savedClient.Clone();
                DialogResult = true;
                Close();
            }
            else
            {
                var messageBox = new WarningMessageBox();
                messageBox.ShowDialog();
            }
        }

        private bool ValidateForm()
        {
            return !string.IsNullOrEmpty(txtFirstName.Text)
                && !string.IsNullOrEmpty(txtLastName.Text)
                && !string.IsNullOrEmpty(txtAge.Text)
                && txtAge.Text.Length <= 2;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out int _);
        }

        private void BorderMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
