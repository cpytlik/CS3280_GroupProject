using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS3280GP.Search
{
    public class clsSearchSQL

    {
        public clsDataAccess clsData;
        public wndSearch windSrch;


        string sAllInvoices;
        string sAllInvoiceNum;
        string sAllInvoiceDate;
        string sAllTotalCost;
        string sSelection;
        public string sInvoiceNum { get; set; } //this is assigned a value when the user selects from the dropdown or from the datagrid
        public string sInvoiceDate { get; set; } //this is assigned a value when the user selects from the dropdown or from the datagrid
        public string sTotalCost { get; set; } //this is assigned a value when the user selects from the dropdown or from the datagrid

        //SQL code

        /// <summary>
        /// This SQL gets all the invoice numbers from the invoices table
        /// </summary>
        /// <returns>all invoice numbers</returns>
        public string AllInvoiceNum()
        {
            try
            {

                sAllInvoiceNum = "SELECT InvoiceNum FROM Invoices";
                return sAllInvoiceNum;

            }

            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This SQL gets all values from the invoices table
        /// </summary>
        /// <returns>all values</returns>
        public string AllInvoices()
        {
            try
            {

                sAllInvoices = "SELECT * FROM Invoices";
                return sAllInvoices;
            }

            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This SQL gets all invoice dates from the invoice table
        /// </summary>
        /// <returns>all invoice dates</returns>
        public string AllInvoiceDate()
        {
            try
            {
                sAllInvoiceDate = "SELECT InvoiceDate FROM Invoices";
                return sAllInvoiceDate;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }


        /// <summary>
        /// this SQL returns all total costs from the invoice table
        /// </summary>
        /// <returns>all total costs</returns>
        public string AllTotalCost()
        {
            try
            {

                sAllTotalCost = "SELECT TotalCost FROM Invoices ORDER BY TotalCost ASC";
                return sAllTotalCost;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }


        }

        /// <summary>
        /// This SQL returns gets the users selection values
        /// </summary>
        /// <returns>users selections</returns>
        public string DisplaySelection()
        {
            try
            {
                bool foundWhereClause = false;
                sSelection = "SELECT * FROM Invoices ";
                if (!string.IsNullOrWhiteSpace(sInvoiceNum))
                {
                    foundWhereClause = true;
                    sSelection += "WHERE InvoiceNum = " + sInvoiceNum;
                }
                if (!string.IsNullOrWhiteSpace(sInvoiceDate))
                {
                    sSelection += foundWhereClause ? "AND " : "WHERE ";
                    sSelection += "InvoiceDate = #" + sInvoiceDate + "# ";
                    foundWhereClause = true;
                }
                if (!string.IsNullOrWhiteSpace(sTotalCost))
                {
                    sSelection += foundWhereClause ? "AND " : "WHERE ";
                    sSelection += "TotalCost = " + sTotalCost;

                }

                return sSelection;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }


        }


        string sSQL;
        int iRet = 0;
        DataSet ds = new DataSet();

        /// <summary>
        /// This method returns a list of all the database invoices
        /// </summary>
        /// <returns>list of all database invoices</returns>
        public List<clsSearchLogic> GetInvoices()
        {
            try
            {
                clsSearchLogic cs = new clsSearchLogic();
                sSQL = AllInvoices();
                clsData = new clsDataAccess();
                ds = clsData.ExecuteSQLStatement(sSQL, ref iRet);

                List<clsSearchLogic> lstInvoices = new List<clsSearchLogic>();

                for (int i = 0; i < iRet; i++)
                {


                    lstInvoices.Add(new clsSearchLogic
                    {
                        sInvoiceNum = $"{ds.Tables[0].Rows[i]["InvoiceNum"]}",
                        sInvoiceDate = $"{ds.Tables[0].Rows[i]["InvoiceDate"]}",
                        sTotalCost = $"{ds.Tables[0].Rows[i]["TotalCost"]}"

                    });



                }

                return lstInvoices;
            }


            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This method compiles a list of invoice numbers
        /// </summary>
        /// <returns>list of invoice numbers</returns>
        public List<clsSearchLogic> GetInvoiceNum()
        {
            try
            {
                clsSearchLogic cs = new clsSearchLogic();
                sSQL = AllInvoiceNum();
                clsData = new clsDataAccess();
                ds = clsData.ExecuteSQLStatement(sSQL, ref iRet);

                List<clsSearchLogic> lstInvoices = new List<clsSearchLogic>();

                for (int i = 0; i < iRet; i++)
                {


                    lstInvoices.Add(new clsSearchLogic
                    {
                        sInvoiceNum = $"{ds.Tables[0].Rows[i]["InvoiceNum"]}"


                    });



                }

                return lstInvoices;
            }


            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This method compiles a list of invoice dates
        /// </summary>
        /// <returns>list of invoice dates</returns>
        public List<clsSearchLogic> GetInvoiceDate()
        {
            try
            {
                clsSearchLogic cs = new clsSearchLogic();
                sSQL = AllInvoiceDate();
                clsData = new clsDataAccess();
                ds = clsData.ExecuteSQLStatement(sSQL, ref iRet);

                List<clsSearchLogic> lstInvoices = new List<clsSearchLogic>();

                for (int i = 0; i < iRet; i++)
                {


                    lstInvoices.Add(new clsSearchLogic
                    {
                        sInvoiceDate = $"{ds.Tables[0].Rows[i]["InvoiceDate"]}"


                    });



                }

                return lstInvoices;
            }


            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
        /// <summary>
        /// This method compiles a list of total cost
        /// </summary>
        /// <returns>list of total costs</returns>
        public List<clsSearchLogic> GetTotalCost()
        {
            try
            {
                clsSearchLogic cs = new clsSearchLogic();
                sSQL = AllTotalCost();
                clsData = new clsDataAccess();
                ds = clsData.ExecuteSQLStatement(sSQL, ref iRet);

                List<clsSearchLogic> lstInvoices = new List<clsSearchLogic>();

                for (int i = 0; i < iRet; i++)
                {


                    lstInvoices.Add(new clsSearchLogic
                    {
                        sTotalCost = $"{ds.Tables[0].Rows[i]["TotalCost"]}"


                    });



                }

                return lstInvoices;
            }


            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
        /// <summary>
        /// This method compiles list of users selected search criteria
        /// </summary>
        /// <returns>list of search criteria</returns>
        public List<clsSearchLogic> ReturnSelection()
        {
            try
            {
                clsSearchLogic cs = new clsSearchLogic();
                sSQL = DisplaySelection();
                clsData = new clsDataAccess();
                ds = clsData.ExecuteSQLStatement(sSQL, ref iRet);

                List<clsSearchLogic> lstInvoices = new List<clsSearchLogic>();

                for (int i = 0; i < iRet; i++)
                {


                    lstInvoices.Add(new clsSearchLogic
                    {
                        sInvoiceNum = $"{ds.Tables[0].Rows[i]["InvoiceNum"]}",
                        sInvoiceDate = $"{ds.Tables[0].Rows[i]["InvoiceDate"]}",
                        sTotalCost = $"{ds.Tables[0].Rows[i]["TotalCost"]}"


                    });



                }

                return lstInvoices;
            }


            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }



    }
}