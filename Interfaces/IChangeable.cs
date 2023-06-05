using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card_management_system.Interfaces
{
    interface IChangeable
    {
        void ChangeStatus(Object obj, bool turner);
    }
}
