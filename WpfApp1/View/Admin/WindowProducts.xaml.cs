using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using WpfApp1.Model;
using WpfApp1.view.Admin.Buttons;
using WpfApp1.view.Admin;

namespace WpfApp1.view.Admin
{
    /// <summary>
    /// Логика взаимодействия для WindowProducts.xaml
    /// </summary>
    public partial class WindowProducts : Window
    {
        Service s;
        public ObservableCollection<Service> Services { get; set; }
        public WindowProducts()
        {
            InitializeComponent();
            DB db = new DB();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM Product", db.getConnection());
            db.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            Services = new ObservableCollection<Service> { };
            while (reader.Read())
            {
                Services.Add(new Service
                {
                    ID = int.Parse(reader[0].ToString()),
                    Title = reader[1].ToString(),
                    Cost = "Цена - " + float.Parse(reader[2].ToString()).ToString() + " рублей",
                    Costedit = float.Parse(reader[2].ToString()).ToString(),
                    DescriptionEdit = reader[3].ToString(),
                    Description = "Описание - " + reader[3].ToString(),
                    MainImagePath = reader[4].ToString()
                });

            }
            productlist.ItemsSource = Services;
        }


       
    }
}
