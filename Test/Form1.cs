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
    }
}
