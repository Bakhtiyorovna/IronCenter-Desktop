using IronCenter.Service.CalculaturServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
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

namespace IronCenter.Desktop.Pages.Calculator
{
    /// <summary>
    /// Interaction logic for ArmaturaCalculator.xaml
    /// </summary>
    public partial class ArmaturaCalculator : Page
    {
        public double Length = 0;
        public double Weight = 0;
        public double d;
        public double density = 0.007850;

        public double result = 0;

        public string filePath = "D:\\Proekts\\DotNet\\IronCenter-Desktop\\src\\IronCenter.Service\\CalculaturServices\\CalculatorHistoryJson.json";

        public ArmaturaCalculator()
        {
            InitializeComponent();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            Title_Txb.Text = "Armatura uzunligini hisoblash";

            Label.Content = "og'irlik";
            Label_birlik.Content = "kg";

            Length = 1;
            Weight = 0;

            infoTxt.Text = "";
            ResultLbl.Content = "Ma'lumotlarni kiriting!";
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            Title_Txb.Text = "Armatura og'irligini hisoblash";

            Label.Content = "uzunlik l";
            Label_birlik.Content = "m";

            Weight = 1;
            Length = 0;

            infoTxt.Text = "";
            ResultLbl.Content = "Ma'lumotlarni kiriting!";
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            double testInfo = Convert.ToDouble(infoTxt.Text);
            double testD= Convert.ToDouble(diometrTxt.Text);

            
            if(Weight == 1 &&  testInfo > 0 && testD > 0 || (Length!=1 && testInfo > 0 && testD > 0)) 
            {
                d = Convert.ToDouble(diometrTxt.Text);
                Length=Convert.ToDouble(infoTxt.Text);
                Weight = Math.Round(Math.Pow((d / 2), 2) * Math.PI * density*Length,3);

                if (Weight > 0)
                {
                    ResultLbl.Content = "Og'irlik: " + Weight + "kg";
                    ResultLbl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#728CA4"));

                    try
                    {
                        string json = File.ReadAllText(filePath);
                        List<CalculatorHistory> calculatorHistory = JsonConvert.DeserializeObject<List<CalculatorHistory>>(json);
                        if (calculatorHistory is null)
                        {
                            CalculatorHistory value = new CalculatorHistory();
                            value.AssortimentName = "Armatura";
                            value.valueOne = d;
                            value.valueTwo = 0;
                            value.valueThree = Length;
                            value.valueFour = 0;
                            value.ResultvalueOne = Weight;
                            value.ResultvalueTwo = 0;

                            calculatorHistory = new List<CalculatorHistory>();
                            calculatorHistory.Add(value);
                            string updatedJson = JsonConvert.SerializeObject(calculatorHistory, Formatting.Indented);

                            File.WriteAllText(filePath, updatedJson);
                        }
                        else
                        {
                            CalculatorHistory value = new CalculatorHistory();
                            value.AssortimentName = "Armatura";
                            value.valueOne = d;
                            value.valueTwo = 0;
                            value.valueThree = Length;
                            value.valueFour = 0;
                            value.ResultvalueOne = Weight;
                            value.ResultvalueTwo = 0;

                            calculatorHistory.Add(value);

                            string updatedJson = JsonConvert.SerializeObject(calculatorHistory, Formatting.Indented);

                            File.WriteAllText(filePath, updatedJson);
                        }

                    }
                    catch
                    {
                        Title_Txb.Text = "Hisoblash tarixga qo'shilmadi";
                    }

                  
                }
                else
                {
                    ResultLbl.Content = "Malu'motlarni e'tibor bilan kiriting!";
                    ResultLbl.Foreground = new SolidColorBrush(Colors.Red);
                }
                Weight = 1;
                Length = 0;

            }
            else if(Length == 1 && testInfo > 0 && testD > 0 || Weight != 1 && testInfo > 0 && testD > 0)
            {
                d = Convert.ToDouble(diometrTxt.Text);
                Weight = Convert.ToDouble(infoTxt.Text);
                
                Length = Math.Round(Weight / (Math.Pow((d / 2), 2) * Math.PI * density),3);
                if (Length > 0)
                {
                    ResultLbl.Content = "Uzunlik: " + Length + "m";
                    ResultLbl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#728CA4"));

                    try
                    {
                        string json = File.ReadAllText(filePath);
                        List<CalculatorHistory> calculatorHistory = JsonConvert.DeserializeObject<List<CalculatorHistory>>(json);
                        if (calculatorHistory is null)
                        {
                            CalculatorHistory value = new CalculatorHistory();
                            value.AssortimentName = "Armatura";
                            value.valueOne = d;
                            value.valueTwo = Weight;
                            value.valueThree = 0;
                            value.valueFour = 0;
                            value.ResultvalueOne = 0;
                            value.ResultvalueTwo = Length;

                            calculatorHistory = new List<CalculatorHistory>();
                            calculatorHistory.Add(value);

                            string updatedJson = JsonConvert.SerializeObject(calculatorHistory, Formatting.Indented);

                            File.WriteAllText(filePath, updatedJson);
                        }
                        else
                        {
                            CalculatorHistory value = new CalculatorHistory();
                            value.AssortimentName = "Armatura";
                            value.valueOne = d;
                            value.valueTwo = Weight;
                            value.valueThree = 0;
                            value.valueFour = 0;
                            value.ResultvalueOne = 0;
                            value.ResultvalueTwo = Length;

                            calculatorHistory.Add(value);

                            string updatedJson = JsonConvert.SerializeObject(calculatorHistory, Formatting.Indented);

                            File.WriteAllText(filePath, updatedJson);
                        }
                    }
                    catch
                    {
                        Title_Txb.Text = "Hisoblash tarixga qo'shilmadi";
                    }
                }
                else
                {
                    ResultLbl.Content = "Malu'motlarni e'tibor bilan kiriting!";
                    ResultLbl.Foreground = new SolidColorBrush(Colors.Red);
                }
                Weight = 0;
                Length = 1;
            }
            else 
            {
                ResultLbl.Content = "Malu'motlarni e'tibor bilan kiriting!";
                ResultLbl.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
