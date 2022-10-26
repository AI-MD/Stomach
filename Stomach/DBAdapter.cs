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
            path ="./record_stomach_new.db"; 
            dataSource = "Data Source="+ path;
            dbCreate();
            dbRecordCreate();
            dbMemberCreate();

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
        public long record_Insert(string table, string value)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(dataSource))
                {
                    conn.Open();
                    string sql = $"INSERT INTO {table} (caseID, recordDate, all_time, stomach_time, Biopsy, E, S1, S2, S3, S4, S5, D1, D2 ,X, S6, Sedation, AI_use, name) VALUES ({value});";

                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = conn;

     
                    cmd.CommandText = sql+ "SELECT last_insert_rowid();";
                    
                    return (long)cmd.ExecuteScalar();
                  

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }

            return -1;
        }

        public void member_Insert(string table, string value)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(dataSource))
                {
                    conn.Open();
                    string sql = $"INSERT INTO {table} (name, regdate) VALUES ({value})";
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
               //MessageBox.Show(e.ToString());
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
                    
                }

              
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
            }
        }

        private void dbRecordCreate()
        {
            try
            {
                // 테이블 생성 코드
                SQLiteConnection sqliteConn = new SQLiteConnection(dataSource);
                sqliteConn.Open();

                string strsql = "CREATE TABLE IF NOT EXISTS record_stomach_new(record_index INTEGER PRIMARY KEY AUTOINCREMENT, caseID TEXT, recordDate TEXT, all_time TEXT, stomach_time TEXT,  Biopsy INTEGER, E INTEGER, S1 INTEGER, S2 INTEGER, S3 INTEGER, S4 INTEGER, S5 INTEGER, D1 INTEGER, D2 INTEGER, X INTEGER, S6 INTEGER, Sedation INTEGER, AI_use INTEGER, name TEXT )";

                SQLiteCommand cmd = new SQLiteCommand(strsql, sqliteConn);
                cmd.ExecuteNonQuery();
                sqliteConn.Close();

              
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
            }
        }
        private void dbMemberCreate()
        {
            try
            { 

                // 테이블 생성 코드
                SQLiteConnection sqliteConn = new SQLiteConnection(dataSource);
                sqliteConn.Open();

                string strsql = "CREATE TABLE IF NOT EXISTS member(memberID Integer PRIMARY KEY autoincrement, name Text, regdate Text )";

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
