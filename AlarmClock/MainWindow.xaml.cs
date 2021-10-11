using AlarmClock.Helper;
using AlarmClock.Model.AddAlarmClock;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace AlarmClock
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MediaPlayer mp = new MediaPlayer();
        public MainWindow()
        {
            InitializeComponent();
            XMLService ser = new XMLService();
            this.ListAlarmClock.ItemsSource = ser.OpenFile();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            XMLService ser = new XMLService((ObservableCollection<AlarmClockModel>)this.ListAlarmClock.ItemsSource);
            ser.SaveFile();
        }
    }
}
