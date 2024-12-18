using UnityEngine;
using SpaceShooter;
using System;

namespace TowerDefense
{
    public class MapCompletion : MonoSingleton<MapCompletion>
    {
        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }
        public static void SaveEpisodeResult(int levelScore)
        {
            Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
        }

        [SerializeField] private EpisodeScore[] completionData;
        public bool TryIndex(int id, out Episode episode, out int score)
        {
            if(id >= 0 && id < completionData.Length)
            {
                episode = completionData[id].episode;
                score = completionData[id].score;
                return true;
            }
            episode = null;
            score = 0;
            return false;
        }

        private void SaveResult(Episode CurrentEpisode, int levelScore)
        {
            foreach(var item in completionData)
            {
                if(item.episode == CurrentEpisode)
                {
                    if (levelScore > item.score) item.score = levelScore;
                }
            }
        }
    }
}
