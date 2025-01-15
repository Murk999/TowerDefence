using UnityEngine;
using System;
using SpaceShooter;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

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
            [SerializeField] private UpgradeAsset AbilityUpgrade;
            [SerializeField] private Button m_UseFireButton;

            public void CheckAbility(int mana)
            {
                m_UseFireButton.interactable = mana >= m_Cost;
                if (mana < m_Cost || Upgrades.GetUpgradeLevel(AbilityUpgrade) == 0)
                {
                    m_UseFireButton.interactable = false;
                }
            }
            public void ApplyingUpgrades()
            {
                var level = Upgrades.GetUpgradeLevel(AbilityUpgrade);
                m_Damage += level * 10;
                Debug.Log(level);
                Debug.Log(m_Damage);
            }
            
            public bool CanUse() // ��������, �������� �� ������
            {
                return Upgrades.GetUpgradeLevel(AbilityUpgrade) > 0;   
            }
            public void Use() 
            {
                if (!CanUse())  return;

                CheckAbility(TDPlayer.Instance.Mana);
                
                if (TDPlayer.Instance.Mana >= m_Cost)
                {
                    TDPlayer.Instance.ChangeMana(-m_Cost);
                }
                
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
            //[SerializeField] private UpgradeAsset AbilityUpgrade;
            [SerializeField] private Button m_UseSlowButton;
            
            
            public void CheckAbility(int mana)
            {
                m_UseSlowButton.interactable = mana >= m_Cost;
                if (mana < m_Cost)
                {
                    m_UseSlowButton.interactable = false;
                } 
            }
            /*
            public bool CanUse() // ��������, �������� �� ������
            {
                return Upgrades.GetUpgradeLevel(AbilityUpgrade) > 0; 
            }
            */
            public void Use() 
            {
                //if (!CanUse()) return;
                
                CheckAbility(TDPlayer.Instance.Mana);
                
                if (TDPlayer.Instance.Mana >= m_Cost)
                {
                    TDPlayer.Instance.ChangeMana(-m_Cost);
                }

                void Slow(Enemy ship)
                {
                    ship.GetComponent<SpaceShip>().HalfMaxLinearVelocity();
                }

                foreach (var ship in FindObjectsOfType<SpaceShip>())
                    ship.HalfMaxLinearVelocity();

                EnemyWaveManager.OnEnemySpawn += Slow;

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_Duration);

                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                        ship.RestoreMaxLinearVelocity();
                    EnemyWaveManager.OnEnemySpawn -= Slow;
                }
                Instance.StartCoroutine(Restore());
                IEnumerator TimeAbilityButton()
                {
                    Instance.m_TimeButton.interactable = false;
                    yield return new WaitForSeconds(m_Cooldown);
                    if (TDPlayer.Instance.Mana < m_Cost)
                    {
                        Instance.m_TimeButton.interactable = false;
                    }
                    else
                    {
                        Instance.m_TimeButton.interactable = true;
                    }
                }
                Instance.StartCoroutine(TimeAbilityButton());
            }
        }
        [SerializeField] public Button m_TimeButton;
        [SerializeField] private Image m_TargetCircle;
        
        [SerializeField] private FireAbility m_FireAbility;
        public void UseFireAbility() => m_FireAbility.Use();
        
        [SerializeField] private TimeAbility m_TimeAbility;
        public void UseTimeAbility() => m_TimeAbility.Use();

        public void UseFireCheckAbility() => m_FireAbility.CheckAbility(TDPlayer.Instance.Mana);
        public void UseSlowCheckAbility() => m_TimeAbility.CheckAbility(TDPlayer.Instance.Mana);
        
        private void Start()
        {
            UseFireCheckAbility();
            m_FireAbility.ApplyingUpgrades();
            UseSlowCheckAbility();
        }  
        private void Update()
        {
            m_FireAbility.CheckAbility(TDPlayer.Instance.Mana);
        }
        
        private void InitiateTargeting(Color color, Action<Vector2>mouseAction)
        {
            //m_TargetCircle.color = color;
            
        }
    }
}
