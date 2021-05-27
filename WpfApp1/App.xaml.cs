using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Context;
using WpfApp1.view.Admin;
using WpfApp1.View.Admin.Buttons;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static DataBaseContext db = new DataBaseContext();

        //private void Application_Startup(object sender, StartupEventArgs e)
        //{
        //    WindowAdminService a = new WindowAdminService();
        //    a.Show();
        //}
    }
}
