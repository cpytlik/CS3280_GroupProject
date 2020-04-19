using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280GP.Items
{
    class clsItemsSQL
    {
        
        /// <summary>
        /// This query will return the ItemCode and Cost from the ItemDesc
        /// </summary>
        /// <param name="ItemDescIn"></param>
        /// <returns></returns>
        public string ItemCodeItemDescripCost(string ItemDescIn)
        {
            try
            {
                string sSQl = "select ItemCode, ItemDesc, Cost from ItemDesc";
                return sSQl;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// This will return the InvoiceNumber 
        /// </summary>
        /// <param name="ItemCodeIn"></param>
        /// <returns></returns>
        public string InvoiceNum(string ItemCodeIn)
        {
            try
            {
                string sSQl = "select distinct(InvoiceNum) from LineItems where ItemCode = ItemCodeIn";
                return sSQl;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// This will update the ItemDesc based off of the parameters passed in
        /// </summary>
        /// <param name="ItemCodeInUpdate"></param>
        /// <returns></returns>
        public string UpdateItemDesc(string ItemDescInUpdate, string ItemCodeInUpdate)
        {
            try
            {
                string sSQl = "Update ItemDesc Set ItemDesc = ItemDescInUpdate, Cost = 123 where ItemCode = ItemCodeIn2";
                return sSQl;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }   

        /// <summary>
        /// This will Insert the data into the database
        /// </summary>
        /// <param name="ItemCodeInInsert"></param>
        /// <param name="ItemDescInInsert"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public string InsertItemDesc(string ItemCodeInInsert, string ItemDescInInsert, string CostInInsert)
        {
            try
            {
                string sSQl = "Insert into ItemDesc (ItemCode, ItemDesc, Cost) Values (ItemCodeInInsert, ItemDescInInsert, CostInInsert)";
                return sSQl;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// This will Delete from the database
        /// </summary>
        /// <param name="ItemDescInDelete"></param>
        /// <returns></returns>
        public string ItemDescDelete(string ItemDescInDelete)
        {
            try
            {
                string sSQl = "Delete from ItemDesc Where ItemCode = ItemDescInDelete";
                return sSQl;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }
    }
}
