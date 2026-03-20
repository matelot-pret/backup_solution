using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace clean_save
{
    class Backup
    {
        string sourcePath;

        //get et setter
        public string source { get; set; }

        //constructeurs
        public Backup(string source)
        {
            sourcePath = source;
        }

        public string dayOfTheWeek()
        {
            
            DateTime todayDate = DateTime.Now;
            int dayNumber = (int)todayDate.DayOfWeek;

            string dayName = " ";
            switch (dayNumber)
            {
                case 1: dayName = "lundi";
                    break;
                case 2: dayName = "mardi";
                    break;
                case 3: dayName = "mercredi";
                    break;
                case 4: dayName = "jeudi";
                    break;
                case 5: dayName = "vendredi";
                    break;
                case 6: dayName = "samedi";
                    break;
                case 7: dayName = "dimanche";
                    break;
                default:
                    return "Une erreur est survenue lors du calcul du jour de la semaine";
            }

            return dayName;
        }

        public string destination()
        {
            string dest = AppDomain.CurrentDomain.BaseDirectory;
            string dest_parent = " ";
            //remonte dans l'arborescence de fichier du dossier Debug vers le dossier racine du projet
            for (int i = 0; i < 3; i++)
            {
                dest_parent = Directory.GetParent(dest).FullName;
            }
            string dayName = dayOfTheWeek();



            return "..\\"+dayName;
        }

    }
}
