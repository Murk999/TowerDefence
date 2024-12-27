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
        private event Action<int> OnGoldUpdate;
        public void GoldUbdateSubsctibe(Action<int> act)
        {
            OnGoldUpdate += act;
            act(Instance.m_gold);
        }
        /*
         // добавили с ментором 
        // добавим метод отписки для gold  
        public static void GoldUbdateUnsubsctibe(Action<int> act)
        {
            OnGoldUpdate -= act;
        }
        */
        public event Action<int> OnLifeUpdate;
        public  void LifeUbdateSubsctibe(Action<int> act)
        {
            OnLifeUpdate += act;
            act(Instance.NumLives);
        }
        /*
          // добавили с ментором 
        // добавим метод отписки для life
        public static void LifeUbdateUnSubsctibe(Action<int> act)
        {
            OnLifeUpdate -= act;
        }

        */
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
            tower.Use(towerAsset);
            
            //tower.GetComponentInChildren<Turret>().m_TurretProperties = towerAsset.TurretProperties;
            Destroy(buildSite.gameObject);
        }
        [SerializeField] private UpgradeAsset healthUpgrade;
        private new void Awake()
        {
            base.Awake();
            var level = Upgrades.GetUpgradeLevel(healthUpgrade);
            TakeDamage(-level * 5);
        }
    }
}
