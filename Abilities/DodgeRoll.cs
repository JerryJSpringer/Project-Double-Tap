using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Util;

namespace GameDevIdiotsProject1.Abilities
{
	class DodgeRoll : DefaultAbility
	{
		public static readonly float SPEED_BOOST = 1.8f;
		public static readonly float COOLDOWN = 200;
		public static readonly float DURATION = 400;
		public static readonly string ANIMATION_KEY = "dodge-roll";

		public DodgeRoll(Command command) : base(command)
		{
			cooldown = COOLDOWN;
			duration = DURATION;
			startAnimation = ANIMATION_KEY;
			updateAnimation = ANIMATION_KEY;
			endAnimation = DEFAULT_IDLE_ANIMATION;

			types.AddRange(new AbilityType[]
			{
				AbilityType.MOVEMENTOVERRIDE,
				AbilityType.INTERRUPT
			});
		}

		public override void Start(in Entity entity)
		{
			ref Velocity velocity = ref entity.Get<Velocity>();

			if (velocity.Value.X != 0 || velocity.Value.Y != 0) {
				base.Start(in entity);
				velocity.speed *= SPEED_BOOST;
			}
		}

		public override void End(in Entity entity)
		{
			base.End(in entity);

			ref Velocity velocity = ref entity.Get<Velocity>();
			velocity.speed /= SPEED_BOOST;
		}
	}
}
