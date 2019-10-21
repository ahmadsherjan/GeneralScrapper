using MyClasses;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Scrappers.Base.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;
using ValueType = Scrappers.Base.Classes.ValueType;

namespace General_Scrapper.Forms
{
    public partial class StepsRunner : Form
    {
        private IWebDriver _driver = null;
        private List<string> lines;
        public StepsRunner()
        {
            InitializeComponent();
            FormLoad();
            lines = new List<string>();
        }
        private void FormLoad()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 3, 0);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var json = "";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Please a step file");
            }
            json = File.ReadAllText(openFileDialog1.FileName);
            lines = new List<string>();

            Queue<AllOperation> operations = new Queue<AllOperation>();
            //operations = JsonConvert.DeserializeObject<Queue<AllOperation>>(json);
            operations.Enqueue(new AllOperation
            {
                Value = "https://www.google.com/",
                ValueType = ValueType.Url,
                UrlOperationType = UrlOperationType.Redirect
            });

            operations.Enqueue(new AllOperation
            {
                Value = "input[title=Search]",
                Text = "C#",
                ValueType = ValueType.Selenium,
                SeleniumSelector = SeleniumSelector.JSSelector,
                SeleniumOperationType = SeleniumOperationType.SetText
            });
            operations.Enqueue(new AllOperation
            {
                Value = "input[type=submit]",
                Text = "C#",
                ValueType = ValueType.Selenium,
                SeleniumSelector = SeleniumSelector.JSSelector,
                SeleniumOperationType = SeleniumOperationType.Click
            });
            operations.Enqueue(new AllOperation
            {
                Value = "<a href=\"(?<url>.*?)\" ping=",
                Text = "url",
                Expressions = new Dictionary<string, string>() { 
                    {"url", "<a href=\"(?<url>.*?)\" ping=" }, 
                    {"url1", "<a href=\"(?<url1>.*?)\" ping="},
                    {"url2", "<a href=\"(?<url2>.*?)\" ping=" }
                },
                ValueType = ValueType.Regex,
                RegexOperationType = RegexOperationType.Extract,
                DependsOnChild = true,
                Nested = new AllOperation()
                {
                    ValueType = ValueType.Compare,
                    RegexOperationType = RegexOperationType.NotNull,
                    Value = "<a[^>]*id=\"pnnext\"[^>]*>(?<data>.*?)</a>",
                    Text = "data",
                    Nested = new AllOperation
                    {
                        Value = "a[id=pnnext]",
                        Text = "C#",
                        ValueType = ValueType.Selenium,
                        SeleniumSelector = SeleniumSelector.JSSelector,
                        SeleniumOperationType = SeleniumOperationType.Click
                    }

                }
            }) ;
            //json = JsonConvert.SerializeObject(operations);
            operations = JsonConvert.DeserializeObject<Queue<AllOperation>>(json);
            while (operations.Count > 0)
            {
                var op = operations.Dequeue();
                PerformOperation(op);
            }
            MessageBox.Show("Completed " + lines.Count);
        }
        private bool PerformOperation(BaseOperation operation)
        {
            Thread.Sleep(operation.WaitInSeconds * 1000);
            switch (operation.ValueType)
            {
                case ValueType.Url:
                    var uop = (AllOperation)operation;
                    switch (uop.UrlOperationType)
                    {
                        case UrlOperationType.Redirect:
                            _driver.Navigate().GoToUrl(uop.Value);
                            break;
                    }
                    break;
                case ValueType.Regex:
                    var rop = (AllOperation)operation;
                    switch (rop.RegexOperationType)
                    {
                        case RegexOperationType.Extract:
                            var tempList = new Dictionary<string, string[]>();

                            foreach (var key in rop.Expressions.Keys)
                            {
                                try
                                {
                                    var data = MyUtilityMethods.getListFromPage(_driver.PageSource, rop.Expressions[key], key);
                                    tempList.Add(key, data);
                                }catch(Exception exp) { }
                            }
                            var longest = tempList.Max(x => x.Value == null ? 0 : x.Value.Length);
                            var format = string.Join(",", Enumerable.Range(0, rop.Expressions.Keys.Count).Select(x => "{" + x + "}").ToArray());
                            for (int i = 0; i < longest; i++)
                            {
                                try
                                {
                                    var temp = tempList.Keys.Select(x => tempList[x].Length > i ? tempList[x][i] : "").ToArray();
                                    var line = string.Format(format, temp);
                                    lines.Add(line);
                                }catch(Exception exp) { }
                            }
                            break;
                    }
                    break;

                case ValueType.Selenium:
                    var sop = (AllOperation)operation;
                    switch (sop.SeleniumOperationType)
                    {
                        case SeleniumOperationType.Click:
                            DriverUtility.ClickByQuery(_driver, sop.Value);
                            break;
                        case SeleniumOperationType.SetText:
                            DriverUtility.SetTextByQuery(_driver, sop.Value, sop.Text);
                            break;
                    }
                    break;
                case ValueType.Compare:
                    var cop = (AllOperation)operation;
                    switch (cop.RegexOperationType)
                    {
                        case RegexOperationType.IsNull:
                            var data = MyUtilityMethods.getListFromPage(_driver.PageSource, cop.Value, cop.Text);
                            if (data == null)
                            {
                                if (cop.Nested != null)
                                    PerformOperation(cop.Nested);
                                return true;
                            }
                            return false;
                        case RegexOperationType.NotNull:
                            data = MyUtilityMethods.getListFromPage(_driver.PageSource, cop.Value, cop.Text);
                            if (data != null)
                            {
                                if (cop.Nested != null)
                                    PerformOperation(cop.Nested);
                                return true;
                            }
                            return false;
                    }
                    break;
            }

            if (operation.DependsOnChild)
            {
                if (PerformOperation(operation.Nested))
                {
                    PerformOperation(operation);
                }
            }
            return false;
        }
    }

}
