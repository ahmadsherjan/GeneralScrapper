using System;
using System.Collections.Generic;

using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;
using System.Net.Mail;
using System.Data.OleDb;
using System.Data.Common;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Web;


namespace MyClasses
{
    public class MyUtilityMethods
    {

        public MyUtilityMethods()
        { }
        public static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
        public static string StripTagsRegex(string source)
        {
            if (source == null)
                return "";
            return Regex.Replace(source, "<.*?>", string.Empty);
        }
        private static string[] getList(MatchCollection match, string group)
        {
            string[] array = new string[match.Count];
            if (match.Count > 0)
            {
                int Index = 0;
                foreach (Match m in match)
                    array[Index++] = m.Groups[group].Value.Trim();
                return array;

            }
            else
                return null;
        }
        private static string[] getList(List<string> list)
        {
            if (list == null)
                return null;
            string[] lst=new string[list.Count];
            int i = 0;
            foreach (string str in list)
            {
                lst[i++] = str;
            }
            return lst;

        }
        public static string getList(string[] array)
        {
            if (array != null)
            {
                string data = "";
                foreach (string str in array)
                    if (str.Trim().Length > 0)
                        data =data+ str;
                return data;
            }
            else
                return "";
        }
        public static string[] getListFromUrl(string url, string regex, string groupName)
        {
            
             return getListFromPage(new WebClient().DownloadString(url),regex,groupName);
   
        }
        public static string[] getListFromPage(string page, string regex, string groupName)
        {

            Regex reg = new Regex(regex,RegexOptions.Multiline|RegexOptions.Singleline|RegexOptions.IgnoreCase);
            return getList(reg.Matches(page), groupName);
        }
        public static string getValueByRegexMatched(string page, string regex, string groupName)
        {
            return Regex.Match(page, regex, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase).Groups[groupName].Value;
        }
        public static string getURL(string str)
        {
            string[] data = getListFromPage(str, "href=\"(?<links>.*?)\"","links");
            if(data!=null)
                return data[0];
            return null;
        }
        public static string getLastURL(string str)
        {
            string[] data = getListFromPage(str, "href=\"(?<links>.*?)\"", "links");
            if (data != null)
                return data[data.Length-1];
            return null;
        }
        public static string getFirstURL(string str)
        {
            string[] data = getListFromPage(str, "href=\"(?<links>.*?)\"", "links");
            if (data != null)
                return data[0];
            return null;
        }
        public static void WriteToFile(string Path, List<string> list, string encoding = "Windows-1252")
        {
            if (list == null)
                return;
            try
            {
                File.WriteAllLines(Path, getList(list),Encoding.GetEncoding(encoding));
            }
            catch (Exception exp)
            { }
        }
        public static string RemoveExtraSpaces(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                    return "";
                return Regex.Replace(text, "\\s+", " ");
            }
            catch (Exception exp)
            {
                return text;
            }
        }
        public static string[] getListFromList(List<string> list)
        {
            
            string[] array=null;
            try
            {
                array = new string[list.Count];
                int i = 0;
                foreach (string str in list)
                    array[i++] = str;
                return array;
            }
            catch (Exception exp)
            {
                return array;
            }
        }
        public static string[] ConcatArrays(string[] array1, string[] array2)
        {
            List<string> list = new List<string>();
            if (array1 != null && array1.Length > 0)
                list.AddRange(array1);
            if (array2 != null && array2.Length > 0)
                list.AddRange(array2);
            return getListFromList(list);
        }
        public static string[] GetPagedUrlsByPageNumber(string[] array, int pageNumber)
        {
            List<string> list = new List<string>();
            try
            {
                for (int i = pageNumber * 10; i < array.Length && i < (pageNumber * 10 + 10); i++)
                {
                    list.Add(array[i]);
                }
            }
            catch (Exception exp)
            { }
            return list.ToArray();
        }
        public static string GetFileNameFromFullPath(string path)
        {
            string fileName = "";
            try
            {
                fileName = path.Substring(path.LastIndexOf("\\")+1);
            }
            catch (Exception exp)
            { } return fileName;
        }

