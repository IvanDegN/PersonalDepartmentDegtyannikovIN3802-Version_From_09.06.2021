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
    /// Логика взаимодействия для ManageEducation.xaml
    /// </summary>
    public partial class ManageEducation : Window
    {

        ObservableCollection<Educations> ListEducation = new ObservableCollection<Educations>();

        public ManageEducation()
        {
            InitializeComponent();
            DB.db.Educations.Load();
            gridEducation.CanUserAddRows = false;
            gridEducation.CanUserDeleteRows = false;
            gridEducation.CanUserSortColumns = false;
            gridEducation.SelectionMode = DataGridSelectionMode.Extended;
            gridEducation.SelectionUnit = DataGridSelectionUnit.FullRow;
            gridEducation.AutoGenerateColumns = false;
            gridEducation.IsReadOnly = true;
        }

        

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Educations educations = new Educations();
            educations.education = TbEducation.Text;
            DB.db.Educations.Add(educations);
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            gridEducation.CanUserAddRows = false;
            DB.db.SaveChanges();
            refresh();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Educations educations = gridEducation.SelectedItem as Educations;

            if (educations != null && gridEducation.Columns != null)
            {
                var result = MessageBox.Show("Удалить образование: " + educations, "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if(result == MessageBoxResult.OK)
                {
                    DB.db.Educations.Remove(educations);
                    gridEducation.SelectedIndex = gridEducation.SelectedIndex == 0 ? 1 : gridEducation.SelectedIndex - 1;
                    ListEducation.Remove(educations);
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

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(gridEducation.SelectedItem != null)
            {
                gridEducation.IsReadOnly = false;
                gridEducation.BeginEdit();
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var education = DB.db.Educations;
            var query = from item in education select item;
            foreach(Educations educations in query)
            {
                ListEducation.Add(educations);
            }
            gridEducation.ItemsSource = ListEducation;
        }

        private void refresh()
        {
            ListEducation.Clear();
            var education = DB.db.Educations;
            var query = from item in education select item;
            foreach (Educations educations in query)
            {
                ListEducation.Add(educations);
            }
            gridEducation.ItemsSource = ListEducation.ToBindingList();
        }
    }
}
