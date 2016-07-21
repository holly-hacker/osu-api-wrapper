using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using osu_api_wrapper;
using osu_api_wrapper.Classes;

namespace UnitTestProject
{
    internal static class HelperMethods
    {
        public static void WriteBeatmapInfo(Beatmap map)
        {
            Debug.WriteLine($"{map.artist} - {map.title} [{map.version}]");
            Debug.WriteLine($"mapped by {map.creator}, {map.bpm}BPM, {Enum.GetName(typeof(GameMode), map.mode)}");
            Debug.WriteLine($"Length: {map.hit_length}/{map.total_length}");
            Debug.WriteLine($"CS:{map.diff_size} AR:{map.diff_approach} OD:{map.diff_overall} HP:{map.diff_drain} Stars:{map.difficultyrating}");
            Debug.WriteLine($"Source: {map.source}, tags: '{map.tags}'");
            Debug.WriteLine($"Approved on {map.approved_date} ({Enum.GetName(typeof(ApprovedState), map.approved)}), last update on {map.last_update}");
            Debug.WriteLine($"Pass vs. play: {map.passcount}/{map.playcount}, favourited {map.favourite_count} times");
            Debug.WriteLine($"Language: {Enum.GetName(typeof(SongLanguage), map.language_id)}, Genre: {Enum.GetName(typeof(SongGenre), map.genre_id)}");
            Debug.WriteLine($"File MD5: {map.file_md5}");
        }

        public static void WriteEverything(object obj)
        {
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields();
            PropertyInfo[] properties = type.GetProperties();

            Dictionary<string, object> values = new Dictionary<string, object>();
            Array.ForEach(fields, (field) => values.Add(field.Name, field.GetValue(obj)));
            Array.ForEach(properties, (property) =>
            {
                if (property.CanRead)
                    values.Add(property.Name, property.GetValue(obj, null));
            });

            int length = values.Max(a => a.Key.Length);
            foreach (KeyValuePair<string, object> value in values) {
                Debug.WriteLine(value.Key.PadRight(length) + ": " + value.Value);
            }
        }
    }
}
