using System;
using System.Drawing;
using System.Windows.Forms;
namespace MyClasses
{
    public class Invoker
    {
        #region instence variables
        public static bool auto_scroll = false;

        #endregion

        #region Delegates
        private delegate void setcontrolText(Control ct, string value);
        private delegate void controlEnabled(Control ct, bool value);
        private delegate string getcontrolText(Control ct);
        
        #endregion
        
        #region methods

        public void setControlText(Control ct, string value)
        {
            if (ct.InvokeRequired)
            {
                setcontrolText d3 = new setcontrolText(setControlText);
                try
                {
                    ct.Invoke(d3, new object[] { ct, value });
                }
                catch (Exception exp) {
                    //LocalUtilityMethods.LogErrors(Settings.LogFilePath + "\\" + Settings.FileName + ".txt", exp.Message);
                }
            }
            else
            {
                ct.Text = value;
            }
        }

        public void setControlEnabled(Control ct, bool value)
        {
            if (ct.InvokeRequired)
            {
                controlEnabled d3 = new controlEnabled(setControlEnabled);
                try
                {
                    ct.Invoke(d3, new object[] { ct, value });
                }
                catch (Exception exp) { }
            }
            else
            {
                ct.Enabled = value;
            }
        }


        public void ShowOrHideControl(Control mycontrol, bool show)
        {
            try
            {
                if (mycontrol.InvokeRequired)
                {
                    controlEnabled del = ShowOrHideControl;
                    mycontrol.Invoke(del, new object[] { mycontrol, show });
                    return;
                }
                if (show)
                {
                    mycontrol.Show();
                }
                else
                {
                    mycontrol.Hide();
                }
            }
            catch { }

        }
       
        public string getControlText(Control mycontrol)
        {
                if (mycontrol.InvokeRequired)
                {
                    getcontrolText del = new getcontrolText(getControlText);
                    return mycontrol.Invoke(del, new object[] { mycontrol }).ToString();
                }
                else
                    return mycontrol.Text;
        }

                
        public string GetTextBoxSelectedText(Control ctrl)
        {
            if (ctrl.InvokeRequired)
            {
                getcontrolText del = new getcontrolText(GetTextBoxSelectedText);
                return ctrl.Invoke(del, new object[] { ctrl}).ToString();
            }
            else
            {
                TextBox tb = (TextBox)ctrl;
                return tb.SelectedText;
            }
        }


        private delegate void myDelUpdateitemColor(ListView listview, string itemName);
        public void updateListViewColor(ListView listView1, string itemName)
        {
            try
            {
                if (listView1.InvokeRequired)
                {
                    myDelUpdateitemColor d5 = new myDelUpdateitemColor(updateListViewColor);
                    listView1.Invoke(d5, new object[] { listView1, itemName });
                }
                else
                {
                    listView1.Items[itemName].BackColor = Color.LightBlue;
                    listView1.EnsureVisible(listView1.Items.Count - 1);
                    listView1.Update();
                }
            }
            catch { }
        }


        private delegate void myDelResetListViewColor(ListView listview);
        public void ResetListViewColor(ListView listView1)
        {
            try
            {
                if (listView1.InvokeRequired)
                {
                    myDelResetListViewColor d5 = new myDelResetListViewColor(ResetListViewColor);
                    listView1.Invoke(d5, new object[] { listView1 });
                }
                else
                {
                    foreach (ListViewItem item in listView1.Items)
                    {
                        item.BackColor = Color.White;
                    }
                    listView1.EnsureVisible(listView1.Items.Count - 1);
                    listView1.Update();
                }
            }
            catch { }
        }



        public void clearlistview(ListView mylistview)
        {
            try
            {
                if (mylistview.InvokeRequired)
                {
                    myDelResetListViewColor del = clearlistview;
                    mylistview.Invoke(del, new object[] { mylistview });
                    return;
                }
                mylistview.Items.Clear();
            }
            catch { }
        }


        public void scrollDownListView(ListView listview)
        {
            try
            {
                if (listview.InvokeRequired)
                {
                    myDelResetListViewColor del = scrollDownListView;
                    listview.Invoke(del, new object[] { listview });
                    return;
                }
                listview.EnsureVisible(listview.Items.Count - 1);  // scrolling off temperary
            }
            catch
            {
            }

        }


        private delegate void UpdateControlTextAndColor(Control mycontrol, string text, Color font_color);
        public void updateControlTextAndColor(Control mycontrol, string text, Color font_color)
        {
            try
            {
                if (mycontrol.InvokeRequired)
                {
                    UpdateControlTextAndColor del = updateControlTextAndColor;
                    mycontrol.Invoke(del, new object[] { mycontrol, text, font_color });
                    return;
                }
                mycontrol.ForeColor = font_color;
                mycontrol.Text = text;
            }
            catch
            { }
        }

        private delegate void setProgressBarValue(ProgressBar myprogressbar, int value, int max_value);
        public void setprogressbarvalue(ProgressBar myprogressbar, int value, int max_value)
        {
            try
            {
                if (myprogressbar.InvokeRequired)
                {
                    setProgressBarValue del = setprogressbarvalue;
                    myprogressbar.Invoke(del, new object[] { myprogressbar, value, max_value });
                    return;
                }
                myprogressbar.Maximum = max_value;
                myprogressbar.Value = value;

            }
            catch { }
        }


