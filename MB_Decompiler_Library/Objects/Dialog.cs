using MB_Decompiler_Library.IO;

namespace MB_Decompiler_Library.Objects
{
    public class Dialog : Skriptum
    {
        private static bool noErrorCode = true;

        public Dialog(string[] rawData) : base(rawData[0], ObjectType.Dialog)
        {
            //try
            //{
            int xx = 0;
            string[] tmpArray = rawData[0].Split(':');
            StartDialogState = RemoveCodePrefix(tmpArray[0]);
            EndDialogState = tmpArray[1].Split('.')[0];
            TalkingPartnerCode = int.Parse(rawData[1]);
            StartDialogStateID = int.Parse(rawData[2]);
            VoiceOverSoundFile = rawData[rawData.Length - 1];
            int tmp = int.Parse(rawData[4].Trim());
            if (tmp > 0)
                ConditionBlock = ProcessBlock(rawData, tmp, 5);
            if (!noErrorCode)
                xx++;
            DialogText = rawData[GetEmptyStringArryBox(rawData, 2 + xx) - 1];
            EndDialogStateID = int.Parse(rawData[GetEmptyStringArryBox(rawData, 2 + xx) + 1]);
            tmp = int.Parse(rawData[GetEmptyStringArryBox(rawData, 3 + xx) + 1]);
            if (tmp > 0)
                ConsequenceBlock = ProcessBlock(rawData, tmp, GetEmptyStringArryBox(rawData, 3 + xx) + 2);
            noErrorCode = true;
            /*}
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + Environment.NewLine + startDialogState.ToString() + Environment.NewLine + endDialogState.ToString() + Environment.NewLine + talkingPartnerCode.ToString()
                    + Environment.NewLine + conditionBlock.Length.ToString() + Environment.NewLine + dialogText.ToString() + Environment.NewLine + consequenceBlock.Length.ToString()
                    + Environment.NewLine + voiceOverSoundFile.ToString());
            }*/
        }

        public string DialogText { get; }

        public string VoiceOverSoundFile { get; }

        public int StartDialogStateID { get; }

        public int EndDialogStateID { get; }

        public string StartDialogState { get; }

        public string EndDialogState { get; }

        public string[] ConditionBlock { get; } = new string[0];

        public string[] ConsequenceBlock { get; } = new string[0];

        public int TalkingPartnerCode { get; }

        private static string[] ProcessBlock(string[] raw_data, int tmp, int start_index)
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

        private static int GetEmptyStringArryBox(string[] array, int maxCount = 1)
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