using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WheatherSystem.Models;

namespace WheatherSystem.ViewModels
{
    class WeatherViewModel
    {
        private ObservableCollection<WeatherData> _weatherItems;
        public ObservableCollection<WeatherData> WeatherItems
        {
            get => _weatherItems;
            set { _weatherItems = value; OnPropertyChanged(); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public WeatherViewModel()
        {
            WeatherItems = new ObservableCollection<WeatherData>();
        }

        public async Task LoadWeatherAsync(double latitude, double longitude)
        {
            IsLoading = true;
            var weatherService = new WeatherService();
            var data = await weatherService.GetWeatherAsync(latitude, longitude);
            WeatherItems.Clear();
            foreach (var item in data)
            {
                WeatherItems.Add(item);
            }
            IsLoading = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
