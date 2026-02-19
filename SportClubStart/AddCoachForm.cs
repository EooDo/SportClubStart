using System;
using System.Windows.Forms;
using SportClubStart.Data;
using SportClubStart.Models;

namespace SportClubStart
{
    public class AddCoachForm : Form
    {
        private readonly SportClubDatabase _db;

        private TextBox txtFullName;
        private TextBox txtSportType;
        private Button btnSave;
        private Button btnCancel;

        public AddCoachForm(SportClubDatabase db)
        {
            _db = db;

            Text = "Добавить тренера";
            Width = 360;
            Height = 210;
            StartPosition = FormStartPosition.CenterParent;

            BuildUi();
        }

        private void BuildUi()
        {
            var lblFullName = new Label { Left = 12, Top = 20, Width = 120, Text = "ФИО:" };
            txtFullName = new TextBox { Left = 140, Top = 16, Width = 180 };

            var lblSportType = new Label { Left = 12, Top = 55, Width = 120, Text = "Вид спорта:" };
            txtSportType = new TextBox { Left = 140, Top = 52, Width = 180 };

            btnSave = new Button { Left = 140, Top = 105, Width = 85, Text = "Сохранить" };
            btnCancel = new Button { Left = 235, Top = 105, Width = 85, Text = "Отмена" };

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.Add(lblFullName);
            Controls.Add(txtFullName);
            Controls.Add(lblSportType);
            Controls.Add(txtSportType);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string sportType = txtSportType.Text.Trim();

            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Введите ФИО.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(sportType))
            {
                MessageBox.Show("Введите вид спорта.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var coach = new Coach
                {
                    FullName = fullName,
                    SportType = sportType
                };

                _db.AddCoach(coach);

                MessageBox.Show("Тренер добавлен.", "Успех",
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AddCoachForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "AddCoachForm";
            this.ResumeLayout(false);

        }
    }
}
