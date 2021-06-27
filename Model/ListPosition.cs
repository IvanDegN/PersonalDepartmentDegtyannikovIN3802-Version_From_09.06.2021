using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalDepartmentDegtyannikovIN3802
{
    public  class ListPosition : ObservableCollection<Positions>
    {
        public ListPosition()
        {

            DbSet<Positions> position = DB.db.Positions;
            var query = from item in position select item;
            foreach (Positions item in query)
            {
                this.Add(item);
                
            }
            

        }


    }
}
