using AlarmClock.Commands;
using AlarmClock.Model;
using AlarmClock.Model.AddAlarmClock;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AlarmClock
{
    class MainViewModel : BaseViewModel
    {
        #region Members
        MediaPlayer player = new MediaPlayer();
        #endregion

        #region Properties

        #region Hour
        private string hour;
        public string Hour
        {
            get => hour;
            set
            {
                hour = value;
                OnPropertyChanged(nameof(Hour));
            }
        }
        #endregion

        #region Minute
        private string minute;
        public string Minute
        {
            get => minute;
            set
            {
                minute = value;
                OnPropertyChanged(nameof(Minute));
            }
        }
        #endregion

        #region Secunda
        private string secunda;
        public string Secunda
        {
            get => secunda;
            set
            {
                secunda = value;
                OnPropertyChanged(nameof(Secunda));
            }
        }
        #endregion

        #region Date
        private string date;
        public string Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
        #endregion

        #region Counter
        private int counter;
        public int Counter
        {
            get => counter;
            set
            {
                counter = value;
                OnPropertyChanged(nameof(Counter));
            }
        }
        #endregion

        #region  ListAlarmClocks
        private ObservableCollection<AlarmClockModel> listAlarmClocks = new ObservableCollection<AlarmClockModel>();

        public ObservableCollection<AlarmClockModel> ListAlarmClocks
        {
            get => listAlarmClocks;
            set
            {
                if (listAlarmClocks != value)
                {
                    listAlarmClocks = value;
                    OnPropertyChanged(nameof(ListAlarmClocks));
                }
            }
        }


        #endregion

        #endregion

        #region Constructors
        public MainViewModel()
        {
            System.Windows.Threading.DispatcherTimer myDispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            myDispatcherTimer.Tick += new EventHandler(Each_Tick);
            myDispatcherTimer.Start();
        }
        #endregion

        #region Methods
        public void Each_Tick(object o, EventArgs sender)
        {
            DateTime curDate = DateTime.Now;

            Hour = curDate.Hour.ToString("00");
            Minute = curDate.Minute.ToString("00");
            Secunda = curDate.Second.ToString("00");
            Date = curDate.Date.ToString("d");

            foreach (var item in ListAlarmClocks)
            {
                var date = item.Date;
                var time = item.Time;

                if (date.Date.Date == curDate.Date 
                    && time.Hour == curDate.Hour
                    && time.Minute == curDate.Minute
                    && time.Second == curDate.Second)
                {
                    MediaPlayer player = new MediaPlayer();
                    player.Open(new Uri(@"C:\Windows\Media\Alarm01.wav", UriKind.Absolute));
                    player.Play();
                }
            }
        }
        #endregion

        #region Commands

        #region AddAlarmClock
        private void AddAlarmClockExecute(object obj)
        {
            try
            {
           
                // запуск окна 
                var win = App.Current.Windows.Cast<Window>()
                      .FirstOrDefault(w => w is AddAlarmClockWindow);
                if (win != null)
                    win.Close();

                var window = new AddAlarmClockWindow
                {
                    DataContext = new AddAlarmClockViewModel(),
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                };

                if (window.ShowDialog() == true)
                {
                    Counter++;
                    AlarmClockModel alarmClock = new AlarmClockModel
                    {
                        Id = Counter,
                        Name = string.IsNullOrEmpty(window.Name.Text) == true
                               ? "Будильник" + Counter.ToString()
                               : window.Name.Text,
                        Date = window?.Date?.SelectedDate ?? DateTime.Now,
                        Time = window?.Time?.SelectedTime ?? DateTime.Now.AddMinutes(1)
                    };
                    ListAlarmClocks.Add(alarmClock);

                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, nameof(Exception));
            }

        }


        private RelayCommand addAlarmClock;
        public ICommand AddAlarmClockCommand
        {
            get
            {
                return addAlarmClock ??
                     (addAlarmClock = new RelayCommand(AddAlarmClockExecute));
            }
        }

        #endregion

        #endregion
    }
}
