using System;
using System.Data;
using System.Windows.Forms;
using SportClubStart.Data;

namespace SportClubStart
{
    public partial class MainForm : Form
    {
        private readonly SportClubDatabase _db;

        public MainForm() : this(new SportClubDatabase())
        {
        }

        public MainForm(SportClubDatabase db)
        {
            InitializeComponent();

            btnChildActivity.Text = "Активность детей";

            _db = db;

            if (!_db.TestConnection(out string errorText))
            {
                throw new Exception("Ошибка подключения к БД: " + errorText);
            }


            dtpStart.Value = DateTime.Now.AddMonths(-1);
            dtpEnd.Value = DateTime.Now;

            Text = "Спортклуб (отчёты)";
        }

        private void btnTruants_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable table = _db.GetTruantsReport(dtpStart.Value, dtpEnd.Value);
                ShowTable(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnCoachStats_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable table = _db.GetCoachStatsReport(dtpStart.Value, dtpEnd.Value);
                ShowTable(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnChildActivity_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable table = _db.GetChildrenActivityReport(dtpStart.Value, dtpEnd.Value);
                ShowTable(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnAddAttendance_Click(object sender, EventArgs e)
        {
            try
            {
                using (var addForm = new AddAttendanceForm(_db))
                {
                    addForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnAddSportsman_Click(object sender, EventArgs e)
        {
            try
            {
                using (var addForm = new AddSportsmanForm(_db))
                {
                    addForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnAddCoach_Click(object sender, EventArgs e)
        {
            try
            {
                using (var addForm = new AddCoachForm(_db))
                {
                    addForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void ShowTable(DataTable table)
        {
            dgvReport.DataSource = table;
            dgvReport.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dgvReport.ReadOnly = true;

            if (table.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных за выбранный период", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
