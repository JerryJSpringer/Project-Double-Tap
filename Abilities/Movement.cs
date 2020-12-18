using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Util;
using Microsoft.Xna.Framework;

namespace GameDevIdiotsProject1.Abilities
{
	class Movement : Ability
	{
		private const float DELAY = 70;
		private readonly MovementDirection _direction;

		public Movement(Command command, MovementDirection direction) : base(command)
		{
			_direction = direction;
		}

		public override void Start(in Entity entity)
		{
		}

		public override void Update(float delta, in Entity entity)
		{
			currentTime += delta;

			CombatStats stats = entity.Get<CombatStats>();
			if (stats.currentAbility.types.Contains(AbilityType.MOVEMENTOVERRIDE))
				return;

			ref Vector2 velocity = ref entity.Get<Velocity>().Value;

			int value = 1;
			if (!command.IsPressed())
			{
				if (currentTime > DELAY)
					value = 0;
			} else
			{
				currentTime = 0;
			}

			switch (_direction)
			{
				case MovementDirection.LEFT:
					velocity.X += -value;
					break;
				case MovementDirection.RIGHT:
					velocity.X += value;
					break;
				case MovementDirection.UP:
					velocity.Y += -value;
					break;
				case MovementDirection.DOWN:
					velocity.Y += value;
					break;
			}
		}

		public override void End(in Entity entity)
		{
		}
	}
}
