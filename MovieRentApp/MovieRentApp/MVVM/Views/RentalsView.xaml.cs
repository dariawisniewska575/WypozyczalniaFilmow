using MovieRentApp.Modals;
using MovieRentApp.MVVM.Models;
using MovieRentApp.MVVM.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MovieRentApp.MVVM.Views
{
    /// <summary>
    /// Interaction logic for RentalsView.xaml
    /// </summary>
    public partial class RentalsView : UserControl
    {
        public RentalsView()
        {
            InitializeComponent();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (MovieDataGrid.SelectedItem == null || MovieDataGrid.SelectedItem is not Rental selectedRental)
                return;

            var vm = (RentalsViewModel)DataContext;

            var indexNonEditedRental = vm.Rentals.IndexOf(selectedRental);

            vm.Rentals[indexNonEditedRental].Movie.IsAvaiable = true;
            vm.Rentals[indexNonEditedRental].ReturnDate = DateTime.UtcNow;

            vm.EditRental(vm.Rentals[indexNonEditedRental]);

            MovieDataGrid.Items.Refresh();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (RentalsViewModel)DataContext;
            var addRentalDialog = new AddRentalModal(vm.GetClients(), vm.GetMovies());
            bool? result = addRentalDialog.ShowDialog();

            if (result == true)
            {
                if (addRentalDialog.Rental is null)
                    return;
                var rental = vm.AddRental(addRentalDialog.Rental);
                vm.Rentals.Add(rental);
                vm.EditMovie(addRentalDialog.Movie);
            }
        }
    }
}
