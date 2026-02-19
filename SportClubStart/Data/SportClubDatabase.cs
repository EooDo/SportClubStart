using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using SportClubStart.Models;

namespace SportClubStart.Data
{

    public class SportClubDatabase
    {
        private readonly string _dbPath;
        private readonly string _connectionString;

        public SportClubDatabase()
        {
            _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SportClub.db");
            _connectionString = "Data Source=SportClub.db;Version=3;";
        }


        public bool TestConnection(out string errorText)
        {
            errorText = null;

            try
            {
                if (!File.Exists(_dbPath))
                {
                    errorText = "Файл БД не найден: " + _dbPath;
                    return false;
                }

                using (var conn = CreateConnection())
                {
                    conn.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                errorText = ex.Message;
                return false;
            }
        }

        private SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection(_connectionString);
        }


        public List<Sportsman> GetSportsmen()
        {
            var result = new List<Sportsman>();

            const string sql = "SELECT Id, FullName, Birthday, ParentPhone FROM Sportsmen ORDER BY FullName;";

            using (var conn = CreateConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new Sportsman
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FullName = Convert.ToString(reader["FullName"]),
                            Birthday = ParseDate(Convert.ToString(reader["Birthday"])),
                            ParentPhone = Convert.ToString(reader["ParentPhone"])
                        };

                        result.Add(item);
                    }
                }
            }

            return result;
        }


        public void AddSportsman(Sportsman sportsman)
        {
            const string sql = @"INSERT INTO Sportsmen (FullName, Birthday, ParentPhone)
                                 VALUES (@fullName, @birthday, @parentPhone);";

            using (var conn = CreateConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@fullName", sportsman.FullName);
                    cmd.Parameters.AddWithValue("@birthday", sportsman.Birthday.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@parentPhone", sportsman.ParentPhone);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<Coach> GetCoaches()
        {
            var result = new List<Coach>();

            const string sql = "SELECT Id, FullName, SportType FROM Coaches ORDER BY FullName;";

            using (var conn = CreateConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new Coach
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FullName = Convert.ToString(reader["FullName"]),
                            SportType = Convert.ToString(reader["SportType"])
                        };

                        result.Add(item);
                    }
                }
            }

            return result;
        }


        public void AddCoach(Coach coach)
        {
            const string sql = @"INSERT INTO Coaches (FullName, SportType)
                                 VALUES (@fullName, @sportType);";

            using (var conn = CreateConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@fullName", coach.FullName);
                    cmd.Parameters.AddWithValue("@sportType", coach.SportType);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public bool AttendanceExists(int sportsmanId, DateTime trainingDate)
        {
            const string sql = @"SELECT COUNT(*) FROM Attendances
                                 WHERE SportsmanId = @sid AND TrainingDate = @date;";

            using (var conn = CreateConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@sid", sportsmanId);
                    cmd.Parameters.AddWithValue("@date", trainingDate.ToString("yyyy-MM-dd"));

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }


        public void AddAttendance(Attendance attendance)
        {
            const string sql = @"INSERT INTO Attendances (SportsmanId, CoachId, TrainingDate, Attended)
                                 VALUES (@sid, @cid, @date, @att);";

            using (var conn = CreateConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@sid", attendance.SportsmanId);
                    cmd.Parameters.AddWithValue("@cid", attendance.CoachId);
                    cmd.Parameters.AddWithValue("@date", attendance.TrainingDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@att", attendance.Attended ? 1 : 0);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public DataTable GetTruantsReport(DateTime start, DateTime end)
        {
            const string sql = @"
                SELECT 
                    s.FullName as 'Ребенок',
                    s.Birthday as 'Дата рождения',
                    s.ParentPhone as 'Телефон родителя',
                    a.TrainingDate as 'Дата пропуска',
                    c.FullName as 'Тренер'
                FROM Attendances a
                INNER JOIN Sportsmen s ON a.SportsmanId = s.Id
                INNER JOIN Coaches c ON a.CoachId = c.Id
                WHERE a.Attended = 0 
                  AND a.TrainingDate BETWEEN @start AND @end
                ORDER BY a.TrainingDate DESC;";

            return ExecuteTable(sql, start, end, null);
        }



        public DataTable GetCoachStatsReport(DateTime start, DateTime end)
        {
            const string sql = @"
                SELECT 
                    c.FullName as 'Тренер',
                    c.SportType as 'Вид спорта',
                    COUNT(DISTINCT a.TrainingDate) as 'Всего тренировок',
                    SUM(CASE WHEN a.Attended = 1 THEN 1 ELSE 0 END) as 'Посещено детьми',
                    CASE 
                        WHEN COUNT(a.Id) > 0 
                        THEN ROUND(CAST(SUM(CASE WHEN a.Attended = 1 THEN 1 ELSE 0 END) AS FLOAT) 
                        / COUNT(a.Id) * 100, 2)
                        ELSE 0 
                    END as 'Процент посещаемости'
                FROM Coaches c
                LEFT JOIN Attendances a ON c.Id = a.CoachId
                    AND a.TrainingDate BETWEEN @start AND @end
                GROUP BY c.Id, c.FullName, c.SportType;";

            return ExecuteTable(sql, start, end, null);
        }


        
        public DataTable GetChildrenActivityReport(DateTime start, DateTime end)
        {
            const string sql = @"
                SELECT 
                    s.FullName as 'ФИО ребенка',
                    s.Birthday as 'Дата рождения',
                    COUNT(a.Id) as 'Всего тренировок',
                    SUM(CASE WHEN a.Attended = 0 THEN 1 ELSE 0 END) as 'Пропусков',
                    CASE 
                        WHEN COUNT(a.Id) > 0 
                        THEN ROUND(CAST(SUM(CASE WHEN a.Attended = 1 THEN 1 ELSE 0 END) AS FLOAT) 
                        / COUNT(a.Id) * 100, 2)
                        ELSE 0 
                    END as 'Процент посещаемости'
                FROM Sportsmen s
                LEFT JOIN Attendances a ON s.Id = a.SportsmanId
                    AND a.TrainingDate BETWEEN @start AND @end
                GROUP BY s.Id, s.FullName, s.Birthday
                ORDER BY s.FullName;";

            DataTable table = ExecuteTable(sql, start, end, null);

            if (!table.Columns.Contains("Возраст"))
                table.Columns.Add("Возраст", typeof(int));

            foreach (DataRow row in table.Rows)
            {
                DateTime birthday = ParseDate(Convert.ToString(row["Дата рождения"]));
                row["Возраст"] = CalculateAge(birthday, DateTime.Today);
            }

            if (table.Columns.Contains("Дата рождения"))
                table.Columns.Remove("Дата рождения");

            if (table.Columns.Contains("Возраст") && table.Columns.Contains("Всего тренировок"))
            {
                table.Columns["Возраст"].SetOrdinal(1);
            }

            return table;
        }

        private static int CalculateAge(DateTime birthday, DateTime today)
        {
            int age = today.Year - birthday.Year;
            if (birthday.Date > today.AddYears(-age)) age--;
            return age;
        }


        private DataTable ExecuteTable(string sql, DateTime start, DateTime end, int? childId)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@start", start.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@end", end.ToString("yyyy-MM-dd"));

                    if (childId.HasValue)
                        cmd.Parameters.AddWithValue("@childId", childId.Value);

                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        var table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
            }
        }

        private static DateTime ParseDate(string value)
        {
            if (DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime dt))
            {
                return dt;
            }

            // мб пригодится парс я хз :////////
            DateTime.TryParse(value, out dt);
            return dt;
        }
    }
}
