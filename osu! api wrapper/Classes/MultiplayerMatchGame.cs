using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_api_wrapper.Classes
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public struct MultiplayerMatchGame
    {
        public byte match_type;     //not documented
        public int game_id, beatmap_id;
        public DateTime start_time, end_time;
        public GameMode play_mode;
        public Mods mods;
        public ScoringType scoring_type;
        public TeamType team_type;
        public MultiplayerMatchGameScore[] scores;
    }
}
