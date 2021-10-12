using AlarmClock.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AlarmClock.Model.AlarmModel
{
    class AlarmViewModel : BaseViewModel
    {
        #region Members
        SoundPlayer sp = new SoundPlayer();
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

        #region ProlongInt
        private int prolongInt = 0;
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

        public AlarmViewModel(string pathMelody)
        {

            #region MyRegion
            //MediaElement player = new MediaElement();
            //player.Source = new Uri(pathMelody, UriKind.Absolute);
            //player.LoadedBehavior = MediaState.Manual;

            //player.Volume = 20f;
            //player.SpeedRatio = 1f;
            ////player.Position = TimeSpan.Zero;
            //player.Play(); 
            #endregion

            Task.Run(() =>
            {
                sp.Stream = GetSound(pathMelody); 
                //sp.Load();
                sp.PlayLooping();
            });

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
        #endregion

        #region Commands
        #region AddAlarmClock
        private void OkAlarmClockExecute(object obj)
        {
            try
            {
                var window = App.Current.Windows.Cast<Window>()
                           .FirstOrDefault(w => w is AlarmModelWindow);

                if (sp != null)
                    sp.Stop();

                window.Close();
               
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, nameof(Exception));
            }

        }


        private RelayCommand selectCell;
        public ICommand OkAlarmClockCommand
        {
            get
            {
                return selectCell ??
                     (selectCell = new RelayCommand(OkAlarmClockExecute));
            }
        }

        #endregion
        #endregion
    }
}
