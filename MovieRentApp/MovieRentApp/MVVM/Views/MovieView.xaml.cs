using MovieRentApp.Modals;
using MovieRentApp.Models;
using MovieRentApp.MVVM.ViewModels;
using System.Runtime.CompilerServices;
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
            if (MovieDataGrid.SelectedItem == null || MovieDataGrid.SelectedItem is not Movie selectedRow) 
                return;
            var vm = (MovieViewModel)DataContext;
            var primaryKey = selectedRow.Id;
            vm.RemoveMovie(primaryKey);
            vm.Movies.Remove(selectedRow);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (MovieDataGrid.SelectedItem == null || MovieDataGrid.SelectedItem is not Movie selectedMovie) 
                return;
            var addOrEditMovieDialog = new AddOrEditMovieModal(selectedMovie);
            bool? result = addOrEditMovieDialog.ShowDialog();
            
            if (result == false)
                return;

            var vm = (MovieViewModel)DataContext;
            var movieToEdit = addOrEditMovieDialog.Movie;
            if (movieToEdit is null)
                return;
            
            vm.EditMovie(movieToEdit);
            var indexNonEditedMovie = vm.Movies.IndexOf(selectedMovie);
            vm.Movies[indexNonEditedMovie] = movieToEdit;
            MovieDataGrid.Items.Refresh();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addMovieDialog = new AddOrEditMovieModal();
            bool? result = addMovieDialog.ShowDialog();

            if (result == true)
            {
                var vm = (MovieViewModel)DataContext;
                if (addMovieDialog.Movie is null) 
                    return;
                var newMovie = vm.AddMovie(addMovieDialog.Movie);
                vm.Movies.Add(newMovie);
            }
        }

        private void MovieDataGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            MovieDataGrid.SelectedItem = null;
        }
    }
}
