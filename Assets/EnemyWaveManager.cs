using System;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWaveManager : MonoBehaviour
    {
        [SerializeField] private Enemy m_EmenyPrefabs; // ссылка на префаб врага
        [SerializeField] private Path[] paths;
        [SerializeField] private EnemyWave currentWave;
        public event Action OnAllWavesDead;
        private int activeEnemyCount = 0;
        private void RecordEnemyDead()
        {
            if (--activeEnemyCount == 0)
            {
                ForceNextWave();
                /*
                if (currentWave)
                {
                    ForceNextWave();
                }
                else
                {
                    OnAllWavesDead?.Invoke(); // проверка что ивент не пустой 
                }*/
            }
        }

        private void Start()
        {
            currentWave.Prepare(SpawnEnemies); // подготовка к выходу волны
        }

        public void ForceNextWave()
        {
            if (currentWave) // проверяем есть ли следующая волна 
            {
                TDPlayer.Instance.ChangeGold((int)currentWave.GetRemainingTime()); // награда за форс волны
                SpawnEnemies();
            }
            else
            {
                if (activeEnemyCount == 0)
                {
                    OnAllWavesDead?.Invoke();
                }
            }
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
                        e.OnEnd += RecordEnemyDead;
                        e.Use(asset);
                        e.GetComponent<TDPatrolController>().SetPath(paths[pathIndex]);
                        activeEnemyCount += 1;
                        
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
