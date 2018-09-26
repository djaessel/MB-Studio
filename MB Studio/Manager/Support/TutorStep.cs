using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB_Studio.Manager.Support
{
    class TutorStep
    {
        public enum Option : short
        {
            None = 0,
            Click = 1,
            Hover = 2,
            Enter = 4,
            Leave = 8,
            Input = 16,
        }

        public TutorStep(string heading, string infoText)
        {
            Heading = heading;
            InfoText = infoText;
        }

        public TutorStep(string heading, string infoText, Option options)
        {
            Heading = heading;
            InfoText = infoText;
            Options = options;
        }

        public TutorStep(string heading, string infoText, string controlName)
        {
            Heading = heading;
            InfoText = infoText;
            ControlName = controlName;
        }

        public TutorStep(string heading, string infoText, string controlName, Option options)
        {
            Heading = heading;
            InfoText = infoText;
            ControlName = controlName;
            Options = options;
        }

        public string Heading { get; }

        public string InfoText { get; }

        public string ControlName { get; } = string.Empty;

        public Option Options { get; } = 0;
    }
}
