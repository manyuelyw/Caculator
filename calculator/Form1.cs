using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator
{
    public partial class Form1 : Form
    {
        double number_right = 0, number_left = 0, number = 0, result = 0;
        int click_number = 0;
        enum Operator {none, plus, minus, multiply, division, remainder, daoshu};//枚举量：运算符
        Operator mode = Operator.none;
        bool hasmoded = false;  //标记当前操作之前是否为“按下运算符”
        bool pre_op = false;    //标记当前操作之前是否为“按下=”
        bool ds = false;        //标记当前操作之前是否为“按下倒数运算符”
        int squre_num = 0;      //记录对当前数的平方次数
        int square_root_num = 0;    //记录对当前数的平方根次数
        int has_point = 0;      //记录当前数是小数点后多少位
        int i = 0;              //遍历所用参数
        double t = 1;           //定义用来与number进行*/等操作调整数值的
        public Form1()
        {
            InitializeComponent();
        }

        public void exist_num(int an)       //当按下数字键(数值=an)
        {
            //当下label_out本身为"0"时，不做任何操作返回：
            if(number == 0&&an == 0&&!label_out.Text.Contains("."))
            {
                return;
            }
            
            //刚按下“=”，现在按下数字键：
            if(pre_op)
            {
                pre_op = false;
                number = 0;
            }
            
            //上一步正在进行“倒数运算符”：
            if(ds)
            {
                //label_mode显示正在运算时：清空右边运算值，变成当前值
                if(label_mode.Text.Contains("-") || label_mode.Text.Contains("+") || label_mode.Text.Contains("×") || label_mode.Text.Contains("÷"))
                {
                    string tmp = string.Concat("1/(", Convert.ToString(1/number));
                    tmp = string.Concat(tmp, ")");
                    label_mode.Text = label_mode.Text.Replace(tmp, "");
                    number = an;
                    label_out.Text = Convert.ToString(number);
                }
                //当前不在运算中，则不清空label_mode：
                else
                {
                    number = an;
                    label_out.Text = Convert.ToString(number);
                }
                //做完之后清空ds
                ds = false;
                return;
            }
            
            //上一步正在进行“平方运算”：
            if(squre_num>=1)
            {
                //做法同上倒数：
                if (label_mode.Text.Contains("-") || label_mode.Text.Contains("+") || label_mode.Text.Contains("×") || label_mode.Text.Contains("÷"))
                {
                    int start = label_mode.Text.IndexOf("sqrt");
                    string tmp = label_mode.Text.Substring(start, label_mode.Text.Length-start);
                    label_mode.Text = label_mode.Text.Replace(tmp, "");
                    number = an;
                    label_out.Text = Convert.ToString(number);
                }
                else
                {
                    number = an;
                    label_out.Text = Convert.ToString(number);
                }
                squre_num = 0;
                return;
            }

            //上一步正在进行“平方根运算”：
            if (square_root_num >= 1)
            {
                //做法同上：
                if (label_mode.Text.Contains("-") || label_mode.Text.Contains("+") || label_mode.Text.Contains("×") || label_mode.Text.Contains("÷"))
                {
                    int start = label_mode.Text.IndexOf("sqrtroot");
                    string tmp = label_mode.Text.Substring(start, label_mode.Text.Length - start);
                    label_mode.Text = label_mode.Text.Replace(tmp, "");
                    number = an;
                    label_out.Text = Convert.ToString(number);
                }
                else
                {
                    number = an;
                    label_out.Text = Convert.ToString(number);
                }
                square_root_num = 0;
                return;
            }

            //如果上一步按下了“运算键”：考虑小数还是整数的清空
            if (hasmoded)
            {
                number = 0;
                if (has_point != 0)
                {
                    number = 0.1;
                    label_out.Text = "0.";
                    return;
                }
                else
                {
                    number = number * 10 + an;
                }
                number_right = number;
                hasmoded = false;
            }
            
            //仅考虑小数还是整数清空，进行number的调整
            else
            {
                if(has_point >= 1)
                {
                    t = 1;
                    for(i = 0;i<has_point;i++)
                    {
                        t *= 10;
                    }
                    number = (number<0?-(-number + an / t) : number + an / t);
                    if (an == 0)
                    {
                        label_out.Text = string.Concat(label_out.Text, '0');
                        has_point++;
                        return;
                    }
                    has_point++;
                }
                else
                {
                    number = number * 10 + an;
                }
            }
            
            //控制label_out输出值
            label_out.Text = Convert.ToString(number);
        }

        private void num7_Click(object sender, EventArgs e)
        {
            click_number = 7;
            exist_num(click_number);
        }

        private void num8_Click(object sender, EventArgs e)
        {
            click_number = 8;
            exist_num(click_number);
        }

        private void num9_Click(object sender, EventArgs e)
        {
            click_number = 9;
            exist_num(click_number);
        }

        private void num4_Click(object sender, EventArgs e)
        {
            click_number = 4;
            exist_num(click_number);
        }

        private void num5_Click(object sender, EventArgs e)
        {
            click_number = 5;
            exist_num(click_number);
        }

        private void num6_Click(object sender, EventArgs e)
        {
            click_number = 6;
            exist_num(click_number);
        }

        private void num1_Click(object sender, EventArgs e)
        {
            click_number = 1;
            exist_num(click_number);
        }

        private void num2_Click(object sender, EventArgs e)
        {
            click_number = 2;
            exist_num(click_number);
        }

        private void num3_Click(object sender, EventArgs e)
        {
            click_number = 3;
            exist_num(click_number);
        }

        private void num0_Click(object sender, EventArgs e)
        {
            click_number = 0;
            exist_num(click_number);
        }

        private void clean_Click(object sender, EventArgs e)    //按下"CE"键：清空一切标记
        {
            number_right = number_left = number = 0;
            label_mode.Text = "";
            label_out.Text = Convert.ToString(number);
            pre_op = hasmoded = ds = false;
            mode = Operator.none;
            has_point = squre_num = square_root_num = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            label_out.Text = Convert.ToString(number);
        }

        private void division_Click(object sender, EventArgs e) //除法运算
        {
            //如果上一步按了“运算符”，则不做任何操作返回
            if (hasmoded)
            {
                return;
            }

            //以下为除法需要做的操作：
            has_point = 0;
            pre_op = false;
            mode = Operator.division;
            string left = Convert.ToString(number);
            string right = "÷";
            label_mode.Text = Convert.ToString(string.Concat(left, right));
            number_right = number_left = number;
            hasmoded = true;
            squre_num = square_root_num = 0;
            ds = false;
        }

        private void multiply_Click(object sender, EventArgs e) //乘法运算
        {
            if(hasmoded)
            {
                return;
            }
            has_point = 0;
            pre_op = false;
            mode = Operator.multiply;
            string left = Convert.ToString(number);
            string right = "×";
            label_mode.Text = Convert.ToString(string.Concat(left, right));
            number_right = number_left = number;
            hasmoded = true;
            squre_num = square_root_num = 0;
            ds = false;
        }

        private void minus_Click(object sender, EventArgs e)    //减法运算
        {
            if (hasmoded)
            {
                return;
            }
            has_point = 0;
            pre_op = false;
            mode = Operator.minus;
            string left = Convert.ToString(number);
            string right = "-";
            label_mode.Text = Convert.ToString(string.Concat(left, right));
            number_right = number_left = number;
            hasmoded = true;
            squre_num = square_root_num = 0;
            ds = false;
        }

        private void plus_Click(object sender, EventArgs e)     //加法运算
        {
            if (hasmoded)
            {
                return;
            }
            has_point = 0;
            pre_op = false;
            mode = Operator.plus;
            string left = Convert.ToString(number);
            string right = "+";
            label_mode.Text = Convert.ToString(string.Concat(left, right));
            number_right = number_left = number;
            hasmoded = true;
            squre_num = square_root_num = 0;
            ds = false;
        }

        private void equal_Click(object sender, EventArgs e)
        {
            //如果上一步为“倒数”运算，且未进行其它双目运算：则不用进行下面的运算过程，直接进行相应操作后返回
            if(ds && !label_mode.Text.Contains('-') && !label_mode.Text.Contains('+') && !label_mode.Text.Contains('×') && !label_mode.Text.Contains('÷'))
            {
                label_mode.Text = string.Concat(label_mode.Text, "=");
                ds = false;
                square_root_num = 0;
                squre_num = 0;
                mode = Operator.none;
                pre_op = true;
                return;
            }
            //同上，单目运算：平方
            if (squre_num>0 && !label_mode.Text.Contains('-') && !label_mode.Text.Contains('+') && !label_mode.Text.Contains('×') && !label_mode.Text.Contains('÷'))
            {
                label_mode.Text = string.Concat(label_mode.Text, "=");
                squre_num = 0;
                square_root_num = 0;
                ds = false;
                mode = Operator.none;
                pre_op = true;
                return;
            }
            //同上，单目运算：平方根
            if (square_root_num > 0 && !label_mode.Text.Contains('-') && !label_mode.Text.Contains('+') && !label_mode.Text.Contains('×') && !label_mode.Text.Contains('÷'))
            {
                label_mode.Text = string.Concat(label_mode.Text, "=");
                square_root_num = 0;
                squre_num = 0;
                ds = false;
                mode = Operator.none;
                pre_op = true;
                return;
            }
            
            //以下进行双目运算的分类讨论：
            number_right = number;
            switch (mode)
            {
                case Operator.plus:
                    result = number_left + number_right;
                    break;
                case Operator.minus:
                    result = number_left - number_right;
                    break;
                case Operator.multiply:
                    result = number_left * number_right;
                    break;
                case Operator.division:
                    if(number_right == 0)
                    {
                        label_out.Text = "除数不能为零！";
                        number = result = 0;
                        hasmoded = ds = false;
                        pre_op = true;
                        mode = Operator.none;
                        has_point = squre_num = square_root_num = 0;
                        return;
                    }
                    result = number_left / number_right;
                    break;
                case Operator.remainder:
                    result = number_left % number_right;
                    break;
                default:
                    result = number;
                    break;
            }
            label_out.Text = Convert.ToString(result);
            if(label_mode.Text.Contains("="))
            {
                label_mode.Text = "";
            }
            string tmp = label_mode.Text;
            if (!ds && squre_num == 0 && square_root_num == 0)
            {
                tmp = string.Concat(label_mode.Text, number_right);
            }
            label_mode.Text = string.Concat(tmp, "=");

            //运算完的相应标记位清空工作：
            number = result;
            hasmoded = ds = false;
            pre_op = true;
            mode = Operator.none;
            has_point = squre_num = square_root_num = 0;
        }

        private void negate_Click(object sender, EventArgs e)   //“-”单目运算：
        {
            if(number == 0 && !label_out.Text.Contains("."))
            {
                return;
            }
            number = -number;
            if(label_out.Text.Contains('-'))
            {
                label_out.Text = label_out.Text.Replace("-", "");
            }
            else
            {
                label_out.Text = string.Concat('-', label_out.Text);
            }
        }

        private void point_Click(object sender, EventArgs e)    //"."小数点操作
        {
            if(hasmoded)
            {
                label_out.Text = "0";
                number = 0;
                hasmoded = false;
            }
            if (ds)
            {
                if (label_mode.Text.Contains("+") || label_mode.Text.Contains("-") || label_mode.Text.Contains("×") || label_mode.Text.Contains("÷"))
                {
                    string to_replace = string.Concat("1/(", Convert.ToString(1 / number));
                    label_mode.Text = label_mode.Text.Replace(string.Concat(to_replace, ")"), "");
                }
                label_out.Text = "0";
                number = 0;
                ds = false;
                
            }
            if(squre_num>0)
            {
                if (label_mode.Text.Contains("+") || label_mode.Text.Contains("-") || label_mode.Text.Contains("×") || label_mode.Text.Contains("÷"))
                {
                    int start = label_mode.Text.IndexOf("sqrt");
                    string temp = label_mode.Text.Substring(start, label_mode.Text.Length-start);
                    label_mode.Text = label_mode.Text.Replace(temp, "");
                }
                label_out.Text = "0";
                number = 0;
                squre_num = 0;
            }
            if(square_root_num>0)
            {
                if (label_mode.Text.Contains("+") || label_mode.Text.Contains("-") || label_mode.Text.Contains("×") || label_mode.Text.Contains("÷"))
                {
                    int start = label_mode.Text.IndexOf("sqrtroot");
                    string temp = label_mode.Text.Substring(start, label_mode.Text.Length - start);
                    label_mode.Text = label_mode.Text.Replace(temp, "");
                }
                label_out.Text = "0";
                number = 0;
                square_root_num = 0;
            }
            if(has_point>=1)
            {
                return;
            }
            if(pre_op && number != 0)
            {
                number = 0;
                label_out.Text = "0";
            }
            has_point ++;
            label_out.Text = string.Concat(label_out.Text, '.');
        }

        private void clear_part_Click(object sender, EventArgs e)
        {
            number = 0;
            label_out.Text = Convert.ToString(number);
            has_point = 0;
            hasmoded = true;
            pre_op = false;
            ds = false;
            squre_num = 0;
            square_root_num = 0;
            mode = Operator.none;
        }

        private void delete_Click(object sender, EventArgs e)   //删除一位数字“delete"操作：
        {
            char last_ch = label_out.Text.ElementAt(label_out.Text.Length-1);
            if (label_out.Text == "0")
            {
                return;
            }
            else if (label_out.Text.Length == 2 && label_out.Text.Substring(0, 1) == "-" || label_out.Text == "-0.")
            {
                label_out.Text = "0";
                number = 0;
                has_point = 0;
            }
            else if (last_ch == '.')
            {
                label_out.Text = label_out.Text.Substring(0, label_out.Text.Length - 1);
                has_point = 0;
            }
            else
            {
                if(label_out.Text.Length == 1)
                {
                    label_out.Text = "0";
                    number = 0;
                }
                else if(!label_out.Text.Contains('.'))
                {
                    int result = ((int)number )/ 10;
                    label_out.Text = Convert.ToString(result);
                    number = result;
                }
                else
                {
                    if(label_out.Text.ElementAt(label_out.Text.Length-1) == '0')
                    {
                        label_out.Text = label_out.Text.Substring(0, label_out.Text.Length - 1);
                        has_point--;
                        return;
                    }
                    
                    int weishu = label_out.Text.IndexOf('.');
                    int last_len = label_out.Text.Length - weishu-2<=0?0: label_out.Text.Length - weishu - 2;
                    number = Math.Round(number, last_len);
                    if(label_out.Text.ElementAt(label_out.Text.Length-1)!='.'&&label_out.Text.ElementAt(label_out.Text.Length - 1) >='5')
                    {
                        double temp = 1;
                        for(int j=0;j<last_len;j++)
                        {
                            temp = temp * 0.1;
                        }
                        if(!label_out.Text.Contains('-')&& last_len % 2 == 1)
                        {
                            number = number - temp;
                        }
                        else if(label_out.Text.Contains('-')&&last_len %2 == 1)
                        {
                            number = number + temp;
                        }
                    }
                    has_point--;
                    label_out.Text = label_out.Text.Substring(0, label_out.Text.Length - 1);
                }
            }
        }

        private void remainder_Click(object sender, EventArgs e)    //"%"取余运算：
        {
            if (hasmoded)
            {
                return;
            }
            has_point = 0;
            pre_op = false;
            mode = Operator.remainder;
            string left = "";
            if (number >= 0)
            {
                left = Convert.ToString(number);
            }
            else
            {
                left = "negate(" + Convert.ToString(number) + ")";
            }
            string right = "%";
            label_mode.Text = Convert.ToString(string.Concat(left, right));
            number_right = number_left = number;
            hasmoded = true;
            squre_num = 0;
            square_root_num = 0;
            ds = false;
        }

        private void daoshu_Click(object sender, EventArgs e)       //倒数运算：
        {
            has_point = 0;
            if(label_mode.Text.Contains("="))
            {
                pre_op = false;
                label_mode.Text = "";
            }
            if (ds)
            {
                string to_ds = "";
                if (mode != Operator.none)
                {
                    char split_ch = ' ';
                    switch (mode)
                    {
                        case Operator.plus:
                            split_ch = '+';
                            break;
                        case Operator.minus:
                            split_ch = '-';
                            break;
                        case Operator.multiply:
                            split_ch = '×';
                            break;
                        case Operator.division:
                            split_ch = '÷';
                            break;
                        default:
                            break;
                    }
                    string[] tmp = label_mode.Text.Split(split_ch);
                    to_ds = tmp[1];
                }
                else
                {
                    to_ds = label_mode.Text != "" ? label_mode.Text : Convert.ToString(number);
                }
                string to_replace = "1/(";
                to_replace = to_replace + to_ds + ")";
                label_mode.Text = label_mode.Text == "" ? to_replace : label_mode.Text.Replace(to_ds, to_replace);

            }
            else
            {
                string to_ds = "";
                if(mode != Operator.none&&hasmoded == false)
                {
                    char split_ch = ' ';
                    switch (mode)
                    {
                        case Operator.plus:
                            split_ch = '+';
                            break;
                        case Operator.minus:
                            split_ch = '-';
                            break;
                        case Operator.multiply:
                            split_ch = '×';
                            break;
                        case Operator.division:
                            split_ch = '÷';
                            break;
                        default:
                            break;
                    }
                    string[] tmp = label_mode.Text.Split(split_ch);
                    to_ds = tmp[1];
                    string right = "1/(";
                    right = string.Concat(right, to_ds);
                    right = string.Concat(right, ")");
                    if (to_ds != "")
                    {
                        label_mode.Text = label_mode.Text.Replace(to_ds, right);
                    }
                    else
                    {
                        label_mode.Text = label_mode.Text + "1/(" + Convert.ToString(number) + ")";
                    }
                }
                else
                {
                    string right = "1/(";
                    right = string.Concat(right, label_mode.Text==""?Convert.ToString(number):label_mode.Text);
                    right = string.Concat(right, ")");
                    label_mode.Text = right;
                }
            }
            if (number == 0)
            {
                label_out.Text = "除数不能为零！";
                hasmoded = false;
                ds = true;
                number = number_left = number_right = 0;
                return;
            }
            number = 1 / number;
            number_right = number;
            label_out.Text = Convert.ToString(number);
            hasmoded = false;
            ds = true;
        }

        private void square_Click(object sender, EventArgs e)       //平方运算：
        {
            has_point = 0;
            if (label_mode.Text.Contains("="))
            {
                pre_op = false;
                label_mode.Text = "";
            }
            if (squre_num>=1)
            {
                string to_squre = "";
                if (mode != Operator.none)
                {
                    char split_ch = ' ';
                    switch (mode)
                    {
                        case Operator.plus:
                            split_ch = '+';
                            break;
                        case Operator.minus:
                            split_ch = '-';
                            break;
                        case Operator.multiply:
                            split_ch = '×';
                            break;
                        case Operator.division:
                            split_ch = '÷';
                            break;
                        default:
                            break;
                    }
                    string[] tmp = label_mode.Text.Split(split_ch);
                    to_squre = tmp[1];
                }
                else
                {
                    to_squre = label_mode.Text != "" ? label_mode.Text : Convert.ToString(number);
                }
                string to_replace = "sqrt(";
                to_replace = to_replace + to_squre + ")";
                label_mode.Text = label_mode.Text == "" ? to_replace : label_mode.Text.Replace(to_squre, to_replace);
            }
            else
            {
                string to_ds = "";
                if (mode != Operator.none && hasmoded == false)
                {
                    char split_ch = ' ';
                    switch (mode)
                    {
                        case Operator.plus:
                            split_ch = '+';
                            break;
                        case Operator.minus:
                            split_ch = '-';
                            break;
                        case Operator.multiply:
                            split_ch = '×';
                            break;
                        case Operator.division:
                            split_ch = '÷';
                            break;
                        default:
                            break;
                    }
                    string[] tmp = label_mode.Text.Split(split_ch);
                    to_ds = tmp[1];
                    string right = "sqrt(";
                    right = string.Concat(right, to_ds);
                    right = string.Concat(right, ")");
                    if(to_ds!="")
                    {
                        label_mode.Text = label_mode.Text.Replace(to_ds, right);
                    }
                    else
                    {
                        label_mode.Text = label_mode.Text + "sqrt(" + Convert.ToString(number) + ")";
                    }
                }
                else
                {
                    string right = "sqrt(";
                    right = string.Concat(right, label_mode.Text == "" ? Convert.ToString(number) :label_mode.Text) ;
                    right = string.Concat(right, ")");
                    label_mode.Text=right;
                }
            }
            number = number * number;
            number_right = number;
            if(number>double.MaxValue||number<double.MinValue)
            {
                label_out.Text = "数字溢出！";
                label_mode.Text = "";
                squre_num = 0;
                number = number_left = number_right = 0;
                hasmoded = false;
                return;
            }
            label_out.Text = Convert.ToString(number);
            hasmoded = false;
            squre_num++;
        }

        private void square_root_Click(object sender, EventArgs e)      //平方根运算：
        {
            has_point = 0;

            //按照windows计算器规则，当label_mode中有等号时，点击“平方根”运算，label_mode会清空原内容为当前运算内容
            if (label_mode.Text.Contains("="))
            {
                pre_op = false;
                label_mode.Text = "";
            }

            //如果连续进行多次“平方根”运算，则label_mode的输出内容需要以下分类讨论：
            if (square_root_num >= 1)
            {
                string to_squre_root = "";

                //正在进行双目运算，当前进行“平方根”的为右边的运算数：
                if (mode != Operator.none)
                {
                    char split_ch = ' ';
                    switch (mode)
                    {
                        case Operator.plus:
                            split_ch = '+';
                            break;
                        case Operator.minus:
                            split_ch = '-';
                            break;
                        case Operator.multiply:
                            split_ch = '×';
                            break;
                        case Operator.division:
                            split_ch = '÷';
                            break;
                        default:
                            break;
                    }
                    string[] tmp = label_mode.Text.Split(split_ch);
                    to_squre_root = tmp[1];
                }
                else
                {
                    to_squre_root = label_mode.Text != "" ? label_mode.Text : Convert.ToString(number);
                }
                string to_replace = "sqrtroot(";
                to_replace = to_replace + to_squre_root + ")";
                label_mode.Text = label_mode.Text == "" ? to_replace : label_mode.Text.Replace(to_squre_root, to_replace);
            }
            else
            {
                string to_ds = "";
                if (mode != Operator.none && hasmoded == false)
                {
                    char split_ch = ' ';
                    switch (mode)
                    {
                        case Operator.plus:
                            split_ch = '+';
                            break;
                        case Operator.minus:
                            split_ch = '-';
                            break;
                        case Operator.multiply:
                            split_ch = '×';
                            break;
                        case Operator.division:
                            split_ch = '÷';
                            break;
                        default:
                            break;
                    }
                    string[] tmp = label_mode.Text.Split(split_ch);
                    to_ds = tmp[1];
                    string right = "sqrtroot(";
                    right = string.Concat(right, to_ds);
                    right = string.Concat(right, ")");
                    if (to_ds != "")
                    {
                        label_mode.Text = label_mode.Text.Replace(to_ds, right);
                    }
                    else
                    {
                        label_mode.Text = label_mode.Text + "sqrtroot(" + Convert.ToString(number) + ")";
                    }
                }
                else
                {
                    string right = "sqrtroot(";
                    right = string.Concat(right, label_mode.Text==""?Convert.ToString(number):label_mode.Text);
                    right = string.Concat(right, ")");
                    label_mode.Text = right;
                }
            }
            if(number<0)
            {
                label_out.Text = "无效输入！";
                number = number_left = number_right = 0;
                hasmoded = false;
                return;
            }
            number = Math.Sqrt(number);
            number_right = number;
            label_out.Text = Convert.ToString(number);
            hasmoded = false;
            square_root_num++;

        }
    }
}
