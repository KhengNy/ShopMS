using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ShopMS.Commont
{
    class DB
    {
        private SqlConnection con = null;
        private SqlCommand cmd = null;
        private SqlDataAdapter da = null;
        private SqlDataReader dr = null;
        private DataSet ds = null;
        private string sqlQuery = "";
        private string strSelect = "";
        private string strFrom = "";
        private string[] joins = null;
        private string strWhere = "";
        private string[] ands = null;
        private string[] ors = null;
        private string strGroupBy = "";
        private string strOrderBy = "";
        private int limitMin = 0;
        private int limitMax = 0;
        private string strInto = "";
        private Dictionary<string, string> fields = null;
        private string strQuery = "";
        private string action = "";
        private string tbUpdate = "";
        private const string sqlDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        private DataTable dt = null;

        public DB()
        {
            connect();
        }

        private void connect()
        {
            string strCon = "Data Source=.;Initial Catalog=DB_SHOP;Integrated Security=True";
            this.con = new SqlConnection(strCon);
            try{
                this.con.Open();
            } catch (SqlException ex){
                this.message(ex.Message);
            }
        }

        // DB.select("*").from("table1 as t1").join("table2 as t2 on t1.id = t2.t1_id").join(...)
        //   .where("t1.status = 'ACTIVE'").or("t1.id != null").or(...).and("t2.name = t1.name").and(...).dataset()
        //   or .run() or .datatable() or .read();
        public static DB select(string str)
        {
            DB db = new DB();
            db.strSelect = str;
            return db;
        }

        public DB from(string str)
        {
            this.strFrom = str;
            return this;
        }

        public DB join(string str)
        {
            this.joins[this.joins.Length] = str;
            return this;
        }

        public DB and(string str)
        {
            this.ands[this.ands.Length] = str;
            return this;
        }

        public DB or(string str)
        {
            this.ors[this.ors.Length] = str;
            return this;
        }

        public DB where(string str)
        {
            this.strWhere = str;
            return this;
        }

        public DB groupBy(string str)
        {
            this.strGroupBy = str;
            return this;
        }

        public DB orderBy(string str)
        {
            this.strOrderBy = str;
            return this;
        }

        public DB limit(int min = 0, int max = 0)
        {
            if(max > 0){
                this.limitMin = min;
                this.limitMax = max;
            }
            return this;
        }

        public DB limit(int max = 0)
        {
            if (max > 0){
                this.limitMax = max;
            }
            return this;
        }

        // DB.query("sql_statement").read() or .run() or .dataset() or .datatable()
        public static DB query(string str)
        {
            DB db = new DB();
            db.action = "QUERY";
            db.strQuery = str;
            return db;
        }

        // DB.insert().into("table_name").set("field_name", "value").set(...).run();
        public static DB inset()
        {
            DB db = new DB();
            db.action = "INSERT";
            db.fields = new Dictionary<string,string>();
            return db;
        }

        public DB into(string str)
        {
            this.strInto = str;
            return this;
        }

        public DB set(string name, string value)
        {
            this.fields[name] = value;
            return this;
        }

        // DB.delete().from("table_name").join(...).where(...).or(...).and(...).run();
        public static DB delete()
        {
            DB db = new DB();
            db.action = "DELETE";
            return db;
        }

        // DB.update("table_name")set(...).where(...).or(...).and(...).run();
        public static DB update(string str)
        {
            DB db = new DB();
            db.tbUpdate = str;
            db.action = "UPDATE";
            db.fields = new Dictionary<string, string>();
            return db;
        }

        public void buildUpdateSql()
        {
            this.sqlQuery = "";
            if (this.tbUpdate != ""){
                this.sqlQuery += "UPDATE " + this.tbUpdate;
            }
            string sets = "";
            foreach (KeyValuePair<string, string> field in this.fields){
                if (sets != ""){
                    sets += ",";
                }
                sets += "[" + field.Key + "]='" + field.Value + "'";
            }
            if (sets != ""){
                sets += ", [UpdateDate]='" + DateTime.Now.ToString(sqlDateTimeFormat) + "'";
                this.sqlQuery += " SET " + sets;
            }
            this.buildWhere();
        }

        public void buildInsetSql()
        {
            this.sqlQuery = "";
            if (this.strInto != ""){
                this.sqlQuery += "INSERT INTO " + this.strInto;
            }
            string keys = "";
            string vals = "";
            foreach (KeyValuePair<string, string> field in this.fields){
                if (keys != "" && vals != ""){
                    keys += ",";
                    vals += ",";
                }
                keys += "[" + field.Key + "]";
                vals += "'" + field.Value + "'";
            }
            if (keys != "" && vals != ""){
                this.sqlQuery += " (" + keys + ", [CreateDate]) VALUES (" + vals + ", '" + DateTime.Now.ToString(sqlDateTimeFormat) + "')";
            }
        }

        public void buildDeleteSql()
        {
            this.sqlQuery = " DELETE ";
            this.buildFrom();
            this.buildWhere();
        }

        public void buildAnd()
        {
            if (this.ands != null){
                foreach (string a in this.ands){
                    if (this.strWhere == ""){
                        this.strWhere = a;
                        this.sqlQuery += " WHERE " + this.strWhere;
                    } else{
                        this.sqlQuery += " AND (" + a + ")";
                    }
                }
            }
        }

        public void buildOr()
        {
            if (this.ors != null){
                foreach (string o in this.ors){
                    if (this.strWhere == ""){
                        this.strWhere = o;
                        this.sqlQuery += " WHERE " + this.strWhere;
                    } else{
                        this.sqlQuery += " OR (" + o + ")";
                    }
                }
            }
        }

        public void buildFrom()
        {
            if (this.strFrom != ""){
                this.sqlQuery += " FROM " + this.strFrom;
            }
            if (this.joins != null){
                foreach (string jo in this.joins){
                    this.sqlQuery += " " + jo;
                }
            }
        }

        public SqlDataReader read()
        {
            try{
                this.dr = this.getCommand().ExecuteReader();
                return this.dr;
            } catch (SqlException ex){
                this.dr = null;
                this.message(ex.Message);
            }
            this.con.Close();
            return null;
        }

        public DB run()
        {
            this.getCommand().ExecuteNonQuery();
            this.con.Close();
            return this;
        }

        public DataSet dataset()
        {
            try {
                this.buildMainSql();
                if (this.sqlQuery != ""){
                    this.da = new SqlDataAdapter(this.sqlQuery, this.con);
                    this.ds = new DataSet();
                    this.da.Fill(this.ds);
                    this.con.Close();
                    return this.ds;
                }
            } catch (SqlException ex){
                this.ds = null;
                this.message(ex.Message);
            }
            return new DataSet();
        }
        
        public DataTable datatable(int index = 0)
        {
            try{
                this.dataset();
                if (this.ds != null){
                    this.dt = new DataTable();
                    this.dt = this.ds.Tables[index];
                    return this.dt;
                }
            } catch (SqlException ex){
                this.dt = null;
                this.message(ex.Message);
            }
            return new DataTable();
        }
        
        private SqlCommand getCommand(string sql = "")
        {
            try{
                if (sql != ""){
                    this.cmd = new SqlCommand(sql, this.con);
                    return this.cmd;
                } else{
                    this.buildMainSql();
                    if (this.sqlQuery != ""){
                        this.cmd = new SqlCommand(this.sqlQuery, this.con);
                        return this.cmd;
                    }
                }
            } catch(SqlException ex){
                this.message(ex.Message);
            }
            return null;
        }

        public void buildMainSql()
        {
            switch (this.action){
                case "INSERT": this.buildInsetSql();
                    break;
                case "DELETE": this.buildDeleteSql();
                    break;
                case "QUERY": this.buildQuerySql();
                    break;
                case "UPDATE": this.buildUpdateSql();
                    break;
                default: this.buildSql();
                    break;
            }
        }

        private void buildQuerySql()
        {
            this.sqlQuery = "";
            if (this.strQuery != ""){
                this.sqlQuery = this.strQuery;
            }
        }

        public void buildWhere()
        {
            if (this.strWhere != ""){
                this.sqlQuery += " WHERE " + this.strWhere;
            }
            this.buildAnd();
            this.buildOr();
        }

        private void buildSql()
        {
            this.sqlQuery = "";
            if (this.strSelect != ""){
                this.sqlQuery += "SELECT " + strSelect;
            }
            this.buildFrom();
            this.buildWhere();
            if(this.strGroupBy != ""){
                this.sqlQuery += " GROUP BY " + this.strGroupBy;
            }
            if (this.strOrderBy != ""){
                this.sqlQuery += " ORDER BY " + this.strOrderBy;
            }
            if (this.limitMin > 0 || this.limitMax > 0){
                if(this.limitMax > 0){
                    this.sqlQuery += " LIMIT " + this.limitMin + "," + this.limitMax;
                }
            }
        }

        private void message(string str)
        {
            MessageBox.Show(str);
        }
        

    }
}
