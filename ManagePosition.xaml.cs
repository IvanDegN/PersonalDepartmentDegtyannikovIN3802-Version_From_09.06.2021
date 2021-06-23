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
    /// Логика взаимодействия для ManagePosition.xaml
    /// </summary>
    public partial class ManagePosition : Window
    {
        ObservableCollection<Positions> ListPositions = new ObservableCollection<Positions>();

        public ManagePosition()
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
            gridPositions.ItemsSource = ListPositions.ToBindingList();
            
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            Positions positions = gridPositions.SelectedItem as Positions;
            

            if(positions != null && gridPositions.Columns != null)
            {
                
                var result = MessageBox.Show("Удалить должность: " + positions, "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if(result == MessageBoxResult.OK)
                {
                    DB.db.Positions.Remove(positions);
                    gridPositions.SelectedIndex = gridPositions.SelectedIndex == 0 ? 1 : gridPositions.SelectedIndex - 1;
                    ListPositions.Remove(positions);
                    try
                    {
                    DB.db.SaveChanges();
                    }
                    catch
                    {
                    MessageBox.Show("Невозможно удалить запись, так как она связана с другой таблицей","Warning",MessageBoxButton.OKCancel,MessageBoxImage.Warning);
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
            
            if (gridPositions.SelectedItem != null)
            {
                
                gridPositions.IsReadOnly = false;
                
                
                gridPositions.BeginEdit();
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования");
            }
            
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Positions positions = new Positions();
            positions.position = TbPos.Text;
            DB.db.Positions.Add(positions);
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            
                gridPositions.CanUserAddRows = false;
                DB.db.SaveChanges();
                refresh();
                
        }
        
    }

    
}