        //public static List<string> GetUniqueList(List<string> list)
        //{
        //    List<string> lst = new List<string>();
        //    if (list == null)
        //        return lst;
        //    if (list.Count == 0)
        //        return lst;
        //    IEnumerable<string> deduplacated = list.Distinct(new PropertyComparer());

        //    foreach (string p in deduplacated)
        //    {
        //        lst.Add(p);
        //    }
        //    list = lst;
        //    return list;
        //}

        public static void LogFile(string Path,string line)
        {

            if (File.Exists(Path))
                {
                    using (StreamWriter writer = new StreamWriter(Path,true, Encoding.GetEncoding("Windows-1252")))
                    {
                        writer.WriteLine(line);
                    }
                }
                else
                {
                    try
                    {
                        using (StreamWriter writer = new StreamWriter(Path, false, Encoding.GetEncoding("Windows-1252")))
                        {
                            writer.WriteLine(line);
                        }
                    }
                    catch { }
                }
        }
        public static bool DeleteFile(string FilePath)
        {
            try
            {
                if (File.Exists(FilePath))
                    File.Delete(FilePath);
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }
        public static List<string> RemoveNulFromList(List<string> list)
        {
            List<string> Lst = new List<string>();
            try
            {
                if (list == null || list.Count == 0)
                    return Lst;
                foreach (string str in list)
                    if (!string.IsNullOrEmpty(str)&&str.Trim().Length>0)
                        Lst.Add(str);
                return Lst;
            }
            catch (Exception exp)
            {
                return Lst;
            }
        }
        public static string GetValueFromArray(string[] array, int index, bool findInAnyCase)
        {
            string result = "";
            try
            {
                do
                {
                    result = array[index];
                } while (string.IsNullOrEmpty(result) && index++ < array.Length && findInAnyCase);
            }
            catch (Exception exp)
            { }
            return result;
        }
        public static string getUrlFromATag(string tag)
        {
            string[] str = getListFromPage(tag, "<a.*?href=\"(?<urls>.*?)\"", "urls");
            if (str != null || str.Length > 0)
                return str[0];
            return "";
        }
        public static List<string> getSubList(List<string> list,int offset,int count)
        {
            if (list == null || list.Count <= 0)
                return null;
            count += offset;
            List<string> lst = new List<string>();
            try
            {
                for (; offset <= count && offset < list.Count; offset++)
                    lst.Add(list[offset]);
                return lst;
            }
            catch (Exception exp)
            {
                return lst;
            }
        }
        public static object getElementFromDataTable(DataTable datatable, int row,int col)
        {
            
            object ob = null;
            try
            {
                DataRow dr = datatable.Rows[row];
                ob=dr[datatable.Columns[col]];
                return ob;
            }
            catch (Exception exp)
            {
                return ob;
            }
        }
        public static bool sendMail(string From,string Password,List<string> To,string Subject,string Body)
        {
            try
            {
                string host = "smtp.gmail.com";
                MailMessage m = new MailMessage();
                SmtpClient sc = new SmtpClient(host);
                m.From = new MailAddress(From, "sent");
                foreach(string to in To)
                    m.To.Add(new MailAddress(to, "sent"));

                //similarly BCC
                m.Subject = Subject;
                m.IsBodyHtml = true;
                m.Body = Body;
                sc.Host = host;
                sc.Port = 587;
                sc.Credentials = new
                  System.Net.NetworkCredential(From, Password);
                sc.EnableSsl = true;
                sc.Send(m);
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }
        public static bool sendMail(string From, string Password, string To, string Subject, string Body)
        {
            try
            {
                List<string> lst = new List<string>();
                lst.Add(Body);
                WriteToFile("test.log", lst);
                string host = "smtp.gmail.com";
                MailMessage m = new MailMessage();
                SmtpClient sc = new SmtpClient(host);
                m.From = new MailAddress(From, "sent");
                
                    m.To.Add(new MailAddress(To, "sent"));

                //similarly BCC
                m.Subject = Subject;
                m.IsBodyHtml = true;
                m.Body = Body;
                sc.Host = host;
                sc.Port = 587;
                sc.Credentials = new
                  System.Net.NetworkCredential(From, Password);
                sc.EnableSsl = true;
                sc.Send(m);
                return true;
            }
            catch (Exception exp)
            {
                MyUtilityMethods.LogFile(AppDomain.CurrentDomain.GetData("DataDirectory") as string + "\\Log.txt", "Surfoclock cron job completed UN successfully  Error: " + exp.Message + "  [date " + DateTime.UtcNow + " ] ");
                return false;
            }
        }
        
        public static int indexOf(string[] list, string token)
        {
            try
            {

                for (int j = 0; j < list.Length; j++)
                    if (list[j].Equals(token))
                        return j;
                return -1;
            }
            catch (Exception exp)
            {
                return -1;
            }
        }
        public static int indexOf(List<string> list, string token)
        {
            try
            {
                
                for (int j = 0; j < list.Count; j++)
                    if (list[j].Equals(token))
                        return j;
                return -1;
            }
            catch (Exception exp)
            {
                return -1;
            }
        }
        public static byte[] GZDecompression(byte[] gzip)
        {
            try
            {
                using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
                {
                    const int size = 4096;
                    byte[] buffer = new byte[size];
                    using (MemoryStream memory = new MemoryStream())
                    {
                        int count = 0;
                        do
                        {
                            count = stream.Read(buffer, 0, size);
                            if (count > 0)
                            {
                                memory.Write(buffer, 0, count);
                            }
                        }
                        while (count > 0);
                        return memory.ToArray();
                    }
                }
            }
            catch (Exception exp)
            {
                return null;
            }
        }
        #region postcode
        public static string getPostalcodeFromAddress(string text)
        {
            string result = "";
            try
            {

                string regex = @"((\b[A-Z]{1,2}[0-9][A-Z0-9]? [0-9][ABD-HJLNP-UW-Z]{2}\b)|(^([A-Z]{1}[0-9]{1})$|^([A-Z]{1}[0-9]{2})$|^([A-Z]{2}[0-9]{1})$|^([A-Z]{2}[0-9]{2})$))";


                if (Regex.IsMatch(text, regex, RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase))
                {
                    result = text;// HttpUtility.HtmlDecode(text);

                    string[] lines = result.Split(',');
                    lines = Regex.Split(lines[lines.Length - 1], "\\s+");
                    result = lines[lines.Length - 2] + " " + lines[lines.Length - 1];

                }

            }
            catch (Exception exp)
            { }
            return result;
        }
 
        #endregion

        #region array manipulation
        public static string[] concateToArray(string start, string[] array)
        {
            try
            {
                for (int i = 0; i < array.Length; i++)
                    array[i] = start + array[i];
            }
            catch (Exception)
            { }
            return array;
        }
        public static string[] concateToArray(string[] array, string end)
        {
            try
            {
                for (int i = 0; i < array.Length; i++)
                    array[i] = array[i] + end;
            }
            catch (Exception)
            { }
            return array;
        }
        #endregion

        #region application management
        public static void RestartApp(int pid, string applicationName)
        {
            // Wait for the process to terminate
            System.Diagnostics.Process process = null;
            try
            {
                process = System.Diagnostics.Process.GetProcessById(pid);
                process.WaitForExit(1000);
            }
            catch (ArgumentException ex)
            {
                // ArgumentException to indicate that the 
                // process doesn't exist?   LAME!!
            }
            System.Diagnostics.Process.Start(applicationName, "");
        }
        #endregion

    }
    class PropertyComparer : IEqualityComparer<string>
    {

        public bool Equals(string x, string y)
        {

            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.Contains(y) || y.Contains(x);
        }

        public int GetHashCode(string p)
        {
            if (Object.ReferenceEquals(p, null)) return 0;

            return p.GetHashCode();
        }
    }
}
