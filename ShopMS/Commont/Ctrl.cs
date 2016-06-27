using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace ShopMS.Commont
{
    class Ctrl
    {
        public static void sqlGridView(string sql, DataGridView grid)
        {
            grid.DataSource = DB.query(sql).datatable();
        }

        public static void sqlGridView(DB db, DataGridView grid)
        {
            grid.DataSource = db.datatable();
        }

        public static void sqlCombo(string sql, ComboBox com, string valName = "", string disName = "")
        {
            DataTable dt = DB.query(sql).datatable();
            com.DataSource = dt;
            com.ValueMember = dt.Columns[0].ColumnName;
            com.DisplayMember = dt.Columns[1].ColumnName;
            if (valName != "") com.ValueMember = valName;
            if (disName != "") com.DisplayMember = disName;
        }

        public static void sqlCombo(DB db, ComboBox com, string valName = "", string disName = "")
        {
            DataTable dt = db.datatable();
            com.DataSource = dt;
            com.ValueMember = dt.Columns[0].ColumnName;
            com.DisplayMember = dt.Columns[1].ColumnName;
            if (valName != "") com.ValueMember = valName;
            if (disName != "") com.DisplayMember = disName;
        }

    }
}
