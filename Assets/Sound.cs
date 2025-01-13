namespace TowerDefense
{
    public enum Sound
    {
        BGM,        //Бекграунд
        Arrow,      //Стрела
        ArrowHit,   //Попадание стрелы
    }
    public static class SoundExtension
    {
        public static void Play(this Sound sound)
        {
            SoundPlayer.Play(sound);

        }
    }
}