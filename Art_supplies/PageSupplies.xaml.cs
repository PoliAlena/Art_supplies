using Art_supplies.ApplicationData;
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

namespace Art_supplies
{
    /// <summary>
    /// Логика взаимодействия для PageSupplies.xaml
    /// </summary>
    public partial class PageSupplies : Page
    {
       
        public PageSupplies()
        {
            InitializeComponent();
            List<Products> products = AppConn.model1.Products.ToList();

            if (products.Count > 0)
            {
                tbCounter.Text = "Найдено " + products.Count + " товаров";
            }
            else
            {
                tbCounter.Text = "Ничего не найдено";
            }
            ListProducts.ItemsSource = products;

            Filtr.Items.Add("Скидка до 3");
            Filtr.Items.Add("Скидка от 3 до 5");
            Filtr.Items.Add("Скидка от 5");

            Sort.Items.Add("В порядке взорастания");
            Sort.Items.Add("В порядке уменьшения");
        }

        Products[] FindProducts()
        {
            List<Products> products = AppConn.model1.Products.ToList();
            var product = AppConn.model1.Products.ToList();
            var productall = product;
            string search = Search.Text;
            if (search != null)
            {
                ListProducts.ItemsSource = AppConn.model1.Products.Where(x => x.Names_products.Name.Contains(search) || x.Description.Contains(search)).ToList();
            }

            if (Filtr.SelectedIndex > 0)
            {
                switch (Filtr.SelectedIndex)
                {
                    case 0:
                        ListProducts.ItemsSource = AppConn.model1.Products.Where(x => x.Real_discount > 0 && x.Real_discount < 3).ToList();
                        break;
                    case 1:
                        ListProducts.ItemsSource = AppConn.model1.Products.Where(x => x.Real_discount >= 3 && x.Real_discount < 5).ToList();
                        break;
                    case 2:
                        ListProducts.ItemsSource = AppConn.model1.Products.Where(x => x.Real_discount >= 5).ToList(); break;
                }
            }


            if (Sort.SelectedIndex > 0)
            {
                switch (Sort.SelectedIndex)
                {
                    case 0:
                        ListProducts.ItemsSource = AppConn.model1.Products.OrderBy(x => x.Cost).ToList();
                        break;
                    case 1:
                        ListProducts.ItemsSource = AppConn.model1.Products.OrderByDescending(x => x.Cost).ToList();
                        break;
                }
            }

            return product.ToArray();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FindProducts();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Log.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AppFrame.frame.Navigate(new add());
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = (Products)ListProducts.SelectedItem;
            if (selectedProduct != null)
            {
                if (MessageBox.Show("Вы точно хотите удалить выбранный товар?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                AppConn.model1.Products.Remove(selectedProduct);
                AppConn.model1.SaveChanges(); ListProducts.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Вы ничего не выбрали", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void add_b_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
