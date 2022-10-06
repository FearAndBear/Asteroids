namespace Asteroids.Enemy
{
    public class EnemyDestroyedEvent
    {
        public EnemyBase EnemyBase { get; }
        
        public DestructionReason DestructionReason { get; }

        public EnemyDestroyedEvent(EnemyBase enemyBase, DestructionReason destructionReason)
        {
            EnemyBase = enemyBase;
            DestructionReason = destructionReason;
        }
    }
    
    public enum DestructionReason
    {
        None,
        Player,
        DeadZone
    }
}