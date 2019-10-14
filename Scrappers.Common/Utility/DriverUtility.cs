using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Utility
{
    public class DriverUtility
    {
        #region actions depends on browser 
        public static IWebElement FindElementInAnyCase(IWebDriver driver, By selector, int counter = 2)
        {
            try
            {
                if (counter-- < 0)
                    return null;
                WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 0, 5));
                var dynamicElement = wait.Until<IWebElement>(d => d.FindElement(selector));

                return dynamicElement;
            }
            catch (Exception exp)
            {
                return FindElementInAnyCase(driver, selector, counter);
            }
        }

        public static IEnumerable<IWebElement> FindElementsInAnyCase(IWebDriver driver, By selector, int counter)
        {
            try
            {
                var dynamicElement = FindElementInAnyCase(driver, selector, counter);
                if (dynamicElement != null)
                    return driver.FindElements(selector);
                return null;
            }
            catch (Exception exp)
            {
                return FindElementsInAnyCase(driver, selector, counter - 1);
            }
        }
        public static void SelectValue(IWebDriver driver, By selector, string text, bool byValue = true)
        {
            try
            {
                var element = FindElementInAnyCase(driver, selector);
                SelectElement dropDown = new SelectElement(element);
                if (byValue)
                    dropDown.SelectByValue(text);
                else
                    dropDown.SelectByText(text);
            }
            catch (Exception exp)
            {
            }
        }


        public static IWebElement FindElement(IWebElement element, By selector, int counter)
        {
            try
            {
                if (counter < 0)
                    return null;
                var dynamicElement = element.FindElement(selector);
                if (dynamicElement != null)
                    return element.FindElement(selector);
                return null;
            }
            catch (Exception exp)
            {
                return FindElement(element, selector, counter - 1);
            }
        }
        public static List<IWebElement> FindElements(IWebElement element, By selector, int counter)
        {
            try
            {
                if (counter < 0)
                    return null;
                var dynamicElement = element.FindElements(selector);
                if (dynamicElement != null)
                    return dynamicElement.ToList();
                return null;
            }
            catch (Exception exp)
            {
                return FindElements(element, selector, counter - 1);
            }
        }
        public static void ClickByID(IWebDriver driver, string id)
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string code = string.Format("document.getElementById('{0}').click()", id);
            js.ExecuteScript(code);
        }
        public static void ClickByName(IWebDriver driver, string name)
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string code = string.Format("document.getElementsByName('{0}')[0].click()", name);
            js.ExecuteScript(code);
        }
        public static void ClickByTagName(IWebDriver driver, string tagName)
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string code = string.Format("document.getElementsByTagName('{0}')[0].click()", tagName);
            js.ExecuteScript(code);
        }
        public static void ClickByClassName(IWebDriver driver, string cls)
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string code = string.Format("document.getElementsByClassName('{0}')[0].click()", cls);
            js.ExecuteScript(code);
        }
        public static void ClickByType(IWebDriver driver, string type)
        {
            //document.querySelectorAll('input[type=text]')
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string code = string.Format("document.querySelectorAll('input[type={0}]')[0].click()", type);
            js.ExecuteScript(code);
        }
        public static void ClickByQuery(IWebDriver driver, string query)
        {
            //document.querySelectorAll('input[type=text]')
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string code = string.Format("document.querySelectorAll('{0}')[0].click()", query);
            js.ExecuteScript(code);
        }
        public static void SetTextByTagAndAttribute(IWebDriver driver, string tagName, string attribute, string attributeValue, string value, bool append = false)
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string code = string.Format("document.querySelectorAll('{0}[{1}={2}]')[0].value = '{3}'", tagName, attribute, attributeValue, value);
            if (append)
            {
                code = string.Format("document.querySelectorAll('{0}[{1}={2}]')[0].value = document.querySelectorAll('{0}[{1}={2}]')[0].value + '{3}'", tagName, attribute, attributeValue, value);
            }
            js.ExecuteScript(code);
        }
        public static void SetTextByQuery(IWebDriver driver, string query, string value, bool append = false)
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string code = string.Format("document.querySelectorAll('{0}')[0].value = '{1}'", query, value);
            if (append)
            {
                code = string.Format("document.querySelectorAll('{0}')[0].value = document.querySelectorAll('{0}')[0].value + '{1}'", query, value);
            }
            js.ExecuteScript(code);
        }

        //title="Search"
        public static void SetTextById(IWebDriver driver, string id, string value)
        {
            //document.getElementById('gadget_url').value = ''
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string code = string.Format("document.getElementById('{0}').value = '{1}'", id, value);
            js.ExecuteScript(code);
        }
        public static void SetTextByType(IWebDriver driver, string type, string value)
        {
            //document.getElementById('gadget_url').value = ''
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string code = string.Format("document.querySelectorAll('input[type={0}]')[0].value = '{1}'", type, value);
            js.ExecuteScript(code);
        }
        /// <summary>
        /// set query as query='input[type=text]'
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="query">query='input[type=text]'</param>
        /// <param name="value"></param>
        public static void SetTextByQuerySelector(IWebDriver driver, string query, string value, bool append = false)
        {
            //document.getElementById('gadget_url').value = ''
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string code = "";
            code = string.Format("document.querySelectorAll('{0}')[0].value = '{1}'", query, value);
            if (append)
            {
                code = string.Format("document.querySelectorAll('{0}')[0].value = document.querySelectorAll('{0}')[0].value + '{1}'", query, value);
            }
            js.ExecuteScript(code);
        }

        //title="Search"
        public static void BlurByTagName(IWebDriver driver, string tagName)
        {
            //document.getElementById('myText').blur();
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string code = string.Format("document.getElementsByName('{0}')[0].blur();", tagName);
            js.ExecuteScript(code);
        }
        public static void SetTextByName(IWebDriver driver, string tagName, string value)
        {
            //document.getElementById('gadget_url').value = ''
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string code = string.Format("document.getElementsByName('{0}')[0].value = '{1}'", tagName, value);
            js.ExecuteScript(code);
        }
        public static void SetTextByTagName(IWebDriver driver, string tagName, string value)
        {
            //document.getElementById('gadget_url').value = ''
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string code = string.Format("document.getElementsByTagName('{0}')[0].value = '{1}'", tagName, value);
            js.ExecuteScript(code);
        }//document.getElementsByName("acc")[0].value

        public static void ExecuteJS(IWebDriver driver, string code)
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            js.ExecuteScript(code);
        }
        #endregion

        #region element Utility

        public static string GetAttribute(IWebElement element, string key)
        {
            for (int i = 0; i < 6; i++)
            {
                var result = element.GetAttribute(key);
                if (result != null)
                    return result;
                Thread.Sleep(1000);
            }
            return null;
        }

        public static string GetText(IWebElement element)
        {
            return element.Text;
        }

        #endregion

        public static void OnBlur(IWebDriver driver, string id)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("var x = $(\'" + id + "\');");
            stringBuilder.Append("x.blur();");
            js.ExecuteScript(stringBuilder.ToString());
        }
        public static void FocusOut(IWebDriver driver, string id)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("var x = $(\'" + id + "\');");
            stringBuilder.Append("x.focusout();");
            js.ExecuteScript(stringBuilder.ToString());
        }
    }
}
