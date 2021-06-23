using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace PersonalDepartmentDegtyannikovIN3802
{
    /// <summary>
    /// Логика взаимодействия для LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        bool reg = true;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void ButnReg(object sender, RoutedEventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            this.Close();
            registrationForm.Show();
        }

        bool CheckEngChars(string txt)
        {
            Regex regex = new Regex("[^A-Z a-z 1234567890]");
            return regex.IsMatch(txt);
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
           
        try
        {
                bool login = false;
                int IdRole = 0;
                Roles roles = new Roles();
                foreach(var user in DB.db.Users)
                {
                    if(TbLogin.Text == user.Login && TbPassBox.Password == user.Password)
                    {
                        login = true;
                        IdRole = (int)user.RoleId;
                        roles = user.Roles;
                        break;
                    }
                }
        
            

            if(login)
            {
                    MessageBox.Show($"Вы успешно зашли под {roles}");
                    switch (IdRole)
                    {
                        case 1:
                            ManageStaffs manageStaffs = new ManageStaffs();
                            manageStaffs.Show();
                            break;

                        case 2:
                            ManageStaffs manageStaffss = new ManageStaffs();
                            manageStaffss.Show();
                            
                            break;
                    }
            }
        }
        catch
        {

        }

            if(TbLogin.Text == "")
            {
                TbLogin.Background = Brushes.Red;
                HintLabel.Visibility = Visibility.Visible;
            }
            else
            {
                TbLogin.Background = Brushes.White;
            }
            if(TbPassBox.Password == "")
            {
                TbPassBox.Background = Brushes.Red;
                HintLabel.Visibility = Visibility.Visible;
            }
            else
            {
                TbPassBox.Background = Brushes.White;
            }


            if(!String.IsNullOrEmpty(TbLogin.Text) && !String.IsNullOrEmpty(TbPassBox.Password))
            {
                foreach(var usr in DB.db.Users)
                {
                    if (TbLogin.Text == usr.Login && TbPassBox.Password == usr.Password)
                    {
                        reg = true;
                        System.Windows.Forms.MessageBox.Show("Вы успешно зашли в аккаунт", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MainWindow mainWindow = new MainWindow();
                        this.Close();
                        mainWindow.Show();
                        break;
                    }  
                    else
                    {
                        reg = false;
                        System.Windows.Forms.MessageBox.Show("Повторите ввод, данные неверны", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                        
                    
                }
            }
        }

        private void TbLogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckEngChars(e.Text);
        }

        private void TbPassBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckEngChars(e.Text);
        }
    }
}
