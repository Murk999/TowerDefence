using UnityEngine;
using SpaceShooter;
using System;


namespace TowerDefense
{
    public class TDPlayer : Player
    {
        public static new TDPlayer Instance
        {
            get 
            { 
                return Player.Instance as TDPlayer; 
            }
        }
        private static event Action<int> OnGoldUpdate;
        public static void GoldUbdateSubsctibe(Action<int> act)
        {
            OnGoldUpdate += act;
            act(Instance.m_gold);
        }

        private static event Action<int> OnLifeUpdate;
        public static void LifeUbdateSubsctibe(Action<int> act)
        {
            OnLifeUpdate += act;
            act(Instance.NumLives);
        }

        [SerializeField] private int m_gold = 0;

        public void ChangeGold(int change)
        {
            m_gold += change;

            OnGoldUpdate(m_gold);
        }
        public void  ReduceLife(int change)
        {
            TakeDamage(change);
            OnLifeUpdate(NumLives);
        }

        //TODO: верим в то что золота на постройку достаточно
        [SerializeField] private Tower m_towerPrefab;

        public void TryBuild(TowerAsset towerAsset, Transform buildSite) // команда строить 
        {
            ChangeGold(-towerAsset.goldCost);
            var tower = Instantiate(m_towerPrefab, buildSite.position, Quaternion.identity); // создаем башню передаем ей параметр префаб позиция и поворот
            tower.GetComponentInChildren<SpriteRenderer>().sprite = towerAsset.sprite; // ищем компонент спрайт рендерер и меняем ему на заданный спрайт 
            tower.GetComponentInChildren<Turret>().m_TurretProperties = towerAsset.TurretProperties;
            Destroy(buildSite.gameObject);
        }
    }
}
