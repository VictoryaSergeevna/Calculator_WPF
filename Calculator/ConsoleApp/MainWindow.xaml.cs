using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConsoleApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string prevOper = null;
        string strFistNum = null;
        string strSecondNum = null;
        string mathSymb = null;//знаки
        double fistNum = 0;
        double secondNum = 0;
        bool flagNumbers = true;
        bool flagFraction = true;
        bool flagResult = false;
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name == "btnDiv" || btn.Name == "btnMult" || btn.Name == "btnMinus" || btn.Name == "btnPlus")
            {

                if (flagNumbers)
                {
                    mathSymb = btn.Content.ToString();
                    PreviousOperation();
                    flagNumbers = false;
                    flagResult = false;
                    strSecondNum = null;
                    flagFraction = true;
                    secondNum = 0;
                }
                else
                {
                    Calculation();
                    mathSymb = null;
                    if (String.IsNullOrEmpty(mathSymb))
                    {
                        mathSymb = btn.Content.ToString();
                        PreviousOperation();
                    }
                    flagResult = false;
                    strSecondNum = null;
                    secondNum = 0;
                    flagFraction = true;
                }

            }
            else if (btn.Name == "btnCE")
            {
                if (flagNumbers)
                {
                    strFistNum = null;
                    fistNum = 0;
                    txtboxCalc.Text = "0";
                }
                else
                {
                    strSecondNum = null;
                    secondNum = 0;
                    txtboxCalc.Text = "0";
                }
            }
            else if (btn.Name == "btnC")
            {
                flagNumbers = true;
                strFistNum = null;
                strSecondNum = null;
                fistNum = 0;
                secondNum = 0;
                txtboxCalc.Text = "0";
                txtboxPrevOper.Text = null;
                prevOper = null;
            }
            else if (btn.Name == "btnEqually")
            {
                if (String.IsNullOrEmpty(strSecondNum)) { return; }
                Calculation();
                flagNumbers = true;
                flagResult= true;
                txtboxPrevOper.Text = null;
                prevOper = null;
            }
            else if (btn.Name == "btnClearNum")
            {
                if (flagNumbers)
                {
                    if (String.IsNullOrEmpty(strFistNum)) { return; }
                    if ((strFistNum.Length - 1) == 0) {
                        fistNum = 0; 
                        txtboxCalc.Text = "0";
                        strFistNum = null;
                    }
                    else
                    {
                        strFistNum = strFistNum.Remove(strFistNum.Length - 1, 1);
                        fistNum = Convert.ToDouble(strFistNum);
                        txtboxCalc.Text = strFistNum;
                    }

                }
                else
                {
                    if (String.IsNullOrEmpty(strSecondNum)) { return; }
                    if ((strSecondNum.Length - 1) == 0) {
                        secondNum = 0;
                        txtboxCalc.Text = "0";
                        strSecondNum = null;
                    }
                    else
                    {
                        strSecondNum = strSecondNum.Remove(strSecondNum.Length - 1, 1);
                        secondNum = Convert.ToDouble(strSecondNum);
                        txtboxCalc.Text = strSecondNum;
                    }
                }
            }
            else if (btn.Name == "btnPoint")
            {
                if (flagFraction)
                {
                    if (flagNumbers)
                    {
                        if (String.IsNullOrEmpty(strFistNum))
                        {
                            strFistNum = "0" + btn.Content.ToString();
                        }
                        else
                        {
                            strFistNum += btn.Content.ToString();
                        }
                        txtboxCalc.Text = strFistNum;
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(strSecondNum))
                        {
                            strSecondNum = "0" + btn.Content.ToString();
                        }
                        else
                        {
                            strSecondNum += btn.Content.ToString();
                        }
                        txtboxCalc.Text = strSecondNum;
                    }
                    flagFraction = false;
                }
            }
            else
            {
                if (flagResult)
                {
                    strFistNum = null;
                    flagResult = false;
                    flagFraction = true;
                }
                if (flagNumbers)
                {
                    if (strFistNum == "0") { strFistNum = null; }
                    strFistNum += btn.Content.ToString();
                    txtboxCalc.Text = strFistNum;
                    fistNum = Convert.ToDouble(strFistNum);
                }
                else
                {
                    if (strSecondNum == "0") {
                        strSecondNum = null;
                    }
                    strSecondNum += btn.Content.ToString();
                    txtboxCalc.Text = strSecondNum;
                    secondNum = Convert.ToDouble(strSecondNum);
                }
            }
        }
        private void Calculation()
        {

            if ( mathSymb== "+")
            {
                fistNum += secondNum;
                strFistNum = fistNum.ToString();
                txtboxCalc.Text = strFistNum;
            }
            else if (mathSymb == "-")
            {
                fistNum -= secondNum;
                strFistNum = fistNum.ToString();
                txtboxCalc.Text = strFistNum;
            }
            else if (mathSymb == "*")
            {
                if (String.IsNullOrEmpty(strSecondNum)) { return; }
                fistNum *= secondNum;
                strFistNum = fistNum.ToString();
                txtboxCalc.Text = strFistNum;
            }
            else if (mathSymb == "/")
            {
                if (String.IsNullOrEmpty(strSecondNum)) { return; }
                if (strSecondNum == "0")
                {
                    flagNumbers = true;
                    flagResult = true;
                    txtboxPrevOper.Text = null;
                    prevOper = null;
                    txtboxCalc.Text = "Делить на 0 нельзя!";
                    strFistNum = null;
                    fistNum = 0;
                    return;
                }
                fistNum /= secondNum;
                strFistNum = fistNum.ToString();
                txtboxCalc.Text =strFistNum;
            }
        }
        private void PreviousOperation()
        {
            if (strFistNum == "0,") { strFistNum = strFistNum.TrimEnd(",".ToCharArray()); }
            if (strSecondNum == "0,") { strSecondNum = strSecondNum.TrimEnd(",".ToCharArray()); }
            if (flagNumbers)
            {
                if (String.IsNullOrEmpty(strFistNum)) { strFistNum = "0"; }
                prevOper += strFistNum + mathSymb;
                txtboxPrevOper.Text = prevOper;
            }
            else
            {
                if (String.IsNullOrEmpty(strSecondNum)) { prevOper = prevOper.Trim("+-/*".ToCharArray()); }
                prevOper += strSecondNum + mathSymb;
                txtboxPrevOper.Text = prevOper;
            }
        }
    }
}
