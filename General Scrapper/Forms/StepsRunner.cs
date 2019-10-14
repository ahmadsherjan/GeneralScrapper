using MyClasses;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            if(openFileDialog1.ShowDialog()!=DialogResult.OK)
            {
                MessageBox.Show("Please a step file");
            }
            json = File.ReadAllText(openFileDialog1.FileName);
            lines = new List<string>();

            Queue<BaseOperation> operations = new Queue<BaseOperation>();
            var temp =JsonConvert.DeserializeObject<Queue<AllOperation>>(json);
            //operations.Enqueue(new UrlOperation
            //{
            //    Value = "https://www.google.com/",
            //    ValueType = ValueType.Url,
            //    UrlOperationType = UrlOperationType.Redirect
            //});

            //operations.Enqueue(new SeleniumOperation
            //{
            //    Value = "input[title=Search]",
            //    Text = "C#",
            //    ValueType = ValueType.Selenium,
            //    SeleniumSelector = SeleniumSelector.JSSelector,
            //    SeleniumOperationType = SeleniumOperationType.SetText
            //});
            //operations.Enqueue(new SeleniumOperation
            //{
            //    Value = "input[type=submit]",
            //    Text = "C#",
            //    ValueType = ValueType.Selenium,
            //    SeleniumSelector = SeleniumSelector.JSSelector,
            //    SeleniumOperationType = SeleniumOperationType.Click
            //});
            //operations.Enqueue(new RegexOperation
            //{
            //    Value = "<a href=\"(?<url>.*?)\" ping=",
            //    Text = "url",
            //    ValueType = ValueType.Regex,
            //    RegexOperationType = RegexOperationType.Extract,
            //    DependsOnChild=true,
            //    Nested = new CompareOperation()
            //    {
            //        ValueType=ValueType.Compare,
            //        RegexOperationType=RegexOperationType.NotNull,
            //        Value= "<a[^>]*id=\"pnnext\"[^>]*>(?<data>.*?)</a>",
            //        Text= "data",
            //        Nested= new SeleniumOperation
            //        {
            //            Value = "a[id=pnnext]",
            //            Text = "C#",
            //            ValueType = ValueType.Selenium,
            //            SeleniumSelector = SeleniumSelector.JSSelector,
            //            SeleniumOperationType = SeleniumOperationType.Click
            //        }

            //    }
            //});
            while (operations.Count > 0)
            {
                var op = operations.Dequeue();
                PerformOperation(op);
            }
            MessageBox.Show("Completed " + lines.Count);
        }
        private bool PerformOperation(BaseOperation operation)
        {
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
                            var data = MyUtilityMethods.getListFromPage(_driver.PageSource, rop.Value, rop.Text);
                            lines.AddRange(data);
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
    public class BaseOperation
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public ValueType ValueType { get; set; }
        public BaseOperation Nested { get; set; }
        public bool DependsOnChild { get; set; }
        public bool ChildDependsOnParent { get; set; }
    }
    public enum ValueType
    {
        Url,
        Regex,
        Selenium,
        Compare
    }
    public class UrlOperation : BaseOperation
    {
        public UrlOperationType UrlOperationType { get; set; }
    }
    public enum UrlOperationType
    {
        Redirect
    }
    public class SeleniumOperation : BaseOperation
    {
        public SeleniumSelector SeleniumSelector { get; set; }
        public SeleniumOperationType SeleniumOperationType { get; set; }
    }
    public enum SeleniumSelector
    {
        Id,
        Tag,
        Name,
        Class,
        CssSelector,
        LinkText,
        PartialLinkText,
        XPath,
        JSSelector,
    }
    public enum SeleniumOperationType
    {
        Click,
        SetText,
        Blur,

    }
    public class RegexOperation : BaseOperation
    {
        public RegexOperationType RegexOperationType { get; set; }

    }
    public enum RegexOperationType
    {
        Extract,
        IsNull,
        NotNull
    }
    public class CompareOperation : RegexOperation
    {

    }
    public class AllOperation:BaseOperation
    {
        public UrlOperationType UrlOperationType { get; set; }
        public RegexOperationType RegexOperationType { get; set; }
        public SeleniumSelector SeleniumSelector { get; set; }
        public SeleniumOperationType SeleniumOperationType { get; set; }
    }
}
