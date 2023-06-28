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
    /// Логика взаимодействия для AddDoctorWindow.xaml
    /// </summary>
    public partial class AddDoctorWindow : Window
    {
        AppContext AC;
        public AddDoctorWindow()
        {
            AC = new AppContext();
            
            InitializeComponent();
            if (Class1.isEdit == 1) {
                AddDoctorButton.Content = "Редактировать доктора";
            }
            else
            {
                AddDoctorButton.Content = "Добавить доктора";
            }
        }
        public string generatePassword(string pass) { 
            Random random = new Random();
            string alf= "a b c d e f g h i j k l m n o p q r s t u v w x y z A B C D E F G H I J K L M N O P Q R S T U V W X Y Z 1 2 3 4 5 6 7 8 9 0";
            string[] al = alf.Split(' ');
            for (int i = 0; i <= 6; i++) {
                pass = pass + al[ random.Next(0, 61)];
            }
            return pass;
        }
        private void AddDoctorButton_Click(object sender, RoutedEventArgs e)
        {
            if (Class1.isEdit == 0)
            {
                string p = generatePassword("");
                Doctor doctor = new Doctor(NameText.Text, SurnameText.Text, PatronomicText.Text, jobTitleText.Text, LoginText.Text, p);
                AC.Doctors.Add(doctor);
                AC.SaveChanges();
                MessageBox.Show("Успешно!\nПароль для входа врача - " + p + "\nЗапомните его.", "MedClean");
                Clipboard.SetText(p);
                Class1.isOpenWin = 0;
                this.Close();
            }
            else {
                var r = AC.Doctors.Where(x => x.login == Class1.savepacient).FirstOrDefault();
                r.surname = SurnameText.Text;
                r.name = NameText.Text;
                r.patronomic= PatronomicText.Text;
                r.login = LoginText.Text;
                r.jobTitle = jobTitleText.Text;
                AC.SaveChanges();
                Class1.isOpenWin = 0;
                Class1.isEdit = 0;
                Class1.savepacient = "";
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Class1.isOpenWin = 0;
            Class1.isEdit = 0;
            Class1.savepacient = "";
        }
    }
}
