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
        public ObservableCollection<WeatherData> GetWeatherItems()
        {
            return _weatherItems;
        }

        public void SetWeatherItems(ObservableCollection<WeatherData> value)
        { _weatherItems = value; OnPropertyChanged(); }

        public WeatherViewModel(ObservableCollection<WeatherData> weatherItems)
        {
            SetWeatherItems(weatherItems);
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
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
            foreach (var item in data)
            {
                GetWeatherItems().Add(item);
            }
            IsLoading = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private class WeatherData
        {
        }
    }