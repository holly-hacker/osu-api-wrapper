using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using osu_api_wrapper.Classes;

namespace osu_api_wrapper
{
    public static class OsuApi
    {
        /// <summary>
        /// Your 40-character api key
        /// </summary>
        public static string ApiKey;

        private const string ApiUrl = "https://osu.ppy.sh/api/";

        public static List<Beatmap> GetBeatmapsFromCreator(string user, bool? isId = null, GameMode? mode = null, bool? includeConverts = null, int limit = -1) 
            => GetBeatmapsInternal(null, -1, -1, user, isId, mode, includeConverts, null, limit);
        public static List<Beatmap> GetBeatmapsFromCreator(int user, GameMode? mode = null, bool? includeConverts = null, int limit = -1) 
            => GetBeatmapsInternal(null, -1, -1, user.ToString(), true, mode, includeConverts, null, limit);
        public static List<Beatmap> GetBeatmapsFromHash(string beatmapHash, GameMode? mode = null, bool? includeConverts = null, int limit = -1) 
            => GetBeatmapsInternal(null, -1, -1, null, null, mode, includeConverts, beatmapHash, limit);
        public static List<Beatmap> GetBeatmapsFromSetId(int beatmapsetId, GameMode? mode = null, bool? includeConverts = null, int limit = -1) 
            => GetBeatmapsInternal(null, beatmapsetId, -1, null, null, mode, includeConverts, null, limit);
        public static List<Beatmap> GetBeatmapsFromId(int beatmapId, GameMode? mode = null, bool? includeConverts = null, int limit = -1) 
            => GetBeatmapsInternal(null, -1, beatmapId, null, null, mode, includeConverts, null, limit);

        private static List<Beatmap> GetBeatmapsInternal(DateTime? since, int beatmapsetId, int beatmapId, string user, bool? userTypeIsId, GameMode? mode, bool? includeConverts, string beatmapHash, int limit)
        {
            if (string.IsNullOrEmpty(ApiKey)) throw new ApiKeyMissingException();

            NameValueCollection nvc = new NameValueCollection {{"k", ApiKey}};

            if (since != null)                      nvc.Add("since", ((DateTime)since).ToMySqlDate());
            if (beatmapsetId != -1)                 nvc.Add("s", beatmapsetId.ToString());
            if (beatmapId != -1)                    nvc.Add("b", beatmapId.ToString());
            if (!string.IsNullOrEmpty(user))        nvc.Add("u", user);
            if (userTypeIsId != null)               nvc.Add("type", (bool)userTypeIsId ? "id" : "string");
            if (mode != null)                       nvc.Add("m", ((int)mode).ToString());
            if (includeConverts != null)            nvc.Add("a", (bool)includeConverts ? "1" : "0");
            if (!string.IsNullOrEmpty(beatmapHash)) nvc.Add("h", beatmapHash);
            if (limit != -1)                        nvc.Add("limit", limit.ToString());

            string str = UploadValues("get_beatmaps", nvc);
            List<Beatmap> beatmaps = JsonConvert.DeserializeObject<List<Beatmap>>(str);
            return beatmaps;
        }


        public static List<User> GetUser(string user, bool? isId = null, GameMode? mode = null, int eventDays = -1)
            => GetUserInternal(user, mode, isId, eventDays);
        public static List<User> GetUser(int userId, GameMode? mode = null, int eventDays = -1)
            => GetUserInternal(userId.ToString(), mode, true, eventDays);

        private static List<User> GetUserInternal(string user, GameMode? mode, bool? userTypeIsId, int eventDays)
        {
            if (string.IsNullOrEmpty(ApiKey)) throw new ApiKeyMissingException();

            NameValueCollection nvc = new NameValueCollection { { "k", ApiKey } };

            nvc.Add("u", user);
            if (mode != null)           nvc.Add("m", ((int)mode).ToString());
            if (userTypeIsId != null)   nvc.Add("type", (bool)userTypeIsId ? "id" : "string");
            if (eventDays != -1)        nvc.Add("event_days", eventDays.ToString());

            string str = UploadValues("get_user", nvc);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(str);
            return users;
        }


        public static List<Score> GetScores(int beatmapId, GameMode? mode = null, string user = null, bool? isId = null, Mods? forMods = null, int limit = -1)
            => GetScoresInternal(beatmapId, user, mode, forMods, isId, limit);

        //ambigious when using only 1 parameter
        //public static List<Score> GetScores(int beatmapId, GameMode? mode = null, int? userid = null, Mods? forMods = null, int limit = -1)
        //    => GetScoresInternal(beatmapId, userid?.ToString(), mode, forMods, true, limit);

