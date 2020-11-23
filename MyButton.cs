using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
   public class MyButton : Button
    {
       public MyButton()
        {
            SetStyle(ControlStyles.Selectable, false);
        }
    }
}
