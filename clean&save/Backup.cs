using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace clean_save
{
    class Backup
    {
        string sourcePath;

        //constructor
        public Backup()
        {
            sourcePath = SourceFolder();
            if (sourcePath == null) throw new InvalidOperationException("[ERREUR] Impossible de déterminer le dossier source.");
        }

        /*
         *utility class to get the name of the day in French
         *static because only other class use it and no instance will use it
         */
        public static string DayOfTheWeek()
        {

            DateTime todayDate = DateTime.Now;
            int dayNumber = (int)todayDate.DayOfWeek;

            string dayName = " ";
            switch (dayNumber)
            {
                case 0:
                    dayName = "dimanche";
                    break;
                case 1:
                    dayName = "lundi";
                    break;
                case 2:
                    dayName = "mardi";
                    break;
                case 3:
                    dayName = "mercredi";
                    break;
                case 4:
                    dayName = "jeudi";
                    break;
                case 5:
                    dayName = "vendredi";
                    break;
                case 6:
                    dayName = "samedi";
                    break;
                default:
                    return "Une erreur est survenue lors du calcul du jour de la semaine";
            }

            return dayName;
        }

        /*
         *utility class to get the path of the root of the project
         *static because only other class use it and no instance will use it
         */
        public static string ProjectRoot()
        {

            string actualPath = AppDomain.CurrentDomain.BaseDirectory;
            string rootPath = null;

            //goes back up the file tree from the Debug folder to the project root folder
            for (int i = 0; i < 5; i++)
            {
                rootPath = Directory.GetParent(actualPath).FullName;
                actualPath = rootPath;
            }
            return rootPath;
        }

        /*
         *utility class to get the path of the destination folder where the backup will go
         *static because only other class use it and no instance will use it
         */
        public static string DestinationFolder()
        {
            string dayName = DayOfTheWeek();
            Directory.CreateDirectory(Path.Combine(ProjectRoot(), "test", "niveau1", "niveau2", "niveau3"));
            return Path.Combine(ProjectRoot(), "destination_backup", dayName);
        }

        /*
         *utility class to get the path of the source project 
         *static because only other class use it and no instance will use it
         */
        public static string SourceFolder()
        {
            return Path.Combine(ProjectRoot(), "source_backup");
        }

        /*
         *launch the backup and returns a BackupState object that indicates whether
         *the backup was successful, failed, or was canceled(this provides greater 
         *clarity than simply returning a value).
         */
        private async Task<BackupState> LaunchBackup(string destination, IProgress<string> progress, CancellationToken token)
        {

            string rclonePath = Path.Combine(ProjectRoot(), "rclone", "rclone.exe");

            Process process = new Process();
            process.StartInfo.UseShellExecute = false;//launch the process without the cmd 
            process.StartInfo.CreateNoWindow = true;//lauch the process without create a window 
            process.StartInfo.FileName = rclonePath;
            process.StartInfo.Arguments = $"copy \"{this.sourcePath}\" \"{destination}\" --update";
            process.EnableRaisingEvents = true;//triggers the Exited event when the process is complete. WITHOUT THIS, THERE'S A PROBLEM💥 (WaitForExitAsync doesn't know when the process is finished)

            process.Start();
            progress.Report("copies des fichiers...");
            try
            {
                await process.WaitForExitAsync(token);//Wait the notification of the Exited event to free the UI Thread
                //BECAREFULL OF process.StandardError WHEN YOU WILL NEED TO KEEP THE LOGS        
            }
            catch (OperationCanceledException)
            {
                process.Kill();
                return BackupState.Canceled;
                
            }

            if(process.ExitCode > 0)
            {
                return BackupState.Failed;
            }
            return BackupState.Success;
        }

        private static async Task<BackupState> Cleanup(IProgress<string> progress, CancellationToken token)
        {
            for (int i = 0; i < 29 && !token.IsCancellationRequested; i++)
            {
                try
                {
                    await Task.Delay(1000, token);
                }
                catch (OperationCanceledException)
                {
                    return BackupState.Canceled;
                }
                progress.Report("Nettoyage en cours...");
            }
            return BackupState.Success;
        }

        public async Task<BackupState> LaunchAll(string destination, IProgress<string> progress, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(destination, "[ERREUR] chemin vers le dossier de destination null.");
            ArgumentNullException.ThrowIfNull(progress, "[ERREUR] message de progres null");

            string rclonePath = Path.Combine(ProjectRoot(), "rclone", "rclone.exe");
            if (!File.Exists(rclonePath))
            {
                throw new FileNotFoundException("[ERREUR] rclone.exe introuvable :", rclonePath);

            }
            Directory.CreateDirectory(destination);

            Task<BackupState> backup = LaunchBackup(destination,progress, token);//To keep the return of the function
            Task<BackupState> clean = Cleanup(progress, token);
            await Task.WhenAll(backup, clean);

            BackupState state = BackupState.Success;

            if (token.IsCancellationRequested)
            {
                state = BackupState.Canceled;
            }else if(clean.Result == BackupState.Failed || backup.Result == BackupState.Failed)
            {
                state = BackupState.Failed;
            }
            return state;
        }
    }
}
