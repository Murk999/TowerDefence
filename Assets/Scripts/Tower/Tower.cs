using SpaceShooter;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEditor.VersionControl;
using UnityEngine;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_Radius = 2.2f;
        private Turret[] turrets;
        private Destructible target = null;

        private void Start()
        {
            turrets = GetComponentsInChildren<Turret>();
        }
        
        [SerializeField] private UpgradeAsset turretUpgrade;
        private void Awake()
        {
            var level = Upgrades.GetUpgradeLevel(turretUpgrade);
            m_Radius += level * 2f;
            print(m_Radius);
        }
        
        private void Update()
        {
            if (target)
            {
                Vector2 targetVector = target.transform.position - transform.position;
                if (targetVector.magnitude <= m_Radius)
                {
                    foreach (var turret in turrets)
                    {
                        turret.transform.up = targetVector;
                        turret.Fire();
                    }
                }
                else
                {
                    target = null;
                }
            }
            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);
                if (enter)
                {
                    target = enter.transform.root.GetComponent<Destructible>();
                }
            }
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;

            Gizmos.DrawWireSphere(transform.position, m_Radius);
        }
    }
}
