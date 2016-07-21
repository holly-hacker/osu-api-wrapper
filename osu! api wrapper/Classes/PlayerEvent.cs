using System;
using System.Diagnostics.CodeAnalysis;

namespace osu_api_wrapper.Classes
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public struct PlayerEvent
    {
        public byte epicfactor;
        public int beatmap_id, beatmapset_id;
        public string display_html;
        public DateTime date;
    }
}
