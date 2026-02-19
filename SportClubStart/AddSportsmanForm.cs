using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SportClubStart.Data;
using SportClubStart.Models;

namespace SportClubStart
{
    public class AddSportsmanForm : Form
    {
        private readonly SportClubDatabase _db;

        private TextBox txtFullName;
        private DateTimePicker dtpBirthday;
        private TextBox txtParentPhone;
        private Button btnSave;
        private Button btnCancel;

        public AddSportsmanForm(SportClubDatabase db)
        {
            _db = db;

            Text = "Добавить спортсмена";
            Width = 360;
            Height = 240;
            StartPosition = FormStartPosition.CenterParent;

            BuildUi();
        }

        private void BuildUi()
        {
            var lblFullName = new Label { Left = 12, Top = 20, Width = 120, Text = "ФИО:" };
            txtFullName = new TextBox { Left = 140, Top = 16, Width = 180 };

            var lblBirthday = new Label { Left = 12, Top = 55, Width = 120, Text = "Дата рождения:" };
            dtpBirthday = new DateTimePicker { Left = 140, Top = 52, Width = 180 };

            var lblPhone = new Label { Left = 12, Top = 90, Width = 120, Text = "Телефон родителя:" };
            txtParentPhone = new TextBox { Left = 140, Top = 86, Width = 180 };

            btnSave = new Button { Left = 140, Top = 135, Width = 85, Text = "Сохранить" };
            btnCancel = new Button { Left = 235, Top = 135, Width = 85, Text = "Отмена" };

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.Add(lblFullName);
            Controls.Add(txtFullName);
            Controls.Add(lblBirthday);
            Controls.Add(dtpBirthday);
            Controls.Add(lblPhone);
            Controls.Add(txtParentPhone);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string parentPhone = txtParentPhone.Text.Trim();

            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Введите ФИО.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(parentPhone))
            {
                MessageBox.Show("Введите телефон родителя.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Формат из ТЗ: +7 (123) 1234567
            if (!Regex.IsMatch(parentPhone, @"^\+7 \(\d{3}\) \d{7}$"))
            {
                MessageBox.Show("Телефон должен быть в формате: +7 (123) 1234567", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
try
            {
                var sportsman = new Sportsman
                {
                    FullName = fullName,
                    Birthday = dtpBirthday.Value.Date,
                    ParentPhone = parentPhone
                };

                _db.AddSportsman(sportsman);

                MessageBox.Show("Спортсмен добавлен.", "Успех",
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
