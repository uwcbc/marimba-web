namespace Marimba
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    static class Program
    {
        public static MainMenu home;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The arguments that marimba is run with</param>
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            home = new MainMenu(args);
            Application.Run(home);
        }
    }
}
