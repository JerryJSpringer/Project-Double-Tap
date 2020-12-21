using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Util;
using Microsoft.Xna.Framework;

namespace GameDevIdiotsProject1.Abilities
{
	class Dash : DoubleTapAbility
	{
		public static readonly float COOLDOWN = 300;
		public static readonly float DURATION = 100;
		public static readonly float DASH_WEIGHT = 10;
		public static readonly float SPEED_BONUS = 1.5f;
		private readonly MovementDirection _direction;

		public Dash(Command command, MovementDirection direction) : base (command)
		{
			_direction = direction;
			cooldown = COOLDOWN;
			duration = DURATION;
		}

		protected override void Action(in Entity entity)
		{
			base.Action(in entity);

			ref CombatStats stats = ref entity.Get<CombatStats>();
			stats.speedBonus += SPEED_BONUS;

			ref Vector2 velocity = ref entity.Get<Velocity>().Value;
			switch (_direction)
			{
				case MovementDirection.LEFT:
					velocity.X -= DASH_WEIGHT;
					break;
				case MovementDirection.RIGHT:
					velocity.X += DASH_WEIGHT;
					break;
				case MovementDirection.UP:
					velocity.Y -= DASH_WEIGHT;
					break;
				case MovementDirection.DOWN:
					velocity.Y += DASH_WEIGHT;
					break;
			}
		}

		public override void End(in Entity entity)
		{
			currentTime = 0;
			state = AbilityState.COOLDOWN;

			ref CombatStats stats = ref entity.Get<CombatStats>();
			stats.speedBonus -= SPEED_BONUS;
		}
	}
}
