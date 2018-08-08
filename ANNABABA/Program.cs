using System;
using System.Windows.Forms;
using ANNABABA.Forms;

namespace ANNABABA
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DressForm());
           // Application.Run(new CreateForm());
        }
    }
}