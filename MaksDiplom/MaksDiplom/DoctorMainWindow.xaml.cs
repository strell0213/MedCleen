using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
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
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;



namespace MaksDiplom
{
    /// <summary>
    /// Логика взаимодействия для DoctorMainWindow.xaml
    /// </summary>
    public partial class DoctorMainWindow : System.Windows.Window
    {
        AppContext AC;
        
        public DoctorMainWindow()
        {
            InitializeComponent();
            AC = new AppContext();
            var r = AC.Doctors.Where(x => x.login == Class1.SaveLogin).FirstOrDefault();
            HelloText.Content = "Здравствуйте, "+r.surname+" "+r.name+" "+r.patronomic+"\n"+r.jobTitle;
            //меняет фон кнопок
            InfoButton.Background = System.Windows.Media.Brushes.White;
            InfoButton.Foreground = System.Windows.Media.Brushes.Black;
            //меняет цвет текста кнопок
            AddVisitButton.Background = System.Windows.Media.Brushes.White;
            AddVisitButton.Foreground = System.Windows.Media.Brushes.Black;
            ProfileButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF4A79FE");
            ProfileButton.Foreground = System.Windows.Media.Brushes.White;
            //закрываются окна которые не нужны(hidden) и открываются которые нужны(Visible)
            MainGrid.Visibility = Visibility.Visible;
            VisitGrid.Visibility = Visibility.Hidden;
            InfoGrid.Visibility = Visibility.Hidden;
            

        }
        
        public void updateVisitList() {
            var m = AC.Doctors.Where(x => x.login == Class1.SaveLogin).FirstOrDefault();
            var r = AC.VisitLists.Where(x => x.DoctorID == m.DoctorID).ToList();
            VisitList.ItemsSource = r.Select(x => "Прием №" + x.VLID + "\nПациент: " + x.PacientID + "\nДата и время: " + x.date + " " + x.time + "\nКабинет: " + x.Cabinet + "\nПримечания: " + x.discriptions).ToList(); ;
        }
        private void AddVisitButton_Click(object sender, RoutedEventArgs e)
        {
            var r1 = AC.Pacients.Select(x => x.policyNumber).ToList();
            PolicyText.ItemsSource = r1;

            for (int i = 8; i <= 24; i++)
            {
                HourBox.Items.Add(i.ToString());
            }
            for (int i = 0; i <= 50; i++)
            {
                if (i % 10 == 0)
                {
                    MinuteBox.Items.Add(i.ToString());
                }
            }
            //меняет фон кнопок
            InfoButton.Background = System.Windows.Media.Brushes.White;
            InfoButton.Foreground = System.Windows.Media.Brushes.Black;
            //меняет цвет текста кнопок
            AddVisitButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF4A79FE");
            AddVisitButton.Foreground = System.Windows.Media.Brushes.White;
            ProfileButton.Background = System.Windows.Media.Brushes.White;
            ProfileButton.Foreground = System.Windows.Media.Brushes.Black;
            //закрываются окна которые не нужны(hidden) и открываются которые нужны(Visible)
            MainGrid.Visibility = Visibility.Hidden;
            VisitGrid.Visibility = Visibility.Visible;
            InfoGrid.Visibility = Visibility.Hidden;
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            updateVisitList();
            //меняет фон кнопок
            InfoButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF4A79FE");
            InfoButton.Foreground = System.Windows.Media.Brushes.White;
            //меняет цвет текста кнопок
            AddVisitButton.Background = System.Windows.Media.Brushes.White;
            AddVisitButton.Foreground = System.Windows.Media.Brushes.Black;
            ProfileButton.Background = System.Windows.Media.Brushes.White;
            ProfileButton.Foreground = System.Windows.Media.Brushes.Black;
            //закрываются окна которые не нужны(hidden) и открываются которые нужны(Visible)
            MainGrid.Visibility = Visibility.Hidden;
            VisitGrid.Visibility = Visibility.Hidden;
            InfoGrid.Visibility = Visibility.Visible;
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            var r = AC.Doctors.Where(x => x.login == Class1.SaveLogin).FirstOrDefault();
            HelloText.Content = "Здравствуйте, " + r.surname + " " + r.name + " " + r.patronomic + "\n" + r.jobTitle;
            //меняет фон кнопок
            InfoButton.Background = System.Windows.Media.Brushes.White;
            InfoButton.Foreground = System.Windows.Media.Brushes.Black;
            //меняет цвет текста кнопок
            AddVisitButton.Background = System.Windows.Media.Brushes.White;
            AddVisitButton.Foreground = System.Windows.Media.Brushes.Black;
            ProfileButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF4A79FE");
            ProfileButton.Foreground = System.Windows.Media.Brushes.White;
            //закрываются окна которые не нужны(hidden) и открываются которые нужны(Visible)
            MainGrid.Visibility = Visibility.Visible;
            VisitGrid.Visibility = Visibility.Hidden;
            InfoGrid.Visibility = Visibility.Hidden;
        }

       

