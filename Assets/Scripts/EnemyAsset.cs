using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    [CreateAssetMenu]
    public sealed class EnemyAsset: ScriptableObject
    {
        public Color color = Color.white;
        public Vector2 spriteScale = new Vector2(3, 3);
        public RuntimeAnimatorController animations;

        public float moveSpeed = 1;
        public int hp = 1;
        public float radius = 0.19f;
    }
}