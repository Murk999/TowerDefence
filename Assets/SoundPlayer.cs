using SpaceShooter;
using UnityEngine;
namespace TowerDefense
{
    public class SoundPlayer : MonoSingleton<SoundPlayer>
    {
        public static void Play(Sound sound)
        {
            print($"playing {sound}");
        }
    }
}