        private void AddVisitButtonOnDb_Click(object sender, RoutedEventArgs e)
        {
            if (Class1.ForEditVisit == 0)
            {
                var r = AC.Doctors.Where(x => x.login == Class1.SaveLogin).FirstOrDefault();
                var r1 = AC.Pacients.Where(x => x.policyNumber == PolicyText.SelectedItem.ToString()).FirstOrDefault();
                string time = HourBox.SelectedItem.ToString() + ":" + MinuteBox.SelectedItem.ToString();
                VisitList visitList = new VisitList(r1.PacientID, r.DoctorID, Convert.ToInt32(CabinetText.Text), DateVisitPicker.Text, time, DisText.Text);
                AC.VisitLists.Add(visitList);
                AC.SaveChanges();
            }
            else if (Class1.ForEditVisit == 1)
            {
                var r2 = AC.VisitLists.Where(c => c.VLID == Class1.saveVisit).FirstOrDefault();
                var r = AC.Doctors.Where(x => x.login == Class1.SaveLogin).FirstOrDefault();
                var r1 = AC.Pacients.Where(x => x.policyNumber == PolicyText.SelectedItem.ToString()).FirstOrDefault();
                string time = HourBox.SelectedItem.ToString() + ":" + MinuteBox.SelectedItem.ToString();
                r2.PacientID = r1.PacientID;
                r2.DoctorID = r.DoctorID;
                r2.date = DateVisitPicker.Text;
                r2.time = time;
                r2.Cabinet = Convert.ToInt32(CabinetText.Text);
                r2.discriptions = DisText.Text;
                AC.SaveChanges();
                updateVisitList();
                //меняет фон кнопок
                InfoButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF4A79FE");
                InfoButton.Foreground = System.Windows.Media.Brushes.White;
                //меняет цвет текста кнопок
                AddVisitButton.Background = System.Windows.Media.Brushes.White;
                AddVisitButton.Foreground = System.Windows.Media.Brushes.Black;
                ProfileButton.Background = System.Windows.Media.Brushes.White;
                ProfileButton.Foreground = System.Windows.Media.Brushes.Black;
                //закрываются окна которые не нужны(hidden) и открываются которые нужны(Visible)
                MainGrid.Visibility = Visibility.Hidden;
                VisitGrid.Visibility = Visibility.Hidden;
                InfoGrid.Visibility = Visibility.Visible;
            }
        }

        private void DeleteListButton_Click(object sender, RoutedEventArgs e)
        {
            var r = AC.VisitLists.Where(x => VisitList.SelectedItem.ToString().Contains("Прием №" + x.VLID)).FirstOrDefault();
            if (r != null) {
                AC.VisitLists.Remove(r);
                AC.SaveChanges();
                updateVisitList();
            }
        }

