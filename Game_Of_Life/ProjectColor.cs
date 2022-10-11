using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public static class ProjectColor
    {
        // Sets the main colors to their default values
        public static Color SetPrimaryCellDefault()
        {
            return Color.Gray;
        }
        public static Color SetGridDefault()
        {
            return Color.Black;
        }

        public static Color GetNewColor()
        {
            ColorDialog clrDialog = new ColorDialog();
            clrDialog.ShowDialog();
            return clrDialog.Color;
        }
    }
}
