using MB_Decompiler_Library.IO;

namespace MB_Decompiler_Library.Objects
{
    public class Dialog : Skriptum
    {
        private int talkingPartnerCode;
        private int startDialogStateID, endDialogStateID;
        private string dialogText, voiceOverSoundFile;
        private string startDialogState, endDialogState;
        private string[] conditionBlock = new string[0];
        private string[] consequenceBlock = new string[0];

        private static bool noErrorCode = true;

        public Dialog(string[] raw_data) : base(raw_data[0], ObjectType.Dialog)
        {
            //try
            //{
            int xx = 0;
            string[] tmp_array = raw_data[0].Split(':');
            startDialogState = tmp_array[0];
            endDialogState = tmp_array[1].Split('.')[0];
            talkingPartnerCode = int.Parse(raw_data[1]);
            startDialogStateID = int.Parse(raw_data[2]);
            voiceOverSoundFile = raw_data[raw_data.Length - 1];
            int tmp = int.Parse(raw_data[4].Trim());
            if (tmp > 0)
                conditionBlock = processBlock(raw_data, tmp, 5);
            if (!noErrorCode)
                xx++;
            dialogText = raw_data[getEmptyStringArryBox(raw_data, 2 + xx) - 1];
            endDialogStateID = int.Parse(raw_data[getEmptyStringArryBox(raw_data, 2 + xx) + 1]);
            tmp = int.Parse(raw_data[getEmptyStringArryBox(raw_data, 3 + xx) + 1]);
            if (tmp > 0)
                consequenceBlock = processBlock(raw_data, tmp, getEmptyStringArryBox(raw_data, 3 + xx) + 2);
            noErrorCode = true;
            /*}
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + Environment.NewLine + startDialogState.ToString() + Environment.NewLine + endDialogState.ToString() + Environment.NewLine + talkingPartnerCode.ToString()
                    + Environment.NewLine + conditionBlock.Length.ToString() + Environment.NewLine + dialogText.ToString() + Environment.NewLine + consequenceBlock.Length.ToString()
                    + Environment.NewLine + voiceOverSoundFile.ToString());
            }*/
        }

        public string DialogText { get { return dialogText; } }

        public string VoiceOverSoundFile { get { return voiceOverSoundFile; } }

        public int StartDialogStateID { get { return startDialogStateID; } }//Name?

        public int EndDialogStateID { get { return endDialogStateID; } }

        public string StartDialogState { get { return startDialogState; } }

        public string EndDialogState { get { return endDialogState; } }

        public string[] ConditionBlock { get { return conditionBlock; } }

        public string[] ConsequenceBlock { get { return consequenceBlock; } }

        public int TalkingPartnerCode { get { return talkingPartnerCode; } }

        private static string[] processBlock(string[] raw_data, int tmp, int start_index)
        {
            int xx = 0;
            int x = -1;
            string[] tmpSX;
            string[] resArray = new string[tmp + 1];
            for (int i = start_index; i < raw_data.Length; i++)
                if (!raw_data[i].Equals(string.Empty))
                    x++;
                else
                    i = raw_data.Length;
            if (raw_data.Length > (start_index + x + 2))
                if (raw_data[start_index + x + 2].Equals("NO_TEXT"))
                    noErrorCode = !noErrorCode;
            resArray[0] = "TEXT";
            if (!noErrorCode)
                xx++;
            tmpSX = new string[x + 1 + xx];
            for (int i = start_index; i < (tmpSX.Length + start_index - 1); i++)
                tmpSX[i - start_index + 1] = raw_data[i];
            tmpSX[0] = resArray[0];
            return CodeReader.GetStringArrayStartFromIndex(CodeReader.DecompileScriptCode(resArray, tmpSX), 1);
        }

        private static int getEmptyStringArryBox(string[] array, int maxCount = 1)
        {
            int x = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(string.Empty))
                    x++;
                if (x == maxCount)
                {
                    x = i;
                    i = array.Length;
                }
            }
            return x;
        }

    }
}