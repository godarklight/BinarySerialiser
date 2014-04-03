using System;
using System.IO;

namespace MessageStream
{
    public class MessageReader : IDisposable
    {
        private int messageType;
        private int messageLength;
        private MemoryStream messageData;
        //Constructor
        public MessageReader(byte[] messageData, bool includeHeader)
        {
            this.messageData = new MemoryStream(messageData);
            if (includeHeader)
            {
                messageType = Read<int>();
                messageLength = Read<int>();
            }
        }
        //Getter
        public int GetMessageType()
        {
            return messageType;
        }

        public int GetMessageLength()
        {
            return messageLength;
        }

        //Writers
        public T Read<T>()
        {
            //Apparently switches can't type type parameters?
            if (typeof(T) == typeof(short))
            {
                return (T)(object)ReadShort();
            }
            if (typeof(T) == typeof(int))
            {
                return (T)(object)ReadInt();
            }
            if (typeof(T) == typeof(long))
            {
                return (T)(object)ReadLong();
            }
            if (typeof(T) == typeof(float))
            {
                return (T)(object)ReadFloat();
            }
            if (typeof(T) == typeof(double))
            {
                return (T)(object)ReadDouble();
            }
            if (typeof(T) == typeof(bool))
            {
                return (T)(object)ReadBool();
            }
            if (typeof(T) == typeof(byte))
            {
                return (T)(object)ReadByte();
            }
            if (typeof(T) == typeof(float[]))
            {
                return (T)(object)ReadFloatArray();
            }
            if (typeof(T) == typeof(double[]))
            {
                return (T)(object)ReadDoubleArray();
            }
            if (typeof(T) == typeof(byte[]))
            {
                return (T)(object)ReadByteArray();
            }
            throw new IOException("Type not supported in deserialiser");
        }

        private short ReadShort()
        {
            CheckDataLeft(sizeof(short));
            byte[] outputData = new byte[sizeof(short)];
            messageData.Read(outputData, 0, sizeof(short));
            return BitConverter.ToInt16(outputData, 0);
        }

        private int ReadInt()
        {
            CheckDataLeft(sizeof(int));
            byte[] outputData = new byte[sizeof(int)];
            messageData.Read(outputData, 0, sizeof(int));
            return BitConverter.ToInt32(outputData, 0);
        }

        private long ReadLong()
        {
            CheckDataLeft(sizeof(long));
            byte[] outputData = new byte[sizeof(long)];
            messageData.Read(outputData, 0, sizeof(long));
            return BitConverter.ToInt64(outputData, 0);
        }

        private float ReadFloat()
        {
            CheckDataLeft(sizeof(float));
            byte[] outputData = new byte[sizeof(float)];
            messageData.Read(outputData, 0, sizeof(float));
            return BitConverter.ToSingle(outputData, 0);
        }

        private double ReadDouble()
        {
            CheckDataLeft(sizeof(double));
            byte[] outputData = new byte[sizeof(double)];
            messageData.Read(outputData, 0, sizeof(double));
            return BitConverter.ToDouble(outputData, 0);
        }

        private bool ReadBool()
        {
            CheckDataLeft(sizeof(bool));
            byte[] outputData = new byte[sizeof(bool)];
            messageData.Read(outputData, 0, sizeof(bool));
            return BitConverter.ToBoolean(outputData, 0);
        }

        private byte ReadByte()
        {
            CheckDataLeft(sizeof(byte));
            byte[] outputData = new byte[sizeof(byte)];
            messageData.Read(outputData, 0, sizeof(byte));
            return outputData[0];
        }

        private float[] ReadFloatArray()
        {
            int numberOfElements = ReadInt();
            CheckDataLeft(sizeof(float) * numberOfElements);
            float[] outputData = new float[numberOfElements];
            for (int element = 0; element < numberOfElements; element++)
            {
                outputData[element] = ReadFloat();
            }
            return outputData;
        }

        private double[] ReadDoubleArray()
        {
            int numberOfElements = ReadInt();
            CheckDataLeft(sizeof(double) * numberOfElements);
            double[] outputData = new double[numberOfElements];
            for (int element = 0; element < numberOfElements; element++)
            {
                outputData[element] = ReadDouble();
            }
            return outputData;
        }

        private byte[] ReadByteArray()
        {
            int numberOfElements = ReadInt();
            CheckDataLeft(sizeof(byte) * numberOfElements);
            byte[] outputData = new byte[numberOfElements];
            messageData.Read(outputData, 0, numberOfElements);
            return outputData;
        }

        private void CheckDataLeft(int size)
        {
            if ((messageData.Position + size) > messageData.Length)
            {
                throw new IOException("Cannot read past the end of the stream!");
            }
        }

        public void Dispose()
        {
            //Yes, it's empty. It allows you to use "using" statements to look neater.
        }
    }
}

