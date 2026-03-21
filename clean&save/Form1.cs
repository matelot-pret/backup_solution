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

namespace clean_save
{
    public partial class Form1 : Form
    {
        Backup backup;
        public Form1()

        {
            InitializeComponent();

            label1.AutoSize = false;
            label1.MaximumSize = new Size(200, 0);
            label1.Size = new Size(200, 100);

            backup = new Backup();
        }

        CancellationTokenSource tokenSource;
        private async void button1_Click(object sender, EventArgs e)
        {
            tokenSource = new CancellationTokenSource();
            button1.Enabled = false;
            button2.Enabled = true;
            string dest = Backup.DestinationFolder();

            Directory.CreateDirectory(@"C:\test\niveau1\niveau2\niveau3");

            IProgress<string> progressBackup = new Progress<string>(message =>
            {
                label1.Text = message;
            });

            CancellationToken token = tokenSource.Token;

            try
            {
                BackupState resultat = await backup.LaunchAll(dest, progressBackup, token);
                if (resultat == BackupState.Success)
                {
                    label1.Text = "Succès de l'opération de sauvegarde et nettoyage !";
                }
                else if (resultat == BackupState.Failed)
                {
                    label1.Text = "Echec de l'opération de sauvegarde et nettoyage !";
                }
                else
                {
                    label1.Text = "Opération de sauvegarde et nettoyage annulé !";
                }
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            tokenSource.Cancel();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
