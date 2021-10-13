using AlarmClock.Commands;
using AlarmClock.Model.AlarmModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<SoundModel> listSounds = new ObservableCollection<SoundModel>();
        public ObservableCollection<SoundModel> ListSounds
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
            if (sound == null)
            {

                ListSounds[0].RuName = music;
                sound = ListSounds[0];
            }
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


        private RelayCommand addAlarmCloc;
        public ICommand AddAlarmClockCommand
        {
            get
            {
                return addAlarmCloc ??
                     (addAlarmCloc = new RelayCommand(AddAlarmClockExecute));
            }
        }

        #endregion

        #region DelCommand
        private void DelAlarmClockExecute(object obj)
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

        #region SelectMusicCommand

        private void SelectMusicExecute(object obj)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();

                if (op.ShowDialog() == false)
                    return;
                // получаем выбранный файл
                string filename = op.FileName;

                string[] rows = filename.Split('\\');
                string name = string.Empty;

                foreach (var item in rows)
                {
                    name = item;
                }

                ListSounds[0].RuName = name;
                ListSounds[0].Name = filename;
                SelectedSound = ListSounds[0];

                OnPropertyChanged(nameof(ListSounds));

               

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, nameof(Exception));
            }

        }

        private RelayCommand selectMusic;
        public ICommand SelectMusicCommand
        {
            get
            {
                return selectMusic ??
                     (selectMusic = new RelayCommand(SelectMusicExecute));
            }
        }

        #endregion

        #endregion
    }
}
