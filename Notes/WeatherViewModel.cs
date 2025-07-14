using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WheatherSystem.ViewModels;

namespace WheatherSystem.ViewModels
{
    public class WeatherViewModel // Changed to 'public' to match accessibility  
    {
        private ObservableCollection<WeatherData> _weatherItems;
        public ObservableCollection<WeatherData> GetWeatherItems() => _weatherItems;

        public void SetWeatherItems(ObservableCollection<WeatherData> value)
        {
            _weatherItems = value;
            OnPropertyChanged();
        }

        public WeatherViewModel(ObservableCollection<WeatherData> weatherItems)
        {
            SetWeatherItems(weatherItems);
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public WeatherViewModel()
        {
            SetWeatherItems(new ObservableCollection<WeatherData>());
        }

        public async Task LoadWeatherAsync(double latitude, double longitude)
        {
            IsLoading = true;
            var weatherService = new WeatherService();
            var data = await weatherService.GetWeatherAsync(latitude, longitude);
            GetWeatherItems().Clear();
            foreach (WeatherData item in data) // Fix: Explicitly cast or ensure the type matches    
            {
                GetWeatherItems().Add(item);
            }
            IsLoading = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public class WeatherData // Changed to 'public' to match accessibility  
        {
        }
    } // Fix: Added missing closing brace for the WeatherViewModel class
