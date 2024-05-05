namespace Runtime.Interfaces
{
    public interface IZombie
    {
        void AttackPlayer();
        void ChasePlayer();
        void LevelUpZombie(uint levelMultiplier);
    }
}