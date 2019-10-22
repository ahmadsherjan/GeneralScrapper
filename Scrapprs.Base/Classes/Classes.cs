using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrappers.Base.Classes
{
    public class BaseOperation
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public Dictionary<string, string> Expressions { get; set; }
        public ValueType ValueType { get; set; }
        public bool DependsOnChild { get; set; }
        public bool ChildDependsOnParent { get; set; }
        public int WaitInSeconds { get; set; }
    }
    public enum ValueType
    {
        Url,
        Regex,
        Selenium,
        Compare
    }
    //public class UrlOperation : BaseOperation
    //{
    //    public UrlOperationType UrlOperationType { get; set; }
    //}
    public enum UrlOperationType
    {
        Redirect
    }
    //public class SeleniumOperation : BaseOperation
    //{
    //    public SeleniumSelector SeleniumSelector { get; set; }
    //    public SeleniumOperationType SeleniumOperationType { get; set; }
    //}
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
    //public class RegexOperation : BaseOperation
    //{
    //    public RegexOperationType RegexOperationType { get; set; }
    //}
    public enum RegexOperationType
    {
        Extract,
        IsNull,
        NotNull
    }
    //public class CompareOperation : RegexOperation
    //{

    //}
    public class AllOperation : BaseOperation
    {
        public UrlOperationType UrlOperationType { get; set; }
        public RegexOperationType RegexOperationType { get; set; }
        public SeleniumSelector SeleniumSelector { get; set; }
        public SeleniumOperationType SeleniumOperationType { get; set; }
        public AllOperation Nested { get; set; }
    }
}