        private void EditListButton_Click(object sender, RoutedEventArgs e)
        {
            var r = AC.VisitLists.Where(x => VisitList.SelectedItem.ToString().Contains("Прием №" + x.VLID)).FirstOrDefault();
            if (r != null)
            {
                Class1.saveVisit = r.VLID;
                Class1.ForEditVisit = 1;
                var r1 = AC.Pacients.Select(x => x.policyNumber).ToList();
                PolicyText.ItemsSource = r1;

                for (int i = 8; i <= 24; i++)
                {
                    HourBox.Items.Add(i.ToString());
                }
                for (int i = 0; i <= 50; i++)
                {
                    if (i % 10 == 0)
                    {
                        MinuteBox.Items.Add(i.ToString());
                    }
                }
                //меняет фон кнопок
                InfoButton.Background = System.Windows.Media.Brushes.White;
                InfoButton.Foreground = System.Windows.Media.Brushes.Black;
                //меняет цвет текста кнопок
                AddVisitButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF4A79FE");
                AddVisitButton.Foreground = System.Windows.Media.Brushes.White;
                ProfileButton.Background = System.Windows.Media.Brushes.White;
                ProfileButton.Foreground = System.Windows.Media.Brushes.Black;
                //закрываются окна которые не нужны(hidden) и открываются которые нужны(Visible)
                MainGrid.Visibility = Visibility.Hidden;
                VisitGrid.Visibility = Visibility.Visible;
                InfoGrid.Visibility = Visibility.Hidden;
                AddVisitButtonOnDb.Content = "Изменить";
            }
        }

        private void DeleteReg_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditReg_Click(object sender, RoutedEventArgs e)
        {
            if (EditPassGrid.Visibility == Visibility.Hidden)
            {
                EditPassGrid.Visibility = Visibility.Visible;
                ReceptPacGrid.Visibility = Visibility.Hidden;
                WritePacGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                EditPassGrid.Visibility = Visibility.Hidden;
            }
        }

        private void ReceptReg_Click(object sender, RoutedEventArgs e)
        {
            if (ReceptPacGrid.Visibility == Visibility.Hidden)
            {
                ReceptPacGrid.Visibility = Visibility.Visible;
                var r1 = AC.Pacients.Select(x => x.policyNumber).ToList();
                ReceptPacGrid_PacBox.ItemsSource = r1;
                EditPassGrid.Visibility = Visibility.Hidden;
                WritePacGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                ReceptPacGrid.Visibility = Visibility.Hidden;
            }
        }

        private void WriteReg_Click(object sender, RoutedEventArgs e)
        {
            
            if (WritePacGrid.Visibility == Visibility.Hidden)
            {
                WritePacGrid.Visibility = Visibility.Visible;
                var r1 = AC.Pacients.Select(x => x.policyNumber).ToList();
                WritePacGrid_PacBox.ItemsSource = r1;
                ReceptPacGrid.Visibility = Visibility.Hidden;
                EditPassGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                WritePacGrid.Visibility = Visibility.Hidden;
            }

        }

