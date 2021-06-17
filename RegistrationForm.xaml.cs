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

namespace PersonalDepartmentDegtyannikovIN3802
{
    /// <summary>
    /// Логика взаимодействия для RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationForm : Window
    {
        public void CheckNumbers(KeyEventArgs e)
        {
            if ((e.Key < Key.A) || (e.Key > Key.Z))
                e.Handled = true;
        }


        public void CheckRusChars(KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '_' || e.KeyChar == (char)Keys.Back)
            {
            }
            else
            {
                e.Handled = true;
                
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
            MessageBox.Show("Вы были успешно зарегистрированы!","Регистрация прошла успешно!",MessageBoxButton.OK);
            
        }

        private void TbSurname_KeyDown(object sender, KeyEventArgs e)
        {
            CheckNumbers(e);
        }

        private void TbName_KeyDown(object sender, KeyEventArgs e)
        {
            CheckNumbers(e);
        }

        private void TbMiddleName_KeyDown(object sender, KeyEventArgs e)
        {
            CheckNumbers(e);
        }

        
    }
}
