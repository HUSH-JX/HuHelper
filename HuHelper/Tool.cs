using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuHelper
{
    public class Tool
    {
        public static byte[] ComibeByteArray(params byte[][] bytes)
        {
            byte[] rs = new byte[0];
            foreach (byte[] item in bytes)
            {
                byte[] temp = rs;
                int len = rs.Length;
                rs = new byte[len + item.Length];
                temp.CopyTo(rs, 0);
                item.CopyTo(rs, len);
            }
            return rs;
        }
        public static float ToSingle(byte[] value, int startIndex, bool isLow = true)
        {
            byte[] data = new byte[4];
            Array.Copy(value, startIndex, data, 0, data.Length);
            if (!isLow) Array.Reverse(data);
            return BitConverter.ToSingle(data, 0);
        }
        public static UInt16 ToUInt16(byte[] data, int index, bool isLow = true)
        {
            byte[] temp = new byte[2];
            Array.Copy(data, index, temp, 0, 2);
            if (!isLow) Array.Reverse(temp);
            return BitConverter.ToUInt16(temp, 0);
        }
        public static UInt32 ToUInt32(byte[] data, int index, bool isLow = true)
        {
            byte[] temp = new byte[4];
            Array.Copy(data, index, temp, 0, 4);
            if (!isLow) Array.Reverse(temp);
            return BitConverter.ToUInt32(temp, 0);
        }
        public static float BytesToSingle(byte[] data, int index, bool isLow = true)
        {
            byte[] temp = new byte[4];
            Array.Copy(data, index, temp, 0, 4);
            if (!isLow) Array.Reverse(temp);
            return BitConverter.ToSingle(temp, 0);
        }
        public static string BytesToString(byte[] data, int count)
        {
            if (data == null || data.Length == 0)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                string s = ByteToString(data[i]);
                sb.Append(s).Append(" ");
            }
            return sb.ToString();
        }
        public static string BytesToString(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                string s = ByteToString(data[i]);
                sb.Append(s).Append(" ");
            }
            return sb.ToString();
        }
        public static string ByteToString(byte value)
        {
            return value.ToString("X2");
        }
        public static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }
        /// <summary>
        /// 获取类的属性名称集合
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="model">实例</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<string> GetKeys<T>(T model) where T : class
        {
            System.Reflection.PropertyInfo[] properties = model.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (properties.Length <= 0)
            {
                throw new Exception("类属性长度为零");
            }
            List<string> Columns = new List<string>();
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                Columns.Add(item.Name);
            }
            return Columns;
        }


        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="fileName">日志名称</param>
        /// <param name="msg">内容</param>
        public static void WriteLog(string fileName, string msg)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\" + fileName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff") + "\t" + msg + "\r\n";
                msg += "----------------------------------------------------------------------\r\n";
                byte[] bs = Encoding.UTF8.GetBytes(msg);
                fs.Write(bs, 0, bs.Length);
                fs.Close();
            }
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="filePath">日志路径</param>
        /// <param name="msg">内容</param>
        public static void WriteFileLog(string filePath, string msg)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath = filePath + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff") + "\t" + msg + "\r\n";
                msg += "----------------------------------------------------------------------\r\n";
                byte[] bs = Encoding.UTF8.GetBytes(msg);
                fs.Write(bs, 0, bs.Length);
                fs.Close();
            }
        }
    }
}
