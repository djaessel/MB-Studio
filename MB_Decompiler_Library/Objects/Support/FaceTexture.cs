namespace MB_Studio_Library.Objects.Support
{
    public class FaceTexture
    {
        private string name;
        private ulong primaryHexValue;
        private string[] textures, textureHexValues;

        public FaceTexture(string name, ulong primaryHexValue, string[] textures, string[] textureHexValues)
        {
            this.name = name;
            this.primaryHexValue = primaryHexValue;
            this.textures = textures;
            this.textureHexValues = textureHexValues;
        }

        public string Name { get { return name; } }

        public ulong PrimaryHexValue { get { return primaryHexValue; } }

        public string[] Textures { get { return textures; } }

        public string[] TextureHexValues { get { return textureHexValues; } }

    }
}