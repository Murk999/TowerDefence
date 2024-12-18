using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Episode episode;
        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(episode);
        }
    }
}