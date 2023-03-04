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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ShopAllEntities bd = new ShopAllEntities();

        public MainWindow()
        {
            InitializeComponent();
            RefreshPagination();
            RefreshButtons();
            RefreshComboBox();
            SortedCB.SelectedIndex = 0;
            FilterCB.SelectedIndex = 0;
        }
        private void BLeft_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber == 0)
                return;
            pageNumber--;
            RefreshPagination();
        }

        private void BRight_Click(object sender, RoutedEventArgs e)
        {
            if (bd.Product.ToList().Count % pageSize == 0)
            {
                if (pageNumber == (bd.Product.ToList().Count / pageSize) - 1)
                    return;
            }
            else
            {
                if (pageNumber == (bd.Product.ToList().Count / pageSize))
                    return;
            }
            pageNumber++;
            RefreshPagination();
        }

        int pageSize = 20;
        int pageNumber;

        private void RefreshPagination()
        {
            MainSP.Children.Clear();
            var a = bd.Product.Where(z => z.ID != 0).ToArray().Skip(pageNumber * pageSize).Take(pageSize).ToList();
            int b = a.Count;
            if (b != 0)
            {
                do
                {
                    Border bor = new Border();
                    bor.Height = 140;
                    bor.BorderBrush = Brushes.Black;
                    bor.BorderThickness = new Thickness(1);
                    bor.Margin = new Thickness(5);

                    Grid gri = new Grid();

                    Image img = new Image();
                    img.HorizontalAlignment = HorizontalAlignment.Left;
                    img.VerticalAlignment = VerticalAlignment.Center;
                    img.Margin = new Thickness(20, 25, 450, 25);
                    if (a[b - 1].Image != null)
                    {
                        img.Source = new BitmapImage(new Uri(a[b - 1].Image.ToString(), UriKind.Relative));
                    }
                    else
                    {
                        img.Source = new BitmapImage(new Uri("Resources/picture.png", UriKind.Relative));
                    }

                    StackPanel sp1 = new StackPanel();
                    sp1.Margin = new Thickness(180, 10, 100, 10);
                    sp1.VerticalAlignment = VerticalAlignment.Center;

                    StackPanel sp2 = new StackPanel();
                    sp2.Orientation = Orientation.Horizontal;

                    TextBlock TypPro = new TextBlock();
                    TypPro.Margin = new Thickness(3);
                    TypPro.Text = a[b - 1].ProductType.Title.ToString();
                    TypPro.FontSize = 19;

                    TextBlock stick = new TextBlock();
                    stick.Margin = new Thickness(3);
                    stick.Text = " | ";
                    stick.FontSize = 19;

                    TextBlock NamPro = new TextBlock();
                    NamPro.Margin = new Thickness(3);
                    NamPro.Text = a[b - 1].Title.ToString();
                    NamPro.FontSize = 19;

                    TextBlock artic = new TextBlock();
                    artic.Margin = new Thickness(3);
                    artic.Text = a[b - 1].ArticleNumber.ToString();
                    artic.FontSize = 16;

                    WrapPanel wp = new WrapPanel();

                    TextBlock mater1 = new TextBlock();
                    mater1.Margin = new Thickness(3);
                    mater1.Text = "Материалы: ";
                    mater1.FontSize = 16;

                    TextBlock mater2 = new TextBlock();
                    mater2.Margin = new Thickness(3);
                    mater2.Text = "дерево 2м^3, бумага 3м";
                    mater2.FontSize = 16;

                    TextBlock price = new TextBlock();
                    price.Margin = new Thickness(600, 20, 20, 20);
                    price.Text = a[b - 1].MinCostForAgent.ToString();
                    price.FontSize = 16;
                    price.HorizontalAlignment = HorizontalAlignment.Right;
                    price.VerticalAlignment = VerticalAlignment.Top;

                    MainSP.Children.Add(bor);
                    bor.Child = gri;
                    gri.Children.Add(img);
                    gri.Children.Add(sp1);
                    sp1.Children.Add(sp2);
                    sp2.Children.Add(TypPro);
                    sp2.Children.Add(stick);
                    sp2.Children.Add(NamPro);
                    sp1.Children.Add(artic);
                    sp1.Children.Add(wp);
                    wp.Children.Add(mater1);
                    wp.Children.Add(mater2);
                    gri.Children.Add(price);

                    b--;
                }
                while (b > 0);
            }
        }

        private void RefreshComboBox()
        {
            SortedCB.Items.Add("Все");
            SortedCB.Items.Add("От А до Я");
            SortedCB.Items.Add("От Я до А");
            SortedCB.Items.Add("Номер цеха по убыванию");
            SortedCB.Items.Add("Номер цеха по возрастанию");
            SortedCB.Items.Add("По убыванию стоимости");
            SortedCB.Items.Add("По возрастанию стоимости");

            FilterCB.Items.Add("Все");
            var a = bd.ProductType.Where(z => z.ID != 0).ToArray();
            int b = a.Count();
            if (b != 0) 
            {
                do
                {
                    FilterCB.Items.Add(a[b-1].Title.ToString());

                    b--;
                } while (b > 0);
            }
        }

        private void RefreshButtons()
        {
            WPButtons.Children.Clear();
            if (bd.Product.ToList().Count % pageSize == 0)
            {
                for (int i = 0; i < bd.Product.ToList().Count / pageSize; i++)
                {
                    Button button = new Button();
                    button.Content = i + 1;
                    button.Click += Button_Click;
                    button.Margin = new Thickness(2);
                    button.Width = 20;
                    button.Height = 20;
                    button.FontSize = 13;
                    button.Background = Brushes.Transparent;
                    button.BorderBrush = Brushes.Transparent;
                    WPButtons.Children.Add(button);
                }
            }
            else
            {
                for (int i = 0; i < bd.Product.ToList().Count / pageSize + 1; i++)
                {
                    Button button = new Button();
                    button.Content = i + 1;
                    button.Click += Button_Click;
                    button.Margin = new Thickness(2);
                    button.Width = 20;
                    button.Height = 20;
                    button.FontSize = 13;
                    button.Background = Brushes.Transparent;
                    button.BorderBrush = Brushes.Transparent;
                    WPButtons.Children.Add(button);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            pageNumber = Convert.ToInt32(button.Content) - 1;
            RefreshPagination();
        }

        private void TB1_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var a = bd.Product.Where(z => z.Title.Contains(TB1.Text)).Skip(pageNumber * pageSize).Take(pageSize).ToArray();
            int b = a.Count();
            if (b != 0)
            {
                do
                {
                    Border bor = new Border();
                    bor.Height = 140;
                    bor.BorderBrush = Brushes.Black;
                    bor.BorderThickness = new Thickness(1);
                    bor.Margin = new Thickness(5);

                    Grid gri = new Grid();

                    Image img = new Image();
                    img.HorizontalAlignment = HorizontalAlignment.Left;
                    img.VerticalAlignment = VerticalAlignment.Center;
                    img.Margin = new Thickness(20, 25, 450, 25);
                    if (a[b - 1].Image != null)
                    {
                        img.Source = new BitmapImage(new Uri(a[b - 1].Image.ToString(), UriKind.Relative));
                    }
                    else
                    {
                        img.Source = new BitmapImage(new Uri("Resources/picture.png", UriKind.Relative));
                    }

                    StackPanel sp1 = new StackPanel();
                    sp1.Margin = new Thickness(180, 10, 100, 10);
                    sp1.VerticalAlignment = VerticalAlignment.Center;

                    StackPanel sp2 = new StackPanel();
                    sp2.Orientation = Orientation.Horizontal;

                    TextBlock TypPro = new TextBlock();
                    TypPro.Margin = new Thickness(3);
                    TypPro.Text = a[b - 1].ProductType.Title.ToString();
                    TypPro.FontSize = 19;

                    TextBlock stick = new TextBlock();
                    stick.Margin = new Thickness(3);
                    stick.Text = " | ";
                    stick.FontSize = 19;

                    TextBlock NamPro = new TextBlock();
                    NamPro.Margin = new Thickness(3);
                    NamPro.Text = a[b - 1].Title.ToString();
                    NamPro.FontSize = 19;

                    TextBlock artic = new TextBlock();
                    artic.Margin = new Thickness(3);
                    artic.Text = a[b - 1].ArticleNumber.ToString();
                    artic.FontSize = 16;

                    WrapPanel wp = new WrapPanel();

                    TextBlock mater1 = new TextBlock();
                    mater1.Margin = new Thickness(3);
                    mater1.Text = "Материалы: ";
                    mater1.FontSize = 16;

                    TextBlock mater2 = new TextBlock();
                    mater2.Margin = new Thickness(3);
                    mater2.Text = "дерево 2м^3, бумага 3м";
                    mater2.FontSize = 16;

                    TextBlock price = new TextBlock();
                    price.Margin = new Thickness(600, 20, 20, 20);
                    price.Text = a[b - 1].MinCostForAgent.ToString();
                    price.FontSize = 16;
                    price.HorizontalAlignment = HorizontalAlignment.Right;
                    price.VerticalAlignment = VerticalAlignment.Top;

                    MainSP.Children.Add(bor);
                    bor.Child = gri;
                    gri.Children.Add(img);
                    gri.Children.Add(sp1);
                    sp1.Children.Add(sp2);
                    sp2.Children.Add(TypPro);
                    sp2.Children.Add(stick);
                    sp2.Children.Add(NamPro);
                    sp1.Children.Add(artic);
                    sp1.Children.Add(wp);
                    wp.Children.Add(mater1);
                    wp.Children.Add(mater2);
                    gri.Children.Add(price);

                    b--;
                }
                while (b > 0);
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow addProductWindow = new AddProductWindow();
            addProductWindow.Show();
        }
    }
}
