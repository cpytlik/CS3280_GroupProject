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
using System.Windows.Shapes;
using CS3280GP.Search;
using CS3280GP.Items;
using System.Reflection;

namespace CS3280GP.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        Search.wndSearch searchWindow;
        Items.wndItems ItemsWindow;
        clsMainLogic MainLogic;
        clsSearchSQL clsSql;
        List<clsLineItems> MyList;
        bool editOrAdd;
        clsSearchLogic clsSL;

        public wndMain()
        {
            try
            {
                InitializeComponent();
                //searchWindow = new Search.wndSearch(this);
                //searchItems = new Items.wndItems(this);
                MainLogic = new clsMainLogic();
                MyList = new List<clsLineItems>();
                editOrAdd = false;                

                //Items go into combo box
                Item_List_2.ItemsSource = MainLogic.ListItems();

                //populates data grid
                Item_Display.ItemsSource = MyList;

                /*
                clsSql.sInvoiceNum = "";
                clsSql.sTotalCost = "";
                clsSql.sInvoiceDate = "";*/

                //initalize window
                ItemsWindow = new Items.wndItems(this);

                clsSL = new clsSearchLogic();

                Edit_Invoice.IsEnabled = false;
                Delete_Invoice.IsEnabled = false;
                Save_Invoice.IsEnabled = false;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }


        private void New_invoice(object sender, RoutedEventArgs e)
        {
            try
            {
                clsSL.sInvoiceDate = DateTime.Now.ToString();
                MyList.Clear();
                Item_List_2.SelectedIndex = -1;
                Quanitiy_Input.Clear();

                UpdateDisplay();
                editOrAdd = false;
                Save_Invoice.IsEnabled = true;
                EnableInputs();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
            
        }

        private void Save_Invoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(Invoice_Date.SelectedDate != null && MyList.Count != 0)
                {
                    string date = Invoice_Date.Text;
                    string tCost = Total_Price.Text;

                    if(editOrAdd)
                    {
                        Int32.TryParse(clsSql.sInvoiceNum, out int x);

                        MainLogic.UpdateInvoice(x, date, tCost);
                        MainLogic.UpdateLineItems(x, MyList);
                    }
                    else
                    {
                        clsSql.sInvoiceNum = MainLogic.CreateInvoice(date, tCost, MyList).ToString();
                        Invoice_Label.Content = "Invoice ID: " + clsSql.sInvoiceNum;

                    }
                    Edit_Invoice.IsEnabled = true;
                    Delete_Invoice.IsEnabled = true;
                    DisableInputs();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        private void Delete_Invoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse(clsSql.sInvoiceNum, out int x);

                MainLogic.DeleteInvoice(x);

                MyList.Clear();

                clsSql.sInvoiceNum = "";
                clsSql.sTotalCost = "";
                clsSql.sInvoiceDate = "";

                UpdateDisplay();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        private void Edit_Invoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                editOrAdd = true;
                Add_Item2.IsEnabled = true;
                Save_Invoice.IsEnabled = true;

                EnableInputs();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        private void Items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(Item_List_2.SelectedIndex != -1)
                {
                    var cost = Item_List_2.SelectedValue as clsLineItems;
                    Price.Text = "$ " + cost.Cost;
                }
                else
                {
                    Price.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// Only allow numbers to be entered for quantity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Quanitiy_Number_Check(object sender, KeyEventArgs e)
        {
            try
            {
                //makes it only numbers
                if(!(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key >= Key.D0 && e.Key <= Key.D9))
                {
                    if(!(e.Key == Key.Back || e.Key == Key.Delete))
                    {
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }

        }

        private void Add_item(object sender, RoutedEventArgs e)
        {
            try
            {
                if(Quanitiy_Input.Text != "" && Item_List_2.SelectedIndex != -1)
                {
                    bool ItemListed = false;
                    var Item = Item_List_2.SelectedValue as clsLineItems;

                    foreach(var listItem in MyList)
                    {
                        if(listItem.ItemCode == Item.ItemCode)
                        {
                            ItemListed = true;
                            Int32.TryParse(Price.Text, out int x);
                            listItem.LineItemNum = x;
                            break;
                        }
                    }

                    if(!ItemListed)
                    {
                        Int32.TryParse(Price.Text, out int x);
                        Item.LineItemNum = x;
                        MyList.Add(Item);
                    }
                    Quanitiy_Input.Text = "";

                    ///needs implementation

                    UpdateDisplay();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        private void Delete_Item_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleteItem = Item_Display.SelectedValue as clsLineItems;
                MyList.Remove(deleteItem);
                UpdateDisplay();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// Menu control for items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_TableClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                ItemsWindow.ShowDialog();
                this.Show();

                Quanitiy_Input.Text = "";
                Price.Text = "";
                Item_List_2.SelectedIndex = -1;
                Save_Invoice.IsEnabled = false;
                DisableInputs();
                UpdateDisplay();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// menu control for search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_InvoiceClick(object sender, RoutedEventArgs e)
        {
            try
            {
                searchWindow = new wndSearch(this);

                this.Hide();
                searchWindow.ShowDialog();
                this.Show();

                Delete_Invoice.IsEnabled = true;
                Edit_Invoice.IsEnabled = true;
                MyList.Clear();
                Quanitiy_Input.Text = "";
                Price.Text = "";
                Item_List_2.SelectedIndex = -1;

                DisableInputs();

                Save_Invoice.IsEnabled = false;

                ///needs implemtation

                UpdateDisplay();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        private void UpdateDisplay()
        {
            try
            {
                Item_List_2.ItemsSource = MainLogic.ListItems();

                double totalCost = 0;

                // need to get selected invoice, if no invoice, invoice ID: TBD

                foreach(var item in MyList)
                {
                    totalCost += (item.Cost * item.LineItemNum);
                }

                Total_Price.Text = totalCost.ToString();

                Item_Display.Items.Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// closes program
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// disables input
        /// </summary>
        public void DisableInputs()
        {
            try
            {
                Add_Item2.IsEnabled = false;
                Item_List_2.IsEnabled = false;
                Item_Display.IsEnabled = false;
                Invoice_Date.IsEnabled = false;
                Quanitiy_Input.IsEnabled = false;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// enables inputs
        /// </summary>
        public void EnableInputs()
        {
            try
            {
                Add_Item2.IsEnabled = true;
                Item_List_2.IsEnabled = true;
                Item_Display.IsEnabled = true;
                Invoice_Date.IsEnabled = true;
                Quanitiy_Input.IsEnabled = true;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// Error handler
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + "->" + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }


    }
}
