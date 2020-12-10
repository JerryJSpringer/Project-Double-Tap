using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using Microsoft.Xna.Framework;

namespace GameDevIdiotsProject1.Abilities
{
	class DodgeRoll : DefaultMovementOverrideAbility
	{
		private const float SPEED_BOOST = 1.8f;
		private const int COOLDOWN = 200;
		private const int DURATION = 400;
		private const string ANIMATION_KEY = "dodge-roll";

		public DodgeRoll()
		{
			cooldown = COOLDOWN;
			duration = DURATION;
		}

		public override void Start(in Entity entity, in World world)
		{
			base.Start(in entity, in world);

			ref Velocity velocity = ref entity.Get<Velocity>();
			velocity.speed *= SPEED_BOOST;

			if (!velocity.Value.Equals(new Vector2(0, 0))) {
				ref Animate animation = ref entity.Get<Animate>();
				animation.currentAnimation = ANIMATION_KEY;
			}
			else {
				End(in entity, in world);
            }
		}

		public override void Update(float state, in Entity entity, in World world)
		{
			base.Update(state, in entity, in world);
		}

		public override void End(in Entity entity, in World world)
		{
			base.End(in entity, in world);

			ref Velocity velocity = ref entity.Get<Velocity>();
			velocity.speed /= SPEED_BOOST;

			ref Animate animation = ref entity.Get<Animate>();
			animation.currentAnimation = "walk";
		}
	}
}
