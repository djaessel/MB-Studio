using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using importantLib;
using MB_Studio.Manager;
using System;

namespace MB_Studio
{
    public partial class Changelog : SpecialFormBlack
    {
        public string BuildName { get; private set; } = string.Empty;

        private List<string> fixedInfos = new List<string>();
        private List<string> changedInfos = new List<string>();
        private List<string> removedInfos = new List<string>();
        private List<string> addedInfos = new List<string>();

        private List<string> infoText = new List<string>();

        public Changelog()
        {
            InitializeComponent();

            BackColor = ToolForm.BaseColor;
        }

        private void AddHeadLine(RichTextBox rtb, string txt, Color col)
        {
            Font f = new Font(rtb.Font.Name, 20, FontStyle.Bold);
            AddText(rtb, txt, col, f);
        }

        private void AddHeadLine(RichTextBox rtb, string txt)
        {
            AddHeadLine(rtb, txt, rtb.ForeColor);
        }

        private void AddText(RichTextBox rtb, string txt, Color col, Font f = null)
        {
            int pos = rtb.TextLength;
            rtb.AppendText(txt);
            //rtb.Select(pos, txt.Length);
            rtb.SelectionStart = pos;
            rtb.SelectionLength = txt.Length;
            rtb.SelectionColor = col;
            if (f != null)
                rtb.SelectionFont = f;
            rtb.Select();
        }

        private void AddText(RichTextBox rtb, string txt)
        {
            AddText(rtb, txt, rtb.ForeColor);
        }

        public void LoadCurrentChangelog()
        {
            ReadCurrentChangelog();

            PrintHeadLine();
            PrintLine();

            PrintChangedElements();
            PrintLine();

            PrintInfoText();
            PrintLine();

            changelog_rtb.Select(0, 0);
            changelog_rtb.ScrollToCaret();
        }

        private void PrintLine()
        {
            AddText(changelog_rtb, "- - - - - - - - - - - - - - - - - - - - - - - " + Environment.NewLine);
        }

        private void PrintHeadLine()
        {
            string[] prodV = Application.ProductVersion.Split('.');
            string versionString = prodV[0];
            for (int i = 1; i < prodV.Length; i++)
                if (int.Parse(prodV[i]) > 0)
                    versionString += '.' + prodV[i];

            AddHeadLine(changelog_rtb, " Changelog - v" + versionString + " - " + BuildName + Environment.NewLine);
        }

        private void PrintChangedElements()
        {
            Font x = changelog_rtb.Font;
            changelog_rtb.Font = new Font(x.Name, 12, FontStyle.Bold);

            char checkMark = '\u2713';
            foreach (string fixedInfo in fixedInfos)
            {
                AddText(changelog_rtb, checkMark + " fixed", Color.LimeGreen);
                AddText(changelog_rtb, " " + fixedInfo + Environment.NewLine);
            }
            changelog_rtb.AppendText(Environment.NewLine);

            char infoSign = '\u25C8';
            foreach (string changedInfo in changedInfos)
            {
                AddText(changelog_rtb, infoSign + " changed", Color.DeepSkyBlue);
                AddText(changelog_rtb, " " + changedInfo + Environment.NewLine);
            }
            changelog_rtb.AppendText(Environment.NewLine);

            foreach (string removedInfo in removedInfos)
            {
                AddText(changelog_rtb, "- removed", Color.Crimson);
                AddText(changelog_rtb, " " + removedInfo + Environment.NewLine);
            }
            changelog_rtb.AppendText(Environment.NewLine);

            foreach (string addedInfo in addedInfos)
            {
                AddText(changelog_rtb, "+ added", Color.ForestGreen);
                AddText(changelog_rtb, " " + addedInfo + Environment.NewLine);
            }
            changelog_rtb.AppendText(Environment.NewLine);
        }

        private void PrintInfoText()
        {
            AddText(changelog_rtb, Environment.NewLine + "Information:" + Environment.NewLine + Environment.NewLine);
            foreach (string info in infoText)
                AddText(changelog_rtb, " " + info + Environment.NewLine);
            changelog_rtb.AppendText(Environment.NewLine);
        }

        private void ReadCurrentChangelog()
        {
            string changelog = "changelog";

            if (!File.Exists(changelog)) return;

            using (StreamReader reader = new StreamReader(changelog))
            {
                string line = string.Empty;
                string prodVers = ":" + Application.ProductVersion;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine().Trim();
                    if (line.StartsWith(prodVers))
                    {
                        string[] prodInfo = line.Split(':');
                        if (prodInfo.Length >= 3)
                            BuildName = prodInfo[2];

                        while (!reader.EndOfStream)
                        {
                            line = reader.ReadLine().Trim();
                            if (line.Equals("- -"))
                            {
                                do
                                {
                                    line = HandleChangelogList(reader, line, fixedInfos, '#');
                                    line = HandleChangelogList(reader, line, changedInfos, '.');
                                    line = HandleChangelogList(reader, line, removedInfos, '-');
                                    line = HandleChangelogList(reader, line, addedInfos, '+');

                                    line = HandleChangelogList(reader, line, infoText, '{', '}');

                                    if (!reader.EndOfStream)
                                        line = reader.ReadLine().Trim();

                                } while (!line.Equals("- -") && !reader.EndOfStream);
                            }
                        }
                    }
                }
            }
        }

        private string HandleChangelogList(StreamReader reader, string line, List<string> list, char startChar, char endChar = '\0')
        {
            string startSign = startChar.ToString();
            string endSign;
            if (endChar == '\0')
                endSign = startSign;
            else
                endSign = endChar.ToString();

            if (!reader.EndOfStream)
                line = reader.ReadLine().Trim();

            if (line.StartsWith(startSign))
            {
                do
                {
                    line = reader.ReadLine().Trim();
                    if (!line.StartsWith(endSign))
                        list.Add(line);
                } while (!line.StartsWith(endSign) && !reader.EndOfStream);
            }

            return line;
        }

        private void Changelog_Load(object sender, EventArgs e)
        {
            title_lbl.Text += " - " + Properties.Resources.buildName;
        }
    }
}
