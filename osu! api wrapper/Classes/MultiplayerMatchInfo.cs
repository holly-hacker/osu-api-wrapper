using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_api_wrapper.Classes
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public struct MultiplayerMatchInfo
    {
        public int match_id;
        public string name;
        public DateTime start_time;
        public DateTime? end_time;  //will always be null
    }
}
