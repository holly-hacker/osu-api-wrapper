using System;
using System.Diagnostics.CodeAnalysis;

namespace osu_api_wrapper.Classes
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public struct Beatmap
    {
        public ushort hit_length, total_length, favourite_count;
        public ushort? max_combo;
        public int beatmap_id, beatmapset_id;
        public uint playcount, passcount;
        public float bpm, difficultyrating, diff_size, diff_overall, diff_approach, diff_drain;
        public string artist, creator, source, title, version, file_md5, tags;
        public DateTime last_update;
        public DateTime? approved_date;
        public ApprovedState approved;
        public GameMode mode;
        public SongGenre genre_id;
        public SongLanguage language_id;
    }
}
