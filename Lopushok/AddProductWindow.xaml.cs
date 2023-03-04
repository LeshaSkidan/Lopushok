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
using Lopushok.Model;

namespace Lopushok
{
    /// <summary>
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow
    {
        ShopAllEntities bd = new ShopAllEntities();

        public AddProductWindow()
        {
            InitializeComponent();
            RefreshComboBox();
        }

        public void RefreshComboBox() 
        {
            var a = bd.ProductType.Where(z => z.ID != 0).ToArray();
            int b = a.Count();
            if (b != 0)
            {
                do
                {
                    TypPro.Items.Add(a[b - 1].Title.ToString());

                    b--;
                } while (b > 0);
            }
        }

        private void SelIma_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            if (NamPro.Text != null && ArtPro.Text != null && DesPro.Text != null 
                && Convert.ToInt32(CouManPro.Text) != 0 && Convert.ToInt32(NumFacPro.Text) != 0 && Convert.ToDecimal(PriPro.Text) != 0)
            {
                Product product = new Product()
                {
                    Title = NamPro.Text,
                    ArticleNumber = ArtPro.Text,
                    Description = DesPro.Text,
                    ProductionPersonCount = Convert.ToInt32(CouManPro.Text),
                    ProductionWorkshopNumber = Convert.ToInt32(NumFacPro.Text),
                    MinCostForAgent = Convert.ToDecimal(PriPro.Text)
                };
                bd.Product.Add(product);
                bd.SaveChanges();

                MessageBox.Show("Продукт добавлен!");
            }
        }

        private void ArtPro_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Q || e.Key == Key.W || e.Key == Key.E || e.Key == Key.R || e.Key == Key.T || e.Key == Key.Y || e.Key == Key.U || e.Key == Key.I || e.Key == Key.O || e.Key == Key.P || e.Key == Key.A || e.Key == Key.S || e.Key == Key.D || e.Key == Key.F || e.Key == Key.G || e.Key == Key.H || e.Key == Key.J
                 || e.Key == Key.K || e.Key == Key.L || e.Key == Key.Z || e.Key == Key.X || e.Key == Key.C || e.Key == Key.V || e.Key == Key.B || e.Key == Key.N || e.Key == Key.M || e.Key == Key.System)
            {
                e.Handled = true;
            }
        }
    }
}
