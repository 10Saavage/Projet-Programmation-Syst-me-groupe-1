using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vue
{
    public partial class form : Form
    {
        private TableLayoutPanel tableLayoutPanel1;
        private Button button1;
        private Panel panel1;
        private Button button2;
        private GroupBox groupBox1;
        private Label label1;

        private string testVariable1 = "bl1lablba1labla1";
        private Button button3;
        private Button button8;
        private Button button7;
        private Button button6;
        private Button button5;
        private Button button10;
        private Button button9;
        private Button button13;
        private string testVariable2 = "blabla2";
        private string affichagebouton = "variabletableau";

        private int nbrebouton = 10;
        private Button button4;
        private TableLayoutPanel tableLayoutPanel3;
        private Button button14;
        private Button button12;
        private Button button11;
        private TextBox textBox1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button button18;
        private Button button17;
        private Button button16;
        private Button button15;
        //string[,] tableau = new string[4, 2]
        //{
        //    {"charles","1" },
        //    {"clement", "2" },
        //    {"marcel","3" },
        //    {"quentin", "4" },
        //};
        private Button button22;
        private Button button21;
        private Button button20;
        private Button button19;
        string[,] tablabel = new string[4, 2]
        {

            {"voiture","train" },
            {"camion", "avion" },
            {"truc1","truc2" },
            {"truc3", "truc4" },
        };

        private Controller.World world;
        private int lastUsedTag = 0;
        private string lastGeneratedButtonTag = "";
        private bool speedUp = false;
        private bool pause = false;
        private delegate void refreshPointer();


        public form()
        {
            InitializeComponent();

            refreshPointer pointer = new refreshPointer(RefreshAllEvent);

            world = (new Controller.Controller()).LoadWorld(new int[] { 20, 20 }, new int[] { 10, 10, 0, 0 }, new int[] { 10, 10, 0, 10, 10, 10, 10, 10, 10 }, new int[] { 10, 10, 10, 0 }, 5, 2, 2, pointer);

            //label1.Text = "coucou";
            label1.Text = "";
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button22 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.button14 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AccessibleName = "";
            this.tableLayoutPanel1.CausesValidation = false;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.00228F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.99771F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1582, 753);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Tag = "";
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.button22);
            this.panel1.Controls.Add(this.button21);
            this.panel1.Controls.Add(this.button20);
            this.panel1.Controls.Add(this.button19);
            this.panel1.Controls.Add(this.button18);
            this.panel1.Controls.Add(this.button17);
            this.panel1.Controls.Add(this.button16);
            this.panel1.Controls.Add(this.button15);
            this.panel1.Controls.Add(this.button13);
            this.panel1.Controls.Add(this.button10);
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1085, 749);
            this.panel1.TabIndex = 1;
            // 
            // button22
            // 
            this.button22.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button22.BackColor = System.Drawing.Color.Transparent;
            this.button22.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button22.BackgroundImage")));
            this.button22.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button22.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button22.Location = new System.Drawing.Point(336, 161);
            this.button22.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(73, 70);
            this.button22.TabIndex = 16;
            this.button22.Tag = "5";
            this.button22.UseVisualStyleBackColor = false;
            // 
            // button21
            // 
            this.button21.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button21.BackColor = System.Drawing.Color.Transparent;
            this.button21.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button21.BackgroundImage")));
            this.button21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button21.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button21.Location = new System.Drawing.Point(86, 161);
            this.button21.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(73, 70);
            this.button21.TabIndex = 15;
            this.button21.Tag = "4";
            this.button21.UseVisualStyleBackColor = false;
            // 
            // button20
            // 
            this.button20.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button20.BackColor = System.Drawing.Color.Transparent;
            this.button20.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button20.BackgroundImage")));
            this.button20.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button20.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button20.Location = new System.Drawing.Point(408, 28);
            this.button20.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(73, 70);
            this.button20.TabIndex = 14;
            this.button20.Tag = "3";
            this.button20.UseVisualStyleBackColor = false;
            // 
            // button19
            // 
            this.button19.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button19.BackColor = System.Drawing.Color.Transparent;
            this.button19.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button19.BackgroundImage")));
            this.button19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button19.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button19.Location = new System.Drawing.Point(268, 28);
            this.button19.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(73, 70);
            this.button19.TabIndex = 13;
            this.button19.Tag = "2";
            this.button19.UseVisualStyleBackColor = false;
            // 
            // button18
            // 
            this.button18.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button18.BackColor = System.Drawing.Color.Transparent;
            this.button18.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button18.BackgroundImage")));
            this.button18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button18.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button18.Location = new System.Drawing.Point(285, 564);
            this.button18.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(73, 70);
            this.button18.TabIndex = 12;
            this.button18.UseVisualStyleBackColor = false;
            // 
            // button17
            // 
            this.button17.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button17.BackColor = System.Drawing.Color.Transparent;
            this.button17.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button17.BackgroundImage")));
            this.button17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button17.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button17.Location = new System.Drawing.Point(432, 564);
            this.button17.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(73, 70);
            this.button17.TabIndex = 11;
            this.button17.Text = "Chef de partie";
            this.button17.UseVisualStyleBackColor = false;
            // 
            // button16
            // 
            this.button16.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button16.BackColor = System.Drawing.Color.Transparent;
            this.button16.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button16.BackgroundImage")));
            this.button16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button16.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button16.ForeColor = System.Drawing.Color.Red;
            this.button16.Location = new System.Drawing.Point(596, 564);
            this.button16.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(73, 70);
            this.button16.TabIndex = 10;
            this.button16.Text = "Chef";
            this.button16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button16.UseVisualStyleBackColor = false;
            // 
            // button15
            // 
            this.button15.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button15.BackColor = System.Drawing.Color.Transparent;
            this.button15.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button15.BackgroundImage")));
            this.button15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button15.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button15.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button15.ForeColor = System.Drawing.Color.Black;
            this.button15.Location = new System.Drawing.Point(762, 564);
            this.button15.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(73, 70);
            this.button15.TabIndex = 9;
            this.button15.Text = "Commis";
            this.button15.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button15.UseVisualStyleBackColor = false;
            // 
            // button13
            // 
            this.button13.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button13.BackColor = System.Drawing.Color.Transparent;
            this.button13.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button13.BackgroundImage")));
            this.button13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button13.ForeColor = System.Drawing.Color.Red;
            this.button13.Location = new System.Drawing.Point(986, 13);
            this.button13.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(73, 70);
            this.button13.TabIndex = 8;
            this.button13.Text = "Hall";
            this.button13.UseVisualStyleBackColor = false;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button10
            // 
            this.button10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button10.BackColor = System.Drawing.Color.Transparent;
            this.button10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button10.BackgroundImage")));
            this.button10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button10.Location = new System.Drawing.Point(147, 378);
            this.button10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(73, 70);
            this.button10.TabIndex = 7;
            this.button10.UseVisualStyleBackColor = false;
            // 
            // button9
            // 
            this.button9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button9.BackColor = System.Drawing.Color.Transparent;
            this.button9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button9.BackgroundImage")));
            this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button9.Location = new System.Drawing.Point(285, 378);
            this.button9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(73, 70);
            this.button9.TabIndex = 6;
            this.button9.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            this.button8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button8.BackColor = System.Drawing.Color.Transparent;
            this.button8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button8.BackgroundImage")));
            this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.ForeColor = System.Drawing.Color.Red;
            this.button8.Location = new System.Drawing.Point(432, 378);
            this.button8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(73, 70);
            this.button8.TabIndex = 5;
            this.button8.Text = "Commande";
            this.button8.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button7.BackColor = System.Drawing.Color.Transparent;
            this.button7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button7.BackgroundImage")));
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.Color.Red;
            this.button7.Location = new System.Drawing.Point(580, 376);
            this.button7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(73, 70);
            this.button7.TabIndex = 4;
            this.button7.Text = "Liste_menu";
            this.button7.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            this.button6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button6.BackColor = System.Drawing.Color.Transparent;
            this.button6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button6.BackgroundImage")));
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button6.Location = new System.Drawing.Point(743, 378);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(73, 70);
            this.button6.TabIndex = 3;
            this.button6.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.Red;
            this.button5.Location = new System.Drawing.Point(909, 376);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(73, 70);
            this.button5.TabIndex = 2;
            this.button5.Text = "stock_plat";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(147, 27);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(73, 70);
            this.button2.TabIndex = 1;
            this.button2.Tag = "1";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(27, 28);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 70);
            this.button1.TabIndex = 0;
            this.button1.Tag = "0";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(1095, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(483, 745);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Location = new System.Drawing.Point(7, 89);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(469, 240);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.Controls.Add(this.button14, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.button12, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.button3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.button4, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.button11, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.textBox1, 5, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(7, 8);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(468, 57);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(311, 3);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(71, 50);
            this.button14.TabIndex = 8;
            this.button14.Text = "PLAY";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(234, 3);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(71, 50);
            this.button12.TabIndex = 7;
            this.button12.Text = "Speed x1";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(4, 4);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(69, 49);
            this.button3.TabIndex = 1;
            this.button3.Text = "Clear label";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(80, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(71, 50);
            this.button4.TabIndex = 5;
            this.button4.Text = "Clear Boutons";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(157, 3);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(71, 50);
            this.button11.TabIndex = 6;
            this.button11.Text = "Refresh";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.Location = new System.Drawing.Point(390, 17);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(72, 22);
            this.textBox1.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Silver;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 346);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(475, 395);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1582, 753);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Restaurant";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }
               
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void BoutonClick(string tag)
        {
            lastUsedTag = Int32.Parse(tag);
            generateButton(world.GetTableContent(lastUsedTag));
        }

        private void RefreshLastClickedElement()
        {
            BoutonClick(lastUsedTag.ToString());
        }

        private void generateButton(List<string[]> tableau)
        {
            //boucle pour générer les boutons
            tableLayoutPanel2.Controls.Clear();

            bool stillButton = false;

            foreach(string[] infos in tableau)
            {
                var button = new Button();
                button.AutoSize = true;
                //button.Size = new Size(75, 23);
                //button.Location = new Point(0, 50);
                button.Click += new EventHandler(boutonClient_click);
                //Console.WriteLine(infos[0] + " " + infos[1]);
                button.Text = infos[0];
                button.Tag = infos[1];

                if(infos[1] == lastGeneratedButtonTag)
                {
                    stillButton = true;
                }

                tableLayoutPanel2.Controls.Add(button);
            }

            if(stillButton == true)
            {
                UpdateLabel(lastGeneratedButtonTag);
            }
            else
            {
                label1.Text = "";
                lastGeneratedButtonTag = "";
            }
        }

        private void envoietag()
        {
            //Console.WriteLine((sender as Button).Tag);
            string tagg;
            tagg = button1.Tag.ToString();
            label1.Text = tagg;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //affiche en cliquant sur un bouton
            //label1.Text = testVariable1 + "      " + testVariable2;
            //this.envoietag();
            BoutonClick((sender as Button).Tag.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //label1.Text = testVariable1;
            //this.generateButton(tableau);
            BoutonClick((sender as Button).Tag.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //bontou clear le label
            label1.Text = "";
        }



        private void button13_Click(object sender, EventArgs e)
        {
            // bouton hall
            // nbres clients + afficher le bouton maitre d'hotel
            tableLayoutPanel2.Controls.Clear();
            Button btnHall = new Button();
            tableLayoutPanel2.Controls.Add(btnHall);
            //btnHall.Text = tableau[0,0];
        }
        private void boutonClient_click(object sender, EventArgs e)
        {
            //label1.Text = tablabel[1, 1] + "\n" + tablabel[0,0];
            UpdateLabel((sender as Button).Tag.ToString());
            lastGeneratedButtonTag = (sender as Button).Tag.ToString();
        }

        private void UpdateLabel(string tag)
        {
            List<String[]> infos = world.GenerateInfosElement(Int32.Parse(tag));
            string textToPrint = "";

            foreach (String[] info in infos)
            {
                textToPrint += info[0] + " : " + info[1] + "\n";
            }
            label1.Text = textToPrint;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //bouton clear les bouton dans le tableau
            tableLayoutPanel2.Controls.Clear();
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            RefreshLastClickedElement();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(speedUp == false)
            {
                speedUp = true;
                world.SetSpeedUp(true);
                (sender as Button).Text = "Speed x10";
            }
            else
            {
                speedUp = false;
                world.SetSpeedUp(false);
                (sender as Button).Text = "Speed x1";
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (pause == false)
            {
                pause = true;
                world.SetPause(true);
                (sender as Button).Text = "PAUSE";
            }
            else
            {
                pause = false;
                world.SetPause(false);
                (sender as Button).Text = "PLAY";
            }
        }

        public void RefreshAll()
        {
            RefreshLastClickedElement();
        }

        public void RefreshAllEvent()
        {
            this.Invoke(new MethodInvoker(delegate{ RefreshAll(); }));
        }
    }
}
