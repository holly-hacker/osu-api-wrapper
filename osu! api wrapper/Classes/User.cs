using System.Diagnostics.CodeAnalysis;

namespace osu_api_wrapper.Classes
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public struct User
    {
        public int user_id, pp_rank, pp_country_rank;    //user can be rank -1
        public uint count300, count100, count50, playcount, count_rank_ss, count_rank_s, count_rank_a;
        public ulong ranked_score, total_score;
        public float pp_raw, level;
        public string username, country;
        public PlayerEvent[] events;
    }
}
