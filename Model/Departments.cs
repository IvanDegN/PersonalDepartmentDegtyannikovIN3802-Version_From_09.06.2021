using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalDepartmentDegtyannikovIN3802
{
   partial class Departments
    {
        public Staffs departmentsid
        {
            get => default;
            set
            {
            }
        }

        public override string ToString()
        {
            return department.ToString();
        }
    }
}
