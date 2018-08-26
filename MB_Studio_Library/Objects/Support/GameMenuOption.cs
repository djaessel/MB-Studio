using importantLib;
using MB_Studio_Library.IO;

namespace MB_Studio_Library.Objects.Support
{
    public class GameMenuOption
    {
        public GameMenuOption(string[] code)
        {
            InitializeGameMenuOptions(code);
        }

        public string[] ConsequenceBlock { get; private set; } = new string[0];

        public string[] ConditionBlock { get; private set; } = new string[0];

        public string Name { get; private set; }

        public string Text { get; private set; }

        public string DoorText { get; private set; }

        private void InitializeGameMenuOptions(string[] code)
        {
            int tts = 0;
            Name = code[0];
            int x = int.Parse(code[1]);
            string[] xss = new string[x + 1], xss2;
            x = 0;
            for (int i = 2; i < code.Length; i++)
            {
                if (!ImportantMethods.IsNumericFKZ2(code[i]))
                    i = code.Length;
                else
                    x++;
            }
            if (x > 0)
            {
                xss2 = new string[x + 1];
                for (int i = 1; i < xss2.Length; i++)
                    xss2[i] = code[i + 1];
                xss[0] = "GAME_MENU_OPTION=" + 0;
                xss2[0] = xss[0];
                xss = CodeReader.DecompileScriptCode(xss, xss2);
                ConditionBlock = new string[xss.Length - 1];
                for (int i = 0; i < ConditionBlock.Length; i++)
                    ConditionBlock[i] = xss[i + 1];
                tts = x + 2;
            }
            else
                tts = 2;
            Text = code[tts];
            tts++;
            x = int.Parse(code[tts]);
            xss = new string[x + 1];
            x = 0;
            tts++;
            for (int i = tts; i < code.Length; i++)
            {
                if (!ImportantMethods.IsNumericFKZ2(code[i]))
                    i = code.Length;
                else
                    x++;
            }
            if (x > 0)
            {
                xss2 = new string[x + 1];
                for (int i = 1; i < xss2.Length; i++)
                    xss2[i] = code[i + tts - 1];
                xss[0] = "GAME_MENU_OPTION=" + 1;
                xss2[0] = xss[0];
                xss = CodeReader.DecompileScriptCode(xss, xss2);
                ConsequenceBlock = new string[xss.Length - 1];
                for (int i = 0; i < ConsequenceBlock.Length; i++)
                    ConsequenceBlock[i] = xss[i + 1];
                tts = x + tts;
            }
            else
                tts++;
            DoorText = code[code.Length - 1];
        }
    }
}
