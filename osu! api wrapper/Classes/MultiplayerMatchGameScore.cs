using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_api_wrapper.Classes
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public struct MultiplayerMatchGameScore
    {
        public object rank; //unused?
        public byte slot, perfect, pass;
        public ushort maxcombo, count50, count100, count300, countmiss, countgeki, countkatu;
        public int user_id, score;
        public Team team;
    }
}
