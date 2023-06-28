using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace MaksDiplom
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppContext AC;
        public MainWindow()
        {
            //объявление базы даннных в классе MainWindow
            AC = new AppContext();
            
            //дефолт
            InitializeComponent();

            //что бы изначально показывало окно с приветствием
            MainGrid.Visibility = Visibility.Visible;
            //текст памятки
            PamyatkaText.Content = "1. При посещении поликлиники - в экстренной и неотложной \nситуации Вас должны без очереди направить в кабинет врача.\n2. При вызове участкового врача на дом медицинский\nрегистратор" +
                " поликлиники фиксирует вызов \nи передает его врачу.\n3. Запись на повторный прием осуществляется врачами\n поликлиники в день первичного приема пациента.";
            //если никто не залогинин то открывается окно авторизации, если авторизирован то открывается окно с приветствием и из базы данных берется фамилия и имя для приветсятвия
            if (Class1.IsLogin == 0)
            {
                LoginWindow loginwindow = new LoginWindow();
                loginwindow.Show();
                this.Close();
            }
            else if (Class1.IsLogin == 1) {
                var main = AC.RegistryPersons.Where(x => x.login == Class1.SaveLogin).FirstOrDefault();
                HelloText.Content = "Здравствуйте, "+main.surname +" " + main.name + " "+main.patronomic;
            }
        }
        public void updateinfo() {
            //обновление всех списков
            var r = AC.Pacients.Select(x => "Пациент\n" + x.surname + " " + x.name + " " + x.patronomic + " \nНомер Полиc: " + x.policyNumber + "\nНомер телефона: " + x.phoneNumber).ToList();
            var r2 = AC.Doctors.Select(x => "Доктор\n" + x.surname + " " + x.name + " " + x.patronomic + " \nДолжность: " + x.jobTitle).ToList();
            PacientList.ItemsSource = r;
            DoctorList.ItemsSource = r2;
        }
        private void AddPacientButton_Click(object sender, RoutedEventArgs e)
        {
            if (Class1.isOpenWin == 0)
            {
                //открытие окна с добавлением пациента
                AddPacientWindow addPacientWindow = new AddPacientWindow();
                addPacientWindow.Show();
                Class1.isOpenWin = 1;
            }
            else { 
                
            }
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            //меняет фон кнопок
            InfoButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF4A79FE");
            InfoButton.Foreground = System.Windows.Media.Brushes.White;
            MainButton.Background = System.Windows.Media.Brushes.White;
            MainButton.Foreground = System.Windows.Media.Brushes.Black;
            //меняет цвет текста кнопок
            AddVisitButton.Background = System.Windows.Media.Brushes.White;
            AddVisitButton.Foreground = System.Windows.Media.Brushes.Black;
            ProfileButton.Background = System.Windows.Media.Brushes.White;
            ProfileButton.Foreground = System.Windows.Media.Brushes.Black;
            //закрываются окна которые не нужны(hidden) и открываются которые нужны(Visible)
            MainGrid.Visibility = Visibility.Hidden;
            InfoGrid.Visibility = Visibility.Visible;
            ProfileGrid.Visibility = Visibility.Hidden;
            VisitGrid.Visibility = Visibility.Hidden;
            //обновление всех списков
            updateinfo();
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            //меняет фон кнопок
            MainButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF4A79FE");
            MainButton.Foreground = System.Windows.Media.Brushes.White;
            InfoButton.Background = System.Windows.Media.Brushes.White;
            InfoButton.Foreground = System.Windows.Media.Brushes.Black;
            //меняет цвет текста кнопок
            AddVisitButton.Background = System.Windows.Media.Brushes.White;
            AddVisitButton.Foreground = System.Windows.Media.Brushes.Black;
            ProfileButton.Background = System.Windows.Media.Brushes.White;
            ProfileButton.Foreground = System.Windows.Media.Brushes.Black;
            //закрываются окна которые не нужны(hidden) и открываются которые нужны(Visible)
            MainGrid.Visibility = Visibility.Visible;
            InfoGrid.Visibility = Visibility.Hidden;
            VisitGrid.Visibility = Visibility.Hidden;
            ProfileGrid.Visibility = Visibility.Hidden;
        }

        private void PacientListButton_Click(object sender, RoutedEventArgs e)
        {
            //открывается список пациентов и закрывается список докторов (в окне с базой данных всех пользователей)
            PacientList.Visibility = Visibility.Visible;
            DoctorList.Visibility = Visibility.Hidden;
            Class1.ForEditList = 0;
        }

        private void DoctorsListButton_Click(object sender, RoutedEventArgs e)
        {
            //открывается список докторов и закрывается список пациентов (в окне с базой данных всех пользователей)
            DoctorList.Visibility = Visibility.Visible;
            PacientList.Visibility = Visibility.Hidden;
            Class1.ForEditList = 1;
        }

        private void AddVisitButton_Click(object sender, RoutedEventArgs e)
        {
            var r = AC.Doctors.Select(x => x.surname + " "+x.name + " "+x.patronomic).ToList();
            DoctorBox.ItemsSource = r;
            var r1 = AC.Pacients.Select(x => x.policyNumber).ToList();
            PolicyText.ItemsSource= r1;

            for(int i = 8; i<= 24; i++)
            {
                HourBox.Items.Add(i.ToString());
            }
            for(int i =0 ; i<= 50; i++)
            {
                if(i % 10 == 0)
                {
                    MinuteBox.Items.Add(i.ToString());
                }
            }
            //меняет фон кнопок
            MainButton.Background = System.Windows.Media.Brushes.White;
            MainButton.Foreground = System.Windows.Media.Brushes.Black;
            InfoButton.Background = System.Windows.Media.Brushes.White;
            InfoButton.Foreground = System.Windows.Media.Brushes.Black;
            //меняет цвет текста кнопок
            AddVisitButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF4A79FE");
            AddVisitButton.Foreground = System.Windows.Media.Brushes.White;
            ProfileButton.Background = System.Windows.Media.Brushes.White;
            ProfileButton.Foreground = System.Windows.Media.Brushes.Black;
            //закрываются окна которые не нужны(hidden) и открываются которые нужны(Visible)
            MainGrid.Visibility = Visibility.Hidden;
            InfoGrid.Visibility = Visibility.Hidden;
            VisitGrid.Visibility = Visibility.Visible;
            ProfileGrid.Visibility = Visibility.Hidden;
        }

        private void DeleteListButton_Click(object sender, RoutedEventArgs e)
        {
            if (Class1.ForEditList == 0)
            {
                //удаление пациента
                try
                {
                    var r = AC.Pacients.Where(c => PacientList.SelectedItem.ToString().Contains(c.policyNumber)).FirstOrDefault();
                    if (r != null)
                    {
                        AC.Pacients.Remove(r);
                        AC.SaveChanges();
                        MessageBox.Show("Успешно");
                        updateinfo();
                    }
                }
                catch
                {

                }
            }
            else if (Class1.ForEditList == 1)
            {
                //удаление доктора
                try
                {
                    var w = AC.Doctors.Where(x => DoctorList.SelectedItem.ToString().Contains(x.surname + " " + x.name + " " + x.patronomic)).FirstOrDefault();
                    if (w != null)
                    {
                        AC.Doctors.Remove(w);
                        AC.SaveChanges();
                        MessageBox.Show("Успешно");
                        updateinfo();
                    }
                }
                catch { }
            }

        }

        private void AddVisitButtonOnDb_Click(object sender, RoutedEventArgs e)
        {
            string[] doc = DoctorBox.SelectedItem.ToString().Split(' ');
            string s = doc[0];
            string n = doc[1];
            string p = doc[2];
            string time = HourBox.SelectedItem.ToString() + ":"+MinuteBox.SelectedItem.ToString();
            var r = AC.Doctors.Where(x => x.surname == s && x.name == n && x.patronomic == p).FirstOrDefault();
            var r1 = AC.Pacients.Where(x => x.policyNumber == PolicyText.SelectedItem.ToString()).FirstOrDefault();
            VisitList visitList = new VisitList(r1.PacientID, r.DoctorID, Convert.ToInt32(CabinetText.Text), DateVisitPicker.Text, time, DisText.Text);
            AC.VisitLists.Add(visitList);
            AC.SaveChanges();
        }

        private void AddDoctorButton_Click(object sender, RoutedEventArgs e)
        {
            if (Class1.isOpenWin == 0)
            {
                AddDoctorWindow addDoctorWindow = new AddDoctorWindow();
                addDoctorWindow.Show();
                Class1.isOpenWin = 1;
            }
            else { 
                
            }
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            var r = AC.RegistryPersons.Where(c => c.login == Class1.SaveLogin).FirstOrDefault();
            RegProfile.Content = r.surname + " "+r.name + " "+r.patronomic + "\nРегистратура";
            //меняет фон кнопок
            MainButton.Background = System.Windows.Media.Brushes.White;
            MainButton.Foreground = System.Windows.Media.Brushes.Black;
            InfoButton.Background = System.Windows.Media.Brushes.White;
            InfoButton.Foreground = System.Windows.Media.Brushes.Black;
            //меняет цвет текста кнопок
            AddVisitButton.Background = System.Windows.Media.Brushes.White;
            AddVisitButton.Foreground = System.Windows.Media.Brushes.Black;
            ProfileButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF4A79FE");
            ProfileButton.Foreground = System.Windows.Media.Brushes.White;
            //закрываются окна которые не нужны(hidden) и открываются которые нужны(Visible)
            MainGrid.Visibility = Visibility.Hidden;
            InfoGrid.Visibility = Visibility.Hidden;
            VisitGrid.Visibility = Visibility.Hidden;
            ProfileGrid.Visibility = Visibility.Visible;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Class1.IsLogin = 0;
            Class1.SaveLogin = "";
            Class1.isOpenWin = 0;
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void DeleteReg_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить аккаунт?", "MedCleen", MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.Yes)
            {
                var r = AC.RegistryPersons.Where(x => x.login == Class1.SaveLogin).FirstOrDefault();
                AC.RegistryPersons.Remove(r);
                AC.SaveChanges();
                MessageBox.Show("Успешно.\nДля создания нового аккаунта нужно будет создавать вручную в SQLite!", "MedCleen", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else { 
                
            }
        }

        private void EditListButton_Click(object sender, RoutedEventArgs e)
        {
            if (Class1.ForEditList == 0)
            {
                //Редактирование пациента
                try
                {
                    var r = AC.Pacients.Where(c => PacientList.SelectedItem.ToString().Contains(c.policyNumber)).FirstOrDefault();
                    if (r != null)
                    {
                        Class1.isEdit = 1;
                        Class1.savepacient = r.policyNumber;
                        AddPacientWindow addPacientWindow = new AddPacientWindow();
                        addPacientWindow.Show();
                        updateinfo();
                    }
                }
                catch
                {

                }
            }
            else if (Class1.ForEditList == 1)
            {
                //Редактирование доктора
                try
                {
                    var w = AC.Doctors.Where(x => DoctorList.SelectedItem.ToString().Contains(x.surname + " " + x.name + " " + x.patronomic)).FirstOrDefault();
                    if (w != null)
                    {
                        Class1.isEdit = 1;
                        Class1.savepacient = w.login;
                        AddDoctorWindow addDoctorWindow = new AddDoctorWindow();
                        addDoctorWindow.Show();
                        updateinfo();
                    }
                }
                catch { }
            }
        }

        private void EditReg_Click(object sender, RoutedEventArgs e)
        {
            if (EditPassGrid.Visibility == Visibility.Hidden)
            {
                EditPassGrid.Visibility = Visibility.Visible;
            }
            else
            {
                EditPassGrid.Visibility = Visibility.Hidden;
            }
        }

        private void EditPasswordReg_1_Click(object sender, RoutedEventArgs e)
        {
            var r = AC.RegistryPersons.Where(c => c.login == Class1.SaveLogin).FirstOrDefault();
            if(r.password == OldPassText.Password)
            {
                r.password = NewPassText.Password;
                AC.SaveChanges();
                MessageBox.Show("Успешно");
                EditPassGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Не верный пароль!");
            }
        }

        private void CabinetText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox box = (TextBox)sender;
            e.Handled = box.Text.Length >= 3;
            if (!Char.IsDigit(e.Text, 0)) 
                e.Handled = true;
        }
    }
}
