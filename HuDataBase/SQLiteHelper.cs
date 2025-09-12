using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Reflection.PortableExecutable;

namespace HuDataBase
{
    public class SQLiteHelper : IDataBase
    {
        private string dbPath;

        public SQLiteHelper(string dataBaseName)
        {
            dbPath = System.AppDomain.CurrentDomain.BaseDirectory + $"{dataBaseName}.db";
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        public bool CreateDataBase()
        {
            try
            {
                if (!File.Exists(dbPath))
                {
                    SQLiteConnection.CreateFile(dbPath);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("新建数据库文件" + dbPath + "失败：" + ex.Message);
            }
        }
        /// <summary>
        /// 判断表是否存在
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool TableExist(string table)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"data source={dbPath}"))
            {
                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM sqlite_master where type='table' and name='" + table + "';";
                    int row = Convert.ToInt32(cmd.ExecuteScalar());
                    if (0 < row)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        /// <summary>
        /// 创建表,使用前请先调用GetColumns方法，或者自己写语句
        /// 例如：ID integer PRIMARY KEY autoincrement,Name varchar(200) default NULL,Age Int32 default NULL
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="Columns">字段SQL语句</param>
        /// <exception cref="Exception"></exception>
        public void CreateTable(string table, List<string> Columns)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection($"data source={dbPath}"))
                {
                    connection.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(connection))
                    {
                        string Column = "";
                        for (int i = 0; i < Columns.Count; i++)
                        {
                            Column += Columns[i] + ",";
                        }
                        Column = Column.Substring(0, Column.Length - 1);
                        cmd.CommandText = " CREATE TABLE " + table + "(" + Column + ")";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("创建表" + table + "失败：" + ex.Message);
            }
        }
        /// <summary>
        /// 获取类的属性名称和类型，并转换成数据库字段
        /// 例如：ID integer PRIMARY KEY autoincrement,Name varchar(200) default NULL,Age Int32 default NULL
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<string> GetColumns<T>(T model) where T : class
        {
            List<string> Columns = new List<string>();
            System.Reflection.PropertyInfo[] properties = model.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (properties.Length <= 0)
            {
                throw new Exception("类属性长度为零");
            }
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string Name = "";
                if (item.Name.ToLower().Equals("id"))
                {
                    Name = item.Name + " integer PRIMARY KEY autoincrement";
                }
                else if (item.Name.Contains("Img"))
                {
                    Name = item.Name + " " + " text default NULL";
                }
                else if (item.PropertyType.ToString().Contains("DateTime") || item.PropertyType.ToString().Contains("String"))
                {
                    Name = item.Name + " " + " varchar(200) default NULL";
                }
                else if (item.PropertyType.ToString().Contains("Int32"))
                {
                    Name = item.Name + " " + item.PropertyType.ToString().Split('.').Last().Replace("]", "") + " default NULL";
                }
                else
                {
                    Name = item.Name + " " + item.PropertyType.ToString().Split('.').Last().Replace("]", "") + "(12, 4) default NULL";
                }
                Columns.Add(Name);
            }
            return Columns;
        }
        /// <summary>
        /// 添加字段，使用前请先调用GetColumns方法
        /// 字段格式例如：ID integer PRIMARY KEY autoincrement,Name varchar(200) default NULL,Age Int32 default NULL
        /// </summary>
        /// <param name="table"></param>
        /// <param name="Colms"></param>
        /// <exception cref="Exception"></exception>
        public void CreateColunm(string table, List<string> colms)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"data source={dbPath}"))
            {
                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    var sql = "select sql from sqlite_master where tbl_name='" + table + "' and type='table';";
                    cmd.CommandText = sql;
                    var com = cmd.ExecuteScalar();
                    for (var i = 0; i < colms.Count; i++)
                    {
                        try
                        {
                            if (!com.ToString().Contains(colms[i]))
                            {
                                var sql2 = "alter table " + table + " add column " + colms[i] + ";";
                                cmd.CommandText = sql2;
                                cmd.ExecuteScalar();
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("新增字段{0}失败" + ex.Message + colms[i]);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 添加字段
        /// 字段格式例如：ID integer PRIMARY KEY autoincrement,Name varchar(200) default NULL,Age Int32 default NULL
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="colm">字段</param>
        /// <param name="colType">字段类型</param>
        /// <exception cref="Exception"></exception>
        public void CreateColunm(string table, string colm)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"data source={dbPath}"))
            {
                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    var sql = "select sql from sqlite_master where tbl_name='" + table + "' and type='table';";
                    cmd.CommandText = sql;
                    var com = cmd.ExecuteScalar();
                    try
                    {
                        if (!com.ToString().Contains(colm))
                        {
                            var sql2 = $"alter table add column {colm} ;";
                            cmd.CommandText = sql2;
                            cmd.ExecuteScalar();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("新增字段{0}失败" + ex.Message + colm);
                    }
                }
            }
        }
        /// <summary>
        /// 判断字段是否存在
        /// </summary>
        /// <param name="table"></param>
        /// <param name="colm"></param>
        /// <exception cref="Exception"></exception>
        public bool ColumnExist(string table, string colm)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"data source={dbPath}"))
            {
                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(connection))
                {
                    var sql = "select sql from sqlite_master where tbl_name='" + table + "' and type='table';";
                    cmd.CommandText = sql;
                    var com = cmd.ExecuteScalar();
                    try
                    {
                        return com.ToString().Contains(colm);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("判断字段是否存在{0}失败" + ex.Message + colm);
                    }
                }
            }
        }
        /// <summary>
        /// 删除字段，SQLite不支持删除字段，请自行备份数据，重建表
        /// </summary>
        /// <param name="table"></param>
        /// <param name="colm"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool DeleteColumn(string table, string colm)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 保存数据，注意时间格式(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="table"></param>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public bool Add(string table, Dictionary<string, string> keyValues)
        {
            string keys_string = "( ";
            string value_string = "( ";
            foreach (var item in keyValues)
            {
                keys_string += "'" + item.Key + "',";
                value_string += "'" + item.Value + "',";
            }
            keys_string = keys_string.Substring(0, keys_string.Length - 1);
            value_string = value_string.Substring(0, value_string.Length - 1);
            keys_string += ")";
            value_string += ")";
            string sql = string.Format("INSERT INTO " + table + " {0} VALUES {1} ;", keys_string, value_string);
            return SqliteDbTransaction(sql);
        }
        /// <summary>
        /// 保存数据，注意时间格式(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="table">表名</param>
        /// <param name="model">数据类实列</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool Add<T>(string table, T model) where T : class
        {
            System.Reflection.PropertyInfo[] properties = model.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (properties.Length <= 0)
            {
                throw new Exception("类属性长度为零");
            }
            string keys_string = "( ";
            string value_string = "( ";
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                var value = item.GetValue(model, null);
                if (item.Name.ToLower().Equals("id")) continue;
                keys_string += "'" + item.Name + "',";
                if (value == null)
                {
                    value_string += "'',";
                }
                else if (value is DateTime)
                {
                    value_string += "'" + Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                else
                {
                    value_string += "'" + value.ToString() + "',";
                }
            }
            keys_string = keys_string.Substring(0, keys_string.Length - 1);
            value_string = value_string.Substring(0, value_string.Length - 1);
            keys_string += ")";
            value_string += ")";
            string sql = string.Format("INSERT INTO " + table + " {0} VALUES {1} ;", keys_string, value_string);
            return SqliteDbTransaction(sql);
        }
        /// <summary>
        /// 更新数据，注意时间格式(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="keyValues">数据字典</param>
        /// <param name="where">判断条件(一定要加 and )</param>
        /// <returns></returns>
        public bool Update(string table, Dictionary<string, string> keyValues, string where = "")
        {
            string set_string = "";
            foreach (var item in keyValues)
            {
                set_string += " " + item.Key + "='" + item.Value + "',";
            }
            set_string = set_string.Substring(0, set_string.Length - 1);
            string sql = string.Format("UPDATE " + table + " SET {0} WHERE 1=1 {1} ;", set_string, where);
            return SqliteDbTransaction(sql);
        }
        /// <summary>
        /// 删除数据
        /// 注意：请谨慎使用，where条件为空时会删除所有数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="where">判断条件(一定要加 and )</param>
        /// <returns></returns>
        public bool Delete(string table, string where = "")
        {
            string sql = string.Format("DELETE FROM " + table + " WHERE 1=1 {0} ;", where);
            return SqliteDbTransaction(sql);
        }
        /// <summary>
        /// 判断数据是否存在
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="where">判断条件(一定要加 and )</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int QueryCount(string table, string where = "")
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection($"data source={dbPath}"))
                {
                    connection.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(connection))
                    {
                        cmd.CommandText = $" SELECT COUNT(*) FROM {table} WHERE 1=1 {where} ;";
                        SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        foreach (DataRow dd in dt.Rows)
                        {
                            object[] objects = dd.ItemArray;
                            if (objects == null || objects.Length <= 0) return 0;
                            return Convert.ToInt32(objects[0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("查询出错:" + table + "\r\n" + ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="table">表名</param>
        /// <param name="where">判断条件(一定要加 and )</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<T> Query<T>(string table, string where = "") where T : new()
        {
            try
            {
                System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                if (properties.Length <= 0)
                {
                    throw new Exception("类属性长度为零");
                }

                List<T> datas = new List<T>();
                using (SQLiteConnection connection = new SQLiteConnection($"data source={dbPath}"))
                {
                    connection.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(connection))
                    {
                        cmd.CommandText = $" SELECT * FROM {table} WHERE 1=1 {where} ;";
                        SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        List<string> columns = new List<string>();
                        for (var i = 0; i < dt.Columns.Count; i++)
                        {
                            columns.Add(dt.Columns[i].ColumnName);
                        }
                        foreach (DataRow dd in dt.Rows)
                        {
                            object[] objects = dd.ItemArray;
                            if (objects == null || objects.Length <= 0) continue;
                            Dictionary<string, object> pairs = new Dictionary<string, object>();
                            for (var i = 0; i < columns.Count; i++)
                            {
                                pairs[columns[i]] = objects[i];
                            }

                            var model = new T();
                            foreach (System.Reflection.PropertyInfo item in properties)
                            {
                                var value = pairs.ContainsKey(item.Name) ? pairs[item.Name] : null;
                                if (value is DBNull)
                                {
                                    if (item.PropertyType == typeof(string))
                                    {
                                        var ds = Convert.ChangeType(string.Empty, item.PropertyType);
                                        item.SetValue(model, ds, null);
                                    }
                                    else if (item.PropertyType == typeof(int) || item.PropertyType == typeof(double))
                                    {
                                        var ds = Convert.ChangeType(0, item.PropertyType);
                                        item.SetValue(model, ds, null);
                                    }
                                    else
                                    {
                                        var ds = Convert.ChangeType(0, item.PropertyType);
                                        item.SetValue(model, ds, null);
                                    }
                                }
                                else
                                {
                                    var ds = Convert.ChangeType(value, item.PropertyType);
                                    item.SetValue(model, ds, null);
                                }
                            }
                            datas.Add(model);
                        }
                    }
                }
                return datas;
            }
            catch (Exception ex)
            {
                throw new Exception("查询出错:" + table + where + "\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="where">判断条件(一定要加 and )</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Dictionary<string, object>> Query(string table, string where = "")
        {
            try
            {
                List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();
                using (SQLiteConnection connection = new SQLiteConnection($"data source={dbPath}"))
                {
                    connection.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(connection))
                    {
                        cmd.CommandText = $" SELECT * FROM {table} WHERE 1=1 {where} ; ";
                        SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        List<string> columns = new List<string>();
                        for (var i = 0; i < dt.Columns.Count; i++)
                        {
                            columns.Add(dt.Columns[i].ColumnName);
                        }
                        foreach (DataRow dd in dt.Rows)
                        {
                            object[] objects = dd.ItemArray;
                            if (objects == null || objects.Length <= 0) continue;
                            Dictionary<string, object> pairs = new Dictionary<string, object>();
                            for (var i = 0; i < columns.Count; i++)
                            {
                                pairs[columns[i]] = objects[i];
                            }
                            datas.Add(pairs);
                        }
                    }
                }
                return datas;
            }
            catch (Exception ex)
            {
                throw new Exception("查询出错:" + table + where + "\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="pageNumber">每页多少条</param>
        /// <param name="pageSize">当前页</param>
        /// <param name="where">判断条件(一定要加 and )</param>
        /// <returns></returns>
        public List<T> QueryPage<T>(string table, int pageNumber, int pageSize, string where = "") where T : new()
        {
            try
            {
                System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                if (properties.Length <= 0)
                {
                    throw new Exception("类属性长度为零");
                }
                using (SQLiteConnection connection = new SQLiteConnection($"data source={dbPath}"))
                {
                    connection.Open();

                    string sql = $@" SELECT * FROM (
                                    SELECT *, ROW_NUMBER() OVER (ORDER BY ID) AS RowNumber
                                    FROM {table} WHERE 1=1 {where}
                                    ) AS PagedTable
                                   WHERE 1=1 {where} AND RowNumber BETWEEN (({pageNumber}-1)*{pageSize} + 1) AND ({pageNumber}*{pageSize})";

                    List<T> datas = new List<T>();
                    using (SQLiteCommand cmd = new SQLiteCommand(connection))
                    {
                        cmd.CommandText = sql;
                        SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        List<string> columns = new List<string>();
                        for (var i = 0; i < dt.Columns.Count; i++)
                        {
                            columns.Add(dt.Columns[i].ColumnName);
                        }
                        foreach (DataRow dd in dt.Rows)
                        {
                            object[] objects = dd.ItemArray;
                            if (objects == null || objects.Length <= 0) continue;
                            Dictionary<string, object> pairs = new Dictionary<string, object>();
                            for (var i = 0; i < columns.Count; i++)
                            {
                                pairs[columns[i]] = objects[i];
                            }

                            var model = new T();
                            foreach (System.Reflection.PropertyInfo item in properties)
                            {
                                var value = pairs.ContainsKey(item.Name) ? pairs[item.Name] : null;
                                if (value is DBNull)
                                {
                                    if (item.PropertyType == typeof(string))
                                    {
                                        var ds = Convert.ChangeType(string.Empty, item.PropertyType);
                                        item.SetValue(model, ds, null);
                                    }
                                    else if (item.PropertyType == typeof(int) || item.PropertyType == typeof(double))
                                    {
                                        var ds = Convert.ChangeType(0, item.PropertyType);
                                        item.SetValue(model, ds, null);
                                    }
                                    else
                                    {
                                        var ds = Convert.ChangeType(0, item.PropertyType);
                                        item.SetValue(model, ds, null);
                                    }
                                }
                                else
                                {
                                    var ds = Convert.ChangeType(value, item.PropertyType);
                                    item.SetValue(model, ds, null);
                                }
                            }
                            datas.Add(model);
                        }
                        return datas;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("分页查询出错:" + table + where + "\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="pageNumber">每页多少条</param>
        /// <param name="pageSize">当前页</param>
        /// <param name="where">判断条件(一定要加 and )</param>
        /// <returns></returns>
        public List<Dictionary<string, object>> QueryPage(string table, int pageNumber, int pageSize, string where = "")
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection($"data source={dbPath}"))
                {
                    connection.Open();

                    string sql = $@" SELECT * FROM (
                                    SELECT *, ROW_NUMBER() OVER (ORDER BY ID) AS RowNumber
                                    FROM {table} WHERE 1=1 {where}
                                    ) AS PagedTable
                                   WHERE 1=1 {where} AND RowNumber BETWEEN (({pageNumber}-1)*{pageSize} + 1) AND ({pageNumber}*{pageSize})";
                    List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();
                    using (SQLiteCommand cmd = new SQLiteCommand(connection))
                    {
                        cmd.CommandText = sql;
                        SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        List<string> columns = new List<string>();
                        for (var i = 0; i < dt.Columns.Count; i++)
                        {
                            columns.Add(dt.Columns[i].ColumnName);
                        }
                        foreach (DataRow dd in dt.Rows)
                        {
                            object[] objects = dd.ItemArray;
                            if (objects == null || objects.Length <= 0) continue;
                            Dictionary<string, object> pairs = new Dictionary<string, object>();
                            for (var i = 0; i < columns.Count; i++)
                            {
                                pairs[columns[i]] = objects[i];
                            }
                            datas.Add(pairs);
                        }
                        return datas;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("分页查询出错:" + table + where + "\r\n" + ex.Message);
            }
        }

        private static readonly object LockObj = new object();
        private bool SqliteDbTransaction(string sqlString)
        {
            lock (LockObj)
            {
                using (SQLiteConnection connection = new SQLiteConnection($"data source={dbPath}"))
                {
                    connection.Open();

                    DbTransaction trans = connection.BeginTransaction();
                    try
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(connection))
                        {
                            int rows = 0;
                            cmd.CommandText = sqlString;
                            rows = cmd.ExecuteNonQuery();
                            trans.Commit();//提交事务
                            return rows > 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();//回滚事务
                        throw new Exception("提交数据库失败" + ex.Message);
                    }
                }
            }
        }

    }
}
