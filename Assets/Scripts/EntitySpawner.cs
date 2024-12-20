﻿using TowerDefense;
using UnityEngine;

namespace SpaceShooter
{
    public class EntitySpawner : Spawner
    {
        /// <summary>
        /// Ссылки на то что спавнить.
        /// </summary>
        [SerializeField] private GameObject[] m_EntityPrefabs; // префабы обьектов которые спавнятся

        protected override GameObject GenerateSpawnedEntity()
        {
            return Instantiate(m_EntityPrefabs[Random.Range(0, m_EntityPrefabs.Length)]);
        }
    }
}