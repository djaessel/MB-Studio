namespace MB_Studio_Library.Objects.Support
{
    public class FaceTexture
    {
        public FaceTexture(string name, uint primaryHexValue, string[] hairMaterials, uint[] textureHexValues)
        {
            Name = name;
            Color = primaryHexValue;
            HairMaterials = hairMaterials;
            HairColors = textureHexValues;
        }

        public string Name { get; }

        public uint Color { get; }

        public string[] HairMaterials { get; }

        public uint[] HairColors { get; }

    }
}