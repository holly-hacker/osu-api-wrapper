using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_api_wrapper.Classes
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public struct MultiplayerMatch
    {
        public MultiplayerMatchInfo match;
        public MultiplayerMatchGame[] games;
    }
}
