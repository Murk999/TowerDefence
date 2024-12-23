using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class NextWaveGUI : MonoBehaviour
    {
        [SerializeField] private Text bonusAmount;
        private EnemyWaveManager manager;
        private float TimeToNextWave;
        private void Start()
        {
            manager = FindObjectOfType<EnemyWaveManager>();

            EnemyWave.OnWavePrepare += (float time) =>
            {
                TimeToNextWave = time;
            };
        }
        public void CallWave()
        {
            manager.ForceNextWave();
        }
        private void Update()
        {
            var bonus = (int)TimeToNextWave;
            if(bonus < 0 ) bonus = 0;
            bonusAmount.text = bonus.ToString();
            TimeToNextWave -= Time.deltaTime;
        }
    }
}
