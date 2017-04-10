using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOKI_encryption
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Helper function out_Of_Range_Error
        public void out_Of_Range_Error()
        {
            // 重设数据
            OA1.Value = 38;
            OA2.Text = "A";
            OB1.Value = 1;
            OB2.Text = "A";
            OC.Value = 1;
            OD.Value = 10;
            OE.Value = 16;
            OF.Value = 1;
            OG.Value = 1;
            OH.Value = 1;

            // 报错
            V2.Text = "输入数据错误";
        }

        // Helper function trans1
        public String trans1(int i)
        {
            String substr;
            if (i < 10) substr = i.ToString();
            else substr = ((char)(i + 55)).ToString();
            return substr;
        }

        // Helper function trans2
        public int trans2(char c)
        {
            int i = 0;
            if (c > 64) i = (int)(c - 55);
            else i = int.Parse(c.ToString());
            return i;
        }

        // Helper function leap_Month_test
        public bool leap_Month_test(int y, int m, int d)
        {
            return ((m < 8 && (m % 2 == 0)) || m == 9 || m == 11) && d > 30 - 2 * Convert.ToInt32(m == 2) + Convert.ToInt32(m == 2 && y % 4 == 0);
        }

        


        private void button1_Click(object sender, EventArgs e)
        {
            // 长度为九的字符串
            String str = "", substr;
            int i;
            if (leap_Month_test((int)OE.Value, (int)OF.Value, (int)OG.Value))
            {
                V1.Text = "日期错误";
            } else
            {
                // 第一位
                i = (int)OA1.Value - 37;
                str += trans1(i);

                // 第二位
                substr = OA2.Text;
                i = (int)(substr.ToCharArray())[0];
                i = ((i - 65) * 10 + (int)OB1.Value);
                str += trans1(i);

                // 第三位
                substr = OB2.Text;
                i = (int)(substr.ToCharArray())[0];
                i = ((i - 65) * 10 + (int)OC.Value);
                str += trans1(i);

                // 第四位
                i = (int)OD.Value;
                i /= 5;
                str += trans1(i);

                // 第五位
                i = (int)OE.Value;
                str += trans1(i);

                // 第六位
                i = (int)OF.Value;
                str += trans1(i);

                // 第七位
                i = (int)OG.Value;
                str += trans1(i);

                // 第八位
                i = (int)OH.Value;
                i %= 36;
                str += trans1(i);

                // 第九位
                i = (int)OH.Value;
                i /= 36;
                str += trans1(i);

                // 返回转码
                V1.Text = str;
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (V1.Text.Length != 9)
            {
                V2.Text = "字符串长度不符合";
            }
            else
            {
                try
                {
                    // 长度为十八的字符串
                    String str = "", substr = V1.Text;
                    Char[] ca = substr.ToCharArray();
                    char c;
                    int i, j;
                    bool no_Error_Flag = true;

                    // 检查所有字母是否36进制
                    for (i = 0; i < ca.Length && no_Error_Flag; i += 1)
                    {
                        c = ca[i];
                        if (c < 48 || c > 90 || (c > 57 && c < 65))
                        {
                            out_Of_Range_Error();
                            no_Error_Flag = false;
                        }
                    }

                    // 第一、二位
                    if (no_Error_Flag)
                    {
                        c = ca[0];
                        i = trans2(c) + 37;
                        if (i > 48 || i < 38)
                        {
                            out_Of_Range_Error();
                            no_Error_Flag = false;
                        }
                        else
                        {
                            OA1.Value = i;
                            str += i.ToString();
                        }
                    }

                    // 第三、四位
                    if (no_Error_Flag)
                    {
                        c = ca[1];
                        i = trans2(c);
                        c = (char)(i / 10 + 65);
                        if (c < 'A' || c > 'D' || i % 10 < 1 || i % 10 > 5)
                        {
                            out_Of_Range_Error();
                            no_Error_Flag = false;
                        }
                        else
                        {
                            OA2.Text = c.ToString();
                            OB1.Value = i % 10;
                            substr = ((char)(i / 10 + 65)).ToString() + (i % 10).ToString();
                            str += substr;
                        }
                    }

                    // 第五、六位

                    if (no_Error_Flag)
                    {
                        c = ca[2];
                        i = trans2(c);
                        c = (char)(i / 10 + 65);
                        if (c < 'A' || c > 'D' || i % 10 < 1 || i % 10 > 3)
                        {
                            out_Of_Range_Error();
                            no_Error_Flag = false;
                        }
                        else
                        {
                            OB2.Text = c.ToString();
                            OC.Value = i % 10;
                            substr = ((char)(i / 10 + 65)).ToString() + (i % 10).ToString();
                            str += substr;
                        }
                    }

                    // 第七、八位

                    if (no_Error_Flag)
                    {
                        c = ca[3];
                        i = trans2(c) * 5;
                        if (i > 80 || i < 10)
                        {
                            out_Of_Range_Error();
                            no_Error_Flag = false;
                        }
                        else
                        {
                            OD.Value = i;
                            str += i.ToString();
                        }
                    }

                    // 第九、十位

                    if (no_Error_Flag)
                    {
                        c = ca[4];
                        i = trans2(c);
                        if (i > 35 || i < 0)
                        {
                            out_Of_Range_Error();
                            no_Error_Flag = false;
                        }
                        else
                        {
                            OE.Value = i;
                            if (i < 10) str += "0";
                            str += i.ToString();
                        }
                    }

                    // 第十一、十二位

                    if (no_Error_Flag)
                    {
                        c = ca[5];
                        i = trans2(c);
                        if (i > 12 || i < 1)
                        {
                            out_Of_Range_Error();
                            no_Error_Flag = false;
                        }
                        else
                        {
                            OF.Value = i;
                            if (i < 10) str += "0";
                            str += i.ToString();
                        }
                    }

                    // 第十三、十四位

                    if (no_Error_Flag)
                    {
                        c = ca[6];
                        i = trans2(c);
                        if (i > 31 || i < 1)
                        {
                            out_Of_Range_Error();
                            no_Error_Flag = false;
                        }
                        else
                        {
                            OG.Value = i;
                            if (i < 10) str += "0";
                            str += i.ToString();
                        }
                    }

                    // 测试闰年
                    if (leap_Month_test((int)OE.Value, (int)OF.Value, (int)OG.Value))
                    {
                        out_Of_Range_Error();
                        no_Error_Flag = false;
                    }

                    // 第十五到十八位
                    if (no_Error_Flag)
                    {
                        c = ca[7];
                        i = trans2(c);
                        c = ca[8];
                        j = trans2(c);
                        if (i > 35 || i < 0 || j > 35 || j < 0)
                        {
                            out_Of_Range_Error();
                            no_Error_Flag = false;
                        }
                        else
                        {
                            i += j * 36;
                            OH.Value = i;
                            if (i < 10) str += "000";
                            else if (i < 100) str += "00";
                            else if (i < 1000) str += "0";
                            str += i.ToString();
                        }
                    }

                    // 返回转码
                    if (no_Error_Flag) V2.Text = str;
                }
                catch (Exception ex) 
                {
                    out_Of_Range_Error();
                }
            }

            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void UX_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OA1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
