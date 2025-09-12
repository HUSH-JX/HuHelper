using HuDataBase;
using HuHelper;
using Test.Model;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //FileConfigs.SetConfig("test.config", "key1", "value1");
            string v = FileConfigs.GetConfig("test.config", "key1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //var class1 = new Class1
            //{
            //    Id = 1,
            //    Name = "test",
            //    Age = 18,
            //    Data = new byte[] { 0x01, 0x02, 0x03, 0x04 },
            //    Price = 99.99,
            //    CreateTime = DateTime.Now
            //};
            //JsonConfigs.SaveConfig(class1);
            var c = JsonConfigs.GetGasConfig<Class1>("Class1");
        }

        #region SQLSERVER按钮事件
        private IDataBase dataBase1 = null!;

        private void btnEnty1_Click(object sender, EventArgs e)
        {
            dataBase1 = new SqlServerHelper(tbserver1.Text, tbuser1.Text, tbpwd1.Text, tbdb1.Text);
        }

        private void btnCdb1_Click(object sender, EventArgs e)
        {
            bool r = dataBase1.CreateDataBase();
        }

        private void btnCta1_Click(object sender, EventArgs e)
        {
            var colms = dataBase1.GetColumns(new DataBaseClass());
            dataBase1.CreateTable("test", colms);
        }

        private void btnAdd1_Click(object sender, EventArgs e)
        {
            var r = dataBase1.Add("test", new Dictionary<string, string>
            {
                { "Name","test" },
                { "Age","18" },
                { "Price","99.99" },
                { "CreateTime","2024-06-20 12:00:00" }
            });
            var r2 = dataBase1.Add<DataBaseClass>("test", new DataBaseClass
            {
                Name = "test2",
                Age = 20,
                Price = 199.99,
                CreateTime = DateTime.Now
            });
        }

        private void btnEdit1_Click(object sender, EventArgs e)
        {
            var r = dataBase1.Update("test", new Dictionary<string, string>
            {
                { "Name","test_edit" },
                { "Age","28" },
                { "Price","199.99" },
                { "CreateTime","2024-06-20 12:00:00" }
            }, "and ID=1");
        }

        private void btnQuery1_Click(object sender, EventArgs e)
        {
            var data = dataBase1.Query("test");
            var data2 = dataBase1.Query<DataBaseClass>("test", "and ID=2");
        }

        private void btnDel1_Click(object sender, EventArgs e)
        {
            var r = dataBase1.Delete("test", "and ID=1");
        }
        #endregion


        #region MYSQL按钮事件
        private IDataBase dataBase2 = null!;
        private void btnEnty2_Click(object sender, EventArgs e)
        {
            dataBase2 = new MySqlHelper(tbserver2.Text, tbuser2.Text, tbpwd2.Text, tbdb2.Text);
        }

        private void btnCdb2_Click(object sender, EventArgs e)
        {
            var r = dataBase2.CreateDataBase();
        }

        private void btnCta2_Click(object sender, EventArgs e)
        {
            var colms = dataBase2.GetColumns(new DataBaseClass());
            dataBase2.CreateTable("test", colms);
        }

        private void btnAdd2_Click(object sender, EventArgs e)
        {
            var r = dataBase2.Add("test", new Dictionary<string, string>
            {
                { "Name","test" },
                { "Age","18" },
                { "Price","99.99" },
                { "CreateTime","2024-06-20 12:00:00" }
            });
            var r2 = dataBase2.Add<DataBaseClass>("test", new DataBaseClass
            {
                Name = "test2",
                Age = 20,
                Price = 199.99,
                CreateTime = DateTime.Now
            });
        }

        private void btnEdit2_Click(object sender, EventArgs e)
        {
            var r = dataBase2.Update("test", new Dictionary<string, string>
            {
                { "Name","test_edit" },
                { "Age","28" },
                { "Price","199.99" },
                { "CreateTime","2024-06-20 12:00:00" }
            }, "and ID=1");
        }

        private void btnQuery2_Click(object sender, EventArgs e)
        {
            var data = dataBase2.Query("test");
            var data2 = dataBase2.Query<DataBaseClass>("test", "and ID=2");
        }

        private void btnDel2_Click(object sender, EventArgs e)
        {
            var r = dataBase2.Delete("test", "and ID=1");
        }
        #endregion


        #region SQLite按钮事件
        private IDataBase dataBase3 = null!;

        private void btnEnty3_Click(object sender, EventArgs e)
        {
            dataBase3 = new SQLiteHelper(tbdb3.Text);
        }

        private void btnCdb3_Click(object sender, EventArgs e)
        {
            var r = dataBase3.CreateDataBase();
        }

        private void btnCta3_Click(object sender, EventArgs e)
        {
            var colms = dataBase3.GetColumns<DataBaseClass>(new DataBaseClass());
            dataBase3.CreateTable("test", colms);
        }

        private void btnAdd3_Click(object sender, EventArgs e)
        {
            var r = dataBase3.Add("test", new Dictionary<string, string>
           {
                { "Name","test" },
                { "Age","18" },
                { "Price","99.99" },
                { "CreateTime","2024-06-20 12:00:00" }
            });
            var r2 = dataBase3.Add<DataBaseClass>("test", new DataBaseClass
            {
                Name = "test2",
                Age = 20,
                Price = 199.99,
                CreateTime = DateTime.Now
            });
        }

        private void btnEdit3_Click(object sender, EventArgs e)
        {
            var r = dataBase3.Update("test", new Dictionary<string, string>
            {
                { "Name","test_edit" },
                { "Age","28" },
                { "Price","199.99" },
                { "CreateTime","2024-06-20 12:00:00" }
            }, "and ID=1");
        }

        private void btnQuery3_Click(object sender, EventArgs e)
        {
            var data = dataBase3.Query("test");
            var data2 = dataBase3.Query<DataBaseClass>("test", "and ID=2");
        }

        private void btnDel3_Click(object sender, EventArgs e)
        {
            var r = dataBase3.Delete("test", "and ID=1");
        }
        #endregion

    }
}
