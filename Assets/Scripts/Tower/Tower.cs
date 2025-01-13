using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_Radius = 2.2f;
        [SerializeField] private float m_Lead = 1f;
        private Turret[] turrets;
        private Rigidbody2D target = null;

        public void Use(TowerAsset asset)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = asset.sprite; // ���� ��������� ������ �������� � ������ ��� �� �������� ������ 
            turrets = GetComponentsInChildren<Turret>();
            foreach (var turret in turrets)
            {
                turret.AssignLoadout(asset.TurretProperties);
            }
           GetComponentInChildren<BuildSite>().SetBuildableTowers(asset.m_UpgradesTo);
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
                //Vector2 targetVector = target.transform.position - transform.position;
                if (Vector3.Distance(target.transform.position, transform.position) <= m_Radius)
                {
                    foreach (var turret in turrets)
                    {
                        turret.transform.up = target.transform.position - turret.transform.position + (Vector3)target.velocity * m_Lead;
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
                    target = enter.transform.root.GetComponent<Rigidbody2D>();
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
