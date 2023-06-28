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
using System.Data.Entity;

namespace MaksDiplom
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        AppContext AC;
        public LoginWindow()
        {
            //объявление базы данных в классе LoginWindow
            AC = new AppContext();
            InitializeComponent();
        }

        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string pass = PasswordText.Password;
                //сначало проверяется есть ли пользователь которые мы написсали в TextBox под текстом "Логин"
                var r = AC.RegistryPersons.Where(x => x.login == LoginText.Text).FirstOrDefault();
                //если нашло то
                if (r != null)
                {
                    //проверяется пароль с найденного логина если успешно то
                    if (r.password == pass)
                    {
                        //Класс ставит значение что пользователь авторизовался
                        Class1.IsLogin = 1;
                        //Класс сохраняет логин пользователя для дальнейшего взаимодействия
                        Class1.SaveLogin = r.login;
                        //открывается основное окно, окно с авторизацией закрывается
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    //если не успешно
                    else
                    {
                        MessageBox.Show("Не правильно введен пароль.");
                    }
                }
                //если не нашло ищем доктора
                else
                {
                    //сначало проверяется есть ли пользователь которые мы написсали в TextBox под текстом "Логин"
                    var r2 = AC.Doctors.Where(x => x.login == LoginText.Text).FirstOrDefault();
                    //если нашло то
                    if (r2 != null)
                    {
                        //проверяется пароль с найденного логина если успешно то
                        if (r2.password == pass)
                        {
                            //Класс ставит значение что пользователь авторизовался
                            Class1.IsLogin = 1;
                            //Класс сохраняет логин пользователя для дальнейшего взаимодействия
                            Class1.SaveLogin = r2.login;
                            //открывается основное окно, окно с авторизацией закрывается
                            DoctorMainWindow doctorMainWindow = new DoctorMainWindow();
                            doctorMainWindow.Show();
                            this.Close();
                        }
                        //если не успешно
                        else
                        {
                            MessageBox.Show("Не правильно введен пароль.");
                        }
                    }
                    //если не нашло
                    else
                    {
                        MessageBox.Show("Такого логина не существует.");
                    }
                }
            }
            catch { }
            
        }
    }
}
