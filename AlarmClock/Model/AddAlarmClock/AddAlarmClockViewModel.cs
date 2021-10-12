using AlarmClock.Commands;
using AlarmClock.Model.AlarmModel;
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

        #region ListSounds 
        private List<SoundModel> listSounds = new List<SoundModel>();
        public List<SoundModel> ListSounds
        {
            get => listSounds;
            set
            {
                listSounds = value;
                OnPropertyChanged(nameof(ListSounds));
            }
        }
        #endregion

        #region SelectedSound
        private SoundModel selectedSound = new SoundModel();
        public SoundModel SelectedSound
        {
            get => selectedSound;
            set
            {
                selectedSound = value;
                OnPropertyChanged(nameof(SelectedSound));
            }
        }
        #endregion

        #endregion

        #region Constructors
        public AddAlarmClockViewModel(AlarmClockModel alarmClock = null)
        {
            CreateListSounds();

            SelectedSound = ListSounds[0];

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
                SelectedSound = GetSound(alarmClock.Music);
            }

        }

        

        private SoundModel GetSound(string music)
        {
            var sound = ListSounds.Where(w => w.RuName == music).FirstOrDefault();
            return sound;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Создает список с мелодиями
        /// </summary>
        private void CreateListSounds()
        {
            ListSounds.Add(new SoundModel() { id = 1, Name = "Alarm01", RuName = "Звонок1" });
            ListSounds.Add(new SoundModel() { id = 1, Name = "Alarm02", RuName = "Звонок2" });
            ListSounds.Add(new SoundModel() { id = 1, Name = "Alarm03", RuName = "Звонок3" });
            ListSounds.Add(new SoundModel() { id = 1, Name = "Alarm04", RuName = "Звонок4" });
            ListSounds.Add(new SoundModel() { id = 1, Name = "Alarm05", RuName = "Звонок5" });
            ListSounds.Add(new SoundModel() { id = 1, Name = "Alarm06", RuName = "Звонок6" });
            ListSounds.Add(new SoundModel() { id = 1, Name = "Alarm07", RuName = "Звонок7" });
            ListSounds.Add(new SoundModel() { id = 1, Name = "Alarm08", RuName = "Звонок8" });
            ListSounds.Add(new SoundModel() { id = 1, Name = "Alarm09", RuName = "Звонок9" });
            ListSounds.Add(new SoundModel() { id = 1, Name = "Alarm10", RuName = "Звонок10" });
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

        #region DelCommand
        private void DelAlarmClockExecute(object obj)
        {
            try
            {

                try
                {
                    IsDeleted = true;
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
            catch (Exception e)
            {
                MessageBox.Show(e.Message, nameof(Exception));
            }

        }

        private RelayCommand delAlarmClock;
        public ICommand DelAlarmClockCommand
        {
            get
            {
                return delAlarmClock ??
                     (delAlarmClock = new RelayCommand(DelAlarmClockExecute));
            }
        }

        #endregion

        #endregion
    }
}
