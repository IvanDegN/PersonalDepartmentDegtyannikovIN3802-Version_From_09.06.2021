using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для AddStaff.xaml
    /// </summary>
    public partial class AddStaff : Window
    {
        public AddStaff()
        {
            InitializeComponent();
        }

        private void BtnAddStaff_Click(object sender, RoutedEventArgs e)
        {

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

            CbSex.ItemsSource = DB.db.Sexes.ToList();
            CbPosition.ItemsSource = DB.db.Positions.ToList();
            CbDepartment.ItemsSource = DB.db.Departments.ToList();
            CbEducation.ItemsSource = DB.db.Educations.ToList();
        }

        private void PassBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TbLogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TbMiddleName_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TbMiddleName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TbName_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TbName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TbSurname_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TbSurname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Address_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TbRoom_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TbWorkExperience_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
