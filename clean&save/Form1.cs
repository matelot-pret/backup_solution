using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

/*
 TODO :
1. Trouver comment gérer l'annulation (peut-être avec un fichier temporaire)
*/
namespace clean_save
{
    public partial class Form1 : Form
    {
        Backup backup;
        public Form1()

        {
            InitializeComponent();
            button2.Enabled = false;
        }

        CancellationTokenSource tokenSource;
        private async void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.Description = "Veuillez selectionnez le dossier à sauvegarder.";
            folderBrowser.ShowNewFolderButton = false;
            folderBrowser.RootFolder = Environment.SpecialFolder.Personal;
            folderBrowser.ShowDialog();
            string sourcePath = folderBrowser.SelectedPath;
            string dest = Backup.DestinationFolder();

            label_source_folder.Text = "Source : " + sourcePath;
            label_destination_folder.Text = "Destination : " + dest;

            backup = new Backup(sourcePath);

            tokenSource?.Dispose();

            tokenSource = new CancellationTokenSource();
            button1.Enabled = false;
            button2.Enabled = true;


            IProgress<string> progressBackup = new Progress<string>(message =>
            {
                progress_backup.AppendText(message + "\n");
            });

            IProgress<string> progressClean = new Progress<string>(message =>
            {
                progress_clean.AppendText(message + "\n");
            });

            CancellationToken token = tokenSource.Token;

            try
            {
                progress_backup.Clear();
                progress_clean.Clear();
                BackupState resultat = await backup.LaunchAll(dest, progressBackup, progressClean, token);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1.Enabled = true;
                button2.Enabled = false;
            }


            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            tokenSource?.Dispose();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            tokenSource?.Cancel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void label_source_folder_Click(object sender, EventArgs e)
        {

        }

        private void Sauvegarde_Click(object sender, EventArgs e)
        {

        }

        private void Nettoyage_Click(object sender, EventArgs e)
        {

        }

        private void label_destination_folder_Click(object sender, EventArgs e)
        {

        }

        private void progress_backup_TextChanged(object sender, EventArgs e)
        {

        }

        private void progress_clean_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
