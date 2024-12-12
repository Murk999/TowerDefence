using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSourse { Gold, Life }
        public UpdateSourse sourse = UpdateSourse.Gold;
        private Text m_text;

        private void Awake()
        {
            m_text = GetComponent<Text>();
            switch (sourse)
            {
                case UpdateSourse.Gold:
                    TDPlayer.OnGoldUpdate += UpdateText;
                    break;
                case UpdateSourse.Life: 
                    TDPlayer.OnLifeUpdate += UpdateText;
                    break;
            }
        }

        private void UpdateText(int money)
        {
            m_text.text = money.ToString();
        }
    }
}

