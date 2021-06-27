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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace PersonalDepartmentDegtyannikovIN3802
{
    /// <summary>
    /// Логика взаимодействия для UserForm.xaml
    /// </summary>
    public partial class UserForm : Window
    {

        ObservableCollection<Staffs> ListStaffs = new ObservableCollection<Staffs>();

        public UserForm()
        {
            InitializeComponent();
            searchText.Visibility = Visibility.Hidden;
            TextBlockSurname.Visibility = Visibility.Hidden;
            TextBlockPosititon.Visibility = Visibility.Hidden;
            TextBoxSurname.Visibility = Visibility.Hidden;
            ComboBoxPosition.Visibility = Visibility.Hidden;
            ButtonFindSurname.Visibility = Visibility.Hidden;
            ButtonFindPosition.Visibility = Visibility.Hidden;
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

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            DB.db.SaveChanges();
            RewriteStaffs();
            GridEmployee.IsReadOnly = true;
        }

        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {
            RewriteStaffs();
            GridEmployee.IsReadOnly = true;
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            RewriteStaffs();
            GridEmployee.IsReadOnly = false;
        }

        private void ButtonFindSurname_Click(object sender, RoutedEventArgs e)
        {
            string surename = TextBoxSurname.Text;
            ListStaffs.Clear();
            var staff = DB.db.Staffs;
            var query = from item in staff
                        where item.Fam == surename
                        select item;
            foreach (Staffs staffs in query)
            {
                ListStaffs.Add(staffs);
            }
            if (ListStaffs.Count > 0)
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

            if (ListStaffs.Count > 0)
            {
                GridEmployee.ItemsSource = ListStaffs;

            }
            else
            {
                MessageBox.Show("Сотрудник с должностью \n" + positions + "\n не найден", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TextBoxSurname_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBoxPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetStaffs();
            GridEmployee.SelectedIndex = 0;
        }
    }
}
