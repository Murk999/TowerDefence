using System.Collections;
using System.Collections.Generic;
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
        public static event Action<int> OnGoldUpdate;
        public static event Action<int> OnLifeUpdate;

        [SerializeField] private int m_gold = 0;

        private void Start()
        {
            OnGoldUpdate(m_gold);
            OnLifeUpdate(NumLives);
        }

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
    }
}
