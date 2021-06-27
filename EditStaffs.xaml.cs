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
    /// Логика взаимодействия для EditStaff.xaml
    /// </summary>
    public partial class EditStaff : Window
    {
       Staffs myStaff { get; set; }
        
        public EditStaff(Staffs staffs)
        {
            
            InitializeComponent();
            myStaff = staffs;
            

            
        }

        private void GoBack()
        {
            
            List<ComboBox> comboBoxes = new List<ComboBox>();
            foreach(var cb in comboBoxes)
            {
                cb.SelectedItem = cb.SelectedItem;
            }    

            List<TextBox> textBoxes = new List<TextBox>();
            foreach (var tb in textBoxes)
            {
                tb.Text = tb.Text;
            }
            MessageBox.Show("Переход к главной форме. Изменения отменены","Notification",MessageBoxButton.OK,MessageBoxImage.Information);
            ManageStaffs manageStaffs = new ManageStaffs();
            manageStaffs.Show();
            this.Close();
        }

        private void TbSurname_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TbSurname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TbName_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TbName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TbMiddleName_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TbMiddleName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TbLogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void PassBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Address_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Переход к главной форме. Нажата кнопка назад", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            ManageStaffs manageStaffs = new ManageStaffs();
            manageStaffs.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CbDepartment.ItemsSource = DB.db.Departments.ToList();
            CbEducation.ItemsSource = DB.db.Educations.ToList();
            CbPosition.ItemsSource = DB.db.Positions.ToList();
            
            CbSex.ItemsSource = DB.db.Sexes.ToList();

            if (myStaff.StaffId != 0)
            {
                TbSurname.Text = myStaff.Fam;
                TbName.Text = myStaff.Name;
                TbMiddleName.Text = myStaff.MiddlleName;
                DpEmployment.SelectedDate = myStaff.DateOfEmployment;
                DpDateOfBirth.SelectedDate = myStaff.DateOfBearth;
                
               CbSex.SelectedItem = myStaff.Sexes;
                Address.Text = myStaff.Address;
               CbEducation.SelectedItem = myStaff.Educations.education;
                CbDepartment.SelectedItem = myStaff.Departments.department;
                TbRoom.Text = myStaff.Room;
                TbWorkExperience.Text = myStaff.WorkExperience;
                TbPhone.Text = myStaff.NumberPhone;
                

            }
            else
            {
                MessageBox.Show("Сотрудник не найден","Warning",MessageBoxButton.OK,MessageBoxImage.Error);
            }


        }

        private void ChangeStaff_Click(object sender, RoutedEventArgs e)
        {
            bool isSaved = true;
            myStaff.Fam = TbSurname.Text;
            myStaff.Name = TbName.Text;
            myStaff.MiddlleName = TbMiddleName.Text;
            myStaff.DateOfEmployment = (DateTime)DpEmployment.SelectedDate;
            myStaff.DateOfBearth = (DateTime)DpDateOfBirth.SelectedDate;
            myStaff.Sexes = CbSex.SelectedItem as Sexes;
            myStaff.Address = Address.Text;
            myStaff.Educations = CbEducation.SelectedItem as Educations;
            myStaff.Departments = CbDepartment.SelectedItem as Departments;
            myStaff.Positions = CbPosition.SelectedItem as Positions;
            myStaff.Room = TbRoom.Text;
            myStaff.WorkExperience = TbWorkExperience.Text;
            myStaff.NumberPhone = TbPhone.Text;
            try
            {
                DB.db.SaveChanges();
            }
            catch 
            {
                isSaved = false;
                if (isSaved == false)
                    MessageBox.Show("Изменения не были сохранены", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    isSaved = true;
                    MessageBox.Show("Изменения успешно сохранены", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            
        }

        private void TbRoom_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TbWorkExperience_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
