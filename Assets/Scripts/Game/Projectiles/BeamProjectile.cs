namespace Asteroids.Projectiles
{
    public class BeamProjectile : ProjectileBaseWithArgs<BeamProjectileArgs>
    {
        private BeamProjectileArgs _data;
        
        public override void Init(BeamProjectileArgs data)
        {
            _data = data;
        }

        protected override void UpdateMethod()
        {
            base.UpdateMethod();

            if (_data != null)
            {
                var newEuler = transform.eulerAngles;
                newEuler.z = _data.Target.rotation.eulerAngles.z;
                transform.eulerAngles = newEuler;
                
                transform.position = _data.Target.position;
            }
        }
    }
}