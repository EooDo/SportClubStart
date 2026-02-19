using System;

namespace SportClubStart.Models
{
    // Модель таблицы Attendances
    public class Attendance
    {
        public int Id { get; set; }
        public int SportsmanId { get; set; }
        public int CoachId { get; set; }
        public DateTime TrainingDate { get; set; }
        public bool Attended { get; set; }
    }
}
