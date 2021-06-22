using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalDepartmentDegtyannikovIN3802
{
    public class EducationList : ObservableCollection<Educations>
    {
        public EducationList()
        {

            DbSet<Educations> educations = DB.db.Educations;
            var query = from item in educations select item;
            foreach (Educations item in query)
            {
                this.Add(item);
            }

        }


    }
}
