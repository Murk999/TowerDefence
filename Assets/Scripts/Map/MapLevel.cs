using UnityEngine;
using SpaceShooter;
using UnityEngine.UI;
namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        private Episode m_episode;
        [SerializeField] private RectTransform resultPanel;
        [SerializeField] private Image[] resultImages;

        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(m_episode);
        }
        public void SetLevelData(Episode episode, int score)
        {
            m_episode = episode;
            resultPanel.gameObject.SetActive(score > 0);
            for(int i= 0; i <score; i++)
            {
                resultImages[i].color = Color.white;
            }
        }
    }
}