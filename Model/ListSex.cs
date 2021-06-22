using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalDepartmentDegtyannikovIN3802
{
  public  class ListSex : ObservableCollection<Sexes>
    {
        public ListSex()
        {

            DbSet<Sexes> sexes = DB.db.Sexes;
            var query = from sex in sexes select sex;
            foreach(Sexes item in query)
            {
                this.Add(item);
            }

        }

        
    }
}
