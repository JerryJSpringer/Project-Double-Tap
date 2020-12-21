using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Util;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameDevIdiotsProject1.Abilities
{
	class Movement : Ability
	{
		private static Dictionary<MovementDirection, bool> movements;
		public static readonly string ANIMATION_KEY = "walk";
		public static readonly float DELAY = 100;
		private readonly MovementDirection _direction;

		public Movement(Command command, MovementDirection direction) : base(command)
		{
			_direction = direction;
			updateAnimation = ANIMATION_KEY;
			endAnimation = DEFAULT_IDLE_ANIMATION;

			if (movements == null)
				movements = new Dictionary<MovementDirection, bool>();

			movements[_direction] = false;
		}
		public static void ResetMovement(in Entity entity)
		{
			Ability currentAbility = entity.Get<CombatStats>().currentAbility;
			if ((currentAbility.types.Contains(AbilityType.MOVEMENTOVERRIDE) && currentAbility.state != AbilityState.PERFORMING && currentAbility.state != AbilityState.STARTING)
				|| !(currentAbility.types.Contains(AbilityType.MOVEMENTOVERRIDE)))
			{
				ref Vector2 velocity = ref entity.Get<Velocity>().Value;
				velocity.X = 0;
				velocity.Y = 0;
			}
		}


		public override bool Start(in Entity entity) 
		{
			return movements[_direction];
		}

		public override void Update(float delta, in Entity entity)
		{
			ref Animate animation = ref entity.Get<Animate>();

			currentTime += delta;

			Ability currentAbility = entity.Get<CombatStats>().currentAbility;
			if (currentAbility.types.Contains(AbilityType.MOVEMENTOVERRIDE) && currentAbility.state == AbilityState.PERFORMING)
				return;

			ref Vector2 velocity = ref entity.Get<Velocity>().Value;

			int value = 1;
			if (!command.IsPressed())
			{
				if (currentTime > DELAY)
				{
					value = 0;
					movements[_direction] = false;
				}
			} 
			else
			{
				currentTime = 0;
				if (CheckAnimationOverride(animation.currentAnimation))
					movements[_direction] = true;
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

			bool isMoving = false;
			foreach (bool movement in movements.Values)
				if (movement)
					isMoving = true;

			if (isMoving && CheckAnimationOverride(animation.currentAnimation))
				animation.currentAnimation = updateAnimation;
			else if (animation.currentAnimation == ANIMATION_KEY)
				animation.currentAnimation = endAnimation;
		}

		private bool CheckAnimationOverride(string currentAnimation)
		{
			return currentAnimation == DEFAULT_IDLE_ANIMATION || currentAnimation == ANIMATION_KEY;
		}

		public override void End(in Entity entity) { }
	}
}
