using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.Sqlite;
using System.Management;


namespace testFirstWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public async Task MonitorProcesess()
        {
            ManagementObjectSearcher processes = new ManagementObjectSearcher("SELECT * FROM Win32_Process");
            if (processes != null)
            {

                foreach (ManagementObject process in processes.Get())
                {
                    await DateBase.AddProcess(process["Name"].ToString(), DateTime.UtcNow.AddHours(3).ToString("T"));
                    Console.WriteLine(process["Name"]);
                }
            }
        }

        public void StartProcess()
        {


        }
    }
}