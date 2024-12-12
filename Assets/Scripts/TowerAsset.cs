using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class TowerAsset : ScriptableObject
    {
        public int goldCost = 15; // изначальная цена в монетах 
        public Sprite sprite; // спрайт самой башни
        public Sprite GUISprite; // выбор спрайта интерфейса 
    }
}
