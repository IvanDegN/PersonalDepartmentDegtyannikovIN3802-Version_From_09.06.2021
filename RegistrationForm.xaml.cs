using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PersonalDepartmentDegtyannikovIN3802
{
    /// <summary>
    /// Логика взаимодействия для RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationForm : Window
    {
        public void CheckNumbers(System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key < Key.A) || (e.Key > Key.Z))
                e.Handled = true;
        }


        bool CheckRusChars(string txt)
        {
                Regex regex = new Regex("[а-я А-Я]");
                return regex.IsMatch(txt);            
        }

        bool CheckEngChars(string txt)
        {
            Regex regex = new Regex("[a-z A-Z]");
            return regex.IsMatch(txt);
        }

        bool OnlyNumbersAndPlus(string txt)
        {
            Regex regex = new Regex("[^+1234567890]");
            return regex.IsMatch(txt);
        }



        public string FormattedPhoneNumber
        {
            get
            {
                if (TbPhone == null)
                    return string.Empty;

                switch (TbPhone.MaxLength)
                {
                    case 7:
                        return Regex.Replace(TbPhone.Text, @"(\d{3})(\d{4})", "$1-$2");
                    case 10:
                        return Regex.Replace(TbPhone.Text, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
                    case 11:
                        return Regex.Replace(TbPhone.Text, @"(\d{1})(\d{3})(\d{3})(\d{4})", "$1-$2-$3-$4");
                    default:
                        return TbPhone.Text;
                }
            }
        }






        LoginForm Form = new LoginForm();
        public RegistrationForm()
        {
            InitializeComponent();
        }

        

        private void BtnBackRegForm(object sender, RoutedEventArgs e)
        {
            
            this.Close();
            Form.Show();
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
            Form.Show();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Вы были успешно зарегистрированы!","Регистрация прошла успешно!",MessageBoxButton.OK);
            
        }

        private void TbSurname_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            CheckNumbers(e);
        }

        private void TbName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            CheckNumbers(e);
        }

       

        private void TbLogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckRusChars(e.Text);
        }


        private void MiddleName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            CheckNumbers(e);
        }

        private void PassBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckRusChars(e.Text);
        }

        private void Address_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckEngChars(e.Text);
        }

        private void TbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = OnlyNumbersAndPlus(e.Text);
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(TbLogin.Text) && !String.IsNullOrEmpty(PassBox.Password))
            {

                foreach (var usr in DB.db.Users)
                {
                    if (TbLogin.Text == usr.Login && PassBox.Password == usr.Password)
                    {

                        System.Windows.Forms.MessageBox.Show("This user is exits yet!", "Error!");
                       // reg = false;

                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("You are registered!", "UwU:3");
                        break;
                    }

                }

            }


           // if (reg)
           // {
               // String date = DpEmployment.GetValue(TopProperty).ToString();

                Users user = new Users
                {
                    Login = TbLogin.Text,
                    Password = PassBox.Password,
                   // Fur_Role = CbRoles.SelectedItem as Fur_Role,
                };

                Staffs staffs = new Staffs
                {
                    Fam = TbSurname.Text,
                    Name = TbName.Text,
                    MiddlleName = TbMiddleName.Text,
                    NumberPhone = TbPhone.Text,
                    Address = Address.Text,
                    DateOfEmployment = (DateTime)DpEmployment.SelectedDate,
                    DateOfBearth = (DateTime)DpDateOfBirth.SelectedDate,
                    Sexes = CbSex.SelectedItem as Sexes,
                    Departments = CbDepartment.SelectedItem as Departments,
                    Positions = CbPosition.SelectedItem as Positions,
                };

            

                DB.db.Staffs.Add(staffs);
                DB.db.SaveChanges();

                DB.db.Users.Add(user);
                DB.db.SaveChanges();





          //  }
            //else
           // {
             //   CheckRegistr(ref counter, charac);
               // MessageBox.Show("Error registration!", "Error!");
           // }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DB.db.Staffs.Load();
            DB.db.Users.Load();
            DB.db.Sexes.Load();
            DB.db.Positions.Load();
            DB.db.Departments.Load();
            
            CbSex.ItemsSource = DB.db.Sexes.ToList();
            CbPosition.ItemsSource = DB.db.Positions.ToList();
            CbDepartment.ItemsSource = DB.db.Departments.ToList();
            if(CbEducation.SelectedItem == null)
            {
                return;
            }
            CbEducation.ItemsSource = DB.db.Staffs.ToList();
        }
    }
}
