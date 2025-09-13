using System.Buffers;
using Content.Shared.Weapons.Melee.Events;

namespace Content.Shared.Stunnable
{
    public sealed partial class CustomStunSystem : EntitySystem
    {
        public override void Initialize()
        {
            base.Initialize();
            SubscribeLocalEvent<CustomStunComponent, MeleeHitEvent>(OnMeleeHit);
        }

        private int _hitCounter = 0;
        public void RegisterHit(Entity<CustomStunComponent> ent, EntityUid target)
        {
            _hitCounter++;

            if (_hitCounter >= ent.Comp.CountToStun)
            {
                StunTarget(ent, target);

                _hitCounter = 0;
            }
        }

        private void StunTarget(Entity<CustomStunComponent> ent, EntityUid target)
        {
            var stunSystem = EntitySystem.Get<SharedStunSystem>();

            stunSystem.TryAddStunDuration(target, TimeSpan.FromSeconds(ent.Comp.StunTime));
            stunSystem.AddKnockdownTime(target, TimeSpan.FromSeconds(ent.Comp.KnockdownTime));
        }

        private void OnMeleeHit(Entity<CustomStunComponent> ent, ref MeleeHitEvent args)
        {
            foreach (var target in args.HitEntities)
            {
                RegisterHit(ent, target);
            }
        }
    }
}
