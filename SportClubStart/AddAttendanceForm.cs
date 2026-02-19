using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SportClubStart.Data;
using SportClubStart.Models;

namespace SportClubStart
{
    public partial class AddAttendanceForm : Form
    {
        private readonly SportClubDatabase _db;

        // Для удобства используем простые классы-обертки для ComboBox
        private class ComboItem
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public override string ToString() => Text;
        }

        private class StatusItem
        {
            public string Text { get; set; }
            public bool Value { get; set; }
            public override string ToString() => Text;
        }

        public AddAttendanceForm(SportClubDatabase db)
        {
            InitializeComponent();

            _db = db;

            LoadComboBoxes();
            dtpDate.Value = DateTime.Now;

            Text = "Добавить посещение";
        }

        private void LoadComboBoxes()
        {
            try
            {
                // Спортсмены
                var sportsmen = _db.GetSportsmen();
                var sportsmenItems = new List<ComboItem>();
                foreach (var s in sportsmen)
                {
                    sportsmenItems.Add(new ComboItem { Id = s.Id, Text = s.FullName });
                }

                cmbSportsman.DataSource = sportsmenItems;
                cmbSportsman.DisplayMember = "Text";
                cmbSportsman.ValueMember = "Id";
                cmbSportsman.SelectedIndex = -1;

                // Тренеры
                var coaches = _db.GetCoaches();
                var coachItems = new List<ComboItem>();
                foreach (var c in coaches)
                {
                    coachItems.Add(new ComboItem { Id = c.Id, Text = c.FullName });
                }

                cmbCoach.DataSource = coachItems;
                cmbCoach.DisplayMember = "Text";
                cmbCoach.ValueMember = "Id";
                cmbCoach.SelectedIndex = -1;

                // Статус
                var statuses = new List<StatusItem>
                {
                    new StatusItem { Text = "Был", Value = true },
                    new StatusItem { Text = "Пропустил", Value = false }
                };

                cmbAttended.DataSource = statuses;
                cmbAttended.DisplayMember = "Text";
                cmbAttended.ValueMember = "Value";
                cmbAttended.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbSportsman.SelectedValue == null)
            {
                MessageBox.Show("Выберите спортсмена!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCoach.SelectedValue == null)
            {
                MessageBox.Show("Выберите тренера!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int sportsmanId = Convert.ToInt32(cmbSportsman.SelectedValue);
                int coachId = Convert.ToInt32(cmbCoach.SelectedValue);
                bool attended = Convert.ToBoolean(cmbAttended.SelectedValue);
                DateTime date = dtpDate.Value.Date;

                // Проверка на дубликат
                if (_db.AttendanceExists(sportsmanId, date))
                {
                    MessageBox.Show("На эту дату уже есть запись для этого спортсмена!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var attendance = new Attendance
                {
                    SportsmanId = sportsmanId,
                    CoachId = coachId,
                    TrainingDate = date,
                    Attended = attended
                };

                _db.AddAttendance(attendance);

                MessageBox.Show("Посещение успешно добавлено!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
