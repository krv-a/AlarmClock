using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Linq;

namespace AlarmClock.Helper
{
    /// <summary>
    /// Сервис для сохранения и восстановления будильников в xml файле
    /// </summary>
    public  class XMLService
    {
        #region Members
        private ObservableCollection<AlarmClockModel> _list; 
        #endregion

        #region Constructors
        public XMLService(ObservableCollection<AlarmClockModel> list)
        {
            _list = list;
        } 
       

        public XMLService()
        {

        }
        #endregion

        #region Methds
        /// <summary>
        /// Сохраняет список будильников в файл
        /// </summary>
        public void SaveFile()
        {
            XDocument xdoc = new XDocument();

            // создаем корневой элемент
            XElement alarmClocks = new XElement("alarmClocks");

            foreach (var item in _list)
            {
                // создаем первый элемент
                XElement alarmClock = new XElement("alarmClock");

                // создаем атрибуты
                XAttribute isDeleted = new XAttribute("IsDeleted", item.IsDeleted);
                XAttribute guid = new XAttribute("Guid", item.Guid);
                XAttribute name = new XAttribute("Name", item.Name);
                XAttribute date = new XAttribute("Date", item.Date);
                XAttribute time = new XAttribute("Time", item.Time);
                XAttribute id = new XAttribute("Id", item.Id);
                XAttribute isChecked = new XAttribute("IsChecked", item.IsChecked);
                XAttribute music = new XAttribute("Music", item.Music);
                XAttribute musicName = new XAttribute("MusicName", item?.MusicName ?? "");
                XAttribute musicPath = new XAttribute("MusicPath", item?.MusicPath ?? "");
                // добавляем атрибут и элементы в первый элемент
                alarmClock.Add(isDeleted);
                alarmClock.Add(guid);
                alarmClock.Add(name);
                alarmClock.Add(date);
                alarmClock.Add(time);
                alarmClock.Add(id);
                alarmClock.Add(isChecked);
                alarmClock.Add(music);
                alarmClock.Add(musicName);
                alarmClock.Add(musicPath);

                // добавляем в корневой элемент
                alarmClocks.Add(alarmClock);
            }

            // добавляем корневой элемент в документ
            xdoc.Add(alarmClocks);
            //сохраняем документ
            xdoc.Save("db.xml");
        }

        /// <summary>
        /// Получает список юудильков из файла
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<AlarmClockModel> OpenFile()
        {
            try
            {
                ObservableCollection<AlarmClockModel> listAlarmClock = new ObservableCollection<AlarmClockModel>();

                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("db.xml");

                // получим корневой элемент
                XmlElement xRoot = xDoc.DocumentElement;
                // обход всех узлов в корневом элементе
                foreach (XmlNode xnode in xRoot)
                {
                    var isDeleted = xnode.Attributes.GetNamedItem("IsDeleted");
                    var guid = xnode.Attributes.GetNamedItem("Guid");
                    var date = xnode.Attributes.GetNamedItem("Date");
                    var time = xnode.Attributes.GetNamedItem("Time");
                    var name = xnode.Attributes.GetNamedItem("Name");
                    var id = xnode.Attributes.GetNamedItem("Id");
                    var isChecked = xnode.Attributes.GetNamedItem("IsChecked");
                    var music = xnode.Attributes.GetNamedItem("Music");
                    var musicName = xnode.Attributes.GetNamedItem("MusicName");
                    var musicPath = xnode.Attributes.GetNamedItem("MusicPath");
                    AlarmClockModel ac = new AlarmClockModel()
                    {
                        IsDeleted = Convert.ToBoolean(isDeleted.Value),
                        Guid = Guid.Parse(guid.Value),
                        Date = Convert.ToDateTime(date.Value),
                        Time = Convert.ToDateTime(time.Value),
                        Name = name.Value,
                        Id = Convert.ToInt32(id.Value),
                        Music = music.Value,
                        MusicName = musicName.Value,
                        MusicPath = musicPath.Value,
                        IsChecked = Convert.ToBoolean(isChecked.Value),


                    };
                    listAlarmClock.Add(ac);
                }

                return listAlarmClock;
            }
            catch (Exception e)
            {
                return new ObservableCollection<AlarmClockModel>();
            }
        } 
        #endregion
    }
}
