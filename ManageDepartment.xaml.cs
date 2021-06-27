using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
            ListDepartments.Add(departments);

            foreach (var item in DB.db.Departments)
            {
                if (item.department == departments.department)
                {
                    MessageBox.Show("Отдел " + departments + " уже существует", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                    DB.db.Departments.Remove(departments);
                    ListDepartments.Remove(departments);
                    break;
                }
            }
            if (String.IsNullOrWhiteSpace(TbDepartment.Text))
            {
                DB.db.Departments.Remove(departments);
                ListDepartments.Remove(departments);
                MessageBox.Show("Поле не должно быть пустым", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);

            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Departments departments = gridDepartment.SelectedItem as Departments;

            if (departments != null && gridDepartment.Columns != null)
            {
                bool isAccept = true;

                foreach (var elem in DB.db.Staffs)
                {

                    if (elem.DepartmentId == departments.DepartmentId)
                    {
                        isAccept = false;
                        MessageBox.Show("Невозможно удалить запись, так как она связана с другой таблицей", "Warning", MessageBoxButton.OKCancel);
                        refresh();
                        break;
                    }

                }
                if (isAccept)
                {

                    if (gridDepartment.CurrentCell != null)
                    {

                        MessageBox.Show("Удалить отдел: " + departments, "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.OK);
                        ListDepartments.Remove(departments);
                        DB.db.Departments.Remove(departments);
                        DB.db.SaveChanges();
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
            try
            {
                if (gridDepartment.Columns.Count > 0)
                {
                    DB.db.SaveChanges();
                    refresh();
                    MessageBox.Show("Данные успешно сохранены!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                }
                else
                {
                    MessageBox.Show("Добавьте данные", "Warning", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
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

        bool CheckNumbers(string numbers)
        {
            Regex regex = new Regex("[1234567890]");
            return regex.IsMatch(numbers);
        }



        

        private void TbDepartment_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
            e.Handled = CheckNumbers(e.Text);
            if (TbDepartment.Text.Length >= 30)
            {

                System.Windows.Forms.MessageBox.Show("Отдел не должен быть длиннее, чем 30 символов.", "Warning!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                TbDepartment.Text = TbDepartment.Text.Remove(TbDepartment.Text.Length - 1);
                TbDepartment.SelectionStart = TbDepartment.Text.Length;
            }
        }
    }

}
