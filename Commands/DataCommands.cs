using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PersonalDepartmentDegtyannikovIN3802.Commands
{
    class DataCommands
    {
        public static RoutedCommand Delete { get; set; }
        public static RoutedCommand Undo { get; set; }
        public static RoutedCommand New { get; set; }
        public static RoutedCommand Save { get; set; }
        public static RoutedCommand Find { get; set; }
        static DataCommands()
        {
            InputGestureCollection inputs = new InputGestureCollection();
           
            inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.D, ModifierKeys.Control, "Ctrl+D"));
            Delete = new RoutedCommand("Delete", typeof(DataCommands), inputs);
            inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.Z, ModifierKeys.Control, "Ctrl+Z"));
            Undo = new RoutedCommand("Undo", typeof(DataCommands), inputs);
            inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl+N"));
            New = new RoutedCommand("New", typeof(DataCommands), inputs);
            inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl+S"));
            Save = new RoutedCommand("Save", typeof(DataCommands), inputs);
            inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.F, ModifierKeys.Control, "Ctrl+F"));
            Find = new RoutedCommand("Find", typeof(DataCommands), inputs);
            
        }
    }
}