        private void EditPasswordReg_1_Click(object sender, RoutedEventArgs e)
        {
            var r = AC.Doctors.Where(c => c.login == Class1.SaveLogin).FirstOrDefault();
            if (r.password == OldPassText.Password)
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

        private void WritePac_1_Click(object sender, RoutedEventArgs e)
        {
            var r = AC.Doctors.Where(x => x.login == Class1.SaveLogin).FirstOrDefault();
            var r1 = AC.Pacients.Where(x => x.policyNumber == WritePacGrid_PacBox.SelectedItem.ToString()).FirstOrDefault();
            Word.Application wrdApp;
            Word._Document wrdDoc;
            Word.Selection wrdSelection;
            Object oMissing = System.Reflection.Missing.Value;
            Object oFalse = false;
            string strtoAdd;

            wrdApp = new Word.Application();
            wrdApp.Visible = true;
            strtoAdd = "Выписка\nФИО: " + r1.surname + " " + r1.name + " " + r1.patronomic + "\n" + WritePacGrid_DisText.Text + "\n\nВрач: " + r.surname + " " + r.name + " " + r.patronomic + "\nПодпись__________________\n\n\n\nMedCleen";
            wrdDoc = wrdApp.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            wrdDoc.Select();

            wrdSelection = wrdApp.Selection;
            wrdSelection.ParagraphFormat.SpaceAfter = 0;
            wrdSelection.ParagraphFormat.Alignment =
                Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wrdSelection.Font.Bold = 1;
            wrdSelection.Font.Size = 16;
            wrdSelection.Font.Name = "Times New Roman";
            wrdSelection.ParagraphFormat.LineSpacing = 11;
            wrdSelection.ParagraphFormat.LineUnitBefore = 0;
            wrdSelection.TypeText(strtoAdd);

            wrdDoc.Saved = true;

            object fileName = AppDomain.CurrentDomain.BaseDirectory +r1.surname+"_"+r1.name+ "_" + r1.patronomic+".doc";
            wrdDoc.SaveAs2(ref fileName, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            wrdDoc.Close(ref oFalse, ref oMissing, ref oMissing);
            wrdApp.Quit(ref oFalse, ref oMissing, ref oMissing);

            wrdDoc = null;
            wrdApp = null;
            MessageBox.Show("Документ сохранен: "+ AppDomain.CurrentDomain.BaseDirectory);
        }

        private void ReceptPac_1_Click(object sender, RoutedEventArgs e)
        {
            var r = AC.Doctors.Where(x => x.login == Class1.SaveLogin).FirstOrDefault();
            var r1 = AC.Pacients.Where(x => x.policyNumber == ReceptPacGrid_PacBox.SelectedItem.ToString()).FirstOrDefault();
            Word.Application wrdApp;
            Word._Document wrdDoc;
            Word.Selection wrdSelection;
            Object oMissing = System.Reflection.Missing.Value;
            Object oFalse = false;
            string strtoAdd;

            wrdApp = new Word.Application();
            wrdApp.Visible = true;
            strtoAdd = "Рецепт\n\nДля " + r1.surname + " " + r1.name + " " + r1.patronomic + "\nПрепарат: " + ReceptPacGrid_PreparatText.Text + "\n"+ReceptPacGrid_DisText.Text+"\n\nВрач: " + r.surname + " " + r.name + " " + r.patronomic + "\nПодпись__________________\n\n\n\nMedCleen";
            wrdDoc = wrdApp.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            wrdDoc.Select();

            wrdSelection = wrdApp.Selection;
            wrdSelection.ParagraphFormat.SpaceAfter = 0;
            wrdSelection.ParagraphFormat.Alignment =
                Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wrdSelection.Font.Bold = 1;
            wrdSelection.Font.Size = 16;
            wrdSelection.Font.Name = "Times New Roman";
            wrdSelection.ParagraphFormat.LineSpacing = 11;
            wrdSelection.ParagraphFormat.LineUnitBefore = 0;
            wrdSelection.TypeText(strtoAdd);

            wrdDoc.Saved = true;

            object fileName = AppDomain.CurrentDomain.BaseDirectory + r1.surname + "_" + r1.name + "_" + r1.patronomic + "_Рецепт.doc";
            wrdDoc.SaveAs2(ref fileName, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            wrdDoc.Close(ref oFalse, ref oMissing, ref oMissing);
            wrdApp.Quit(ref oFalse, ref oMissing, ref oMissing);

            wrdDoc = null;
            wrdApp = null;
            MessageBox.Show("Документ сохранен: " + AppDomain.CurrentDomain.BaseDirectory);
        }

        private void CabinetText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox box = (TextBox)sender;
            e.Handled = box.Text.Length >= 3;
            if (!Char.IsDigit(e.Text, 0))
                e.Handled = true;
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
    }
}
