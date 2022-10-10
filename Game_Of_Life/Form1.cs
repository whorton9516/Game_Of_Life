using Game_Of_Life.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Of_Life
{
    public partial class Form1 : Form
    {
        // The universe array
        bool[,] universe = new bool[20, 20];

        // The scratchPad array
        bool[,] scratchPad = new bool[20, 20];

        // The wasAlive array
        bool[,] wasAlive = new bool[20, 20];

        // The clipboard array
        bool[,] clipboard = new bool[20, 20];

        // The templates dictionary
        Dictionary<string, bool[,]> templates = new Dictionary<string, bool[,]>();

        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            timer.Interval = 100; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer running
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {


            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    int count = CountNeighborsToroidal(x, y);
                    /*
                     * Any live cell with fewer than two live neighbours dies, as if by underpopulation.
                     * Any live cell with two or three live neighbours lives on to the next generation.
                     * Any live cell with more than three live neighbours dies, as if by overpopulation.
                     * Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
                     */
                    if (universe[x,y] == false)
                    {
                        if (count == 3) 
                        { 
                            scratchPad[x, y] = true; 
                            wasAlive[x, y] = true;
                        }
                    }
                    else
                    {
                        if (count < 2 || count > 3) { scratchPad[x, y] = false; }
                        else { scratchPad[x, y] = true; }
                    }
                }
            }

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    universe[x, y] = scratchPad[x, y];
                }
            }

            graphicsPanel1.Invalidate();

            // Increment generation count
            generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            float cellWidth = ((float)graphicsPanel1.ClientSize.Width / universe.GetLength(0)) - 0.3f;
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            float cellHeight = ((float)graphicsPanel1.ClientSize.Height / universe.GetLength(1)) - 0.3f;

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // A Brush for filling previously living cells interiors (color)
            Brush wasAliveBrush = new SolidBrush(Color.LightGray);

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    RectangleF cellRect = Rectangle.Empty;
                    cellRect.X = (x * cellWidth);
                    cellRect.Y = (y * cellHeight);
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }
                    else if (wasAlive[x,y] == true)
                    {
                        e.Graphics.FillRectangle(wasAliveBrush, cellRect);
                    }

                    // Outline the cell with a pen
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                float cellWidth = (graphicsPanel1.ClientSize.Width / universe.GetLength(0)) - 0.3f;
                float cellHeight = (graphicsPanel1.ClientSize.Height / universe.GetLength(1)) + 0.5f;

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                float x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                float y = e.Y / cellHeight;

                // Toggle the cell's state
                universe[(int)x, (int)y] = !universe[(int)x, (int)y];
                wasAlive[(int)x, (int)y] = true;

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        private int CountNeighborsToroidal(int x, int y)
        {
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int xCheck = x + xOffset;
                    int yCheck = y + yOffset;

                    // if xOffset and yOffset are both equal to 0 then continue
                    if (xOffset == 0 && yOffset == 0) { continue; }

                    // if xCheck is less than 0 then set to xLen - 1
                    if (xCheck < 0) { xCheck = xLen - 1; }

                    // if yCheck is less than 0 then set to yLen - 1
                    if (yCheck < 0) { yCheck = yLen - 1; }

                    // if xCheck is greater than or equal too xLen then set to 0
                    if (xCheck >= xLen) { xCheck = 0; }

                    // if yCheck is greater than or equal too yLen then set to 0
                    if (yCheck >= yLen) { yCheck = 0; }

                    if (universe[xCheck, yCheck] == true) count++;
                }
            }
            return count;
        }

        private void NewGrid()
        {
            timer.Stop();
            Array.Clear(universe, 0, universe.Length);
            Array.Clear(scratchPad, 0, scratchPad.Length);
            Array.Clear(wasAlive, 0, wasAlive.Length);
            graphicsPanel1.Invalidate();
        }
        
        private string GetJsonFilepath()
        {
            try
            {
                var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
                while (directory.Name != "Game_Of_Life")
                {
                    directory = directory.Parent;
                }

                directory = directory.GetDirectories("Resources")[0];

                return directory.GetFiles("templates.json")[0].FullName;
            }
            catch (Exception e)
            {
                return String.Empty;
            }
        }

        private void SaveTemplate(string name, bool[,] universe)
        {
            templates.Add(name, universe);
        }

        private void SaveJson()
        {
            string templateString = JsonConvert.SerializeObject(templates);
            File.WriteAllText(GetJsonFilepath(), templateString);
        }

        private void LoadTemplatesToDictionary()
        {
            templates.Clear();
            string templateString = File.ReadAllText(GetJsonFilepath());
            if (templateString != null)
            {
                templates = JsonConvert.DeserializeObject<Dictionary<string, bool[,]>>(templateString);
            }
            else return;
        }

        private void DeleteTemplate(string name)
        {
            templates.Remove(name);
        }
        



        // UI Methods
        private void startToolStripButton_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void pauseToolStripButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void stepToolStripButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
            NextGeneration();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    clipboard[x, y] = universe[x, y];
                }
            }

            NewGrid();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    clipboard[x, y] = universe[x, y];
                }
            }
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    universe[x, y] = clipboard[x, y];
                }
            }
            graphicsPanel1.Invalidate();
        }

        private void loadToolStripButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
            GetJsonFilepath();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveJson();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTemplatesToDictionary();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateIOModal modal = new TemplateIOModal();
            modal.Apply += new ApplyEventHandler(modalApply);
            if(DialogResult.OK == modal.ShowDialog())
            {
                SaveTemplate(modal.templateName, universe);
            }
        }

        private void modalApply(Object sender, ApplyEventArgs e)
        {
            string templateName = e.name;
        }
    }
}
