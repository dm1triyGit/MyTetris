using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{   

    public partial class FormTetris : Form
    {
        public FormTetris()
        {
            InitializeComponent();
        }

        Field field;

        private void BtnStartGame_Click(object sender, EventArgs e)
        {
            field = new Field(this);
        }

        private void splitContainer1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!field.gameOver)
                switch (e.KeyCode)
                {
                    case Keys.A:
                        field.MoveLeft();
                        break;

                    case Keys.D:
                        field.MoveRight();
                        break;

                    case Keys.S:
                        field.Fall();
                        break;

                    case Keys.W:
                        field.Turn();
                        break;
                   

                    case Keys.Left:
                        field.MoveLeft();
                        break;

                    case Keys.Right:
                        field.MoveRight();
                        break;

                    case Keys.Down:
                        field.Fall();
                        break;

                    case Keys.Up:
                        field.Turn();
                        break;
                }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            field.Fall();
        }
    }
}
