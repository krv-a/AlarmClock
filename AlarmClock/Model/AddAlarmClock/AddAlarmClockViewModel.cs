using AlarmClock.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AlarmClock.Model.AddAlarmClock
{
    class AddAlarmClockViewModel : BaseViewModel
    {
        #region Properties
       
        #region Id
        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        #endregion

        #region Name
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        #endregion

        #region Data
        private DateTime data;
        public DateTime Data
        {
            get => data;
            set
            {
                data = value;
                OnPropertyChanged(nameof(Data));
            }
        }
        #endregion

        #region Time
        private DateTime time;
        public DateTime Time
        {
            get => time;
            set
            {
                time = value;
                OnPropertyChanged(nameof(Time));
            }
        }
        #endregion

        #endregion

        #region Command

        #region AddAlarmClock
        private void AddAlarmClockExecute(object obj)
        {
            try
            {
                var window = App.Current.Windows.Cast<Window>()
                           .FirstOrDefault(w => w is AddAlarmClockWindow);


                window.DialogResult = true;
                window.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, nameof(Exception));
            }

        }


        private RelayCommand selectCell;
        public ICommand AddAlarmClockCommand
        {
            get
            {
                return selectCell ??
                     (selectCell = new RelayCommand(AddAlarmClockExecute));
            }
        }

        #endregion

        #endregion
    }
}
