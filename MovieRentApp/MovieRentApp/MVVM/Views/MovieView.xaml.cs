using MovieRentApp.Modals;
using MovieRentApp.Models;
using MovieRentApp.MVVM.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MovieRentApp.MVVM.Views
{
    /// <summary>
    /// Interaction logic for MovieView.xaml
    /// </summary>
    public partial class MovieView : UserControl
    {
        public MovieView()
        {
            InitializeComponent();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MovieViewModel)DataContext;
            if (MovieDataGrid.SelectedItem == null) return;

            if (MovieDataGrid.SelectedItem is not Movie selectedRow) return;

            var primaryKey = selectedRow.Id;
            vm.RemoveMovie(primaryKey);
            vm.Movies.Remove(selectedRow);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addMovieDialog = new MovieFormModal();
            bool? result = addMovieDialog.ShowDialog();

            if (result == true)
            {
                var vm = (MovieViewModel)DataContext;
                if (addMovieDialog.NewMovie is null) 
                    return;
                var newMovie = vm.AddMovie(addMovieDialog.NewMovie);
                vm.Movies.Add(newMovie);
            }
        }

        private void MovieDataGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            MovieDataGrid.SelectedItem = null;
        }
    }
}
