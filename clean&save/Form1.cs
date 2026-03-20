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
        public Form1()
            
        {

            InitializeComponent();
        }

        CancellationTokenSource _cts;
        Task maTache;
        CancellationToken token;
        private async void button1_Click(object sender, EventArgs e)
        {
            _cts = new CancellationTokenSource();
            token = _cts.Token;

            button1.Enabled = false;
            button2.Enabled = true;

            maTache = simulatedTask();
            await maTache;

            if (token.IsCancellationRequested)
            {
                Invoke(new Action(() => label1.Text = label1.Text = "Annulé proprement"));
            }
            else
            {
                Invoke(new Action(() => label1.Text = label1.Text = "Terminé"));
            }

            button1.Enabled = true; button2.Enabled = false;
        }

        public async Task simulatedTask()
        {
            bool continu = token.IsCancellationRequested;
            int i;
            for (i = 0; i < 10 && continu; i++)
            {
                Invoke(new Action(() => label1.Text = label1.Text = "Étape " + i + "/10"));
                await Task.Delay(1000);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                _cts.Cancel();
            }
            catch (Exception ex)
            {
                Invoke(new Action(() => label1.Text = " Veuillez appuyer Lancer avant d'appuyer Annuler !"));
            }
        }
    }
}
