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
        #region Properties

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
                SoundPlayer sp = new SoundPlayer();
                sp.Stream = Properties.Resources.Alarm01;// pathMelody;
                //sp.Load();
                sp.PlayLooping();
            }
            );

        }

        #endregion

        #region Methods

        #endregion

        #region Commands

        #endregion
    }
}
