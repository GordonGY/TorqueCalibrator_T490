using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Reflection;
using MySqlConnector;

namespace TorqueCalibrator.untils
{
    public static class MysqlTool
    {
        private static readonly string StrCon = ConfigurationManager.ConnectionStrings["MysqlConnection"].ToString();
        //废弃代码
        #region
        ////static String connetStr = "server=127.0.0.1;port=3306;user=root;password=root; database=fridge;";
        //static MySqlConnection conn = new MySqlConnection();
        //static MySqlCommand cmd = new MySqlCommand();
        //public static bool inquire()
        //{
        //    conn = new MySqlConnection(connetStr);
        //    conn.Open();
        //    string sql = "select * from sys_workplace";
        //    MySqlCommand cmd = new MySqlCommand(sql, conn);
        //    MySqlDataReader reader = cmd.ExecuteReader();
        //    while (reader.Read())//初始索引是-1，执行读取下一行数据，返回值是bool
        //    {
        //        string str = reader.GetInt32("id") + reader.GetString("workplace_num") + reader.GetString("workplace_name");//"userid"是数据库对应的列名，推荐这种方式            
        //    }
        //    return true;
        //}


        //public static DataTable adoRead(string constr)
        //{
        //    conn.ConnectionString = connetStr;
        //    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(constr, conn);
        //    DataTable dt = new DataTable();
        //    mySqlDataAdapter.Fill(dt);
        //    return dt;
        //}

        ///// <summary>
        ///// 插入数据
        ///// </summary>
        ///// <param name="insert_str">插入的字符串</param>
        ///// <returns>被修改的数据条数</returns>
        //public static int adoInsert(string insert_str)
        //{
        //    try
        //    {
        //        conn.ConnectionString = connetStr;
        //        conn.Open();
        //        cmd = new MySqlCommand(insert_str, conn);
        //        int num = cmd.ExecuteNonQuery();
        //        conn.Close();
        //        return num;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public static int adoDelete(string delete_str)
        //{
        //    try
        //    {
        //        conn.ConnectionString = connetStr;
        //        conn.Open();

        //        conn.Close();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        #endregion


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sqlStr">查询语句</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public static DataTable queryData(string sqlStr, params MySqlParameter[] parameter)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(StrCon))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
                    DataSet dt = new DataSet();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    cmd.Parameters.AddRange(parameter);
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dt);
                    conn.Close();
                    return dt.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("查询数据异常" + ex.Message);
            }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="sqlStr">插入语句</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public static bool addData(string sqlStr, params MySqlParameter[] parameter)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(StrCon))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
                    cmd.Parameters.AddRange(parameter);
                    var row = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (row > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("添加数据异常" + ex.Message);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sqlStr">删除语句</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public static bool deleteData(string sqlStr, params MySqlParameter[] parameter)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(StrCon))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
                    cmd.Parameters.AddRange(parameter);
                    var row = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (row > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("删除数据异常" + ex.Message);
            }
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="sqlStr">更新语句</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public static bool updateData(string sqlStr, params MySqlParameter[] parameter)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(StrCon))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
                    cmd.Parameters.AddRange(parameter);
                    //cmd.CommandText = "update `fridge`.`pro_pset_list_actual` set `id_workplace`=1, `torque_index`=1, `index`=1, `pset`=123, `torque_set`=12, `angle_set`=13, `num`=8, `version`=1 WHERE product_kind='haier2'";
                    var row = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (row > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("更新数据异常" + ex.Message);
            }
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="sqlStr">插入语句</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public static int addOneData(string sqlStr, params MySqlParameter[] parameter)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(StrCon))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
                    cmd.Parameters.AddRange(parameter);
                    var row = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (row > 0)
                    {
                        return (int)cmd.LastInsertedId;
                    }
                    return -1;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("添加数据异常" + ex.Message);
            }
        }

        /// <summary>    
        /// DataTable 转换为List 集合    
        /// </summary>    
        /// <typeparam name="TResult">类型</typeparam>    
        /// <param name="dt">DataTable</param>    
        /// <returns></returns>    
        public static List<T> toList<T>(this DataTable dt) where T : class, new()
        {
            //创建一个属性的列表    
            List<PropertyInfo> prlist = new List<PropertyInfo>();
            //获取TResult的类型实例  反射的入口    

            Type t = typeof(T);
            var cc = t.GetProperties();
            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表     
            Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });

            //创建返回的集合    

            List<T> oblist = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例    
                T ob = new T();
                //找到对应的数据  并赋值    
                prlist.ForEach(p => { if (row[p.Name] != DBNull.Value) p.SetValue(ob, row[p.Name], null); });
                //放入到返回的集合中.    
                oblist.Add(ob);
            }
            return oblist;
        }

        #region 事务相关
        static MySqlCommand cmdTrans;
        static MySqlConnection connTrans;
        static MySqlTransaction trans;
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="sqlStr">插入语句</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public static void beginTransaction()
        {
            try
            {
                connTrans = new MySqlConnection(StrCon);
                connTrans.Open();

                trans = connTrans.BeginTransaction(IsolationLevel.ReadCommitted);
                cmdTrans = new MySqlCommand();
                cmdTrans.Connection = trans.Connection;
                cmdTrans.Transaction = trans;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("开启事务失败" + ex.Message);
            }
        }
        /// <summary>
        /// 添加事务,一个个添加
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public static int addTransaction(string sqlStr)
        {
            try
            {
                cmdTrans.CommandText = sqlStr;
                cmdTrans.ExecuteNonQuery();
                return 0;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                connTrans.Close();
                throw new ApplicationException("事务执行失败,已经回滚" + ex.Message);
                //return -1;
            }
        }
        /// <summary>
        /// 事务的最终提交
        /// </summary>
        /// <returns></returns>
        public static int executeTransaction()
        {
            try
            {
                trans.Commit();
                connTrans.Close();
                return 0;
            }
            catch (Exception ex)
            {
                //throw new ApplicationException("事务提交失败" + ex.Message);
                return -1;
            }
        }
        #endregion
    }
}
