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
using System.Data.Entity;

namespace PersonalDepartmentDegtyannikovIN3802
{

    /// <summary>
    /// Логика взаимодействия для ManageStaffs.xaml
    /// </summary>
    public partial class ManageStaffs : Window
    {
       // private Staffs
        
       // ObservableCollection<Staffs> ListStaffs = new ObservableCollection<Staffs>();
       // ObservableCollection<Positions> ListPositions = new ObservableCollection<Positions>();

        public static PersonalDepartmentDB PersonalDepartmentDB { get; set; }
        ObservableCollection<Positions> ListPositions;
        ObservableCollection<Staffs> ListStaffs;
        

        public ManageStaffs()
        {
            InitializeComponent();

            PersonalDepartmentDB = new PersonalDepartmentDB();
            ListPositions = new ObservableCollection<Positions>();
            ListStaffs = new ObservableCollection<Staffs>();
            searchText.Visibility = Visibility.Hidden;
            TextBlockSurname.Visibility = Visibility.Hidden;
            TextBlockPosititon.Visibility = Visibility.Hidden;
            TextBoxSurname.Visibility = Visibility.Hidden;
            ComboBoxPosition.Visibility = Visibility.Hidden;
            ButtonFindSurname.Visibility = Visibility.Hidden;
            ButtonFindPosition.Visibility = Visibility.Hidden;
            GridEmployee.ItemsSource = DB.db.Staffs.ToList();


        }

        public AddStaff AddStaff
        {
            get => default;
            set
            {
            }
        }

        public ManagePosition ManagePosition
        {
            get => default;
            set
            {
            }
        }

        public ManageEducation ManageEducation
        {
            get => default;
            set
            {
            }
        }

        public ManageDepartment ManageDepartment
        {
            get => default;
            set
            {
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // GetStaffs();
            //GridEmployee.Items.Refresh();
            //GridEmployee.SelectedIndex = 0;
            DB.db.Departments.Load();
            DB.db.Educations.Load();
            DB.db.Positions.Load();
            DB.db.Roles.Load();
            DB.db.Sexes.Load();
            GridEmployee.ItemsSource = DB.db.Staffs.ToList();
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


        private void DelStaff()
        {
            Staffs staffs = GridEmployee.SelectedItem as Staffs;

            if (staffs != null && GridEmployee.CurrentColumn != null)
            {
                var index = GridEmployee.CurrentColumn.DisplayIndex;
                var result = MessageBox.Show("Удалить сотрудника: " + staffs.Fam.Trim() + " " + staffs.Name.Trim() + " " + staffs.MiddlleName.Trim(), "Warning", MessageBoxButtons.OKCancel);



                if (result == System.Windows.Forms.DialogResult.OK)
                {
                   
                    
                        DB.db.Staffs.Remove(staffs);
                        RewriteStaffs();
                        ListStaffs.Remove(staffs);
                        DB.db.SaveChanges();





                    //GridEmployee.SelectedIndex = GridEmployee.SelectedIndex == 0 ? 1 : GridEmployee.SelectedIndex - 1;

                }
                if(result == System.Windows.Forms.DialogResult.Cancel)
                {
                    MessageBox.Show("Отмена удаления сотрудника: " + staffs.Fam.Trim() + " " + staffs.Name.Trim() + " " + staffs.MiddlleName.Trim(), "Warning", MessageBoxButtons.OKCancel);
                }

            }
            else
            {
                MessageBox.Show("Выберите строку для удаления");
            }
        }



        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            DelStaff();


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

        private void GetPositions()
        {
            var employess = DB.db.Positions;
            var query = from pos in employess
                        orderby pos.position
                        select pos;
            foreach (Positions pos in query)
            {
                ListPositions.Add(pos);
                
            }
            GridEmployee.ItemsSource = ListPositions;
            //GridEmployee.Items.Refresh();
            
        }


        private void RewriteStaffs()
        {
            ListStaffs.Clear();
            ListPositions.Clear();
            GetPositions();
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
            ManagePosition managePosition = new ManagePosition(null);
            managePosition.Show();
            ComboBoxPosition.ItemsSource = ListPositions;
            GridEmployee.Items.Refresh();
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
           // GetPositions();
            //ListPositions.Clear();
            //GetPositions();
            
            GridEmployee.IsReadOnly = false;
            GridEmployee.Items.Refresh();
            DB.db.SaveChanges();
            //System.Windows.Forms.Application.Restart();
            
            //ManageStaffs manageStaffs = new ManageStaffs();
            //LoginForm loginForm = new LoginForm();
            //manageStaffs.Close();
            //System.Windows.Forms.Application.Restart();
            //this.Hide();
            //System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            //Application.Current.Shutdown();
            //ListPositions.Clear();
            //RewriteStaffs();
            //DB.db.SaveChanges();
            //var employess = DB.db.Positions;
            //var query = from pos in employess
            //            orderby pos.position
            //            select pos;
            //foreach (Positions pos in query)
            //{
            //    ListPositions.Add(pos);
            //    ComboBoxPosition.Items.Add(pos);
            //}
            //GridEmployee.ItemsSource = ListPositions;
            //ComboBoxPosition.Items.Add(pos);



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

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            
            Staffs staffs = GridEmployee.SelectedItem as Staffs;
            EditStaff editStaff = new EditStaff(staffs);
            this.Close();
            editStaff.Show();
            
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            DelStaff();
            //Staffs staffs = GridEmployee.SelectedItem as Staffs;
            //DB.db.Staffs.Remove(staffs);
            //DB.db.SaveChanges();
            //Window_Loaded(sender, e);
        }
    }


}

