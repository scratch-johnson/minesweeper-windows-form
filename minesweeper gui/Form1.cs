using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minesweeper_gui
{
    public partial class Form1 : Form
    {
        private int buttonWidth = 23;
        private int buttonHeight = 23;
        private int distance = 20;
        private int start_x = 10;
        private int start_y = 10;

        //field
        private int m_height;
        private int m_width;
        private int m_bombsAmt;
        private int m_bombsRemaining;
        private int m_spacesRemaining;
        private char[,] m_spaces = new char[Globals.MAX_HEIGHT, Globals.MAX_WIDTH];
        private char[,] m_spacesReveal = new char[Globals.MAX_HEIGHT, Globals.MAX_WIDTH];
        private bool[,] m_isBlank = new bool[Globals.MAX_HEIGHT, Globals.MAX_WIDTH];
        private int[,] m_chunkNum = new int[Globals.MAX_HEIGHT, Globals.MAX_WIDTH];
        private Bomb[] m_bombs = new Bomb[Globals.MAX_BOMBS];
        private Button[] buttons;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Minesweeper";
            warnLbl.Visible = false;
            endLbl.Visible = false;
        }

        public void field(int width, int height, int bombsAmt)
        {
            if (width > Globals.MAX_WIDTH || height > Globals.MAX_HEIGHT || bombsAmt > Globals.MAX_BOMBS || width < 1 || height < 1 || bombsAmt < 1)
            {
                //error check, obsolete
            }

            m_width = width;
            m_height = height;
            m_bombsAmt = bombsAmt;
            m_bombsRemaining = m_bombsAmt;
            m_spacesRemaining = width * height;

            for (int r = 0; r < m_height; r++)          //fills field
                for (int c = 0; c < m_width; c++)
                    m_spaces[r, c] = '#';

            for (int m = 0; m < m_bombsAmt; m++)        //fills the field with bombs
            {
                m_bombs[m] = new Bomb();
            }

            randBombPos(m_bombs, m_bombsAmt, m_width - 1, m_height - 1);

            //last chunk fills uncovered field
            for (int r = 0; r < m_height; r++)          //fills uncovered field
                for (int c = 0; c < m_width; c++)
                {
                    int adjBombs = 0;       //sets number of adjacent bombs
                    for (int m = 0; m < m_bombsAmt; m++)
                    {
                        if (m_bombs[m].give_y() == r + 1 && m_bombs[m].give_x() == c - 1) adjBombs++;
                        if (m_bombs[m].give_y() == r + 1 && m_bombs[m].give_x() == c) adjBombs++;
                        if (m_bombs[m].give_y() == r + 1 && m_bombs[m].give_x() == c + 1) adjBombs++;
                        if (m_bombs[m].give_y() == r && m_bombs[m].give_x() == c - 1) adjBombs++;
                        if (m_bombs[m].give_y() == r && m_bombs[m].give_x() == c + 1) adjBombs++;
                        if (m_bombs[m].give_y() == r - 1 && m_bombs[m].give_x() == c - 1) adjBombs++;
                        if (m_bombs[m].give_y() == r - 1 && m_bombs[m].give_x() == c) adjBombs++;
                        if (m_bombs[m].give_y() == r - 1 && m_bombs[m].give_x() == c + 1) adjBombs++;
                    }
                    m_spacesReveal[r, c] = (char)(adjBombs + '0');
                    if (adjBombs > 0) continue;
                    m_spacesReveal[r, c] = ' ';
                }
            for (int m = 0; m < m_bombsAmt; m++)
                m_spacesReveal[m_bombs[m].give_y(), m_bombs[m].give_x()] = 'b';

            int spacesAmt = 0;
            for (int r = 0; r < m_height; r++)                  //creates array of all blank spaces in field
                for (int c = 0; c < m_width; c++)
                    if (m_spacesReveal[r, c] == ' ')
                    {
                        m_isBlank[r, c] = true;
                        spacesAmt++;
                    }
                    else
                        m_isBlank[r, c] = false;

            //##initial chunk pass
            int chunksAmt = 1;
            bool[,] wasViewed = new bool[Globals.MAX_HEIGHT, Globals.MAX_WIDTH];
            //int m_chunkNum[MAX_HEIGHT,MAX_WIDTH];
            for (int r = 0; r < m_height; r++)          //creates initial wasViewed array to avoid undefinded behaviour
                for (int c = 0; c < m_width; c++)
                    wasViewed[r, c] = false;

            for (int r = 0; r < m_height; r++)                  //this code evaluates chunks of blank spaces
                for (int c = 0; c < m_width; c++)
                    if (m_isBlank[r, c] && !wasViewed[r, c])    //this conditional evaluates whether a blank space is in a chunk
                    {                                           //more accurate checking occurs in the chunk combination below
                        wasViewed[r, c] = true;
                        int[] x = new int[] { c + 1, c + 1, c + 1, c, c, c - 1, c - 1, c - 1 };
                        int[] y = new int[] { r + 1, r - 1, r, r - 1, r + 1, r + 1, r - 1, r };
                        int viewedNeighbors = 0;
                        for (int h = 0; h < 8; h++)
                        {
                            if (!(x[h] >= 0 && x[h] < m_width && y[h] >= 0 && y[h] < m_height)) continue;   //error check
                            if (wasViewed[y[h], x[h]] && m_isBlank[y[h], x[h]])     //if true r,c is part of a chunk
                            {
                                viewedNeighbors++;
                                m_chunkNum[r, c] = m_chunkNum[y[h], x[h]];              //assigns space to the connected chunk
                            }
                        }
                        if (viewedNeighbors == 0)
                        {
                            m_chunkNum[r, c] = chunksAmt;               //creates new chunk if space is not connected to a chunk
                            chunksAmt++;
                        }
                    }
                    else
                    {
                        wasViewed[r, c] = true;         //if a position is not a space mark as viewed and don't add to a chunk
                        m_chunkNum[r, c] = 0;
                    }

            //##begin chunk combination

            for (int r = 0; r < m_height; r++)          //reinitializes wasViewed array to avoid unwanted behaviour
                for (int c = 0; c < m_width; c++)
                    wasViewed[r, c] = false;

            for (int r = 0; r < m_height; r++)          //combines separate chunks that should be connected
                for (int c = 0; c < m_width; c++)
                    if (m_isBlank[r, c] && !wasViewed[r, c])
                    {
                        int[] x = new int[] { c + 1, c + 1, c + 1, c, c, c - 1, c - 1, c - 1 };
                        int[] y = new int[] { r + 1, r - 1, r, r - 1, r + 1, r + 1, r - 1, r };
                        for (int h = 0; h < 8; h++)
                        {
                            if (!(x[h] >= 0 && x[h] < m_width && y[h] >= 0 && y[h] < m_height)) continue;   //error check
                            if (m_chunkNum[y[h], x[h]] != m_chunkNum[r, c] && m_chunkNum[r, c] != 0 && m_chunkNum[y[h], x[h]] != 0)
                                for (int r1 = 0; r1 < m_height; r1++)
                                    for (int c1 = 0; c1 < m_width; c1++)
                                        if (m_chunkNum[r1, c1] == m_chunkNum[r, c])             //puts all members of additional
                                            m_chunkNum[r1, c1] = m_chunkNum[y[h], x[h]];        //chunk into the proper chunk
                        }
                    }

            //##end chunk combination
        }

        private void randBombPos(Bomb[] bombArr, int bombAmt, int maxW, int maxH)
        {
            for (int k = 0; k < bombAmt; k++)
            {
                Random rand = new Random();
                int bombX = rand.Next(0, maxW);
                int bombY = rand.Next(0, maxH);

                bombArr[k].set_pos(bombX, bombY);
                if (k == 0) continue;

                bool cont = false;
                for (int h = 0; h < k; h++)
                {
                    if (bombArr[k].give_x() == bombArr[h].give_x() && bombArr[k].give_y() == bombArr[h].give_y())
                    {
                        cont = true;
                        break;
                    }
                }
                if (cont)
                {
                    k--;
                    //cerr << "repeat" << k << endl;
                    continue;
                }
            }
        }

        private void endGame(string endTxt)
        {
            if (endTxt == "Game Over")
            {
                foreach (Button b in buttons)
                {
                    b.Enabled = false;
                    if (m_spacesReveal[buttonCoords(b)[1], buttonCoords(b)[0]] == 'b')
                        b.Text = "B";
                }
            }
            else
                foreach (Button b in buttons)
                    b.Visible = false;
            endLbl.Text = endTxt;
            endLbl.Visible = true;
        }

        private void selectSpace(Button thisBtn)
        {
            if (thisBtn == null) return;
            if (thisBtn.Enabled == false) return;
            
            int x = buttonCoords(thisBtn)[0];
            int y = buttonCoords(thisBtn)[1];
            if ((m_spacesReveal[y, x] != ' ') && (m_spacesRemaining == m_width * m_height))
            {
                field(m_width, m_height, m_bombsAmt);
                selectSpace(thisBtn);
                return;
            }
            thisBtn.Enabled = false;
            if (m_spacesReveal[y, x] == 'b')         //GAME OVER
            {
                endGame("Game Over");
            }
            else if (m_spacesReveal[y, x] == ' ')   //blank space selected
            {
                m_spacesRemaining--;
                int chunkNum = m_chunkNum[y, x];
                for (int r = 0; r < m_height; r++)
                    for (int c = 0; c < m_width; c++)
                        if (m_chunkNum[r, c] == chunkNum)   //select surrounding spaces
                        {
                            if (buttons[c + r * m_width].Enabled == true) m_spacesRemaining--;
                            buttons[c + r * m_width].Enabled = false;
                            //decrement rem for each space disabled
                            int[] adjX = new int[] { c + 1, c + 1, c + 1, c, c, c - 1, c - 1, c - 1 };
                            int[] adjY = new int[] { r + 1, r - 1, r, r - 1, r + 1, r + 1, r - 1, r };
                            for (int h = 0; h < 8; h++)
                                if (adjX[h] >= 0 && adjX[h] < m_width && adjY[h] >= 0 && adjY[h] < m_height)
                                    selectSpace(buttons[adjX[h] + adjY[h] * m_width]);
                        }
            }
            else if (isNum(m_spacesReveal[y, x]))   //number space selected
            {
                m_spacesRemaining--;
                thisBtn.Text = m_spacesReveal[y, x].ToString();
                Color[] colors = new Color[] { Color.Blue, Color.Green, Color.Red, Color.Purple,
                    Color.Yellow, Color.AliceBlue, Color.Orange, Color.Black };
                int index = (int)(m_spacesReveal[y, x] - '0');
                thisBtn.ForeColor = colors[index];
            }

            if (m_spacesRemaining <= m_bombsAmt)
            {
                endGame("You Win");
            }
        }

        private int[] buttonCoords(Button thisBtn)      //get coordinates of a button
        {
            int top = thisBtn.Top;
            int left = thisBtn.Left;
            int[] coords = new int[2];
            int x = (left - start_y - distance) / buttonWidth;
            int y = (top - start_x - distance) / buttonHeight;
            coords[0] = x;
            coords[1] = y;
            return coords;
        }

        private bool isNum(char intChar)
        {
            if (intChar == '0' || intChar == '1' || intChar == '2' || intChar == '3' || intChar == '4' || 
                intChar == '5' || intChar == '6' || intChar == '7' || intChar == '8' || intChar == '9')
            {
                return true;
            }
            else return false;
        }

        private void button1_Click(object sender, EventArgs e)  //start button
        {
            int height, width, bombsAmt;
            if (Int32.TryParse(heightTB.Text, out height) && 
                Int32.TryParse(widthTB.Text, out width) && 
                Int32.TryParse(bombTB.Text, out bombsAmt) &&
                height > 0 && height <= 30 && width > 0 && width <= 30 &&
                bombsAmt > 0 && bombsAmt <= 100)
            {
                warnLbl.Visible = false;

                field(width, height, bombsAmt);

                //upon successful value input
                heightLbl.Visible = false;
                widthLbl.Visible = false;
                bombLbl.Visible = false;
                heightTB.Visible = false;
                widthTB.Visible = false;
                bombTB.Visible = false;
                startBtn.Visible = false;
                bmbCountLbl.Visible = true;
                bmbCountLbl.Text = "bombs remaining: " + m_bombsAmt.ToString();

                int btnNum = height * width;   //change to get grid size
                buttons = new Button[btnNum];

                int btnAmt = 0;
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Button tmpButton = new Button();
                        tmpButton.Top = start_x + (x * buttonHeight + distance);
                        tmpButton.Left = start_y + (y * buttonWidth + distance);
                        tmpButton.Width = buttonWidth;
                        tmpButton.Height = buttonHeight;
                        tmpButton.Text = "";

                        //event to control what hppens when a button is clicked
                        tmpButton.MouseDown += (s, e) =>
                        {
                            switch (e.Button)
                            {
                                //tile selecting
                                case MouseButtons.Left:
                                    if (tmpButton.Text == "f") break;
                                    selectSpace(tmpButton);
                                    break;
                                //tile flagging/unflagging
                                case MouseButtons.Right:
                                    if (tmpButton.Text == "f")
                                    {
                                        tmpButton.Text = "";
                                        m_bombsRemaining++;
                                        bmbCountLbl.Text = "bombs remaining: " + m_bombsRemaining.ToString();
                                    }
                                    else
                                    {
                                        tmpButton.Text = "f";
                                        m_bombsRemaining--;
                                        bmbCountLbl.Text = "bombs remaining: " + m_bombsRemaining.ToString();
                                    }
                                    break;
                            }
                        };
                        this.Controls.Add(tmpButton);
                        buttons[btnAmt] = tmpButton;
                        btnAmt++;
                    }

                }
            }
            else
            {
                warnLbl.Visible = true;
            }
        }
        private void label1_Click(object sender, EventArgs e) { }
    }
    public class Globals
    {
        public const int MAX_HEIGHT = 30;
        public const int MAX_WIDTH = 30;
        public const int MAX_BOMBS = 100;
    }
    public class Bomb
    {
        private int m_x;
        private int m_y;
        public Bomb()
        {
            this.m_x = 0;
            this.m_y = 0;
        }
        public int give_x()
        {
            return this.m_x;
        }
        public int give_y()
        {
            return this.m_y;
        }
        public void set_pos(int x, int y)
        {
            this.m_x = x;
            this.m_y = y;
        }
    }
}
