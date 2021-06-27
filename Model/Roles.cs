using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalDepartmentDegtyannikovIN3802
{
    public partial  class Roles
    {
        public Users Users1
        {
            get => default;
            set
            {
            }
        }

        public override string ToString()
        {
            return Role.ToString();
        }
    }
}
