using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class TDLevelController : LevelController
    {
        public int levelScore => 1; 
        private new void Start()
        {
            base.Start();
            TDPlayer.Instance.OnPlayerDead += () =>     // лямбда выражение мы говорим что у нас будет безымянная функция и будет делать следующие действия
                                                        // лучше записать таким способом потому что этот метод больше нигде не используется только здесь
            {
                StopLevelActivity();
                LevelResultController.Instance.Show(false);
            };

            m_EventLevelCompleted.AddListener(() =>
            {
                StopLevelActivity();
                MapCompletion.SaveEpisodeResult(levelScore);
            });
        }
        /*
        Аналогичная более длинная запись
        private new void Start()
        {
            base.Start();
            TDPlayer.Instance.OnPlayerDead += EndLevel;
        }
        private void EndLevel()
        {
            StopLevelActivity();
            LevelResultController.Instance.Show(false);
        }
        */
        private void StopLevelActivity()
        {
            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.GetComponent<SpaceShip>().enabled = false;
                enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }

            void DisableAll<T>() where T : MonoBehaviour // делаем эту функцию вместо кучи повторящиегося кода ниже
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }
            DisableAll<Spawner>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
            /*
            foreach(var spawner in FindObjectsOfType<Spawner>())
            {
                spawner.enabled = false;
            }
            foreach (var projectile in FindObjectsOfType<Projectile>())
            {
                projectile.enabled = false;
            }
            foreach (var tower in FindObjectsOfType<Tower>())
            {
                tower.enabled = false;
            }
            */
        }
    }
}
