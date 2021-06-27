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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace PersonalDepartmentDegtyannikovIN3802
{
    /// <summary>
    /// Логика взаимодействия для ManagePosition.xaml
    /// </summary>
    public partial class ManagePosition : Window
    {
        ObservableCollection<Positions> ListPositions = new ObservableCollection<Positions>();
        private Staffs currentStaffs = new Staffs();
        public ManagePosition(Staffs staffs)
        {
            InitializeComponent();
            DB.db.Positions.Load();
            gridPositions.CanUserAddRows = false;
            gridPositions.CanUserDeleteRows = false;
            gridPositions.CanUserSortColumns = false;
           
            gridPositions.SelectionMode = DataGridSelectionMode.Extended;
            gridPositions.SelectionUnit = DataGridSelectionUnit.FullRow;
            gridPositions.AutoGenerateColumns = false;
            gridPositions.IsReadOnly = true;
            
            if(staffs != null)
            {
                currentStaffs = staffs;
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var positions = DB.db.Positions;
            

            var query = from item in positions
                        select item;
            foreach(Positions pos in query)
            {
                ListPositions.Add(pos);
            }
            gridPositions.ItemsSource = ListPositions;
            
            

        }

        private void refresh()
        {
            ListPositions.Clear();
            var positions = DB.db.Positions;
            var query = from item in positions
                        select item;
            foreach (Positions pos in query)
            {
                ListPositions.Add(pos);
            }
            gridPositions.ItemsSource = ListPositions;
            gridPositions.Items.Refresh();


        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            
            Positions positions = gridPositions.SelectedItem as Positions;
            
            
            

            
            if (positions != null && gridPositions.Columns != null)
            {
                bool isAccept = true;

                foreach (var elem in DB.db.Staffs)
                {

                    if (elem.PositionId == positions.PositionId)
                    {
                        isAccept = false;
                        MessageBox.Show("Невозможно удалить запись, так как она связана с другой таблицей", "Warning", MessageBoxButton.OKCancel);
                        refresh();
                        break;
                    }

                }
                if (isAccept)
                {
                    
                    if (gridPositions.CurrentCell != null)
                    {

                        MessageBox.Show("Удалить должность: " + positions, "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.OK);
                        ListPositions.Remove(positions);
                        DB.db.Positions.Remove(positions);
                        DB.db.SaveChanges();
                    }
                }


            }
            else
            {
                MessageBox.Show("Выберите строку для удаления", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            
            if (gridPositions.SelectedItem != null)
            {
                
                gridPositions.IsReadOnly = false;
                
                
                gridPositions.BeginEdit();
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Positions positions = new Positions();
            positions.position = TbPos.Text;
            DB.db.Positions.Add(positions);
            ListPositions.Add(positions);

            foreach(var item in DB.db.Positions)
            {
                if(item.position == positions.position)
                {
                    MessageBox.Show("Должность " + positions + " уже существует","Warning",MessageBoxButton.OK,MessageBoxImage.Warning,MessageBoxResult.OK);
                    DB.db.Positions.Remove(positions);
                    ListPositions.Remove(positions);
                    break;
                }
            }
            if(String.IsNullOrWhiteSpace(TbPos.Text))
            {
                DB.db.Positions.Remove(positions);
                ListPositions.Remove(positions);
                MessageBox.Show("Поле не должно быть пустым", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                
            }
            

            
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            
                gridPositions.CanUserAddRows = false;
            try
            {
                if(gridPositions.Columns.Count >0)
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

        

        bool CheckNumbers(string numbers)
        {
            Regex regex = new Regex("[1234567890]");
            return regex.IsMatch(numbers);
        }

        

        

        

        

        private void TbPos_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
            e.Handled = CheckNumbers(e.Text);
            if (TbPos.Text.Length >= 30)
            {
                
                System.Windows.Forms.MessageBox.Show("Должность не должна быть длиннее, чем 30 символов.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TbPos.Text = TbPos.Text.Remove(TbPos.Text.Length - 1);
                TbPos.SelectionStart = TbPos.Text.Length;
            }
        }
    }

   



}


