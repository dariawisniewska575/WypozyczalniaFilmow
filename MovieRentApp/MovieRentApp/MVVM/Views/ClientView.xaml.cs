using MovieRentApp.Modals;
using MovieRentApp.MVVM.Models;
using MovieRentApp.MVVM.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MovieRentApp.MVVM.Views
{
    /// <summary>
    /// Interaction logic for ClientView.xaml
    /// </summary>
    public partial class ClientView : UserControl
    {
        public ClientView()
        {
            InitializeComponent();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MovieDataGrid.SelectedItem == null || MovieDataGrid.SelectedItem is not Client selectedClient)
                return;
            var vm = (ClientViewModel)DataContext;
            vm.RemoveClient(selectedClient);
            vm.Clients.Remove(selectedClient);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (MovieDataGrid.SelectedItem == null || MovieDataGrid.SelectedItem is not Client selectedClient)
                return;
            var addOrEditClientDialog = new AddOrEditClientModal(selectedClient);
            bool? result = addOrEditClientDialog.ShowDialog();

            if (result == false)
                return;

            var vm = (ClientViewModel)DataContext;
            var clientToEdit = addOrEditClientDialog.Client;
            if (clientToEdit is null)
                return;

            vm.EditClient(clientToEdit);
            var indexNonEditedClient = vm.Clients.IndexOf(selectedClient);
            vm.Clients[indexNonEditedClient] = clientToEdit;
            MovieDataGrid.Items.Refresh();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addClientDialog = new AddOrEditClientModal();
            bool? result = addClientDialog.ShowDialog();

            if (result == true)
            {
                var vm = (ClientViewModel)DataContext;
                if (addClientDialog.Client is null)
                    return;
                var newClient = vm.AddClient(addClientDialog.Client);
                vm.Clients.Add(newClient);
            }
        }
    }
}
