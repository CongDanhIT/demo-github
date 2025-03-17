using QLKD_CHTT.View.Chủ_Quán;
using QLKD_CHTT.View.Nhân_Viên;

namespace QLKD_CHTT
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new _2_GiaoDienNV());
        }
    }
}