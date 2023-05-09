using MovieRentApp.Core;
using MovieRentApp.Repository;
using System.Threading.Tasks;

namespace MovieRentApp.MVVM.ViewModels;

public class MainViewModel : ObservableObject
{
    public RelayCommand MovieViewCommand { get; set; }
    public RelayCommand ClientsViewCommand { get; set; }
    public RelayCommand RentalsViewCommand { get; set; }
    public MovieViewModel MovieViewModel { get; set; }
    public ClientViewModel ClientsViewModel { get; set; }
    public RentalsViewModel AllRentalsViewModel { get; set; }

    private object? _currentView;

    public object? CurrentView
	{
		get { return _currentView; }
		set 
		{
            _currentView = value;
            OnPropertyChanged();
		}
	}

    public MainViewModel()
    {
        
    }

    public MainViewModel(IRepository repository)
    {
        MovieViewModel = new MovieViewModel(repository);
        ClientsViewModel = new ClientViewModel(repository);
        AllRentalsViewModel = new RentalsViewModel(repository);

        CurrentView = MovieViewModel;

		MovieViewCommand = new RelayCommand(o =>
		{
			CurrentView = MovieViewModel;
		});

		ClientsViewCommand = new RelayCommand(o =>
		{
			CurrentView = ClientsViewModel;
		});

        RentalsViewCommand = new RelayCommand(o =>
        {
            CurrentView = AllRentalsViewModel;
        });
    }
}
