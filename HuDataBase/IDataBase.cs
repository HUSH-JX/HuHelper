using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuDataBase
{
    internal interface IDataBase
    {
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <exception cref="Exception"></exception>
        bool CreateDataBase();
        /// <summary>
        /// 判断表是否存在
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        bool TableExist(string table);
        /// <summary>
        /// 获取类的属性名称和类型，并转换成数据库字段语句
        /// 例如：ID integer PRIMARY KEY autoincrement,Name varchar(200) default NULL,Age Int32 default NULL
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        List<string> GetColumns<T>(T model) where T : class;
        /// <summary>
        /// 创建表,使用前请先调用GetColumns方法，或者自己写语句
        /// 例如：ID integer PRIMARY KEY autoincrement,Name varchar(200) default NULL,Age Int32 default NULL
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="Columns">字段SQL语句</param>
        /// <exception cref="Exception"></exception>
        void CreateTable(string table, List<string> colms);
        /// <summary>
        /// 添加字段，使用前请先调用GetColumns方法
        /// 字段格式例如：ID integer PRIMARY KEY autoincrement,Name varchar(200) default NULL,Age Int32 default NULL
        /// </summary>
        /// <param name="table"></param>
        /// <param name="Colms"></param>
        /// <exception cref="Exception"></exception>
        void CreateColunm(string table, List<string> colms);
        /// <summary>
        /// 添加字段
        /// 字段格式例如：ID integer PRIMARY KEY autoincrement,Name varchar(200) default NULL,Age Int32 default NULL
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="colm">字段</param>
        /// <param name="colType">字段类型</param>
        /// <exception cref="Exception"></exception>
        void CreateColunm(string table, string colm);
        /// <summary>
        /// 删除字段，（SQLite不支持删除字段，请自行备份数据，重建表）
        /// </summary>
        /// <param name="table"></param>
        /// <param name="colm"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        bool DeleteColumn(string table, string colm);
        /// <summary>
        /// 判断字段是否存在
        /// </summary>
        /// <param name="table"></param>
        /// <param name="colm"></param>
        /// <exception cref="Exception"></exception>
        bool ColumnExist(string table, string colm);
        /// <summary>
        /// 保存数据，注意时间格式(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="table"></param>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        bool Add(string table, Dictionary<string, string> keyValues);
        /// <summary>
        /// 保存数据，注意时间格式(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="table">表名</param>
        /// <param name="model">数据类实列</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        bool Add<T>(string table, T model) where T : class;
        /// <summary>
        /// 更新数据，注意时间格式(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="keyValues">数据字典</param>
        /// <param name="where">判断条件(一定要加 and )</param>
        /// <returns></returns>
        bool Update(string table, Dictionary<string, string> keyValues, string where);
        /// <summary>
        /// 删除数据
        /// 注意：请谨慎使用，where条件为空时会删除所有数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="where">判断条件(一定要加 and )</param>
        /// <returns></returns>
        bool Delete(string table, string where);
        /// <summary>
        /// 判断数据是否存在
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="where">判断条件(一定要加 and )</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        int QueryCount(string table, string where = "");
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="table">表名</param>
        /// <param name="where">判断条件(一定要加 and )</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        List<T> Query<T>(string table, string where = "") where T : new();
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="where">判断条件(一定要加 and )</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        List<Dictionary<string, object>> Query(string table, string where = "");
        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="pageNumber">每页多少条</param>
        /// <param name="pageSize">当前页</param>
        /// <param name="where">判断条件(一定要加 and )</param>
        /// <returns></returns>
        List<Dictionary<string, object>> QueryPage(string table, int pageNumber, int pageSize, string where = "");

    }
}
