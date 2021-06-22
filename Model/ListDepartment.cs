using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalDepartmentDegtyannikovIN3802
{
    public class ListDepartment : ObservableCollection<Departments>
    {
        public ListDepartment()
        {
            DbSet<Departments> departments = DB.db.Departments;
            var query = from item in departments select item;
            foreach (Departments item in query)
            {
                this.Add(item);
            }

        }


    }
}
