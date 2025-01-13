namespace TowerDefense
{
    public enum Sound
    {
        BGM,        //���������
        Arrow,      //������
        ArrowHit,   //��������� ������
    }
    public static class SoundExtension
    {
        public static void Play(this Sound sound)
        {
            SoundPlayer.Play(sound);

        }
    }
}