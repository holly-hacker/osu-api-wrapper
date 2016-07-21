using System;
using System.Diagnostics.CodeAnalysis;

namespace osu_api_wrapper.Classes
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public struct Score
    {
        public byte perfect;
        public ushort count300, count100, count500, countmiss, countkatu, countgeki;
        public int score, user_id;
        public float pp;
        public string username, rank;
        public DateTime date;
        public Mods enabled_mods;
    }
}
