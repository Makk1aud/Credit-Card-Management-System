using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card_management_system.Classes.ClassAboutUser
{
    internal class AboutUser
    {
        public string CardNumber { get; set; }
        public int Balance { get; set; }
        public DateTime CardDate { get; set; }
        public string CardProvider { get; set; }
        public string CardCVV { get; set; }
    }
}
