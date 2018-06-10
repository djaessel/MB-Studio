namespace importantLib
{
    public class SuperGZ_256Bit : SuperGZ
    {
        public SuperGZ_256Bit(string valueString) : base(valueString, 8)
        {
            SetValueULong();
        }

        public SuperGZ_256Bit(uint[] valueUInt) : base(valueUInt)
        {
            SetValueULong();
        }

        private void SetValueULong()
        {
            ValueULong = new ulong[4];
            for (int i = 0; i < ValueULong.Length; i++)
                ValueULong[i] = ulong.Parse(HexConverter.Hex2Dec_16CHARS(valueString.Substring(i * 16, 16)).ToString());
        }

        public ulong[] ValueULong { get; private set; }

    }
}
