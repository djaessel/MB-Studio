using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class Music : Skriptum
    {
        private string trackFile, trackFlags, continueTrackFlags;

        public Music(string[] raw_data) : base(raw_data[0], ObjectType.MUSIC)
        {
            trackFile = raw_data[1];
            trackFlags = raw_data[2];
            continueTrackFlags = raw_data[3];
        }

        public string TrackFile { get { return trackFile; } }

        public string TrackFlags { get { return trackFlags; } }

        public string ContinueTrackFlags { get { return continueTrackFlags; } }

    }
}