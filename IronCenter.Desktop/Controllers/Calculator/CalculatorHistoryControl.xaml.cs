using IronCenter.Service.CalculaturServices;
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

namespace IronCenter.Desktop.Controllers.Calculator
{
   
    /// <summary>
    /// Interaction logic for CalculatorHistoryControl.xaml
    /// </summary>
    public partial class CalculatorHistoryControl : UserControl
    {
        public string assortimentName = "";
        public Func<Task> Refresh { get; set; }

        public CalculatorHistoryControl()
        {
            InitializeComponent();
        }

        public void SetData(CalculatorHistory calculatorHistory)
        {
            assortimentName=calculatorHistory.AssortimentName;
            if (assortimentName == "Armatura")
            {
                if (calculatorHistory.ResultvalueOne == 0)
                {
                    Title.Text = assortimentName + "  D=" + calculatorHistory.valueOne + "  Og'irlik=" + calculatorHistory.valueTwo+"kg";
                    ResultTitle.Text = "Uzunlik: " + calculatorHistory.ResultvalueTwo;
                }
                else
                {
                    Title.Text = assortimentName + "  D=" + calculatorHistory.valueOne + "  Uzunlik=" + calculatorHistory.valueThree+ "m";
                    ResultTitle.Text = "Og'irlik: " + calculatorHistory.ResultvalueOne;
                }
            }
            else if(assortimentName=="Dumaloq quvur")
            {
                if (calculatorHistory.valueThree == 0)
                {
                    Title.Text = assortimentName + " D=" + calculatorHistory.valueOne + " t=" + calculatorHistory.valueTwo + "Og'irlik= " + calculatorHistory.valueFour;
                    ResultTitle.Text = "Uzunlik=" + calculatorHistory.ResultvalueOne;
                }
                else
                {
                    Title.Text = assortimentName + " D=" + calculatorHistory.valueOne + " t=" + calculatorHistory.valueTwo + "Uzunlik= " + calculatorHistory.valueThree;
                    ResultTitle.Text = "Og'irlik=" + calculatorHistory.ResultvalueOne;
                }
            }

        }
    }
}
