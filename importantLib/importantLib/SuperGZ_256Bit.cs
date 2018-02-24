using skillhunter;

namespace importantLib
{
    public class SuperGZ_256Bit : SuperGZ
    {
        private ulong[] valueULong;

        public SuperGZ_256Bit(string valueString) : base(valueString, 8)
        {
            setValueULong();
        }

        public SuperGZ_256Bit(uint[] valueUInt) : base(valueUInt)
        {
            setValueULong();
        }

        private void setValueULong()
        {
            valueULong = new ulong[4];
            for (int i = 0; i < valueULong.Length; i++)
                valueULong[i] = ulong.Parse(SkillHunter.Hex2Dec_16CHARS(valueString.Substring(i * 16, 16)).ToString());
        }

        public ulong[] ValueULong { get { return valueULong; } }

    }
}
