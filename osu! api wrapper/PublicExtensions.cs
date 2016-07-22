using System;
using System.Collections.Generic;

namespace osu_api_wrapper
{
    public static class PublicExtensions
    {
        public static Mods[] GetArray(this Mods mods)
        {
            List<Mods> modsList = new List<Mods>();

            foreach (Mods value in Enum.GetValues(typeof(Mods)))
                if (mods.HasFlag(value))
                    modsList.Add(value);

            return modsList.ToArray();
        }
    }
}
