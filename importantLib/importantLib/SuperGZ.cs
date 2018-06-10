using System;

namespace importantLib
{
    public abstract class SuperGZ
    {
        #region Attributes

        protected string valueString;
        protected uint[] valueUInt;
        protected byte[] value;

        #endregion

        #region Contructors

        public SuperGZ(string valueString, int uintCount = 6)
        {
            if (valueString.IndexOf('x') == 1 && valueString.Substring(0, 1).Equals("0"))
                valueString = valueString.Substring(2);
            // Adding missing valueIndicies //
            for (int i = 0; i < (uintCount * 8) - valueString.Length; i++) // 8 because uint.MaxValue = 0xffffffff
                valueString = '0' + valueString;
            //              END             //
            this.valueString = valueString;
            setValueFromString();
            valueUInt = new uint[uintCount];
            for (int i = 0; i < valueUInt.Length; i++)
                valueUInt[i] = uint.Parse(HexConverter.Hex2Dec(valueString.Substring((valueUInt.Length - i - 1) * uintCount, 8)).ToString()); // 8 because uint.MaxValue = 0xffffffff
        }

        public SuperGZ(uint[] valueUInt)
        {
            this.valueUInt = valueUInt;
            valueString = string.Empty;
            for (int i = 0; i < valueUInt.Length; i++)
                valueString += HexConverter.Dec2Hex(valueUInt[i]);
            setValueFromString();
        }

        #endregion

        #region Properties

        public string ValueString { get { return valueString; } }

        public uint[] ValueUInt { get { return valueUInt; } }

        public byte[] Value { get { return value; } }

        #endregion

        #region Methods

        protected void setValueFromString(string s = null)
        {
            if (s == null)
                s = valueString;
            char[] cc = s.ToCharArray();
            value = new byte[cc.Length * 2];
            for (int i = 0; i < cc.Length; i++)
            {
                value[i * 2] = (byte)(((short)cc[i] & 0xff00) >> 8);
                value[i * 2 + 1] = (byte)((short)cc[i] & 0x00ff);
            }
        }

        public char getCharacterFromIndex(int i)
        {
            char c;
            try
            {
                char[] cc = valueString.ToCharArray();
                c = cc[i];
            }
            catch (Exception)
            {
                throw;
            }
            return c;
        }

        public uint getUIntFromIndex(int i)
        {
            uint u;
            try
            {
                u = valueUInt[i];
            }
            catch (Exception)
            {
                throw;
            }
            return u;
        }

        public byte getByteFromIndex(int i)
        {
            byte b;
            try
            {
                b = value[i];
            }
            catch (Exception)
            {
                throw;
            }
            return b;
        }

        #endregion
    }
}
