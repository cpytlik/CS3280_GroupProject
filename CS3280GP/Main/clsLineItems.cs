﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280GP.Main
{

    /// <summary>
    /// Used to make items for the main menu
    /// </summary>
    class clsLineItems
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
