using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignmentPrototype
{
    class clsItemLines
    {
        class clsItemLines
        {
            public string ItemCode { get; set; }
            public string ItemDesc { get; set; }
            public double Cost { get; set; }
            public int LineItemNum { get; set; }

            public override string ToString()
            {
                return ItemDesc;
            }
        }

    }
}
