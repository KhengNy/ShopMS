using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ShopMS.Commont
{
    class Utils
    {
        public static Boolean login(string userName, string password)
        {
            DataTable dt = DB.select("ID, FirstName, LastName, Hint, Password").from("[system].[USER]").where("UserName=N'" + userName + "'").datatable();
            if (dt.Rows.Count > 0){
                foreach (DataRow row in dt.Rows){
                    string hint = Crypto.Decrypt(row["Password"].ToString(), password);
                    if (string.Equals(row["Hint"].ToString(), hint)) return true;
                }
            }
            return false;
        }
    }
}
