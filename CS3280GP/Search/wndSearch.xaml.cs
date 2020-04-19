using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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

namespace CS3280GP.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        clsSearchSQL clsSql;
        clsSearchLogic clsSL;
        Main.wndMain mainWindow;

        string InvoiceNumSelection = "";
        string InvoiceDateSelection = "";
        string InvoiceTotalCost = "";
        public wndSearch(Main.wndMain wndMain)

        {

            InitializeComponent();

            clsSql = new clsSearchSQL();
            clsSL = new clsSearchLogic();



            dgInvoices.ItemsSource = clsSql.GetInvoices();
            cbInvoiceNum.ItemsSource = clsSql.GetInvoiceNum();
            cbInvoiceNum.DisplayMemberPath = nameof(clsSearchLogic.sInvoiceNum);
            cbInvoiceDate.ItemsSource = clsSql.GetInvoiceDate();
            cbInvoiceDate.DisplayMemberPath = nameof(clsSearchLogic.sInvoiceDate);
            cbTotalCost.ItemsSource = clsSql.GetTotalCost();
            cbTotalCost.DisplayMemberPath = nameof(clsSearchLogic.sTotalCost);
            mainWindow = wndMain;
        }
        /// <summary>
        /// This method resest the search window to default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearSelectionBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                //this button will return the search window to its default settings

                cbInvoiceNum.SelectedIndex = -1;
                cbInvoiceDate.SelectedIndex = -1;
                cbTotalCost.SelectedIndex = -1;
                dgInvoices.ItemsSource = clsSql.GetInvoices();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
        /// <summary>
        /// This method returns the user invoice selections to datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dgInvoices.ItemsSource = clsSql.ReturnSelection();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This method returns the users selection to the main window datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgInvoices.SelectedItem != null)
                {
                    clsSql.sInvoiceNum = ((clsSearchLogic)dgInvoices.SelectedItem).sInvoiceNum;
                    mainWindow.Item_Display.ItemsSource = clsSql.ReturnSelection();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This method will close the search window and return user to the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
                mainWindow.Search_Invoice.IsChecked = false;
                mainWindow.Item_Table.IsChecked = false;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This method closes the search window and resets the portion of main window that allows user to then select search again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                mainWindow.Search_Invoice.IsChecked = false;
                mainWindow.Item_Table.IsChecked = false;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method updates sInvoiceNum string according to user selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbInvoiceNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbInvoiceNum.SelectedIndex != -1)
                {
                    clsSql.sInvoiceNum = ((clsSearchLogic)cbInvoiceNum.SelectedItem).sInvoiceNum;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This method updates sInvoiceDate string according to user selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbInvoiceNum.SelectedIndex != -1)
                    clsSql.sInvoiceDate = ((clsSearchLogic)cbInvoiceDate.SelectedItem).sInvoiceDate;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method updates sTotalCost string according to user selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbTotalCost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbInvoiceNum.SelectedIndex != -1)
                    clsSql.sTotalCost = ((clsSearchLogic)cbTotalCost.SelectedItem).sTotalCost;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}