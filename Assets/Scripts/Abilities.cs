using UnityEngine;
using System;
using SpaceShooter;
using System.Collections;
using UnityEngine.UI;

namespace TowerDefense
{
    public class Abilities : MonoSingleton<Abilities>
    {
        [Serializable]
        public class FireAbility
        {
            [SerializeField] private int m_Cost = 5;
            [SerializeField] private int m_Damage = 2;
            [SerializeField] private Color m_TargetingColor;
            public void Use() 
            {
                ClickProtection.Instance.Activate((Vector2 v) =>
                {
                    Vector3 position = v;
                    position.z = -Camera.main.transform.position.z;
                    position = Camera.main.ScreenToWorldPoint(position);
                    foreach (var collider in Physics2D.OverlapCircleAll(position, 5))
                    {
                        print(collider.name);
                        if(collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                        {
                            enemy.TakeDamage(m_Damage, TDProjectile.DamageType.Magic);
                        }
                    }
                });
                
            } 
        }
        
        [Serializable]
        public class TimeAbility
        {
            [SerializeField] private int m_Cost = 10;
            [SerializeField] private float m_Cooldown = 15f;
            [SerializeField] private float m_Duration = 5;
            public void Use() 
            {
                Debug.Log("Time used");
                void Slow(Enemy ship)
                {
                    print($"{ship.name} is slowed");
                    ship.GetComponent<SpaceShip>().HalfMaxLinearVelocity();
                }
                print("Everyon slowed");
                foreach (var ship in FindObjectsOfType<SpaceShip>())
                    ship.HalfMaxLinearVelocity();

                EnemyWaveManager.OnEnemySpawn += Slow;

                IEnumerator Restore()
                {
                    Debug.Log("Restore scheduled");
                    yield return new WaitForSeconds(m_Duration);
                    print("All restored");
                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                        ship.RestoreMaxLinearVelocity();
                    EnemyWaveManager.OnEnemySpawn -= Slow;
                }
                Instance.StartCoroutine(Restore());
                IEnumerator TimeAbilityButton()
                {
                    Instance.m_TimeButton.interactable = false;
                    yield return new WaitForSeconds(m_Cooldown);
                    Instance.m_TimeButton.interactable = true;
                }
                Instance.StartCoroutine(TimeAbilityButton());
            }
        }
        [SerializeField] private Button m_TimeButton;
        [SerializeField] private Image m_TargetCircle;

        [SerializeField] private FireAbility m_FireAbility;
        public void UseFireAbility() => m_FireAbility.Use();
        [SerializeField] private TimeAbility m_TimeAbility;
        public void UseTimeAbility() => m_TimeAbility.Use();

        private void InitiateTargeting(Color color, Action<Vector2>mouseAction)
        {
            //m_TargetCircle.color = color;
            
        }
    }
}
