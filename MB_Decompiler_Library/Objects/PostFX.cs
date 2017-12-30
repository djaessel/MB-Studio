﻿using skillhunter;
using System.Collections.Generic;

namespace MB_Decompiler_Library.Objects
{
    public class PostFX : Skriptum
    {
        private ulong flags, tonemapOperatorType;
        List<string[]> shaderParameters123 = new List<string[]>();

        public PostFX(string raw_data) : base(raw_data.Split()[0], ObjectType.POST_FX)
        {
            raw_data = raw_data.Trim();
            while (raw_data.Contains("  "))
                raw_data = raw_data.Replace("  ", " ");

            string[] tmpS = raw_data.Split();
            //string[] tmpS2 = tmpS[0].Split();
            flags = ulong.Parse(tmpS[1]);
            tonemapOperatorType = ulong.Parse(tmpS[2]);
            //for (int i = 1; i < tmpS.Length; i++)
            //    shaderParameters123.Add(tmpS[i].Split());
            AddValueFromIndexToShaderParameters123(tmpS, 3, 6);
            AddValueFromIndexToShaderParameters123(tmpS, 7, 10);
            AddValueFromIndexToShaderParameters123(tmpS, 11);
        }

        private void AddValueFromIndexToShaderParameters123(string[] values, int index1 = 0, int index2 = -1)
        {
            List<string> valuesX = new List<string>();
            if (index2 < 0)
                index2 = values.Length;
            else
                index2++;
            for (int i = index1; i < index2; i++)
                valuesX.Add(values[i]);
            shaderParameters123.Add(valuesX.ToArray());
        }

        public ulong Flags { get { return flags; } }

        public ulong TonemapOperatorType { get { return tonemapOperatorType; } }

        public string[] ShaderParameter1 { get { return shaderParameters123[0]; } }

        public string[] ShaderParameter2 { get { return shaderParameters123[1]; } }

        public string[] ShaderParameter3 { get { return shaderParameters123[2]; } }

        public List<string[]> AllShaderParameters { get { return shaderParameters123; } }

    }
}