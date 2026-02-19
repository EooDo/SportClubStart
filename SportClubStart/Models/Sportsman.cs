using System;

namespace SportClubStart.Models
{
    // Модель таблицы Sportsmen
    public class Sportsman
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string ParentPhone { get; set; }

        public override string ToString()
        {
            // для логов это
            return $"{Id}: {FullName}";
        }
    }
}
