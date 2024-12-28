
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense 
{
    public class BuyControl : MonoBehaviour
    {
        [SerializeField] private TowerBuyControl m_TowerBuyPrefab;
        [SerializeField] private TowerAsset[] m_TowerAssets;
        [SerializeField] private UpgradeAsset m_MageTowerUpgrade;
        private List<TowerBuyControl> m_ActiveControl;
        private RectTransform m_RectTransform;

        #region События Unity
        private void Awake()
        {
            m_RectTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
            gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            BuildSite.OnClickEvent -= MoveToBuildSite;
        }
        #endregion
        private void MoveToBuildSite(Transform buildSite)
        {
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.position);
                print(position);
                m_RectTransform.anchoredPosition = position;
                gameObject.SetActive(true);
                m_ActiveControl = new List<TowerBuyControl>();
                for (int i = 0; i < m_TowerAssets.Length; i++)
                {
                    if (i != 1 || Upgrades.GetUpgradeLevel(m_MageTowerUpgrade) > 0) // ???? не работает постройка летят ошибки
                    {
                        var newControl = Instantiate(m_TowerBuyPrefab, transform);
                        m_ActiveControl.Add(newControl);
                        newControl.transform.position += Vector3.left * 80 * i;
                        newControl.SetTowerAsset(m_TowerAssets[i]);
                    }
                }
            }
            else
            {
                foreach (var control in m_ActiveControl) Destroy(control.gameObject);
                gameObject.SetActive(false);
            }
            foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
            {
                tbc.SetBuildSite(buildSite);
            }
        }
    }
}
