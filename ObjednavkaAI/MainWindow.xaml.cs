using ObjednavkaAI.Tridy;
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

namespace ObjednavkaAI
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

        private void btnVypocitat_Click(object sender, RoutedEventArgs e)
        {
            decimal cena = decimal.Parse(txtCena.Text);

            int doprava = cmbDoprava.SelectedIndex;

            Objednavka objednavka =
                new Objednavka(cena, doprava);

            decimal celkem = objednavka.VypocitejCelkem();

            txtVysledek.Text = $"Celkem: {celkem:N2} Kč";
        }
    }
}