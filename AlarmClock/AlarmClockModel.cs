using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmClock
{
    public class AlarmClockModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public bool IsChecked { get; set; }
        public string Music { get; set; }
        public string MusicName { get; set; }
        public string MusicPath { get; set; }
        public bool IsDeleted { get; set; }
        public Guid Guid { get; set; }

    }
}
