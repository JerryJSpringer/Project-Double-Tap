using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Util;
using Microsoft.Xna.Framework;

namespace GameDevIdiotsProject1.Abilities
{
	class Dash : DoubleTapAbility
	{
		private const float COOLDOWN = 500;
		private const float DURATION = 100;
		private const float DASH_DISTANCE = 30;
		private float _distance;
		private MovementDirection _direction;

		public Dash(Command command, MovementDirection direction) : base (command)
		{
			_direction = direction;
			_distance = DASH_DISTANCE;
			cooldown = COOLDOWN;
			duration = DURATION;
		}

		protected override void Action(in Entity entity)
		{
			base.Action(in entity);

			ref Vector2 position = ref entity.Get<Position>().Value;

			switch (_direction)
			{
				case MovementDirection.LEFT:
					position.X += -_distance;
					break;
				case MovementDirection.RIGHT:
					position.X += _distance;
					break;
				case MovementDirection.UP:
					position.Y += -_distance;
					break;
				case MovementDirection.DOWN:
					position.Y += _distance;
					break;
			}
		}
	}
}
