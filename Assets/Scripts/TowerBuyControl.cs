using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public partial class TowerBuyControl : MonoBehaviour
    {

        [SerializeField] private TowerAsset m_ta; // настройка башни
        [SerializeField] private Text m_text; // получаем текст
        [SerializeField] private Button m_button;
        [SerializeField] private Transform buildSite;

        public void SetBuildSite(Transform value)
        { 
            buildSite = value; 
        }

        private void Start()
        {
            TDPlayer.GoldUbdateSubsctibe(GoldStatusCheck); // проверяем есть ли у нас нужное количество монет
            
            m_text.text = m_ta.goldCost.ToString();
            m_button.GetComponent<Image>().sprite = m_ta.GUISprite;
        }
        private void GoldStatusCheck(int gold)
        {
            // добавляем проверку на существование m_button
            if (!m_button) return;
            if (gold > m_ta.goldCost != m_button.interactable)
            {
                m_button.interactable = !m_button.interactable;
                m_text.color = m_button.interactable ? Color.white : Color.red;
            }
        }


        // TODO: количество денег -предусловие этой процедуры
        public void Buy() 
        {
            TDPlayer.Instance.TryBuild(m_ta, buildSite);
            BuildSite.HideControls();
        }
    }
}
