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

namespace TP_BoardGameGeek_xmlAPI_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BGGConnection connection;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int parsedId;
            string id = FindById_TextBox.Text;
            
            if (int.TryParse(id, out parsedId))
            {
                FindById_Result.Text = BGGConnection.GetGameById(parsedId);
            } else
            {
                MessageBox.Show("Erreur, ID invalide.");
            }
        }

        private void FindByName_Button_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(FindByName_TextBox.Text))
            {
                MessageBox.Show("Erreur, entrée invalide");
            } else
            {
                FindByName_Result.Text = "- " + string.Join("\n- ", BGGConnection.GetGameByName(FindByName_TextBox.Text));
                if (string.IsNullOrEmpty(FindByName_Result.Text))
                {
                    FindByName_Result.Text = "No match found !";
                }
            }
        }

        private void FindGameDetail_Button_Click(object sender, RoutedEventArgs e)
        {
           GameDetail_TextBlock.Text = BGGConnection.GetGameDetails(FindGameDetails_TextBox.Text);
        }
    }
}