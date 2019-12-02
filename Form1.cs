using System;
using System.Drawing;
//using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Labyrinth
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private IContainer components;
        // maze array
        private MainMenu mainMenu1;
        private MenuItem menuItem1;
        private MenuItem SolveMenu;
        private MenuItem DimensionsMenu;
        private MenuItem menuItem2;
        private PictureBox pictureBox1;
        private MenuItem menuItem3;
        private MenuItem menuItem4;
        private MenuItem menuItem5;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private Maze TheMaze = new Maze();

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            TheMaze.Generate();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        protected override void WndProc(ref Message m)
        { // Запрет на перемещение Form1
            const int WM_NCLBUTTONDOWN = 161;
            const int WM_SYSCOMMAND = 274;
            const int HTCAPTION = 2;
            const int SC_MOVE = 61456;

            if ((m.Msg == WM_SYSCOMMAND) && (m.WParam.ToInt32() == SC_MOVE))
            {
                return;
            }

            if ((m.Msg == WM_NCLBUTTONDOWN) && (m.WParam.ToInt32() == HTCAPTION))
            {
                return;
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.DimensionsMenu = new System.Windows.Forms.MenuItem();
            this.SolveMenu = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem4,
            this.menuItem5,
            this.DimensionsMenu,
            this.SolveMenu,
            this.menuItem3,
            this.menuItem2});
            this.menuItem1.Text = "Файл";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 0;
            this.menuItem4.Text = "Сохранить лабиринт";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            this.menuItem5.Text = "Загрузить лабиринт";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // DimensionsMenu
            // 
            this.DimensionsMenu.Index = 2;
            this.DimensionsMenu.Text = "Сгенерировать лабиринт";
            this.DimensionsMenu.Click += new System.EventHandler(this.DimensionsMenu_Click);
            // 
            // SolveMenu
            // 
            this.SolveMenu.Index = 3;
            this.SolveMenu.Text = "Пройти лабиринт";
            this.SolveMenu.Click += new System.EventHandler(this.SolveMenu_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 4;
            this.menuItem3.Text = "Очистить стартовую позицию";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 5;
            this.menuItem2.Text = "Выход";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(710, 710);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Maze files (*maze)|*maze";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Maze files (*maze)|*maze";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(734, 727);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Labyrinth";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }

        private void DimensionsMenu_Click(object sender, System.EventArgs e)
        {
            DimensionsDialog theDialog = new DimensionsDialog();
            theDialog.numericUpDown1.Value = Maze.kDimension;
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                Maze.kDimension = (int)theDialog.numericUpDown1.Value;
                Cell.kCellSize = (int)(pictureBox1.Width - 10) / Maze.kDimension;
                TheMaze.Initialize();
                TheMaze.Generate();
                pictureBox1.Refresh();
                Maze.EnterMarker = false;
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SolveMenu_Click(object sender, EventArgs e)
        {
            if (Maze.EnterMarker)
            {
                Graphics g = pictureBox1.CreateGraphics();
                TheMaze.WaveTracingSolve(g);
            }
            else MessageBox.Show("Задайте стартовую позицию!");
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.White, ClientRectangle);
            TheMaze.Draw(g);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //установка стартовой позиции
            if (!Maze.EnterMarker)
            {
                int x = e.Location.X;
                int y = e.Location.Y;
                Graphics g = pictureBox1.CreateGraphics();
                TheMaze.SetStartPoint(x, y, g);
            }
            else MessageBox.Show("Стартовая позиция уже установлена, что бы её очистить выбирите соответствующий пункт меню!");
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            //очистка стартовой позиции
            if (Maze.EnterMarker)
            {
                TheMaze.ClearStartPoint();
                Graphics g = pictureBox1.CreateGraphics();
                g.FillRectangle(Brushes.White, ClientRectangle);
                TheMaze.Draw(g);
            }
        }

        private void menuItem4_Click(object sender, EventArgs e)
        { // ЗАПИСЬ БИНАРНОГО ФАЙЛА ЛАБИРИНТА
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TheMaze.SaveMaze(saveFileDialog1.FileName);
                MessageBox.Show("Сохранение лабиринта успешно завешено в файл " + saveFileDialog1.FileName + ".maze");
            }
        }

        private void menuItem5_Click(object sender, EventArgs e)
        { // ЧТЕНИЕ БИНАРНОГО ФАЙЛА ЛАБИРИНТА
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TheMaze.LoadMaze(openFileDialog1.FileName);
                Cell.kCellSize = (int)(pictureBox1.Width - 10) / Maze.kDimension;
                Graphics g = pictureBox1.CreateGraphics();
                g.FillRectangle(Brushes.White, ClientRectangle);
                TheMaze.Draw(g);
                MessageBox.Show("Загрузка лабиринта успешно завершена из файла " + openFileDialog1.FileName);
            }
        }
    }
}
