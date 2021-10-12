using AlarmClock.Commands;
using AlarmClock.Helper;
using AlarmClock.Model;
using AlarmClock.Model.AddAlarmClock;
using AlarmClock.Model.AlarmModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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

        #region IsDeleted
        private bool isDeleted;
        public bool IsDeleted
        {
            get => isDeleted;
            set
            {
                isDeleted = value;
                OnPropertyChanged(nameof(IsDeleted));
            }
        }
        #endregion

        #region Guid
        private Guid guid;
        public Guid Guid
        {
            get => guid;
            set
            {
                guid = value;
                OnPropertyChanged(nameof(Guid));
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
                
                    listAlarmClocks = value;
                    OnPropertyChanged(nameof(ListAlarmClocks));
            }
        }


        #endregion

        #region SelectedAlarmClock

        private AlarmClockModel selectedAlarmClock = new AlarmClockModel();

        public AlarmClockModel SelectedAlarmClock
        {
            get => selectedAlarmClock;
            set
            {
                if (selectedAlarmClock != value)
                {
                    selectedAlarmClock = value;
                    if(value != null)
                        OpenAlarmClock(value);
                    OnPropertyChanged(nameof(SelectedAlarmClock));
                }
            }
        }

       

        private void DeleteAlarmClock(AlarmClockModel value)
        {
            var alarmClock = ListAlarmClocks.Where(w => w.Guid == value.Guid && w.IsDeleted == true).FirstOrDefault();
            if (alarmClock != null)
                ListAlarmClocks.Remove(alarmClock);
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

            XMLService ser = new XMLService();
            ListAlarmClocks = ser.OpenFile();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Отслеживает изменение времени
        /// </summary>
        /// <param name="o"></param>
        /// <param name="sender"></param>
        public void Each_Tick(object o, EventArgs sender)
        {
            DateTime curDate = DateTime.Now;

            Hour = curDate.Hour.ToString("00");
            Minute = curDate.Minute.ToString("00");
            Secunda = curDate.Second.ToString("00");
            Date = curDate.Date.ToString("d");

            if (ListAlarmClocks != null)
            {
                foreach (var item in ListAlarmClocks)
                {
                    var date = item.Date;
                    var time = item.Time;

                    if (date.Date.Date == curDate.Date
                        && time.Hour == curDate.Hour
                        && time.Minute == curDate.Minute
                        && time.Second == curDate.Second
                        && item.IsChecked)
                    {
                        OpenAlarmForm(item);

                    }
                } 
            }
        }

        /// <summary>
        /// Форма открывается при срабатывании будильника
        /// </summary>
        /// <param name="item"></param>
        private  void OpenAlarmForm(AlarmClockModel item)
        {

            App.Current.Dispatcher.Invoke(() =>
           {
               var window = new AlarmModelWindow
               {
                   Title = item.Name,
                   DataContext = new AlarmViewModel(item.Music, item.Guid),
                   WindowStartupLocation = WindowStartupLocation.CenterScreen,
               };

               window.Show();

           });

        }
        
        /// <summary>
        /// Открывает форму для добавления/редактирования будильника
        /// </summary>
        /// <param name="value"></param>
        private void OpenAlarmClock(AlarmClockModel value)
        {
            // запуск окна 
            var win = App.Current.Windows.Cast<Window>()
                  .FirstOrDefault(w => w is AddAlarmClockWindow);
            if (win != null)
                win.Close();

            var window = new AddAlarmClockWindow
            {
                DataContext = new AddAlarmClockViewModel(SelectedAlarmClock),
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
            };

            if (window.ShowDialog() == true)
            {
                if (!((AddAlarmClockViewModel)(window.DataContext)).IsDeleted)
                {
                    SelectedAlarmClock.Name = window.Name.Text;
                    SelectedAlarmClock.Date = window?.Date?.SelectedDate ?? DateTime.Now;
                    SelectedAlarmClock.Time = window?.Time?.SelectedTime ?? DateTime.Now.AddSeconds(10);
                    SelectedAlarmClock.Music = window?.Sound?.Text;

                    XMLService ser = new XMLService(ListAlarmClocks);
                    ser.SaveFile();
                    ListAlarmClocks = ser.OpenFile();
                }
                else
                {
                    ListAlarmClocks.Remove(SelectedAlarmClock);
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
                        Guid = Guid.NewGuid(),
                        IsDeleted = false,
                        Id = Counter,
                        Name = string.IsNullOrEmpty(window.Name.Text) == true
                               ? "Будильник" + Counter.ToString()
                               : window.Name.Text,
                        Date = window?.Date?.SelectedDate ?? DateTime.Now,
                        Time = window?.Time?.SelectedTime ?? DateTime.Now.AddSeconds(10),
                        IsChecked = true,
                        Music = window.Sound.Text
                    };
                    ListAlarmClocks.Add(alarmClock);
                    OnPropertyChanged(nameof(ListAlarmClocks));

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

        #region SelectedAlarmClockCommand

        private void SelectedAlarmClockExecute(object obj)
        {
            try
            {
                OpenAlarmClock(SelectedAlarmClock);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, nameof(Exception));
            }

        }

        private RelayCommand selectedAlarmClockCommand;
        public ICommand SelectedAlarmClockCommand
        {
            get
            {
                return selectedAlarmClockCommand ??
                     (selectedAlarmClockCommand = new RelayCommand(SelectedAlarmClockExecute));
            }
        }
        #endregion

        #endregion
    }
}
