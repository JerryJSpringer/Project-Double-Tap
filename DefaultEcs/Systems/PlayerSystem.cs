using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.Abilities;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameDevIdiotsProject1.DefaultEcs.Systems
{
	public sealed class PlayerSystem : AEntitySystem<float>
	{
		private MouseState _mouseState;
		private KeyboardState _keyState;
		private GamePadState _gamePadState;

		public PlayerSystem(World world)
			:base(world.GetEntities()
				.With<PlayerInput>()
				.With<CombatStats>()
				.With<Position>()
				.With<Velocity>()
				.AsSet())
		{
		}

		protected override void PreUpdate(float state)
		{
			_mouseState = Mouse.GetState();
			_keyState = Keyboard.GetState();
			_gamePadState = GamePad.GetState(PlayerIndex.One);
			Command.Update(_keyState, _mouseState, _gamePadState);
		}

		protected override void Update(float state, in Entity entity)
		{
			// Update aiming
			ref Aim aim = ref entity.Get<Aim>();
			Vector2 position = CameraFactory.GetPosition();
			aim.Value.X = _mouseState.X - position.X;
			aim.Value.Y = _mouseState.Y - position.Y;

			// Update combat and movement
			ref Velocity velocity = ref entity.Get<Velocity>();
			ref CombatStats stats = ref entity.Get<CombatStats>();
			var abilities = stats.abilities;
			ref Ability currentAbility = ref stats.currentAbility;

			// Reset movement
			if ((currentAbility.types.Contains(AbilityType.MOVEMENTOVERRIDE) && currentAbility.state == AbilityState.COOLDOWN) 
				|| !(currentAbility.types.Contains(AbilityType.MOVEMENTOVERRIDE)))
			{
				velocity.Value.X = 0;
				velocity.Value.Y = 0;
			}

			// Update all abilities including cooldowns
			foreach (Ability ability in abilities)
				ability.Update(state, in entity);

			// Check if any abilities should be triggered
			foreach (Ability ability in abilities)
			{
				if (!ability.command.IsPressed() || (ability.state != AbilityState.AVAILABLE && ability.state != AbilityState.ACTIVE))
					continue;

				// If current ability is over, the ability is instant, or ability can override
				if (currentAbility.state != AbilityState.PERFORMING
					|| ability.types.Contains(AbilityType.INSTANT)
					|| (currentAbility.types.Contains(AbilityType.OVERRIDABLE) && ability.types.Contains(AbilityType.INTERRUPT)))
				{
					ability.Start(in entity);
					currentAbility = ability;
				}
			}
		}
	}
}