        private static List<Score> GetScoresInternal(int beatmapId, string user, GameMode? mode, Mods? mods, bool? userTypeIsId, int limit = -1)
        {
            if (string.IsNullOrEmpty(ApiKey)) throw new ApiKeyMissingException();

            NameValueCollection nvc = new NameValueCollection { { "k", ApiKey } };
            
            nvc.Add("b", beatmapId.ToString());
            if (!string.IsNullOrEmpty(user))    nvc.Add("u", user);
            if (mode != null)                   nvc.Add("m", ((int)mode).ToString());
            if (mods != null)                   nvc.Add("mods", ((int)mods).ToString());
            if (userTypeIsId != null)           nvc.Add("type", (bool)userTypeIsId ? "id" : "string");
            if (limit != -1)                    nvc.Add("limit", limit.ToString());

            foreach (string s in nvc) {
                Debug.WriteLine(s + " : " + (s=="k" ? "-snip-" : nvc[s]));
            }

            string str = UploadValues("get_scores", nvc);
            Debug.WriteLine("response: " + str);
            List<Score> users = JsonConvert.DeserializeObject<List<Score>>(str);
            return users;
        }


        public static List<Score> GetScoresBest(string user, bool? isId = null, GameMode? mode = null, int limit = -1)
            => GetScoresBestInternal(user, mode, isId, limit);
        public static List<Score> GetScoresBest(int userId, GameMode? mode = null, int limit = -1)
            => GetScoresBestInternal(userId.ToString(), mode, true, limit);

        private static List<Score> GetScoresBestInternal(string user, GameMode? mode, bool? userTypeIsId, int limit = -1)
        {
            if (string.IsNullOrEmpty(ApiKey)) throw new ApiKeyMissingException();

            NameValueCollection nvc = new NameValueCollection { { "k", ApiKey } };

            nvc.Add("u", user);
            if (mode != null) nvc.Add("m", ((int)mode).ToString());
            if (userTypeIsId != null) nvc.Add("type", (bool)userTypeIsId ? "id" : "string");
            if (limit != -1) nvc.Add("limit", limit.ToString());

            foreach (string s in nvc)
                Debug.WriteLine(s + " : " + (s == "k" ? "-snip-" : nvc[s]));

            string str = UploadValues("get_user_best", nvc);
            Debug.WriteLine("response: " + str);
            List<Score> users = JsonConvert.DeserializeObject<List<Score>>(str);
            return users;
        }


        public static List<Score> GetScoresRecent(string user, bool? isId = null, GameMode? mode = null, int limit = -1)
            => GetScoresRecentInternal(user, mode, isId, limit);
        public static List<Score> GetScoresRecent(int userId, GameMode? mode = null, int limit = -1)
            => GetScoresRecentInternal(userId.ToString(), mode, true, limit);

        private static List<Score> GetScoresRecentInternal(string user, GameMode? mode, bool? userTypeIsId, int limit = -1)
        {
            if (string.IsNullOrEmpty(ApiKey)) throw new ApiKeyMissingException();

            NameValueCollection nvc = new NameValueCollection { { "k", ApiKey } };

            nvc.Add("u", user);
            if (mode != null) nvc.Add("m", ((int)mode).ToString());
            if (userTypeIsId != null) nvc.Add("type", (bool)userTypeIsId ? "id" : "string");
            if (limit != -1) nvc.Add("limit", limit.ToString());

            foreach (string s in nvc)
                Debug.WriteLine(s + " : " + (s == "k" ? "-snip-" : nvc[s]));

            string str = UploadValues("get_user_recent", nvc);
            Debug.WriteLine("response: " + str);
            List<Score> users = JsonConvert.DeserializeObject<List<Score>>(str);
            return users;
        }


        public static List<MultiplayerMatch> GetMultiplayerMatch(int matchId)   //TODO: check if this works
        {
            if (string.IsNullOrEmpty(ApiKey)) throw new ApiKeyMissingException();

            NameValueCollection nvc = new NameValueCollection { { "k", ApiKey } };
            nvc.Add("mp", matchId.ToString());

            string str = UploadValues("get_user_recent", nvc);
            List<MultiplayerMatch> users = JsonConvert.DeserializeObject<List<MultiplayerMatch>>(str);
            return users;
        }

        public static byte[] GetReplay(string user, int beatmapId, GameMode mode)
        {
            if (string.IsNullOrEmpty(ApiKey)) throw new ApiKeyMissingException();

            NameValueCollection nvc = new NameValueCollection { { "k", ApiKey } };
            nvc.Add("m", ((int)mode).ToString());
            nvc.Add("b", beatmapId.ToString());
            nvc.Add("u", user);

            string str = UploadValues("get_replay", nvc);
            Debug.WriteLine("Response length: " + str.Length);
            Dictionary<string, string> users = JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
            Debug.WriteLine("item2 length: " + users["content"].Length);
            return Convert.FromBase64String(users["content"]);
        }

        private static string UploadValues(string apiName, NameValueCollection nvc)
        {
            return new WebClient().DownloadString(ApiUrl + apiName + nvc.ToQueryString());  //GET
            //return new WebClient().UploadValues(ApiUrl + apiName, nvc).ToUtf8String();    //POST
        }
    }
}
