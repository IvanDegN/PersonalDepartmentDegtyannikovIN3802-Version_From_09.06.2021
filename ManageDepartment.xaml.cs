using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для ManageDepartment.xaml
    /// </summary>
    public partial class ManageDepartment : Window
    {
        ObservableCollection<Departments> ListDepartments = new ObservableCollection<Departments>();

        public ManageDepartment()
        {
            InitializeComponent();
            DB.db.Departments.Load();
            gridDepartment.CanUserAddRows = false;
            gridDepartment.CanUserDeleteRows = false;
            gridDepartment.CanUserSortColumns = false;
            gridDepartment.SelectionMode = DataGridSelectionMode.Extended;
            gridDepartment.SelectionUnit = DataGridSelectionUnit.FullRow;
            gridDepartment.AutoGenerateColumns = false;
            gridDepartment.IsReadOnly = true;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (gridDepartment.SelectedItem != null)
            {
                gridDepartment.IsReadOnly = false;
                gridDepartment.BeginEdit();
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования");
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Departments departments = new Departments();
            departments.department = TbDepartment.Text;
            DB.db.Departments.Add(departments);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Departments departments = gridDepartment.SelectedItem as Departments;

            if (departments != null && gridDepartment.Columns != null)
            {
                var result = MessageBox.Show("Удалить отдел: " + departments, "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    DB.db.Departments.Remove(departments);
                    gridDepartment.SelectedIndex = gridDepartment.SelectedIndex == 0 ? 1 : gridDepartment.SelectedIndex - 1;
                    ListDepartments.Remove(departments);
                    try
                    {
                        DB.db.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно удалить запись, так как она связана с другой таблицей", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                        refresh();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления");
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            gridDepartment.CanUserAddRows = false;
            DB.db.SaveChanges();
            refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var departments = DB.db.Departments;
            var query = from item in departments select item;
            foreach (Departments dep in query)
            {
                ListDepartments.Add(dep);
            }
            gridDepartment.ItemsSource = ListDepartments;
        }

        private void refresh()
        {
            ListDepartments.Clear();
            var departments = DB.db.Departments;
            var query = from item in departments select item;
            foreach (Departments dep in query)
            {
                ListDepartments.Add(dep);
            }
            gridDepartment.ItemsSource = ListDepartments.ToBindingList();
        }
    }
}
