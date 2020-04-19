using CS3280GP.Main;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS3280GP.Items
{
    class clsItemsLogic
    {

        clsDataAccess db;
        clsItemsSQL Query;
        private object listItems;

        public string ItemDescIn { get; private set; }

        /// <summary>
        /// Initiate the class level
        /// </summary>
        public clsItemsLogic() 
            {

                try 
	            {	        
		            Query = new clsItemsSQL();
                    db = new clsDataAccess();
	            }
	            catch (Exception e)
	            {

                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
}
            }
        /// <summary>
        /// This will get all the items 
        /// </summary>
        /// <returns></returns>
        public List<clsLineItems> items()
        {
            try
            {
                List<clsLineItems> items = new List<clsLineItems>();

                int rows = 0;
                DataSet rawdata = db.ExecuteSQLStatement(Query.ItemCodeItemDescripCost(ItemDescIn), ref rows);

                for (int x = 0; x < rows; x++)
                {
                    Double.TryParse(rawdata.Tables[0].Rows[x][2].ToString(), out double total);

                    items.Add(new clsLineItems
                    {
                        ItemCode = rawdata.Tables[0].Rows[x][0].ToString(),
                        ItemDesc = rawdata.Tables[0].Rows[x][1].ToString(),
                        Cost = total
                    });
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// This class should update the Item Description
        /// </summary>
        /// <param name="ItemDescInUpdate"></param>
        /// <param name="ItemCodeInUpdate"></param>
        public void UpdateItemDesc(string ItemDescInUpdate, string ItemCodeInUpdate)
        {
            try
            {
                db.ExecuteNonQuery(Query.UpdateItemDesc(ItemDescInUpdate, ItemCodeInUpdate));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// This class will delete from the database
        /// </summary>
        /// <param name="ItemDescInDelete"></param>
        public void ItemDescDelete(string ItemDescInDelete)
        {
            try
            {
                string input = ItemDescInDelete;
                ItemDescDelete(input);
                
                db.ExecuteNonQuery(Query.ItemDescDelete(input));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }

        /// <summary>
        /// This will create new
        /// </summary>
        /// <param name="id"></param>
        /// <param name="listItems"></param>
        public void InsertItemDesc(string ItemCodeInInsert, string ItemDescInInsert, string CostInInsert)
        {
            string itemCode = "";
            string itemDesc = "";
            string itemCost = "";
            try
            {
                    db.ExecuteNonQuery(Query.InsertItemDesc(itemCode, itemDesc, itemCost));
                
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType + "." +
                    MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }
    }
}
