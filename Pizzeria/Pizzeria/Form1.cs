using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace Pizzeria
{
    
    public partial class Form1 : Form
    {
        new int Click = 0;
        public List<Person> Human = new List<Person>();
        public class Person
        {
            public Label Label = new Label();
            public TextBox FIO = new TextBox();
            public TextBox Address = new TextBox();
            public TextBox Pizza = new TextBox();
            public TextBox Diametr = new TextBox();
            public CheckBox Sauce = new CheckBox();
        }

        public List<Treatment> HumanTreat = new List<Treatment>();
        [Serializable]
        public class Treatment
        {
            public string Label;
            public string FIO;
            public string Address;
            public string Pizza;
            public string Diametr;
            public bool Sauce;
        }
        
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "XML files(*.xml)|*.xml";
            saveFileDialog1.Filter = "XML files(*.xml)|*.xml";  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Click++;
            Human.Add(new Person());
            Draw(Click - 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Delete(1);
        }

        private void Draw(int element)
        {
            tableLayoutPanel1.RowCount++;
            for (int column = 0; column < 6; column++)
            {
                tableLayoutPanel1.Controls.Add(Qwaqwa(element, column), column, tableLayoutPanel1.RowCount-2);
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                tableLayoutPanel1.RowStyles[1+ element].Height = 30;
                Qwaqwa(element, column).Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left);
                Qwaqwa(element, column).Font = new System.Drawing.Font("Times New Roman", this.Height / 30);
            }
            Human.ElementAt(element).Sauce.CheckAlign = ContentAlignment.MiddleCenter;
            Human.ElementAt(element).Label.Text = (tableLayoutPanel1.RowCount-2).ToString();
        }
        
        private void Delete(int Row)
        {
            for (int i = 0; i < Row; i++)
            {
                if (tableLayoutPanel1.RowCount > 2)
                {
                    tableLayoutPanel1.RowCount--;
                    for (int column = 0; column < 6; column++)
                        tableLayoutPanel1.Controls.Remove(Qwaqwa(Human.Count - 1, column));
                    Human.RemoveAt(Human.Count - 1);
                    Click--;
                }
            }
        }

        private void OpenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            Delete(tableLayoutPanel1.RowCount - 2);
            XmlSerializer formatter = new XmlSerializer(typeof(List<Treatment>));
            using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.OpenOrCreate))
            {
                HumanTreat = (List<Treatment>)formatter.Deserialize(fs);
            }
            Open();
            HumanTreat.Clear();
        }

        private void Open()
        {
            for (int i = 0; i < HumanTreat.Count; i++)
            {
                Human.Add(new Person());
                Human.ElementAt(i).Label.Text = HumanTreat.ElementAt(i).Label;
                Human.ElementAt(i).FIO.Text = HumanTreat.ElementAt(i).FIO;
                Human.ElementAt(i).Address.Text = HumanTreat.ElementAt(i).Address;
                Human.ElementAt(i).Pizza.Text = HumanTreat.ElementAt(i).Pizza;
                Human.ElementAt(i).Diametr.Text = HumanTreat.ElementAt(i).Diametr;
                Human.ElementAt(i).Sauce.Checked = HumanTreat.ElementAt(i).Sauce;
                Draw(i);
                Click++;
            }
        }

        private void SaveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Save();
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string fileNameSave = saveFileDialog1.FileName;
            XmlSerializer formatter = new XmlSerializer(typeof(List<Treatment>));
            using (FileStream fs = new FileStream(fileNameSave, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, HumanTreat);
            }
            HumanTreat.Clear();
        }

        private void Save()
        {
            for (int i = 0; i < Human.Count; i++)
            {
                HumanTreat.Add(new Treatment());
                HumanTreat.ElementAt(i).Label = Human.ElementAt(i).Label.Text;
                HumanTreat.ElementAt(i).FIO = Human.ElementAt(i).FIO.Text;
                HumanTreat.ElementAt(i).Address = Human.ElementAt(i).Address.Text;
                HumanTreat.ElementAt(i).Pizza = Human.ElementAt(i).Pizza.Text;
                HumanTreat.ElementAt(i).Diametr = Human.ElementAt(i).Diametr.Text;
                HumanTreat.ElementAt(i).Sauce = Human.ElementAt(i).Sauce.Checked;
            }
        }

        private Control Qwaqwa(int i, int column)
        {
            switch (column)
            {
                case 0:
                    return Human.ElementAt(i).Label;
                case 1:
                    return Human.ElementAt(i).FIO;
                case 2:
                    return Human.ElementAt(i).Address;
                case 3:
                    return Human.ElementAt(i).Pizza;
                case 4:
                    return Human.ElementAt(i).Diametr;
                default:
                    return Human.ElementAt(i).Sauce;
            }
        }
    }
}
