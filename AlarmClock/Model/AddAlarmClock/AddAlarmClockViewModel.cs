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

        #region Date
        private DateTime date = DateTime.Now;
        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
        #endregion

        #region Time
        private DateTime time = DateTime.Now;
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

        #region IsChecked
        private bool isChecked;
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
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

        #region Music
        private string music;
        public string Music
        {
            get => music;
            set
            {
                music = value;
                OnPropertyChanged(nameof(Music));
            }
        }
        #endregion

        #endregion

        #region Constructors
        public AddAlarmClockViewModel(AlarmClockModel alarmClock = null)
        {

            if (alarmClock != null)
            {
                Id = alarmClock.Id;
                Guid = alarmClock.Guid;
                Name = alarmClock.Name;
                Date = alarmClock.Date;
                Time = alarmClock.Time;
                IsChecked = alarmClock.IsChecked;
                IsDeleted = alarmClock.IsDeleted;
                Music = alarmClock.Music; 
            }

        }  
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
