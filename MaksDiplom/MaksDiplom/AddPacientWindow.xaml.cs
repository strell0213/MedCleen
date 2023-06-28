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

namespace MaksDiplom
{
    /// <summary>
    /// Логика взаимодействия для AddPacientWindow.xaml
    /// </summary>
    public partial class AddPacientWindow : Window
    {
        AppContext AC;
        public AddPacientWindow()
        {
            
            AC = new AppContext();
            InitializeComponent();
            if(Class1.isEdit == 1) {
                AddPacientButton.Content = "Редактировать пользователя";
            }
            else
            {
                AddPacientButton.Content = "Добавить пользователя";
            }
        }

        private void AddPacientButton_Click(object sender, RoutedEventArgs e)
        {
            if (Class1.isEdit == 0)
            {
                int count = 0;
                //проверка
                if (NameText.Text == "")
                {
                    MessageBox.Show("Вы не ввели имя пациента!");
                    count++;
                }
                else if (SurnameText.Text == "")
                {
                    MessageBox.Show("Вы не ввели фамилию пациента!");
                    count++;
                }
                else if (PatronomicText.Text == "")
                {
                    MessageBox.Show("Вы не ввели отчество пациента!");
                    count++;
                }
                else if (PolicyText.Text == "" || PolicyText.Text.Length < 16)
                {
                    MessageBox.Show("Вы не ввели ПОЛИС пациента!");
                    count++;
                }
                else if (PhoneText.Text == "" || PhoneText.Text.Length < 11)
                {
                    MessageBox.Show("Вы не ввели номер телефона пациента!");
                    count++;
                }
                if (count == 0)
                {
                    //добавление пациента. объявляется класс Pacient и туда добавляется все написанное из TextBox и далее база данных сохраняется и окно закрывается.
                    Pacient pacient = new Pacient(NameText.Text, SurnameText.Text, PatronomicText.Text, PolicyText.Text, PhoneText.Text);
                    AC.Pacients.Add(pacient);
                    AC.SaveChanges();
                    MessageBox.Show("Успешно");
                    count = 0;
                    Class1.isOpenWin = 0;
                    this.Close();
                }
                else
                {
                    count = 0;
                }
            }
            else {
                int count = 0;
                //проверка
                if (NameText.Text == "")
                {
                    MessageBox.Show("Вы не ввели имя пациента!");
                    count++;
                }
                else if (SurnameText.Text == "")
                {
                    MessageBox.Show("Вы не ввели фамилию пациента!");
                    count++;
                }
                else if (PatronomicText.Text == "")
                {
                    MessageBox.Show("Вы не ввели отчество пациента!");
                    count++;
                }
                else if (PolicyText.Text == "" || PolicyText.Text.Length < 16)
                {
                    MessageBox.Show("Вы не ввели ПОЛИС пациента!");
                    count++;
                }
                else if (PhoneText.Text == "" || PhoneText.Text.Length < 11)
                {
                    MessageBox.Show("Вы не ввели номер телефона пациента!");
                    count++;
                }
                if (count == 0)
                {
                    var w = AC.Pacients.Where(d => d.policyNumber == Class1.savepacient).FirstOrDefault();
                    w.surname = SurnameText.Text;
                    w.name = NameText.Text;
                    w.patronomic = PatronomicText.Text;
                    w.phoneNumber = PhoneText.Text;
                    w.policyNumber = PolicyText.Text;
                    AC.SaveChanges();
                    count = 0;
                    Class1.isOpenWin = 0;
                    Class1.isEdit = 0;
                    Class1.savepacient = "";
                    this.Close();
                }
                else
                {
                    count = 0;
                }
                
            
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Class1.isOpenWin = 0;
            Class1.isEdit = 0;
            Class1.savepacient = "";
        }

        private void PolicyText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox box = (TextBox)sender;
            e.Handled = box.Text.Length >= 16;
            if (!Char.IsDigit(e.Text, 0))
                e.Handled = true;
        }

        private void PhoneText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox box = (TextBox)sender;
            e.Handled = box.Text.Length >= 11;
            if (!Char.IsDigit(e.Text, 0))
                e.Handled = true;
        }
    }
}
