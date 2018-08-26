namespace MB_Studio_Library.Objects.Support
{
    public class FaceKey
    {
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

            ID = id;
            Width = width;
            Height = height;
            CorX = corX;
            CorY = corY;
            Text = text.Replace('_', ' ');
        }

        public string ID { get; }

        public string Text { get; }

        public int Width { get; }

        public int Height { get; }

        public double CorX { get; }

        public double CorY { get; }

    }
}