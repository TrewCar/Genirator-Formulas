using System.Diagnostics;
using System.Resources;

namespace WinFormsApp1
{
    public partial class RandomFormula : Form
    {
        static readonly Random rand = new();
        static readonly string[] Trigenometria = new string[]
        {
        "cos " , "sin " , "tg " , "ctg "
        };

        static readonly string[] DelUmn = new string[]
        {
        " / " , " * "
        };

        static readonly string[] PlusMinus = new string[]
        {
        " + " , " - "
        };
        static readonly string[] Skobki = new string[]
        {
        "( " , " )"
        };
        static readonly string sqrt = "sqrt ";
        static readonly string Stepen = " ^ ";
        public RandomFormula()
        {
            InitializeComponent();
        }

        private void Genirate_Click(object sender, EventArgs e)
        {
            int firsrtNumber = int.Parse(FirsrtNumber.Text);
            int secondNumber = int.Parse(SecondNumber.Text);
            int chisel = int.Parse(ChiselFormul.Text);

            int TrPr = int.Parse(TrirginomPr.Text);
            int NbPr = int.Parse(NumberPr.Text);
            int sqrtPr = int.Parse(SqrtPr.Text);
            int skobO = int.Parse(SkobkiO.Text);

            int PlMn = int.Parse(PlusMinusPr.Text);
            int DlUm = int.Parse(DelUmnPr.Text);
            int step = int.Parse(StepenPr.Text);
            int skobZ = int.Parse(SkobkaZ.Text);

            if (skobO == 100)
            {
                MessageBox.Show("При данном исходе всегда будут появлятся только скобки" +
                    " (будет вечный цикл)," +
                    " и из-за этого к вероятности появления плюсов и мнусов добавлено 5%", "Ошибка");
                skobZ -= 5;
                PlMn += 5;
                PlusMinusPr.Text = "5";
                SkobkaZ.Text = "95";
            }
            if (skobO == 100)
            {
                MessageBox.Show("При данном исходе всегда будут появлятся только скобки" +
                    " (будет вечный цикл)," +
                    " и из-за этого к вероятности появления цифр добавлено 5%", "Ошибка");
                skobO -= 5;
                NbPr += 5;
                NumberPr.Text = "5";
                SkobkiO.Text = "95";
            }
            int NumberElementSkobki = 0; //Счётчик открытых скобок
            int i = 0;
            string formula = string.Empty;

        backFirst:
            if(formula.EndsWith(")"))
            {
                goto rrr;
            }
            int first = rand.Next(0, 101);

            if(first >= 1 && first <= TrPr)
            {
                if (TrPr == 0)
                {
                    goto backFirst;
                }
                formula += Trigenometria[rand.Next(Trigenometria.Length)];
            }
            else if (first >= TrPr+1 && first <= (skobO + TrPr))
            {
                if (skobO == 0)
                {
                    goto backFirst;
                }
                
                formula += Skobki[0];
                NumberElementSkobki++;
            }
            else if (first <= 100 - NbPr-1 && first > (skobO + TrPr))
            {
                if (sqrtPr == 0)
                {
                    goto backFirst;
                }
                formula += sqrt;
            }
            else if (first>= 100 - NbPr)
            {
                if(NbPr == 0)
                {
                    goto backFirst;
                }

                formula += Convert.ToString(rand.Next(firsrtNumber, secondNumber));
            }            
            if (formula.EndsWith("( "))
            {
                goto backFirst;
            }
            if (formula.EndsWith("sqrt ") || formula.EndsWith("cos ") || formula.EndsWith("tg ") || formula.EndsWith("ctg ") || formula.EndsWith("sin ")) // Если да, добавить "( " или какую нибуть цифру и продолжить, если нет то просто продолжить
            {
                int rrr = rand.Next(0, 101);
                if(rrr>0 && rrr<= skobO)
                {
                    if (skobO == 0)
                    {
                        formula += Convert.ToString(rand.Next(firsrtNumber, secondNumber));
                        goto rrrr;
                    }
                    formula += Skobki[0];
                    NumberElementSkobki++;
                    goto backFirst;
                }
                else if ( rrr>= skobO + 1)
                {
                    
                    formula += Convert.ToString(rand.Next(firsrtNumber, secondNumber));
                }
                
            }
            rrrr:
            
            i++; //Счётчик цифр
            if (i == chisel || chisel == 0)
            {
                goto end;
            }

            
            rrr:
            int second = rand.Next(0, 101);

            if (second >= 0 && second <= skobZ)
            {
                if (skobZ == 0)
                {
                    goto rrr;
                }
                
                if (NumberElementSkobki == 0)
                {
                    goto rrr;
                }
                formula += Skobki[1];
                NumberElementSkobki--;
            }
            else if (second>=skobZ && second <= skobZ + PlMn)
            {
                if (PlMn == 0)
                {
                    goto rrr;
                }
                formula += PlusMinus[rand.Next(PlusMinus.Length)];
            }
            else if(second<= 100 - step - 1 && second > (skobZ + PlMn))
            {
                if (DlUm == 0)
                {
                    goto rrr;
                }
                formula += DelUmn[rand.Next(DelUmn.Length)];
            }
            else if ( second>= 100 - step)
            {
                if (step == 0)
                {
                    goto rrr;
                }
                formula += Stepen;
            }            
            goto backFirst;
        end:
            for (int g = 0; g < NumberElementSkobki; g++)
            {
                formula += " )";
            }
            
            if( chisel == 0)
            {
                Vivod.Text= string.Empty;
            }
            else
            {
                Vivod.Text = formula;
            }        
        }

        private void всегоЧиселToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Это в общем сколько чисел появится в формуле, числа под " +
                "тригинометрией или под корнем тоже считаются", "Всего чисел");
        }

        private void первыеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Это те знаки (они же числа), которые всегда в формулах " +
                "или в уровнениях всегда находятся перед" +
                " или после математического знака.", "\"Первые\" знаки");
        }

        private void вторыеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Это те знаки (он же математический знак)," +
                " которые всегда в формулах или в уровнениях всегда находятся после" +
                " или до последнего числа, он же не может появится перед всей формулой", "\"Вторые\" знки");
        }
    }
}