using MovieRentApp.Core;
using MovieRentApp.MVVM.Models;
using MovieRentApp.Repository;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRentApp.MVVM.ViewModels
{
    public class RentalsViewModel : ObservableObject
    {
        private readonly IRepository _repository;

        private ObservableCollection<Rental>? _rentals;

        public ObservableCollection<Rental> Rentals
        {
            get { return _rentals; }
            set
            {
                _rentals = value;
                OnPropertyChanged();
            }
        }

        private bool _isEditButtonEnabled;
        public bool IsEditButtonEnabled
        {
            get { return _isEditButtonEnabled; }
            set
            {
                _isEditButtonEnabled = value;
                OnPropertyChanged(nameof(IsEditButtonEnabled));
            }
        }

        private Rental _selectedItem;
        public Rental SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                IsEditButtonEnabled = _selectedItem != null;
            }
        }

        public RentalsViewModel()
        {

        }

        public RentalsViewModel(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            LoadRentals();
        }

        public void RemoveRental(Rental rental) => Task.Run(() => _repository.DeleteEntityAsync(rental));

        public Rental AddRental(Rental rental) => Task.Run(() => _repository.AddEntityAsync(rental)).Result;
        public void EditRental(Rental rental) => Task.Run(() => _repository.EditEntityAsync(rental));
        public void EditMovie(Movie movie) => Task.Run(() => _repository.EditEntityAsync(movie));

        public ObservableCollection<Client> GetClients()
        {
            var clients = _repository.GetEntities<Client>();

            return new ObservableCollection<Client>(clients);
        }

        public ObservableCollection<Movie> GetMovies()
        {
            var movies = _repository.GetEntities<Movie>();
            movies = movies.Where(x => x.IsAvaiable == true);
            return new ObservableCollection<Movie>(movies);
        }
        public async Task LoadRentalsAsync()
        {
            var rentals = await _repository.GetEntitiesAsync<Rental>();
            Rentals = new ObservableCollection<Rental>(rentals);
        }

        private void LoadRentals()
        {
            var rentals = _repository.GetEntities<Rental>();

            Rentals = new ObservableCollection<Rental>(rentals);
        }
    }
}
