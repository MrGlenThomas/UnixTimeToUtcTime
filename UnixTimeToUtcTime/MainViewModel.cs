
namespace UnixTimeToUtcTime
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using Humanizer;

    class MainViewModel : ViewModelBase
    {
        private string _unixTime;
        private DateTime _utcTime;
        private DateTime _localTime;
        private string _timePassed;

        public string UnixTime
        {
            get
            {
                return _unixTime;
            }
            set
            {
                _unixTime = value;
                this.OnPropertyChanged();
            }
        }

        public DateTime UtcTime
        {
            get
            {
                return _utcTime;
            }
            set
            {
                _utcTime = value;
                this.OnPropertyChanged();
            }
        }

        public DateTime LocalTime
        {
            get
            {
                return _localTime;
            }
            set
            {
                _localTime = value;
                this.OnPropertyChanged();
            }
        }

        public string TimePassed
        {
            get
            {
                return _timePassed;
            }
            set
            {
                _timePassed = value;
                this.OnPropertyChanged();
            }
        }

        public ICommand ConvertCommand { get; set; }

        public MainViewModel()
        {
            ConvertCommand = new RelayCommand(Convert, o => o != null);
        }

        private void Convert(object obj)
        {
            double numValue;

            if (!double.TryParse(obj.ToString(), out numValue))
            {
                MessageBox.Show("Invalid number!");
            }

            UtcTime = UnixTimeStampToDateTime(numValue);
            LocalTime = UtcTime.ToLocalTime();
            TimePassed = (DateTime.UtcNow - UtcTime).Humanize(4);
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var baseDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            baseDateTime = baseDateTime.AddSeconds(unixTimeStamp);
            return baseDateTime;
        }
    }
}
