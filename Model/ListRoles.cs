using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalDepartmentDegtyannikovIN3802
{
   public class ListRoles : ObservableCollection<Roles>
    {
        public ListRoles()
        {
            DbSet<Roles> roles = DB.db.Roles;
            var query = from item in roles select item;
            foreach (Roles item in query)
            {
                this.Add(item);
            }
        }
    }
}
