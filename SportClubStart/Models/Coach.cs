using System;

namespace SportClubStart.Models
{
    // Модель таблицы Coaches
    public class Coach
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string SportType { get; set; }

        public override string ToString()
        {
            return $"{Id}: {FullName} ({SportType})";
        }
    }
}
