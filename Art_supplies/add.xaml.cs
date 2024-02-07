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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Art_supplies.ApplicationData;

namespace Art_supplies
{
    /// <summary>
    /// Логика взаимодействия для add.xaml
    /// </summary>
    public partial class add : Page
    {
        private Entities _currentGoods = new Entities();
        public add()
        {
            InitializeComponent();
            DataContext = _currentGoods;
            categ.ItemsSource = Entities.GetContext().Categories.Select(x => x.Category).ToList(); manuf.ItemsSource = Entities.GetContext().Manufacturers.Select(x => x.Name).ToList();
            prov.ItemsSource = Entities.GetContext().Providers.Select(x => x.Name).ToList(); unit.ItemsSource = Entities.GetContext().Units.Select(x => x.Name).ToList();
            name.ItemsSource = Entities.GetContext().Names_products.Select(x => x.Name).ToList();
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (AppConn.model1.Products.Count(x => x.Articul == articul.Text) > 0)
            {
                MessageBox.Show("Товар с таким артиклем существует!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error); return;
            }
            int istock = Convert.ToInt32(stock.Text);
            int act = Convert.ToInt32(real_dis.Text); int _max = Convert.ToInt32(max.Text);
            int _cost = Convert.ToInt32(cost.Text);
            try
            {
                Products prod = new Products()
                {
                    Articul = articul.Text,
                    Name_product = Convert.ToInt32(name.SelectedIndex + 1),
                    Unit = Convert.ToInt32(unit.SelectedIndex + 1),
                    Cost = _cost,
                    Max_discount = _max,
                    Manufacturer = Convert.ToInt32(manuf.SelectedIndex + 1),
                    Provider = Convert.ToInt32(prov.SelectedIndex + 1),
                    Category = Convert.ToInt32(categ.SelectedIndex + 1),
                    Real_discount = act,
                    In_stock = istock,
                    Description = desc.Text,
                    IMG = null
                }; 
                AppConn.model1.Products.Add(prod);
                AppConn.model1.SaveChanges(); MessageBox.Show("Товар успешно добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frame.GoBack();
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
