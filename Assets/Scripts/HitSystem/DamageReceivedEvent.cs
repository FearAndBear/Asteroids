namespace Asteroids.HitSystem
{
    public class DamageReceivedEvent
    {
        public HitReceiver HitReceiver { get; }
        public HitDealer HitDealer { get; }
        
        public DamageReceivedEvent(HitReceiver hitReceiver, HitDealer hitDealer)
        {
            HitReceiver = hitReceiver;
            HitDealer = hitDealer;
        }
    }
}