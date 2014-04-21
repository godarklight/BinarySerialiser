using System;
using System.IO;
using System.Text;

namespace MessageStream
{
    public class MessageWriter : IDisposable
    {
        private int messageType;
        private bool includeHeader;
        private MemoryStream messageData;
        UnicodeEncoding encoder = new UnicodeEncoding();
        //Constructors
        public MessageWriter()
        {
            this.messageType = 0;
            this.includeHeader = false;
            messageData = new MemoryStream();
        }

        public MessageWriter(int messageType)
        {
            this.messageType = messageType;
            this.includeHeader = true;
            messageData = new MemoryStream();
        }

        [Obsolete("Use the int constructor to specify a header, or no parameters to make it headerless.")]
        public MessageWriter(int messageType, bool includeHeader)
        {
            this.messageType = messageType;
            this.includeHeader = includeHeader;
            messageData = new MemoryStream();
        }
        //Getter
        public byte[] GetMessageBytes()
        {
            if (includeHeader)
            {
                byte[] returnData = new byte[8 + messageData.Length];
                BitConverter.GetBytes(messageType).CopyTo(returnData, 0);
                BitConverter.GetBytes((int)messageData.Length).CopyTo(returnData, 4);
                messageData.ToArray().CopyTo(returnData, 8);
                return returnData;
            }
            else
            {
                return messageData.ToArray();
            }
        }
        //Writers
        public void Write<T>(T inputData)
        {
            //Apparently switches can't type type parameters?
            if (typeof(T) == typeof(short))
            {
                WriteShort((short)(object)inputData);
                return;
            }
            if (typeof(T) == typeof(int))
            {
                WriteInt((int)(object)inputData);
                return;
            }
            if (typeof(T) == typeof(long))
            {
                WriteLong((long)(object)inputData);
                return;
            }
            if (typeof(T) == typeof(float))
            {
                WriteFloat((float)(object)inputData);
                return;
            }
            if (typeof(T) == typeof(double))
            {
                WriteDouble((double)(object)inputData);
                return;
            }
            if (typeof(T) == typeof(bool))
            {
                WriteBool((bool)(object)inputData);
                return;
            }
            if (typeof(T) == typeof(byte))
            {
                WriteByte((byte)(object)inputData);
                return;
            }
            if (typeof(T) == typeof(string))
            {
                WriteString((string)(object)inputData);
                return;
            }
            if (typeof(T) == typeof(short[]))
            {
                WriteShortArray((short[])(object)inputData);
                return;
            }
            if (typeof(T) == typeof(int[]))
            {
                WriteIntArray((int[])(object)inputData);
                return;
            }
            if (typeof(T) == typeof(long[]))
            {
                WriteLongArray((long[])(object)inputData);
                return;
            }
            if (typeof(T) == typeof(float[]))
            {
                WriteFloatArray((float[])(object)inputData);
                return;
            }
            if (typeof(T) == typeof(double[]))
            {
                WriteDoubleArray((double[])(object)inputData);
                return;
            }
            if (typeof(T) == typeof(bool[]))
            {
                WriteBoolArray((bool[])(object)inputData);
                return;
            }
            if (typeof(T) == typeof(byte[]))
            {
                WriteByteArray((byte[])(object)inputData);
                return;
            }
            if (typeof(T) == typeof(string[]))
            {
                WriteStringArray((string[])(object)inputData);
                return;
            }
            throw new IOException("Type not supported");
        }

        private void WriteShort(short inputData)
        {
            messageData.Write(BitConverter.GetBytes(inputData), 0, sizeof(short));
        }

        private void WriteInt(int inputData)
        {
            messageData.Write(BitConverter.GetBytes(inputData), 0, sizeof(int));
        }

        private void WriteLong(long inputData)
        {
            messageData.Write(BitConverter.GetBytes(inputData), 0, sizeof(long));
        }

        private void WriteFloat(float inputData)
        {
            messageData.Write(BitConverter.GetBytes(inputData), 0, sizeof(float));
        }

        private void WriteDouble(double inputData)
        {
            messageData.Write(BitConverter.GetBytes(inputData), 0, sizeof(double));
        }

        private void WriteBool(bool inputData)
        {
            messageData.Write(BitConverter.GetBytes(inputData), 0, sizeof(bool));
        }

        private void WriteByte(byte inputData)
        {
            byte[] inputDataArray = new byte[sizeof(byte)];
            inputDataArray[0] = inputData;
            messageData.Write(inputDataArray, 0, sizeof(byte));
        }

        private void WriteString(string inputData)
        {
            //Protect against empty strings
            if (inputData == null)
            {
                inputData = "";
            }
            byte[] inputDataArray = encoder.GetBytes(inputData);
            WriteByteArray(inputDataArray);
        }

        private void WriteShortArray(short[] inputData)
        {
            WriteInt(inputData.Length);
            foreach (short element in inputData)
            {
                WriteShort(element);
            }
        }

        private void WriteIntArray(int[] inputData)
        {
            WriteInt(inputData.Length);
            foreach (int element in inputData)
            {
                WriteInt(element);
            }
        }

        private void WriteLongArray(long[] inputData)
        {
            WriteInt(inputData.Length);
            foreach (long element in inputData)
            {
                WriteLong(element);
            }
        }

        private void WriteFloatArray(float[] inputData)
        {
            WriteInt(inputData.Length);
            foreach (float element in inputData)
            {
                WriteFloat(element);
            }
        }

        private void WriteDoubleArray(double[] inputData)
        {
            WriteInt(inputData.Length);
            foreach (double element in inputData)
            {
                WriteDouble(element);
            }
        }

        private void WriteBoolArray(bool[] inputData)
        {
            WriteInt(inputData.Length);
            foreach (bool element in inputData)
            {
                WriteBool(element);
            }
        }

        private void WriteByteArray(byte[] inputData)
        {
            WriteInt(inputData.Length);
            messageData.Write(inputData, 0, inputData.Length);
        }

        private void WriteStringArray(string[] inputData)
        {
            WriteInt(inputData.Length);
            foreach (string element in inputData)
            {
                WriteString(element);
            }
        }

        public void Dispose()
        {
            //Yes, it's empty. It allows you to use "using" statements to look neater.
        }
    }
}

