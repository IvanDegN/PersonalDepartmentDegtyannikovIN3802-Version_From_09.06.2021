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
        private bool isDirty = true;

        ObservableCollection<Staffs> ListStaffs = new ObservableCollection<Staffs>();

        public ManageStaffs()
        {
            InitializeComponent();

        }

        private void UndoCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Отмена");
            isDirty = false;
            
        }

        private void UndoCommandBinding_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void NewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void NewCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
           
        }

        private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !isDirty;
        }

        private void FindCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Найти");
            isDirty = true;
        }

        private void FindCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }



        private void DeleteCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }


        private void DeleteCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

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
                isDirty = false;
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
            if (GridEmployee.SelectedItem == GridEmployee.SelectedItem)
            {
                MessageBox.Show("Изменений не было");
                UndoBtn.IsEnabled = false;
            }
            else
            {
                RewriteStaffs();
                GridEmployee.IsReadOnly = true;
                isDirty = true;
            }
            
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (GridEmployee.SelectedItem == GridEmployee.SelectedItem)
            {
                MessageBox.Show("Изменений не было");
                saveBtn.IsEnabled = false;
            }
            else
            {
                DB.db.SaveChanges();
                isDirty = true;
                GridEmployee.IsReadOnly = true;
            }
        }
    }


}

