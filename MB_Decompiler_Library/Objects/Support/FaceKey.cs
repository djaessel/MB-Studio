namespace MB_Decompiler_Library.Objects.Support
{
    public class FaceKey
    {
        private int width, height;
        private double corX, corY;
        private string text, id;

        public FaceKey(string id, int width, int height, double corX, double corY, string text) //string[] raw_data
        {
            #region OLD
            
            /*id = raw_data[0];
            width = int.Parse(raw_data[1]);
            height = int.Parse(raw_data[2]);
            corX = double.Parse(CodeReader.repl_DotWComma(raw_data[3]));
            corY = double.Parse(CodeReader.repl_DotWComma(raw_data[4]));
            text = raw_data[5];*/

            #endregion

            this.id = id;
            this.width = width;
            this.height = height;
            this.corX = corX;
            this.corY = corY;
            this.text = text.Replace('_', ' ');
        }

        public string ID { get { return id; } }

        public string Text { get { return text; } }

        public int Width { get { return width; } }

        public int Height { get { return height; } }

        public double CorX { get { return corX; } }

        public double CorY { get { return corY; } }

    }
}