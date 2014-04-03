using System;
using System.Linq;
using MessageStream;

namespace MessageStreamTester
{
    class MessageStreamTester
    {
        public static void Main()
        {
            Random r = new Random();
            int messageType = 1;
            short shortTest = 2;
            int intTest = r.Next();
            long longTest = DateTime.UtcNow.Ticks;
            bool boolTest = true;
            bool bool2Test = false;
            byte byteTest = (byte)6;
            float floatTest = (float)Math.PI;
            double doubleTest = Math.PI;
            float[] floatArrayTest = new float[4];
            floatArrayTest[0] = (float)Math.PI;
            floatArrayTest[1] = (float)Math.PI + 1;
            floatArrayTest[2] = (float)Math.PI + 2;
            floatArrayTest[3] = (float)Math.PI + 3;
            double[] doubleArrayTest = new double[3];
            doubleArrayTest[0] = Math.PI;
            doubleArrayTest[1] = Math.PI + 1;
            doubleArrayTest[2] = Math.PI + 2;
            string stringTest = "This is a string";
            string[] stringArrayTest = new string[2];
            stringArrayTest[0] = "This is a";
            stringArrayTest[1] = "string array";
            byte[] byteArrayTest = new byte[1024];
            r.NextBytes(byteArrayTest);
            byte[] messageBytes;

            using (MessageWriter mw = new MessageWriter(messageType, false))
            {
                mw.Write<short>(shortTest);
                mw.Write<int>(intTest);
                mw.Write<long>(longTest);
                mw.Write<bool>(boolTest);
                mw.Write<bool>(bool2Test);
                mw.Write<byte>(byteTest);
                mw.Write<float>(floatTest);
                mw.Write<double>(doubleTest);
                mw.Write<float[]>(floatArrayTest);
                mw.Write<double[]>(doubleArrayTest);
                mw.Write<string>(stringTest);
                mw.Write<string[]>(stringArrayTest);
                mw.Write<byte[]>(byteArrayTest);
                messageBytes = mw.GetMessageBytes();
            }

            using (MessageReader mr = new MessageReader(messageBytes, false))
            {
                short shortReturn = mr.Read<short>();
                int intReturn = mr.Read<int>();
                long longReturn = mr.Read<long>();
                bool boolReturn = mr.Read<bool>();
                bool bool2Return = mr.Read<bool>();
                byte byteReturn = mr.Read<byte>();
                float floatReturn = mr.Read<float>();
                double doubleReturn = mr.Read<double>();
                float[] floatArrayReturn = mr.Read<float[]>();
                double[] doubleArrayReturn = mr.Read<double[]>();
                string stringReturn = mr.Read<string>();
                string[] stringArrayReturn = mr.Read<string[]>();
                byte[] byteArrayReturn = mr.Read<byte[]>();
                //Uncomment this to make it throw an exception.
                //mr.Read<byte>();
                Console.WriteLine("Message Type: " + mr.GetMessageType());
                Console.WriteLine("Message Length: " + mr.GetMessageLength());
                //===Tests===
                //Short test
                if (shortReturn == shortTest)
                {
                    Console.WriteLine("Short is correct: " + shortReturn);
                }
                else
                {
                    Console.WriteLine("WARNING: Short failed: " + shortReturn);
                }
                //Int test
                if (intReturn == intTest)
                {
                    Console.WriteLine("Int is correct: " + intReturn);
                }
                else
                {
                    Console.WriteLine("WARNING: Int failed:" + intReturn);
                }
                //Long test
                if (longReturn == longTest)
                {
                    Console.WriteLine("Long is correct: " + longReturn);
                }
                else
                {
                    Console.WriteLine("WARNING: Long failed: " + longReturn);
                }
                //Bool test
                if (boolReturn == boolTest)
                {
                    Console.WriteLine("Bool is correct: " + boolReturn);
                }
                else
                {
                    Console.WriteLine("WARNING: Bool failed: " + boolReturn);
                }
                //Bool test 2
                if (bool2Return == bool2Test)
                {
                    Console.WriteLine("Bool2 is correct: " + bool2Return);
                }
                else
                {
                    Console.WriteLine("WARNING: Bool2 failed: " + bool2Return);
                }
                //Byte test
                if (byteReturn == byteTest)
                {
                    Console.WriteLine("Byte is correct: " + (int)byteReturn);
                }
                else
                {
                    Console.WriteLine("WARNING: Byte failed: " + (int)byteReturn);
                }
                //Float test
                if (floatReturn == floatTest)
                {
                    Console.WriteLine("Float is correct: " + floatReturn);
                }
                else
                {
                    Console.WriteLine("WARNING: Float failed: " + floatReturn);
                }
                //Double test
                if (doubleReturn == doubleTest)
                {
                    Console.WriteLine("Double is correct: " + doubleReturn);
                }
                else
                {
                    Console.WriteLine("WARNING: Double failed: " + doubleReturn);
                }
                //Float array test
                if (floatArrayReturn.SequenceEqual(floatArrayTest))
                {
                    Console.WriteLine("Float array is correct: [" + floatArrayReturn[0] + "," + floatArrayReturn[1] + "," + floatArrayReturn[2] + "," + floatArrayReturn[3] + "]");
                }
                else
                {
                    Console.WriteLine("WARNING: Float array failed: [" + floatArrayReturn[0] + "," + floatArrayReturn[1] + "," + floatArrayReturn[2] + "," + floatArrayReturn[3] + "]");
                }
                //Double array test
                if (doubleArrayReturn.SequenceEqual(doubleArrayTest))
                {
                    Console.WriteLine("Double array is correct: [" + doubleArrayReturn[0] + "," + doubleArrayReturn[1] + "," + doubleArrayReturn[2] + "]");
                }
                else
                {
                    Console.WriteLine("WARNING: Double array failed: [" + doubleArrayReturn[0] + "," + doubleArrayReturn[1] + "," + doubleArrayReturn[2] + "]");
                }
                //String test
                if (stringReturn == stringTest)
                {
                    Console.WriteLine("String is correct: " + stringReturn);
                }
                else
                {
                    Console.WriteLine("WARNING: String failed: " + stringReturn);
                }
                //String array test
                if (stringArrayReturn.SequenceEqual(stringArrayTest))
                {
                    Console.WriteLine("String array is correct: [" + stringArrayReturn[0] + "," + stringArrayReturn[1] + "]");
                }
                else
                {
                    Console.WriteLine("WARNING: String array failed: [" + stringArrayReturn[0] + "," + stringArrayReturn[1] + "]");
                }
                //Byte array test
                if (byteArrayReturn.SequenceEqual(byteArrayTest))
                {
                    Console.WriteLine("Byte array is correct: " + byteArrayReturn.Length + " bytes");
                }
                else
                {
                    Console.WriteLine("WARNING: Byte array failed: " + byteArrayReturn.Length);
                }
            }
        }
    }
}
