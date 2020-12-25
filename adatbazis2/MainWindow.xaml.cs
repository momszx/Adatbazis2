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

namespace adatbazis2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CarService CarService;
        string ID = string.Empty;

        private bool LoggedIn()
        {
            return ID != string.Empty;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CarService = new CarService();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try {
                if (LoggedIn() && ID != "WRONG_LOGIN_INFO") {
                    ID = string.Empty;
                    CarService.Logout(ID);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lbMessege.Content = "";
                if (!LoggedIn())
                {
                    ID = CarService.Login(tbUsername.Text, tbPassword.Password);
                    if (ID != "WRONG_LOGIN_INFO")
                    {
                        GridLogin.Visibility = Visibility.Hidden;
                        //GridMenu.Visibility = Visibility.Visible;
                        lbMessege.Content = "Kérlek add meg a felhasználóneved és a jelszót!";
                        //ListItems();
                    }
                    else
                    {
                        ID = string.Empty;
                        lbMessege.Content = "Rossz felhasználónév vagy jelszó";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (LoggedIn()) {
                    GridMenu.Visibility = Visibility.Hidden;
                    GridAdd.Visibility = Visibility.Visible;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (LoggedIn()) {
                    Car car = (Car)dgList.SelectedItem;
                    if (car != null) {
                        if (CarService.Delete(car.TulajdonosId,car.AutoId,car.TipusId, ID)) {
                            ListItems();
                        } else MessageBox.Show("Sikertelen törlés");
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btLogout_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (LoggedIn()) {
                    ID = string.Empty;
                    CarService.Logout(ID);
                    lbMessege.Content = "Kérlek add meg a felhasználóneved és a jelszót!";
                    GridMenu.Visibility = Visibility.Hidden;
                    GridLogin.Visibility = Visibility.Visible;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (LoggedIn()) {
                    ListItems();
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btModify_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (LoggedIn())
                {
                    Car car = (Car) dgList.SelectedItem;
                    tbModifyCarTulajdonos.Text = car.Tulajdonos;
                    tbModifyCarRendszam.Text = car.Rendszam;
                    tbModifyCarAlvazszam.Text = car.Alvazszam;
                    tbModifyCarTipus.Text = car.Tipus;
                    tbModifyCarMarka.Text = car.Marka;
                    lbTulajdonosID.Content = car.TulajdonosId;
                    lbCarID.Content = car.AutoId;
                    lbTipusID.Content = car.TipusId;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btAddCar_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (LoggedIn())
                {
                    string Cartulajdonos = tbCarTulajdonos.Text,
                        CarRendzsam = tbCarRendszam.Text,
                        CarAlvazszam = tbCarAlvazszam.Text,
                        CarTipus= tbCarTipus.Text,
                        CarMarka= tbCarMarka.Text;
                    if (Cartulajdonos.Length > 0 && CarRendzsam.Length > 0&& CarAlvazszam.Length > 0&& CarTipus.Length > 0&& CarMarka.Length > 0) {
                        if (CarService.Add(CarAlvazszam,CarRendzsam,Cartulajdonos,CarTipus,CarMarka,ID))
                        {
                            tbCarTulajdonos.Text = "";
                            tbCarRendszam.Text = "";
                            tbCarAlvazszam.Text = "";
                            tbCarTipus.Text = "";
                            tbCarMarka.Text = "";
                            ListItems();
                            GridAdd.Visibility = Visibility.Hidden;
                            GridMenu.Visibility = Visibility.Visible;
                        } else MessageBox.Show("Hiba hozzáadás közben!");
                    } else MessageBox.Show("Minden mezőt kötelező kitölteni!");
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (LoggedIn()) {
                    tbCarTulajdonos.Text = "";
                    tbCarRendszam.Text = "";
                    tbCarAlvazszam.Text = "";
                    tbCarTipus.Text = "";
                    tbCarMarka.Text = "";
                    tbModifyCarTulajdonos.Text = "";
                    tbModifyCarRendszam.Text = "";
                    tbModifyCarAlvazszam.Text = "";
                    tbModifyCarTipus.Text = "";
                    tbModifyCarMarka.Text = "";
                    GridAdd.Visibility = Visibility.Hidden;
                    GriModify.Visibility = Visibility.Hidden;
                    GridMenu.Visibility = Visibility.Visible;
                    ListItems();
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private void btModifySendCar_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (LoggedIn())
                {
                    string CarTulajdonos = tbModifyCarTulajdonos.Text,
                        CarRendzsam = tbModifyCarRendszam.Text,
                        CarAlvazszam =  tbModifyCarAlvazszam.Text,
                        CarTipus= tbModifyCarTipus.Text,
                        CarMarka= tbModifyCarMarka.Text;
                    int CartulajdonosID = (int) lbTulajdonosID.Content,
                        CarId = (int) lbCarID.Content,
                        CarTipusId = (int) lbTipusID.Content;
                    if (CarTulajdonos.Length > 0 && CarRendzsam.Length > 0&& CarAlvazszam.Length > 0&& CarTipus.Length > 0&& CarMarka.Length > 0) {
                        if (CarService.Modify(CartulajdonosID, CarId, CarTipusId, CarAlvazszam, CarRendzsam,
                            CarTulajdonos, CarTipus, CarMarka, ID))
                        {
                            tbCarTulajdonos.Text = "";
                            tbCarRendszam.Text = "";
                            tbCarAlvazszam.Text = "";
                            tbCarTipus.Text = "";
                            tbCarMarka.Text = "";
                            ListItems();
                            GriModify.Visibility = Visibility.Hidden;
                            GridMenu.Visibility = Visibility.Visible;
                        } else MessageBox.Show("Hiba hozzáadás közben!");
                    } else MessageBox.Show("Minden mezőt kötelező kitölteni!");
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private void ListItems() {
            try {
                if (LoggedIn()) {
                    List<Car> cars = CarService.List(ID).ToList<Car>();
                    dgList.ItemsSource = cars;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

        }
        
    }
}
