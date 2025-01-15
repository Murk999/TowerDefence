using UnityEngine;


namespace TowerDefense
{
    public enum Sound
    {
        Arrow = 0,          // Стрела
        ArrowHit = 1,       // Попадание стрелы
        EnemyDie = 2,       // Уничтожение врага 
        EnemyWin = 3,       // Победа врага
        PlayerWin = 4,      // Победа игрока
        PlayerLose =5,      // Проигрыш игрока
        BGM = 6,            // Бекграунд    
    }
    public static class SoundExtension
    {
        public static void Play(this Sound sound)
        {
            SoundPlayer.Instance.Play(sound);
        }
    }
}