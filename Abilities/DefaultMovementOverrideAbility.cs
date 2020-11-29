using DefaultEcs;

namespace GameDevIdiotsProject1.Abilities
{
	class DefaultMovementOverrideAbility : DefaultAbility
	{
		public override void Start(in Entity entity, in World world)
		{
			base.Start(in entity, in world);
			movementOverride = true;
		}

		public override void Update(float state, in Entity entity, in World world)
		{
			base.Update(state, in entity, in world);
		}

		public override void End(in Entity entity, in World world)
		{
			base.End(in entity, in world);
			movementOverride = false;
		}
	}
}
