using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValueType = Scrappers.Base.Classes.ValueType;
using Scrappers.Base.Classes;
using System.IO;

namespace General_Scrapper.Forms
{
    public partial class StepsIdentifier : Form
    {
        private AllOperation _operation;
        private List<BaseOperation> operations;

        public StepsIdentifier()
        {
            InitializeComponent();
            PopulateForm();
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            _operation.Text = txtText.Text;
            _operation.Value = txtValue.Text;
            operations.Add(_operation);
            ClearForm();
        }
        private void ClearForm()
        {
            comboValueTypes.Items.Clear();
            comboValueTypes.Text = "";
            comboOperationType.Items.Clear();
            comboOperationType.Text = "";
            comboSelectorType.Items.Clear();
            comboSelectorType.Text = "";
            txtText.Text = txtValue.Text = "";
            _operation = new AllOperation();
            comboValueTypes.Items.AddRange(GetValues(typeof(ValueType)).ToArray());
        }
        #region private functions
        private void PopulateForm()
        {
            operations = new List<BaseOperation>();
            _operation = new AllOperation();
            comboValueTypes.Items.AddRange(GetValues(typeof(ValueType)).ToArray());
        }
        private List<string> GetValues(Type enumType)
        {
            if (!typeof(Enum).IsAssignableFrom(enumType))
                throw new ArgumentException("enumType should describe enum");

            var names = Enum.GetNames(enumType).Cast<object>();
            var values = Enum.GetValues(enumType).Cast<int>();
            
            return names.Zip(values, (name, value) => string.Format("{1}", value, name))
                        .ToList();
        }
        #endregion

        private void comboValueTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            comboSelectorType.Enabled = false;
            switch((ValueType)comboValueTypes.SelectedIndex)
            {
                case ValueType.Url:
                    _operation.ValueType = ValueType.Url;
                    comboOperationType.Items.Clear();
                    comboOperationType.Items.AddRange(GetValues(typeof(UrlOperationType)).ToArray());
                    break;
                case ValueType.Regex:
                    btnAdd.Enabled = true;
                    _operation.ValueType = ValueType.Regex;
                    comboOperationType.Items.Clear();
                    comboOperationType.Items.AddRange(GetValues(typeof(RegexOperationType)).ToArray());
                    break;
                case ValueType.Selenium:
                    comboSelectorType.Enabled = true;
                    _operation.ValueType = ValueType.Selenium;
                    comboSelectorType.Items.Clear();
                    comboSelectorType.Items.AddRange(GetValues(typeof(SeleniumSelector)).ToArray());
                    comboOperationType.Items.Clear();
                    comboOperationType.Items.AddRange(GetValues(typeof(SeleniumOperationType)).ToArray());
                    break;
                case ValueType.Compare:
                    _operation.ValueType = ValueType.Compare;
                    comboOperationType.Items.Clear();
                    comboOperationType.Items.AddRange(GetValues(typeof(RegexOperationType)).ToArray());
                    break;
            }
        }

        private void comboOperationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (_operation.ValueType)
            {
                case ValueType.Url:
                    _operation.UrlOperationType = (UrlOperationType)comboOperationType.SelectedIndex;

                    break;
                case ValueType.Regex:
                    _operation.RegexOperationType = (RegexOperationType)comboOperationType.SelectedIndex;
                    break;
                case ValueType.Selenium:
                    _operation.SeleniumOperationType = (SeleniumOperationType)comboOperationType.SelectedIndex;
                    break;
                case ValueType.Compare:
                    _operation.RegexOperationType = (RegexOperationType)comboOperationType.SelectedIndex;
                    break;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            var temp=Newtonsoft.Json.JsonConvert.SerializeObject(operations);
            File.WriteAllText("operation.json", temp);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _operation.Expressions.Add(txtText.Text, txtValue.Text);
        }

        private void comboSelectorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _operation.SeleniumSelector = (SeleniumSelector)comboSelectorType.SelectedIndex;
        }
    }
}
