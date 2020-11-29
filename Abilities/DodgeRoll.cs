using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;

namespace GameDevIdiotsProject1.Abilities
{
	class DodgeRoll : DefaultMovementOverrideAbility
	{
		private const int SPEED_BOOST = 2;
		private const int COOLDOWN = 200;
		private const int DURATION = 200;

		public DodgeRoll()
		{
			cooldown = COOLDOWN;
			duration = DURATION;
		}

		public override void Start(in Entity entity, in World world)
		{
			base.Start(in entity, in world);

			ref Velocity velocity = ref entity.Get<Velocity>();
			velocity.Value *= SPEED_BOOST;
		}

		public override void Update(float state, in Entity entity, in World world)
		{
			base.Update(state, in entity, in world);
		}

		public override void End(in Entity entity, in World world)
		{
			base.End(in entity, in world);

			ref Velocity velocity = ref entity.Get<Velocity>();
			velocity.Value /= SPEED_BOOST;
		}
	}
}
