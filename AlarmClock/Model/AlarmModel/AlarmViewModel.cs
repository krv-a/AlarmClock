using AlarmClock.Commands;
using AlarmClock.Helper;
using System;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AlarmClock.Model.AlarmModel
{
    class AlarmViewModel : BaseViewModel
    {
        #region Members
        SoundPlayer sp = new SoundPlayer();
        Guid guid;
        #endregion

        #region Properties

        #region IsCheckedStop
        private bool isCheckedStop = true;
        public bool IsCheckedStop
        {
            get => isCheckedStop;
            set
            {
                isCheckedStop = value;
                if (!value)
                    sp.Stop();
                OnPropertyChanged(nameof(IsCheckedStop));
            }
        }
        #endregion

        #region IsProlong
        private bool isProlong = false;
        public bool IsProlong
        {
            get => isProlong;
            set
            {
                isProlong = value;
                OnPropertyChanged(nameof(IsProlong));
            }
        }
        #endregion

        #region ProlongInt
        private int prolongInt = 5;
        public int ProlongInt
        {
            get => prolongInt;
            set
            {
                prolongInt = value;
                OnPropertyChanged(nameof(ProlongInt));
            }
        }
        #endregion

        #region Prolong
        private string prolong = "5";
        public string Prolong
        {
            get => prolong;
            set
            {
                if (int.TryParse(value, out prolongInt))
                {
                    prolong = value;
                    OnPropertyChanged(nameof(Prolong));
                }
            }
        }
        #endregion

        #endregion

        #region Constructors

        public AlarmViewModel(string pathMelody, Guid guid, string musicPath)
        {
            this.guid = guid;
            #region MyRegion
            //MediaElement player = new MediaElement();
            //player.Source = new Uri(pathMelody, UriKind.Absolute);
            //player.LoadedBehavior = MediaState.Manual;

            //player.Volume = 20f;
            //player.SpeedRatio = 1f;
            ////player.Position = TimeSpan.Zero;
            //player.Play(); 
            #endregion

            if (string.IsNullOrEmpty(musicPath))
            {
                Task.Run(() =>
                   {
                       sp.Stream = GetSound(pathMelody);
                       //sp.Load();
                       sp.PlayLooping();
                   });
            }
            else
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(musicPath, UriKind.Absolute));
                player.Play();
            }

        }

        #endregion

        #region Methods

        /// <summary>
        /// Получет мелодию из ресурсов
        /// </summary>
        /// <param name="pathMelody"></param>
        /// <returns></returns>
        private System.IO.UnmanagedMemoryStream GetSound(string pathMelody)
        {
            System.IO.UnmanagedMemoryStream alarm = null;
            switch (pathMelody)
            {
                case "Звонок1":
                    alarm = Properties.Resources.Alarm01;
                    break;
                case "Звонок2":
                    alarm = Properties.Resources.Alarm02;
                    break;
                case "Звонок3":
                    alarm = Properties.Resources.Alarm03;
                    break;
                case "Звонок4":
                    alarm = Properties.Resources.Alarm04;
                    break;
                case "Звонок5":
                    alarm = Properties.Resources.Alarm05;
                    break;
                case "Звонок6":
                    alarm = Properties.Resources.Alarm06;
                    break;
                case "Звонок7":
                    alarm = Properties.Resources.Alarm07;
                    break;
                case "Звонок8":
                    alarm = Properties.Resources.Alarm08;
                    break;
                case "Звонок9":
                    alarm = Properties.Resources.Alarm09;
                    break;
                case "Звонок10":
                    alarm = Properties.Resources.Alarm10;
                    break;
            }

            return alarm;
        }

        /// <summary>
        /// Продление будильника
        /// </summary>
        /// <param name="window"></param>
        private void AddMitutes(Window window)
        {
            if (((AlarmViewModel)(window.DataContext)).IsProlong)
            {
                var win = App.Current.Windows.Cast<Window>()
                       .FirstOrDefault(w => w is MainWindow);

                var res = ((AlarmViewModel)(window.DataContext)).ProlongInt;

                var list = ((MainViewModel)win.DataContext).ListAlarmClocks;

                var alarmClock = list.Where(w => w.Guid == this.guid).FirstOrDefault();

                alarmClock.Time = alarmClock.Time.AddMinutes(res);

                XMLService ser = new XMLService(list);
                ser.SaveFile();
                ((MainViewModel)win.DataContext).ListAlarmClocks = ser.OpenFile();
            }
        }
        #endregion

        #region Commands

        #region OkAlarmClockCommand
        private void OkAlarmClockExecute(object obj)
        {
            try
            {
                var window = App.Current.Windows.Cast<Window>()
                           .FirstOrDefault(w => w is AlarmModelWindow);

                if (sp != null)
                    sp.Stop();

                window.Close();

                AddMitutes(window);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, nameof(Exception));
            }

        }


        private RelayCommand okAlarmClock;
        public ICommand OkAlarmClockCommand
        {
            get
            {
                return okAlarmClock ??
                     (okAlarmClock = new RelayCommand(OkAlarmClockExecute));
            }
        }

        #endregion

        #region CloseAlarmClockCommand
        private void CloseAlarmClockExecute(object obj)
        {
            try
            {
                if (sp != null)
                    sp.Stop();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, nameof(Exception));
            }

        }


        private RelayCommand closeAlarmClock;
        public ICommand CloseAlarmClockCommand
        {
            get
            {
                return closeAlarmClock ??
                     (closeAlarmClock = new RelayCommand(CloseAlarmClockExecute));
            }
        }

        #endregion

        #endregion
    }
}
