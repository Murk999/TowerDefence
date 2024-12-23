using System;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWaveManager : MonoBehaviour
    {
        [SerializeField] private Enemy m_EmenyPrefabs; // ссылка на префаб врага
        [SerializeField] private Path[] paths;
        [SerializeField] private EnemyWave currentWave;

        private void Start()
        {
            currentWave.Prepare(SpawnEnemies); // подготовка к выходу волны
        }

        public void ForceNextWave()
        {
            SpawnEnemies();
            // награда за форс волны
            TDPlayer.Instance.ChangeGold((int)currentWave.GetRemainingTime());
        }

        private void SpawnEnemies()
        {
            // создать врагов 
            foreach ((EnemyAsset asset, int count, int pathIndex) in currentWave.EnumerateSquads())
            {
                if (pathIndex < paths.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var e = Instantiate(m_EmenyPrefabs, paths[pathIndex].StartArea.RandomInsideZone, Quaternion.identity);// Quaternion.identity говорим что поворот не нужен 
                        e.Use(asset);
                        e.GetComponent<TDPatrolController>().SetPath(paths[pathIndex]);
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid pathIndex in {name}");
                }
            }

            currentWave = currentWave.PrepareNext(SpawnEnemies);
        }
    }
}
