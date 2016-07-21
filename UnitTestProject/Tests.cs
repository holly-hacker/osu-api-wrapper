using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using osu_api_wrapper;
using osu_api_wrapper.Classes;

namespace UnitTestProject
{
    [TestClass]
    public class Tests
    {
        static Tests()
        {
            string apiKeyLocation = "c:/_osu api key.txt";  //will load for other test classes too
            OsuApi.ApiKey = File.ReadAllText(apiKeyLocation);
        }

        [TestMethod]
        public void Test_ApiBeatmapSet()
        {
            const int id = 93398;

            List<Beatmap> maps = OsuApi.GetBeatmapsFromSetId(id);

            Debug.WriteLine("Amount: " + maps.Count);
            Assert.IsTrue(maps.Count >= 1);

            Debug.WriteLine("");
            foreach (Beatmap map in maps)
            {
                HelperMethods.WriteBeatmapInfo(map);
                Debug.WriteLine("---");
            }
        }

        [TestMethod]
        public void Test_ApiBeatmap()
        {
            const int id = 252002;

            List<Beatmap> maps = OsuApi.GetBeatmapsFromId(id);

            Debug.WriteLine("Amount: " + maps.Count);
            Assert.AreEqual(maps.Count, 1);

            foreach (Beatmap map in maps) HelperMethods.WriteBeatmapInfo(map);
        }

        [TestMethod]
        public void Test_ApiBeatmapHash()
        {
            const string hash = "c8f08438204abfcdd1a748ebfae67421";

            List<Beatmap> maps = OsuApi.GetBeatmapsFromHash(hash);

            Debug.WriteLine("Amount: " + maps.Count);
            Assert.AreEqual(maps.Count, 1);

            foreach (Beatmap map in maps) HelperMethods.WriteBeatmapInfo(map);
        }

        [TestMethod]
        public void Test_ApiBeatmapUser()
        {
            const string name = "peppy";
            const int id = 2;

            List<Beatmap> maps = OsuApi.GetBeatmapsFromCreator(name);
            List<Beatmap> maps2 = OsuApi.GetBeatmapsFromCreator(id);

            Assert.AreEqual(maps.Count, maps2.Count);
            for (int i = 0; i < maps.Count; i++)
                Assert.AreEqual(maps[i], maps2[i]);

            Debug.WriteLine("Amount: " + maps.Count);
            Assert.IsTrue(maps.Count >= 1);

            Debug.WriteLine("");
            foreach (Beatmap map in maps)
            {
                HelperMethods.WriteBeatmapInfo(map);
                Debug.WriteLine("---");
            }

            Debug.WriteLine("---------");
            Debug.WriteLine("Amount: " + maps.Count);
            Assert.IsTrue(maps.Count >= 1);

            Debug.WriteLine("");
            foreach (Beatmap map in maps)
            {
                HelperMethods.WriteBeatmapInfo(map);
                Debug.WriteLine("---");
            }
        }

        [TestMethod]
        public void Test_ApiUser()
        {
            const string name = "peppy";
            const int id = 2;

            List<User> user1 = OsuApi.GetUser(name);
            List<User> user2 = OsuApi.GetUser(id);

            Assert.AreEqual(user1.Count, user2.Count);
            Assert.IsTrue((user1.Count == 1) || (user1.Count == 0));
            if (user1.Count == 1) Assert.AreEqual(user1[0].user_id, user2[0].user_id);

            foreach (User user in user1)
            {
                HelperMethods.WriteEverything(user);
                Debug.WriteLine("---");
            }
        }

        [TestMethod]
        public void Test_ApiScores()
        {
            const int id = 545555;

            List<Score> scores = OsuApi.GetScores(id);

            Assert.AreNotEqual(scores.Count, 0, "No scores found!");

            foreach (Score score in scores)
            {
                HelperMethods.WriteEverything(score);
                Debug.WriteLine("---");
            }
        }

        [TestMethod]
        public void Test_ApiScoresBest()
        {
            const string name = "peppy";
            const int id = 2;

            List<Score> scores1 = OsuApi.GetScoresBest(name);
            List<Score> scores2 = OsuApi.GetScoresBest(id);

            Assert.AreEqual(scores1.Count, scores2.Count);
            for (int i = 0; i < scores1.Count; i++)
                Assert.AreEqual(scores1[i], scores2[i]);

            Debug.WriteLine("Amount: " + scores1.Count);

            Debug.WriteLine("");
            foreach (Score score in scores1)
            {
                HelperMethods.WriteEverything(score);
                Debug.WriteLine("---");
            }
        }

        [TestMethod]
        public void Test_ApiScoresRecent()
        {
            const string name = "peppy";//"Rafis";
            const int id = 2;//2558286;

            List<Score> scores1 = OsuApi.GetScoresRecent(name, false, GameMode.Standard, 20);
            List<Score> scores2 = OsuApi.GetScoresRecent(id, GameMode.Standard, 20);

            Assert.AreEqual(scores1.Count, scores2.Count);
            for (int i = 0; i < scores1.Count; i++)
                Assert.AreEqual(scores1[i], scores2[i]);

            Debug.WriteLine("Amount: " + scores1.Count);

            Debug.WriteLine("");
            foreach (Score score in scores1)
            {
                HelperMethods.WriteEverything(score);
                Debug.WriteLine("---");
            }
        }
    }
}
