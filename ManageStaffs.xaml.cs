using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;

namespace PersonalDepartmentDegtyannikovIN3802
{

    /// <summary>
    /// Логика взаимодействия для ManageStaffs.xaml
    /// </summary>
    public partial class ManageStaffs : Window
    {
        

        ObservableCollection<Staffs> ListStaffs = new ObservableCollection<Staffs>();
        

        public ManageStaffs()
        {
            InitializeComponent();
            Roles roles = new Roles();
            if(roles.RoleId == 2)
            {
                addBtn.Visibility = Visibility.Hidden;
                delBtn.Visibility = Visibility.Hidden;
                editBtn.Visibility = Visibility.Hidden;
                BtnPosition.Visibility = Visibility.Hidden;
                BtnEducation.Visibility = Visibility.Hidden;
                BtnDepartment.Visibility = Visibility.Hidden;
                System.Windows.Controls.MenuItem menuItem = new System.Windows.Controls.MenuItem();
                menuItem.IsEnabled = false;
                menuItem.Visibility = Visibility.Hidden;
            }

            searchText.Visibility = Visibility.Hidden;
            TextBlockSurname.Visibility = Visibility.Hidden;
            TextBlockPosititon.Visibility = Visibility.Hidden;
            TextBoxSurname.Visibility = Visibility.Hidden;
            ComboBoxPosition.Visibility = Visibility.Hidden;
            ButtonFindSurname.Visibility = Visibility.Hidden;
            ButtonFindPosition.Visibility = Visibility.Hidden;

            if(roles.Role == "user")
            {
                addBtn.Visibility = Visibility.Hidden;
                addBtn.IsEnabled = false;
                delBtn.Visibility = Visibility.Hidden;
                editBtn.Visibility = Visibility.Hidden;
                BtnPosition.Visibility = Visibility.Hidden;
                BtnEducation.Visibility = Visibility.Hidden;
                BtnDepartment.Visibility = Visibility.Hidden;
                System.Windows.Controls.MenuItem menuItem = new System.Windows.Controls.MenuItem();
                menuItem.IsEnabled = false;
                menuItem.Visibility = Visibility.Hidden;
            }

        }
        
        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetStaffs();
            GridEmployee.SelectedIndex = 0;
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            var index = -1;
            if (GridEmployee.CurrentColumn != null)
            {
                index = GridEmployee.CurrentColumn.DisplayIndex;
                GridEmployee.IsReadOnly = false;
                
                GridEmployee.BeginEdit();
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования");
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            AddStaff addStaff = new AddStaff();
            this.Close();
            addStaff.Show();
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {

                Staffs staffs = GridEmployee.SelectedItem as Staffs;

                    if (staffs != null && GridEmployee.CurrentColumn != null)
                    {
                    var index = GridEmployee.CurrentColumn.DisplayIndex;
                    var result = MessageBox.Show("Удалить сотрудника: " + staffs.Fam.Trim() + " " + staffs.Name.Trim() + " " + staffs.MiddlleName.Trim(), "Warning", MessageBoxButtons.OKCancel);



                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            DB.db.Staffs.Remove(staffs);
                            GridEmployee.SelectedIndex = GridEmployee.SelectedIndex == 0 ? 1 : GridEmployee.SelectedIndex - 1;
                            ListStaffs.Remove(staffs);
                            DB.db.SaveChanges();
                        }

                    }
                else
                {
                    MessageBox.Show("Выберите строку для удаления");
                }
        }
        

        private void GetStaffs()
        {
            var employess = DB.db.Staffs;
            var query = from empl in employess
                        orderby empl.Fam
                        select empl;
            foreach (Staffs emp in query)
            {
                ListStaffs.Add(emp);
            }
            GridEmployee.ItemsSource = ListStaffs;
        }

        private void RewriteStaffs()
        {
            ListStaffs.Clear();
            GetStaffs();
        }

        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {
                RewriteStaffs();
                GridEmployee.IsReadOnly = true;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
                DB.db.SaveChanges();
                RewriteStaffs();
                GridEmployee.IsReadOnly = true;   
        }

        private void ButtonFindSurname_Click(object sender, RoutedEventArgs e)
        {
            string surename = TextBoxSurname.Text;
            ListStaffs.Clear();
            var staff = DB.db.Staffs;
            var query = from item in staff
                        where item.Fam == surename
                        select item;
            foreach(Staffs staffs in query)
            {
                ListStaffs.Add(staffs);
            }
            if(ListStaffs.Count > 0)
            {
                GridEmployee.ItemsSource = ListStaffs;
                ButtonFindSurname.IsEnabled = true;
                ButtonFindPosition.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Сотрудник с фамилией \n" + surename + "\n не найден",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        

        private void TextBoxSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonFindSurname.IsEnabled = true;
            ButtonFindPosition.IsEnabled = true;
            ComboBoxPosition.SelectedIndex = -1;
        }

        

        private void ButtonFindPosition_Click(object sender, RoutedEventArgs e)
        {
            ListStaffs.Clear();
            
            Positions positions = ComboBoxPosition.SelectedItem as Positions;
            var position = DB.db.Staffs;
            var query = from item in position
                        where item.PositionId == positions.PositionId
                        orderby item.Fam
                        select item;
            foreach (Staffs pos in query)
            {
                ListStaffs.Add(pos);
            }
            GridEmployee.ItemsSource = ListStaffs;

            if(ListStaffs.Count > 0)
            {
                GridEmployee.ItemsSource = ListStaffs;
               
            }
            else
            {
                MessageBox.Show("Сотрудник с должностью \n" + positions + "\n не найден", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ComboBoxPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonFindPosition.IsEnabled = true;
            ButtonFindSurname.IsEnabled = false;
            TextBoxSurname.Text = "";
        }

         

        private void BtnPosition_Click(object sender, RoutedEventArgs e)
        {
            ManagePosition managePosition = new ManagePosition();
            managePosition.Show();
        }

        private void BtnEducation_Click(object sender, RoutedEventArgs e)
        {
            ManageEducation manageEducation = new ManageEducation();
            manageEducation.Show();
        }

        private void BtnDepartment_Click(object sender, RoutedEventArgs e)
        {
            ManageDepartment manageDepartment = new ManageDepartment();
            manageDepartment.Show();
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            RewriteStaffs();
            GridEmployee.IsReadOnly = false;
           
        }

        private void findBtn_Click(object sender, RoutedEventArgs e)
        {
            searchText.Visibility = Visibility.Visible;
            TextBlockSurname.Visibility = Visibility.Visible;
            TextBlockPosititon.Visibility = Visibility.Visible;
            TextBoxSurname.Visibility = Visibility.Visible;
            ComboBoxPosition.Visibility = Visibility.Visible;
            ButtonFindSurname.Visibility = Visibility.Visible;
            ButtonFindPosition.Visibility = Visibility.Visible;
        }
    }


}

