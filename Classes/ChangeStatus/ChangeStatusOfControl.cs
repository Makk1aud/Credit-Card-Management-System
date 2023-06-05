using Card_management_system.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card_management_system.Classes
{
    abstract class ChangeStatusOfControl : IChangeable
    {
        public abstract void ChangeStatus(object obj, bool turner);
    }
}
