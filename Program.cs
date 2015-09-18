using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Marimba
{
    static class Program
    {
        public static MainMenu home;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            home = new MainMenu(args);
            Application.Run(home);
        }
    }
}
