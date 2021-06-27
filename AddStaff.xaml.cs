using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для AddStaff.xaml
    /// </summary>
    public partial class AddStaff : Window
    {

        bool reg = true;
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

        bool WorkExp(string txt)
        {
            Regex regex = new Regex("[^1234567890.]");
            return regex.IsMatch(txt);
        }

        bool OnlyNumbers(string num)
        {
            Regex regex = new Regex("[^1234567890]");
            return regex.IsMatch(num);
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

        public AddStaff()
        {
            InitializeComponent();
        }

        private void BtnAddStaff_Click(object sender, RoutedEventArgs e)
        {

            List<System.Windows.Controls.TextBox> textBoxes = new List<System.Windows.Controls.TextBox>() { TbLogin, TbMiddleName, TbName, TbPhone, TbSurname, Address, TbWorkExperience, TbRoom };

            foreach (System.Windows.Controls.TextBox tb in textBoxes)
            {
                if (String.IsNullOrEmpty(tb.Text))
                {
                    reg = false;
                    tb.Background = Brushes.Red;
                    LabelHint.Visibility = Visibility;

                }
                else
                {
                    reg = false;
                    tb.Background = Brushes.White;
                    LabelHint.Visibility = Visibility.Hidden;
                }


            }

            if (String.IsNullOrEmpty(PassBox.Password))
            {
                PassBox.Background = Brushes.Red;
                LabelHint.Visibility = Visibility;
            }
            else
            {
                PassBox.Background = Brushes.White;
                LabelHint.Visibility = Visibility.Hidden;
            }

            if (!String.IsNullOrEmpty(TbLogin.Text) && !String.IsNullOrEmpty(PassBox.Password))
            {

                foreach (var usr in DB.db.Users)
                {
                    if (TbLogin.Text == usr.Login && PassBox.Password == usr.Password)
                    {
                        reg = false;
                        System.Windows.Forms.MessageBox.Show("Этот пользователь уже существует", "Error!",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);


                    }
                    else
                    {
                        reg = true;
                        System.Windows.Forms.MessageBox.Show("Пользователь успешно добавлен", "Successful",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        break;
                    }

                }

            }



            if (DpEmployment.SelectedDate == null)
            {
                reg = false;
                System.Windows.Forms.MessageBox.Show("Выберите дату приёма на работу", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (DpDateOfBirth.SelectedDate == null)
            {
                reg = false;
                System.Windows.Forms.MessageBox.Show("Выберите дату рождения", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (CbDepartment.SelectedItem == null)
            {
                reg = false;
                System.Windows.Forms.MessageBox.Show("Выберите отдел из списка", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (CbEducation.SelectedItem == null)
            {
                reg = false;
                System.Windows.Forms.MessageBox.Show("Выберите образование из списка", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (CbPosition.SelectedItem == null)
            {
                reg = false;
                System.Windows.Forms.MessageBox.Show("Выберите должность из списка", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (CbSex.SelectedItem == null)
            {
                reg = false;
                System.Windows.Forms.MessageBox.Show("Выберите пол из списка", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (CbRoles.SelectedItem == null)
            {
                reg = false;
                System.Windows.Forms.MessageBox.Show("Выберите роль из списка", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



            if (reg)

            {
                Users user = new Users
                {
                    Login = TbLogin.Text,
                    Password = PassBox.Password,
                    Roles = CbRoles.SelectedItem as Roles
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
                    WorkExperience = TbWorkExperience.Text,
                    Room = TbRoom.Text,
                    Educations = CbEducation.SelectedItem as Educations

                };

                DB.db.Staffs.Add(staffs);           
                DB.db.Users.Add(user);
                DB.db.SaveChanges();
            }


        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ManageStaffs manageStaffs = new ManageStaffs();
            this.Close();
            manageStaffs.Show();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ManageStaffs manageStaffs = new ManageStaffs();
            this.Close();
            manageStaffs.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DB.db.Staffs.Load();
            DB.db.Users.Load();
            DB.db.Sexes.Load();
            DB.db.Positions.Load();
            DB.db.Departments.Load();
            DB.db.Educations.Load();
            DB.db.Roles.Load();

            CbSex.ItemsSource = DB.db.Sexes.ToList();
            CbPosition.ItemsSource = DB.db.Positions.ToList();
            CbDepartment.ItemsSource = DB.db.Departments.ToList();
            CbEducation.ItemsSource = DB.db.Educations.ToList();
            CbRoles.ItemsSource = DB.db.Roles.ToList();
        }

        private void PassBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckRusChars(e.Text);
        }

        private void TbLogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckRusChars(e.Text);
        }

        private void TbMiddleName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            CheckNumbers(e);
        }

        private void TbMiddleName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckEngChars(e.Text);
        }

        private void TbName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            CheckNumbers(e);
        }

        private void TbName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckEngChars(e.Text);
        }

        private void TbSurname_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            CheckNumbers(e);
        }

        private void TbSurname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckEngChars(e.Text);
        }

        private void Address_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckEngChars(e.Text);
        }

        private void TbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = OnlyNumbersAndPlus(e.Text);
            if (TbPhone.Text.Length >= 12)
            {
                reg = false;
                System.Windows.Forms.MessageBox.Show("Номер телефона должен быть длинной в 12 символов.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TbPhone.Text = TbPhone.Text.Remove(TbPhone.Text.Length - 1);
                TbPhone.SelectionStart = TbPhone.Text.Length;
            }
        }

        private void TbRoom_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = OnlyNumbers(e.Text);
        }

        private void TbWorkExperience_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = WorkExp(e.Text);
        }
    }
}
