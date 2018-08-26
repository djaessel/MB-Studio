namespace MB_Studio_Library.Objects.Support
{
    public class FaceTexture
    {
        public FaceTexture(string name, uint primaryHexValue, string[] hairMaterials, ulong[] textureHexValues)
        {
            Name = name;
            Color = primaryHexValue;
            HairMaterials = hairMaterials;
            HairColors = textureHexValues;
        }

        public string Name { get; }

        public uint Color { get; }

        public string[] HairMaterials { get; }

        public ulong[] HairColors { get; }

    }
}