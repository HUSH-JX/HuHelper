namespace HuHelper
{
    public class CrcHelper
    {
        public static byte[] GBcrc16(byte[] data, int len)
        {
            byte[] array = new byte[2];
            int num = 65535;
            int num2 = 40961;
            for (int i = 0; i < len; i++)
            {
                num = ((num >> 8) ^ data[i]);
                for (int j = 0; j < 8; j++)
                {
                    int num3 = num & 1;
                    num >>= 1;
                    if (num3 == 1)
                    {
                        num ^= num2;
                    }
                }
            }

            array[0] = (byte)(num >> 8);
            array[1] = (byte)(num & 0xFF);
            return array;
        }
        public static byte[] Crc16(byte[] data, int len)
        {
            byte[] array = new byte[2];
            int num = 65535;
            int num2 = 40961;
            for (int i = 0; i < len; i++)
            {
                num ^= data[i];
                for (int j = 0; j < 8; j++)
                {
                    int num3 = num & 1;
                    num >>= 1;
                    if (num3 == 1)
                    {
                        num ^= num2;
                    }
                }
            }

            array[0] = (byte)(num & 0xFF);
            array[1] = (byte)(num >> 8);
            return array;
        }
        public static byte[] HBcrc16(byte[] data, int len)
        {
            byte[] array = new byte[2];
            int num = 65535;
            int num2 = 64193;
            for (int i = 0; i < len; i++)
            {
                num = ((num >> 8) ^ data[i]);
                for (int j = 0; j < 8; j++)
                {
                    int num3 = num & 1;
                    num >>= 1;
                    if (num3 == 1)
                    {
                        num ^= num2;
                    }
                }
            }

            array[0] = (byte)(num >> 8);
            array[1] = (byte)(num & 0xFF);
            return array;
        }
        public static byte[] Crc16(byte[] data)
        {
            byte[] array = new byte[data.Length + 2];
            int num = 65535;
            int num2 = 40961;
            for (int i = 0; i < data.Length; i++)
            {
                num ^= data[i];
                for (int j = 0; j < 8; j++)
                {
                    int num3 = num & 1;
                    num >>= 1;
                    if (num3 == 1)
                    {
                        num ^= num2;
                    }
                }
            }

            Array.Copy(data, 0, array, 0, data.Length);
            array[array.Length - 2] = (byte)(num & 0xFF);
            array[array.Length - 1] = (byte)(num >> 8);
            return array;
        }
        public static byte[] CRC16_C(byte[] data)
        {
            byte b = byte.MaxValue;
            byte b2 = byte.MaxValue;
            byte b3 = 1;
            byte b4 = 160;
            for (int i = 0; i < data.Length; i++)
            {
                b = (byte)(b ^ data[i]);
                for (int j = 0; j <= 7; j++)
                {
                    byte num = b2;
                    byte b5 = b;
                    b2 = (byte)(b2 >> 1);
                    b = (byte)(b >> 1);
                    if ((num & 1) == 1)
                    {
                        b = (byte)(b | 0x80);
                    }

                    if ((b5 & 1) == 1)
                    {
                        b2 = (byte)(b2 ^ b4);
                        b = (byte)(b ^ b3);
                    }
                }
            }

            return new byte[2]
            {
                b2,
                b
            };
        }
        public static ushort UpdateCRC(byte[] input, int Length)
        {
            ushort num = ushort.MaxValue;
            for (short num2 = 0; num2 < Length; num2 = (short)(num2 + 1))
            {
                num = (ushort)(num ^ (input[num2] << 8));
                for (short num3 = 0; num3 < 8; num3 = (short)(num3 + 1))
                {
                    if ((num & 0x8000) == 32768)
                    {
                        num = (ushort)(num << 1);
                        num = (ushort)(num ^ 0x1021);
                    }
                    else
                    {
                        num = (ushort)(num << 1);
                    }
                }
            }

            return num;
        }

    }
}
