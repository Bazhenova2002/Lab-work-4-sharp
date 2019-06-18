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

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string path;
        BinaryWriter bw;
        BinaryReader br;
        List<Square> square = new List<Square>();
        List<SquarePrism> prism = new List<SquarePrism>();
        bool flagOftb2 = false;

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog1.SelectedPath;
                tb1.Enabled = tb2.Enabled = bt3.Enabled = true;
            }
        }

        private void bt3_Click(object sender, EventArgs e)
        {
            int amountOfSquares, amountOfPrism;
            if (flagOftb2 == true)
            {
                for (int i = 0; i < dg1.RowCount; i++)
                {
                    square.Add(new Square(dg1[0, i].Value == null ? 1 : Convert.ToInt32(dg1[0, i].Value)));
                    bw.Write(square[i].Side);
                }
                bw.Write("-");
                for (int i = 0; i < dg2.RowCount; i++)
                {
                    prism.Add(new SquarePrism(dg2[0, i].Value == null ? 1 : Convert.ToInt32(dg2[0, i].Value), dg2[1, i].Value == null ? 1 : Convert.ToInt32(dg2[1, i].Value)));
                    bw.Write(prism[i].Height);
                    bw.Write(prism[i].Side);
                }
                dg1.Visible = dg2.Visible = false;
                dg1.Rows.Clear();
                dg2.Rows.Clear();
            }
            if (flagOftb2 == false)
            {
                if (tb1.Text != "" && tb2.Text != "")
                {
                    bw = new BinaryWriter(File.OpenWrite(path + "\\test.txt"));
                    bw.Write(tb1.Text);
                    bw.Write(tb2.Text);
                    amountOfSquares = Convert.ToInt32(tb1.Text);
                    amountOfPrism = Convert.ToInt32(tb2.Text);
                    tb1.Enabled = tb2.Enabled = false;
                    dg1.Rows.Add(amountOfSquares);
                    dg2.Rows.Add(amountOfPrism);
                    flagOftb2 = true;
                }
                else MessageBox.Show("Введите количество фигур");
            }
            if (square.Count != 0)
            {
                int maxS, number = 0;
                double maxP = 0;
                maxS = 0;
                textBox1.Visible = textBox2.Visible = label1.Visible = label2.Visible = label3.Visible = label4.Visible = textBox3.Visible = textBox4.Visible = true;
                for (int i = 0; i < square.Count; i++)
                {
                    if (square[i].Area() >= maxS)
                    {
                        maxS = square[i].Area();
                        number = i + 1;
                    }
                }
                textBox1.Text = number.ToString();
                textBox3.Text = maxS.ToString();
                number = 0;
                for (int i = 0; i < prism.Count; i++)
                {
                    if (prism[i].DiagonalPrism() >= maxP)
                    {
                        maxP = prism[i].DiagonalPrism();
                        number = i + 1;
                    }
                }
                textBox2.Text = number.ToString();
                textBox4.Text = maxP.ToString();
                bt3.Enabled = false;

            }
        }

        private void bt1_Click(object sender, EventArgs e)
        {
            textBox1.Visible = textBox2.Visible = label1.Visible = label2.Visible = label3.Visible = label4.Visible = textBox3.Visible = textBox4.Visible = false;
            bool flag = false;
            int countRowsSquare = 0;
            int countRowsPrism = 0;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                br = new BinaryReader(File.OpenRead(path));
                dg1.Rows.Add(br.ReadInt32());
                dg1.Rows.Add(br.ReadInt32());
                while (true)
                {
                    try
                    {
                        if (br.ReadString() == "-")
                        {
                            flag = true;
                            continue;
                        }
                        else if (flag == false)
                        {
                            dg1[0, countRowsPrism++].Value = br.ReadInt32();
                        }
                        else if (flag == true)
                        {
                            dg2[0, countRowsPrism].Value = br.ReadInt32();
                            dg2[1, countRowsPrism++].Value = br.ReadInt32();
                        }
                    }
                    catch (EndOfStreamException)
                    {
                        break;
                    }

                }
            }
        }
    }
}
