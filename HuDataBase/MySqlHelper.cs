using MySql.Data.MySqlClient;
using System.Text;

namespace HuDataBase
{
    public class MySqlHelper : IDataBase
    {
        public string _connectionString = string.Empty;
        private string server = string.Empty;
        private string user = string.Empty;
        private string pwd = string.Empty;
        private string database = string.Empty;

        public MySqlHelper(string server, string user, string pwd, string database)
        {
            this.server = server;
            this.user = user;
            this.pwd = pwd;
            this.database = database;
            _connectionString = $"Server={server};User={user};Password={pwd};port=3306;Database={database};SslMode=none;charset=utf8;";
        }
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <exception cref="Exception"></exception>
        public bool CreateDataBase()
        {
            try
            {
                string connectionString = $"Server={server};User={user};Password={pwd};port=3306;SslMode=none;charset=utf8;AllowPublicKeyRetrieval=True;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string createDatabaseQuery = $"CREATE DATABASE IF NOT EXISTS {database} DEFAULT CHARACTER SET utf8;";
                    using (MySqlCommand command = new MySqlCommand(createDatabaseQuery, connection))
                    {
                        var count = command.ExecuteNonQuery();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("创建数据库失败，" + ex.Message);
            }
        }
        /// <summary>
        /// 判断表是否存在
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool TableExist(string table)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string createDatabaseQuery = $"SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{database}' AND table_name = '{table}';";
                using (MySqlCommand command = new MySqlCommand(createDatabaseQuery, connection))
                {
                    var count = command.ExecuteNonQuery();
                    return count > 0;
                }
            }
        }
        /// <summary>
        /// 获取类的属性名称和类型，并转换成数据库字段语句
        /// 例如：ID INT AUTO_INCREMENT PRIMARY KEY,Name varchar(200) default NULL,Age Int32 default NULL
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
                    Name = item.Name + " INT AUTO_INCREMENT PRIMARY KEY";
                }
                else if (item.Name.Contains("Img"))
                {
                    Name = item.Name + " " + " text DEFAULT NULL";
                }
                else if (item.PropertyType.ToString().Contains("DateTime"))
                {
                    Name = item.Name + " " + " TIMESTAMP DEFAULT CURRENT_TIMESTAMP";
                }
                else if (item.PropertyType.ToString().Contains("String"))
                {
                    Name = item.Name + " " + " Varchar(200) DEFAULT NULL";
                }
                else if (item.PropertyType.ToString().Contains("Int32"))
                {
                    Name = item.Name + " INT DEFAULT NULL";
                }
                else
                {
                    Name = item.Name + " FLOAT DEFAULT NULL";
                }
                Columns.Add(Name);
            }
            return Columns;
        }
        /// <summary>
        /// 创建表,使用前请先调用GetColumns方法，或者自己写语句
        /// 例如：ID INT AUTO_INCREMENT PRIMARY KEY,Name varchar(200) default NULL,Age Int32 default NULL
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="Columns">字段SQL语句</param>
        /// <exception cref="Exception"></exception>
        public void CreateTable(string table, List<string> colms)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                StringBuilder builder = new StringBuilder();
                for (var i = 0; i < colms.Count; i++)
                {
                    if (i == colms.Count - 1)
                    {
                        builder.Append(colms[i]);
                    }
                    else
                    {
                        builder.Append(colms[i] + ",");
                    }
                }
                string query = $"CREATE TABLE {table} ({builder.ToString()});";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    var count = command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 添加字段
        /// 字段格式例如：ID INT AUTO_INCREMENT PRIMARY KEY,Name varchar(200) default NULL,Age Int32 default NULL
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="colm">字段</param>
        /// <exception cref="Exception"></exception>
        public void CreateColunm(string table, List<string> colms)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    foreach (var colm in colms)
                    {
                        var colName = colm.Split(' ')[0];
                        if (ColumnExist(table, colName)) continue;
                        string query = $"ALTER TABLE {table} ADD {colm} ;";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("添加字段失败，" + ex.Message);
            }
        }
        /// <summary>
        /// 添加字段
        /// 字段格式例如：ID INT AUTO_INCREMENT PRIMARY KEY,Name varchar(200) default NULL,Age Int32 default NULL
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="colm">字段</param>
        /// <exception cref="Exception"></exception>
        public void CreateColunm(string table, string colm)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var colName = colm.Split(' ')[0];
                    if (ColumnExist(table, colName)) return;
                    string query = $"ALTER TABLE {table} ADD {colm} ;";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("添加字段失败，" + ex.Message);
            }
        }
        /// <summary>
        /// /删除字段
        /// </summary>
        /// <param name="table"></param>
        /// <param name="colm"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool DeleteColumn(string table, string colm)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = $"ALTER TABLE {table} DROP COLUMN {colm} ;";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    var count = command.ExecuteNonQuery();
                    return count > 0;
                }
            }
        }
        /// <summary>
        /// 判断是否存在字段
        /// </summary>
        /// <param name="table"></param>
        /// <param name="colm"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool ColumnExist(string table, string colm)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = $@"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS 
                               WHERE TABLE_NAME = '{table}' 
                               AND COLUMN_NAME = '{colm}';";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    var count = command.ExecuteNonQuery();
                    return count > 0;
                }
            }
        }
        /// <summary>
        /// 保存数据，注意时间格式(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="table"></param>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool Add(string table, Dictionary<string, string> keyValues)
        {
            try
            {
                StringBuilder columnBuilder = new StringBuilder();
                StringBuilder valueBuilder = new StringBuilder();
                var index = 0;
                foreach (var item in keyValues)
                {
                    columnBuilder.Append(item.Key);
                    valueBuilder.Append($"'{item.Value}'");
                    if (index < keyValues.Count - 1)
                    {
                        columnBuilder.Append(", ");
                        valueBuilder.Append(", ");
                    }
                    index++;
                }
                string query = $"INSERT INTO {table} ({columnBuilder.ToString()}) VALUES ({valueBuilder.ToString()});";
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        var count = command.ExecuteNonQuery();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("添加数据失败，" + ex.Message);
            }
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
            try
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
                    keys_string += item.Name + ",";
                    if (value == null)
                    {
                        value_string += "'',";
                    }
                    else if (value is DateTime)
                    {
                        var dt = (DateTime)value;
                        value_string += "'" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',";
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
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        var count = command.ExecuteNonQuery();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 更新数据，注意时间格式(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="table"></param>
        /// <param name="keyValues"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Update(string table, Dictionary<string, string> keyValues, string where = "")
        {
            try
            {
                StringBuilder setBuilder = new StringBuilder();
                var index = 0;
                foreach (var item in keyValues)
                {
                    setBuilder.Append($"{item.Key} = '{item.Value}'");
                    if (index < keyValues.Count - 1)
                    {
                        setBuilder.Append(", ");
                    }
                    index++;
                }
                string query = $"UPDATE {table} SET {setBuilder.ToString()} WHERE 1=1 {where} ;";
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        var count = command.ExecuteNonQuery();
                        return (count > 0);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = $"DELETE FROM {table} WHERE 1=1 {where};";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    var count = command.ExecuteNonQuery();
                    return (count > 0);
                }
            }
        }
        /// <summary>
        /// 判断数据是否存在
        /// </summary>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int QueryCount(string table, string where = "")
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = $"SELECT COUNT(*) FROM {table} WHERE 1=1 {where} ;";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        int count = (int)command.ExecuteScalar();
                        return count;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var query = $" SELECT * FROM {table} WHERE 1=1 {where} ;";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
                            while (reader.Read())
                            {
                                Dictionary<string, object> row = new Dictionary<string, object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row[reader.GetName(i)] = reader.GetValue(i);
                                }
                                var model = new T();
                                foreach (System.Reflection.PropertyInfo item in properties)
                                {
                                    var value = row.ContainsKey(item.Name) ? row[item.Name] : null;
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
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = $"SELECT * FROM {table} WHERE 1=1 {where};";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
                        while (reader.Read())
                        {
                            Dictionary<string, object> row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.GetValue(i);
                            }
                            result.Add(row);
                        }
                        return result;
                    }
                }
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
                List<T> datas = new List<T>();
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = $@" SELECT * FROM (
                                    SELECT *, ROW_NUMBER() OVER (ORDER BY ID) AS RowNumber
                                    FROM {table} WHERE 1=1 {where}
                                    ) AS PagedTable
                                   WHERE 1=1 {where} AND RowNumber BETWEEN (({pageNumber}-1)*{pageSize} + 1) AND ({pageNumber}*{pageSize})";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
                            while (reader.Read())
                            {
                                Dictionary<string, object> row = new Dictionary<string, object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row[reader.GetName(i)] = reader.GetValue(i);
                                }
                                var model = new T();
                                foreach (System.Reflection.PropertyInfo item in properties)
                                {
                                    var value = row.ContainsKey(item.Name) ? row[item.Name] : null;
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
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string sql = $@" SELECT * FROM (
                                    SELECT *, ROW_NUMBER() OVER (ORDER BY ID) AS RowNumber
                                    FROM {table} WHERE 1=1 {where}
                                    ) AS PagedTable
                                   WHERE 1=1 {where} AND RowNumber BETWEEN (({pageNumber}-1)*{pageSize} + 1) AND ({pageNumber}*{pageSize})";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
                        while (reader.Read())
                        {
                            Dictionary<string, object> row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.GetValue(i);
                            }
                            result.Add(row);
                        }
                        return result;
                    }
                }
            }
        }

    }
}
