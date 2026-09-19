using StudentSubjectManagement.Student_Subject_Management;

namespace ExampleCAdvance
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            System.Windows.Forms.Application.Run(new MainForm());
        }
    }
}
