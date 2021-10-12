using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        #endregion

        #region Constructors

        public AlarmViewModel(string pathMelody)
        {

            //MediaElement player = new MediaElement();


            //player.Source = new Uri(pathMelody, UriKind.Absolute);
            //player.LoadedBehavior = MediaState.Manual;

            //player.Volume = 20f;
            //player.SpeedRatio = 1f;
            ////player.Position = TimeSpan.Zero;
            //player.Play();

            Task.Run(() =>
            {
                sp.Stream = Properties.Resources.Alarm01;
                //sp.Load();
                sp.PlayLooping();
            });

        }

        #endregion

        #region Methods

        #endregion

        #region Commands

        #endregion
    }
}
