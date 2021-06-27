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
            ListEducation.Add(educations);
            
            

            foreach (var item in DB.db.Educations)
            {
                if (item.education == educations.education)
                {
                    MessageBox.Show(educations + " образование" + " уже существует", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                    DB.db.Educations.Remove(educations);
                    ListEducation.Remove(educations);
                    break;
                }
            }
            if (String.IsNullOrWhiteSpace(TbEducation.Text))
            {
                DB.db.Educations.Remove(educations);
                ListEducation.Remove(educations);
                MessageBox.Show("Поле не должно быть пустым", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);

            }
            

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            gridEducation.CanUserAddRows = false;
            try
            {
                if (gridEducation.Columns.Count > 0)
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

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Educations educations = gridEducation.SelectedItem as Educations;

            if (educations != null && gridEducation.Columns != null)
            {
                bool isAccept = true;

                foreach (var elem in DB.db.Staffs)
                {

                    if (elem.EducationId == educations.EducationId)
                    {
                        isAccept = false;
                        MessageBox.Show("Невозможно удалить запись, так как она связана с другой таблицей", "Warning", MessageBoxButton.OKCancel);
                        refresh();
                        break;
                    }

                }
                if (isAccept)
                {

                    if (gridEducation.CurrentCell != null)
                    {

                        MessageBox.Show("Удалить образование: " + educations, "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.OK);
                        ListEducation.Remove(educations);
                        DB.db.Educations.Remove(educations);
                        DB.db.SaveChanges();
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

        bool CheckNumbers(string numbers)
        {
            Regex regex = new Regex("[1234567890]");
            return regex.IsMatch(numbers);
        }

        private void TbEducation_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckNumbers(e.Text);
            if (TbEducation.Text.Length >= 30)
            {

                System.Windows.Forms.MessageBox.Show("Образование не должно быть длиннее, чем 30 символов.", "Warning!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                TbEducation.Text = TbEducation.Text.Remove(TbEducation.Text.Length - 1);
                TbEducation.SelectionStart = TbEducation.Text.Length;
            }
        }
    }
}
