using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Util;

namespace GameDevIdiotsProject1.Abilities
{
	class DodgeRoll : DefaultAbility
	{
		public static readonly float SPEED_BOOST = 0.8f;
		public static readonly float COOLDOWN = 200;
		public static readonly float STARTUP = 150;
		public static readonly float DURATION = 400;
		public static readonly string ANIMATION_KEY = "dodge-roll";

		public DodgeRoll(Command command) : base(command)
		{
			cooldown = COOLDOWN;
			startup = STARTUP;
			duration = DURATION;
			updateAnimation = ANIMATION_KEY;
			endAnimation = DEFAULT_IDLE_ANIMATION;

			types.AddRange(new AbilityType[]
			{
				AbilityType.MOVEMENTOVERRIDE,
				AbilityType.INTERRUPT
			});
		}

		public override bool Start(in Entity entity)
		{
			ref Velocity velocity = ref entity.Get<Velocity>();

			if (velocity.Value.X != 0 || velocity.Value.Y != 0) {
				if (base.Start(in entity))
				{
					ref CombatStats combatStats = ref entity.Get<CombatStats>();
					combatStats.speedBonus += SPEED_BOOST;
				}
			}

			return false;
		}

		public override void End(in Entity entity)
		{
			base.End(in entity);

			ref CombatStats combatStats = ref entity.Get<CombatStats>();
			combatStats.speedBonus -= SPEED_BOOST;
		}
	}
}
