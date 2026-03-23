namespace clean_save
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            groupBox1 = new GroupBox();
            Nettoyage = new Label();
            Sauvegarde = new Label();
            progress_clean = new RichTextBox();
            progress_backup = new RichTextBox();
            label_source_folder = new Label();
            label_destination_folder = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(231, 405);
            button1.Name = "button1";
            button1.Size = new Size(81, 33);
            button1.TabIndex = 0;
            button1.Text = "Lancez";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(451, 405);
            button2.Name = "button2";
            button2.Size = new Size(82, 33);
            button2.TabIndex = 1;
            button2.Text = "Annuler";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // folderBrowserDialog1
            // 
            folderBrowserDialog1.HelpRequest += folderBrowserDialog1_HelpRequest;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(Nettoyage);
            groupBox1.Controls.Add(Sauvegarde);
            groupBox1.Controls.Add(progress_clean);
            groupBox1.Controls.Add(progress_backup);
            groupBox1.Controls.Add(label_source_folder);
            groupBox1.Controls.Add(label_destination_folder);
            groupBox1.Location = new Point(48, 25);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(694, 374);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // Nettoyage
            // 
            Nettoyage.AutoSize = true;
            Nettoyage.Location = new Point(403, 212);
            Nettoyage.Name = "Nettoyage";
            Nettoyage.Size = new Size(148, 15);
            Nettoyage.TabIndex = 10;
            Nettoyage.Text = "Progression du nettoyage :";
            Nettoyage.Click += Nettoyage_Click;
            // 
            // Sauvegarde
            // 
            Sauvegarde.AutoSize = true;
            Sauvegarde.Location = new Point(22, 212);
            Sauvegarde.Name = "Sauvegarde";
            Sauvegarde.Size = new Size(166, 15);
            Sauvegarde.TabIndex = 9;
            Sauvegarde.Text = "Progression de la sauvegarde :";
            Sauvegarde.Click += Sauvegarde_Click;
            // 
            // progress_clean
            // 
            progress_clean.Location = new Point(403, 230);
            progress_clean.Name = "progress_clean";
            progress_clean.Size = new Size(242, 118);
            progress_clean.TabIndex = 8;
            progress_clean.Text = "";
            progress_clean.TextChanged += progress_clean_TextChanged;
            // 
            // progress_backup
            // 
            progress_backup.Location = new Point(22, 230);
            progress_backup.Name = "progress_backup";
            progress_backup.Size = new Size(242, 118);
            progress_backup.TabIndex = 7;
            progress_backup.Text = "";
            progress_backup.TextChanged += progress_backup_TextChanged;
            // 
            // label_source_folder
            // 
            label_source_folder.Location = new Point(22, 44);
            label_source_folder.Name = "label_source_folder";
            label_source_folder.Size = new Size(242, 59);
            label_source_folder.TabIndex = 5;
            label_source_folder.Click += label_source_folder_Click;
            // 
            // label_destination_folder
            // 
            label_destination_folder.Location = new Point(22, 127);
            label_destination_folder.Name = "label_destination_folder";
            label_destination_folder.Size = new Size(242, 60);
            label_destination_folder.TabIndex = 4;
            label_destination_folder.Click += label_destination_folder_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private FolderBrowserDialog folderBrowserDialog1;
        private GroupBox groupBox1;
        private Label label_destination_folder;
        private Label label_source_folder;
        private RichTextBox progress_clean;
        private RichTextBox progress_backup;
        private Label Nettoyage;
        private Label Sauvegarde;
    }
}
