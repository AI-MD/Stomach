using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Stomach
{
    using System;
    using System.Data.SQLite;
    using System.Data;
    using System.Windows.Forms;


    class DBAdapter
    {
        string path; 
        string dataSource; 
        
        SQLiteDataAdapter adpt;
        
        public DBAdapter()
        {
            path ="./record_stomach.db"; 
            dataSource = "Data Source="+ path;

            dbCreate();
        }

        public DataSet SelectAll(string table)
        {
            try
            {
                DataSet ds = new DataSet();

                string sql = $"SELECT * FROM {table}";
                adpt = new SQLiteDataAdapter(sql, dataSource);
                adpt.Fill(ds, table);

                if (ds.Tables.Count > 0) return ds;
                else return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public DataSet SelectDetail(string table, string condition, string where = "")
        {
            try
            {
                DataSet ds = new DataSet();

                string sql = $"SELECT {condition} FROM {table} {where}";
                adpt = new SQLiteDataAdapter(sql, dataSource);
                adpt.Fill(ds, table);

                if (ds.Tables.Count > 0) return ds;
                else return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public void Insert(string table, string value)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(dataSource))
                {
                    conn.Open();
                    string sql = $"INSERT INTO {table} VALUES ({value})";
                    Console.WriteLine(sql);
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public void Update(string table, string setvalue, string wherevalue = "")
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(dataSource))
                {
                    conn.Open();
                    string sql = $"UPDATE {table} SET {setvalue} WHERE {wherevalue}";
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }

        public void DeleteAll(string table)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(dataSource))
                {
                    conn.Open();
                    string sql = $"DELETE FROM {table}";
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }

        public void DeleteDetail(string table, string wherecol, string wherevalue)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(dataSource))
                {
                    conn.Open();
                    string sql = $"DELETE FROM {table} WHERE {wherecol}='{wherevalue}'";
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }
        private  void dbCreate( )
        {
            try
            {
                //sqlite.db가 해당 경로 폴더 안에 있는지 체크
                if (!System.IO.File.Exists(path))
                {
                    SQLiteConnection.CreateFile(path);
                }
                else
                {
                    //MessageBox.Show("created DB");
                    return;
                }

                // 테이블 생성 코드
                SQLiteConnection sqliteConn = new SQLiteConnection(dataSource);
                sqliteConn.Open();

                string strsql = "CREATE TABLE IF NOT EXISTS record_stomach (caseID TEXT PRIMARY KEY, recordDate TEXT, all_time TEXT, stomach_time TEXT, biopsy_flag INTEGER, E INTEGER, S1 INTEGER, S2 INTEGER, S3 INTEGER, S4 INTEGER, S5 INTEGER, D1 INTEGER, D2 INTEGER )";
                 
                SQLiteCommand cmd = new SQLiteCommand(strsql, sqliteConn);
                cmd.ExecuteNonQuery();
                sqliteConn.Close();

            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
            }
        }
    
    }
}
