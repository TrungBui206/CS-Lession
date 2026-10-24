
using System;
using System.Windows.Forms;
using StudentSubjectManagement;

namespace ExampleCAdvance
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new StudentManagementForm());
        }
    }
}
