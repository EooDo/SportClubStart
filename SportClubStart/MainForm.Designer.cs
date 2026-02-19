namespace SportClubStart
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        private void InitializeComponent()
        {
            this.btnTruants = new System.Windows.Forms.Button();
            this.btnCoachStats = new System.Windows.Forms.Button();
            this.btnChildActivity = new System.Windows.Forms.Button();
            this.btnAddAttendance = new System.Windows.Forms.Button();
            this.btnAddSportsman = new System.Windows.Forms.Button();
            this.btnAddCoach = new System.Windows.Forms.Button();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // Кнопка Прогульщики крч
            this.btnTruants.Location = new System.Drawing.Point(65, 85);
            this.btnTruants.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTruants.Name = "btnTruants";
            this.btnTruants.Size = new System.Drawing.Size(176, 28);
            this.btnTruants.TabIndex = 0;
            this.btnTruants.Text = "Прогульщики";
            this.btnTruants.UseVisualStyleBackColor = true;
            this.btnTruants.Click += new System.EventHandler(this.btnTruants_Click);
            // Тренеры статистика
            this.btnCoachStats.Location = new System.Drawing.Point(271, 85);
            this.btnCoachStats.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCoachStats.Name = "btnCoachStats";
            this.btnCoachStats.Size = new System.Drawing.Size(176, 28);
            this.btnCoachStats.TabIndex = 1;
            this.btnCoachStats.Text = "Статистика тренеров";
            this.btnCoachStats.UseVisualStyleBackColor = true;
            this.btnCoachStats.Click += new System.EventHandler(this.btnCoachStats_Click);
            // Активность детей
            this.btnChildActivity.Location = new System.Drawing.Point(476, 88);
            this.btnChildActivity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnChildActivity.Name = "btnChildActivity";
            this.btnChildActivity.Size = new System.Drawing.Size(176, 25);
            this.btnChildActivity.TabIndex = 2;
            this.btnChildActivity.Text = "Активность детей";
            this.btnChildActivity.UseVisualStyleBackColor = true;
            this.btnChildActivity.Click += new System.EventHandler(this.btnChildActivity_Click);
            // Кнопка добавить посещение
            this.btnAddAttendance.Location = new System.Drawing.Point(65, 144);
            this.btnAddAttendance.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddAttendance.Name = "btnAddAttendance";
            this.btnAddAttendance.Size = new System.Drawing.Size(176, 28);
            this.btnAddAttendance.TabIndex = 3;
            this.btnAddAttendance.Text = "Добавить посещение";
            this.btnAddAttendance.UseVisualStyleBackColor = true;
            this.btnAddAttendance.Click += new System.EventHandler(this.btnAddAttendance_Click);
            // Кнопка добавить спортсмена
            this.btnAddSportsman.Location = new System.Drawing.Point(271, 144);
            this.btnAddSportsman.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddSportsman.Name = "btnAddSportsman";
            this.btnAddSportsman.Size = new System.Drawing.Size(176, 28);
            this.btnAddSportsman.TabIndex = 8;
            this.btnAddSportsman.Text = "Добавить спортсмена";
            this.btnAddSportsman.UseVisualStyleBackColor = true;
            this.btnAddSportsman.Click += new System.EventHandler(this.btnAddSportsman_Click);
            // Кнопка добавить тренера
            this.btnAddCoach.Location = new System.Drawing.Point(476, 144);
            this.btnAddCoach.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddCoach.Name = "btnAddCoach";
            this.btnAddCoach.Size = new System.Drawing.Size(176, 28);
            this.btnAddCoach.TabIndex = 9;
            this.btnAddCoach.Text = "Добавить тренера";
            this.btnAddCoach.UseVisualStyleBackColor = true;
            this.btnAddCoach.Click += new System.EventHandler(this.btnAddCoach_Click);
            // dtpStart
            this.dtpStart.Location = new System.Drawing.Point(65, 30);
            this.dtpStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(265, 22);
            this.dtpStart.TabIndex = 4;
            // dtpEnd
            this.dtpEnd.Location = new System.Drawing.Point(387, 30);
            this.dtpEnd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(265, 22);
            this.dtpEnd.TabIndex = 5;
            // dgvReport
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Location = new System.Drawing.Point(65, 193);
            this.dgvReport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.RowHeadersWidth = 51;
            this.dgvReport.Size = new System.Drawing.Size(587, 346);
            this.dgvReport.TabIndex = 6;
            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 554);
            this.Controls.Add(this.dgvReport);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.btnAddCoach);
            this.Controls.Add(this.btnAddSportsman);
            this.Controls.Add(this.btnAddAttendance);
            this.Controls.Add(this.btnChildActivity);
            this.Controls.Add(this.btnCoachStats);
            this.Controls.Add(this.btnTruants);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTruants;
        private System.Windows.Forms.Button btnCoachStats;
        private System.Windows.Forms.Button btnChildActivity;
        private System.Windows.Forms.Button btnAddAttendance;
        private System.Windows.Forms.Button btnAddSportsman;
        private System.Windows.Forms.Button btnAddCoach;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DataGridView dgvReport;
    }
}