        private delegate void Progressbar(ProgressBar myprogressbar, int max_value);
        public void Progressbarstep(ProgressBar myprogressbar, int max_value)
        {
            try
            {
                if (myprogressbar.InvokeRequired)
                {
                    Progressbar del = Progressbarstep;
                    myprogressbar.Invoke(del, new object[] { myprogressbar, max_value });
                    return;
                }
                myprogressbar.Maximum = max_value;
                myprogressbar.Step = 1;
                myprogressbar.PerformStep();
            }
            catch { }
        }

        private delegate void AddlistViewItem(ListView mylistview, string message);
        public void AddItemToList(ListView mylistview, string message)
        {
            try
            {
                if (mylistview.InvokeRequired)
                {
                    AddlistViewItem del = AddItemToList;
                    mylistview.Invoke(del, new object[] { mylistview, message });
                    return;
                }
                //ListViewItem item = new ListViewItem(message);
                ListViewItem item = new ListViewItem(message);
                item.Name = message;
                mylistview.Items.Add(item);
            }
            catch { }
        }

        private delegate void UpdatelistViewItem(ListView mylistview, string itemName, string message);
        public void UpdateListviewItem(ListView mylistview, string itemName, string message)
        {
            try
            {
                if (mylistview.InvokeRequired)
                {
                    UpdatelistViewItem del = UpdateListviewItem;
                    mylistview.Invoke(del, new object[] { mylistview, itemName, message });
                    return;
                }
                //ListViewItem item = new ListViewItem(message);
                if (mylistview.Items[itemName] != null)
                {
                    mylistview.Items[itemName].Text = message;
                }
            }
            catch { }
        }



        private delegate void AddSublistViewItem(ListView mylistview, string itemName, string message);
        public void AddSubItemToList(ListView mylistview, string itemName, string message)
        {
            try
            {
                if (mylistview.InvokeRequired)
                {
                    AddSublistViewItem del = AddSubItemToList;
                    mylistview.Invoke(del, new object[] { mylistview, itemName, message });
                    return;
                }
                mylistview.Items[itemName].SubItems.Add(message);
                if (auto_scroll)
                {
                    mylistview.EnsureVisible(mylistview.Items.Count - 1);  // scrolling off temperary
                }
            }
            catch { }
        }

        private delegate void updateSublistViewItem(ListView mylistview, string itemName, int subitem_index, string message);
        public void UpdateSubItemToList(ListView mylistview, string itemName, int subitem_index, string message)
        {
            try
            {
                if (mylistview.InvokeRequired)
                {
                    updateSublistViewItem del = UpdateSubItemToList;
                    mylistview.Invoke(del, new object[] { mylistview, itemName, subitem_index, message });

                }
                if (mylistview.Items[itemName] != null)
                {
                    mylistview.Items[itemName].SubItems[subitem_index].Text = message;
                    if (auto_scroll)
                    {
                        mylistview.EnsureVisible(mylistview.Items.Count - 1);  // scrolling off temperary
                    }
                }
            }
            catch { }
        }

        private delegate void SetlistViewItemColor(ListView mylistview, string itemName, Color color);
        public void SetlistViewItemcolor(ListView mylistview, string itemName, Color color)
        {
            try
            {
                if (mylistview.InvokeRequired)
                {
                    SetlistViewItemColor del = SetlistViewItemcolor;
                    mylistview.Invoke(del, new object[] { mylistview, itemName, color });
                    return;
                }
                foreach (ListViewItem item in mylistview.Items)
                {
                    if (item.Name == itemName)
                        item.SubItems[0].BackColor = color;
                    else
                        item.SubItems[0].BackColor = Color.White;
                }
                //mylistview.Items[itemName].SubItems[0].BackColor = color;
            }
            catch { }
        }

        private delegate void setlistViewSubItemColor(ListView mylistview, string itemName, int subitem_index, Color color);
        public void SetlistviewSubitemcolor(ListView mylistview, string itemName, int subitem_index, Color color)
        {
            try
            {
                if (mylistview.InvokeRequired)
                {
                    setlistViewSubItemColor del = SetlistviewSubitemcolor;
                    mylistview.Invoke(del, new object[] { mylistview, itemName, subitem_index, color });
                    return;
                }
                mylistview.Items[itemName].SubItems[subitem_index].BackColor = color;
            }
            catch { }
        }


        private delegate void CheckedUncheckedCheckBox(CheckBox checkbox, bool Checked);
        public void checkedUncheckedCheckBox(CheckBox checkbox, bool Checked)
        {
            try
            {
                if (checkbox.InvokeRequired)
                {
                    CheckedUncheckedCheckBox del = checkedUncheckedCheckBox;
                    checkbox.Invoke(del, new object[] { checkbox, Checked });
                    return;
                }
                checkbox.Checked = Checked;
            }
            catch
            {
            }

        }



        #endregion
    }
}
