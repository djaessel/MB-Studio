using importantLib;
using MB_Studio_Library.IO;

namespace MB_Studio_Library.Objects.Support
{
    public class GameMenuOption
    {
        private string name, text, doorText;
        private string[] conditionBlock = new string[0];
        private string[] consequenceBlock = new string[0];

        public GameMenuOption(string[] code) // mno_choose_options  0  Continue...  1 2060 1 864691128455135463  . 
        {
            InitializeGameMenuOptions(code);
        }

        public string[] ConsequenceBlock { get { return consequenceBlock; } }

        public string[] ConditionBlock { get { return conditionBlock; } }

        public string Name { get { return name; } }

        public string Text { get { return text; } }

        public string DoorText { get { return doorText; } }

        private void InitializeGameMenuOptions(string[] code)
        {
            int tts = 0;
            name = code[0];
            int x = int.Parse(code[1]);
            string[] xss = new string[x + 1], xss2;
            x = 0;
            for (int i = 2; i < code.Length; i++)
                if (!ImportantMethods.IsNumericFKZ2(code[i]))
                    i = code.Length;
                else
                    x++;
            if (x > 0)
            {
                xss2 = new string[x + 1];
                for (int i = 1; i < xss2.Length; i++)
                    xss2[i] = code[i + 1];
                xss[0] = "GAME_MENU_OPTION=" + 0;
                xss2[0] = xss[0];
                xss = CodeReader.DecompileScriptCode(xss, xss2);
                conditionBlock = new string[xss.Length - 1];
                for (int i = 0; i < conditionBlock.Length; i++)
                    conditionBlock[i] = xss[i + 1];
                tts = x + 2;
            }
            else
                tts = 2;
            text = code[tts];
            tts++;
            x = int.Parse(code[tts]);
            xss = new string[x + 1];
            x = 0;
            tts++;
            for (int i = tts; i < code.Length; i++)
                if (!ImportantMethods.IsNumericFKZ2(code[i]))
                    i = code.Length;
                else
                    x++;
            if (x > 0)
            {
                xss2 = new string[x + 1];
                for (int i = 1; i < xss2.Length; i++)
                    xss2[i] = code[i + tts - 1];
                xss[0] = "GAME_MENU_OPTION=" + 1;
                xss2[0] = xss[0];
                xss = CodeReader.DecompileScriptCode(xss, xss2);
                consequenceBlock = new string[xss.Length - 1];
                for (int i = 0; i < consequenceBlock.Length; i++)
                    consequenceBlock[i] = xss[i + 1];
                tts = x + tts;
            }
            else
                tts++;
            doorText = code[code.Length - 1];
        }

    }
}
