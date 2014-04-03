using System;
using System.IO;

namespace MessageStream
{
    public class MessageWriter : IDisposable
    {
        private int messageType;
        private bool includeHeader;
        private MemoryStream messageData;
        //Constructor
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
                BitConverter.GetBytes(messageData.Length).CopyTo(returnData, 4);
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
            if (inputData.GetType() == typeof(short))
            {
                WriteShort((short)(object)inputData);
                return;
            }

            if (inputData.GetType() == typeof(int))
            {
                WriteInt((int)(object)inputData);
                return;
            }
            if (inputData.GetType() == typeof(long))
            {
                WriteLong((long)(object)inputData);
                return;
            }
            if (inputData.GetType() == typeof(float))
            {
                WriteFloat((float)(object)inputData);
                return;
            }
            if (inputData.GetType() == typeof(double))
            {
                WriteDouble((double)(object)inputData);
                return;
            }
            if (inputData.GetType() == typeof(bool))
            {
                WriteBool((bool)(object)inputData);
                return;
            }
            if (inputData.GetType() == typeof(byte))
            {
                WriteByte((byte)(object)inputData);
                return;
            }
            if (inputData.GetType() == typeof(float[]))
            {
                WriteFloatArray((float[])(object)inputData);
                return;
            }
            if (inputData.GetType() == typeof(double[]))
            {
                WriteDoubleArray((double[])(object)inputData);
                return;
            }
            if (inputData.GetType() == typeof(byte[]))
            {
                WriteByteArray((byte[])(object)inputData);
                return;
            }
            throw new IOException("Type not supported in serialiser");
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

        private void WriteByteArray(byte[] inputData)
        {
            WriteInt(inputData.Length);
            messageData.Write(inputData, 0, inputData.Length);
        }

        public void Dispose()
        {
            //Yes, it's empty. It allows you to use "using" statements to look neater.
        }
    }
}

