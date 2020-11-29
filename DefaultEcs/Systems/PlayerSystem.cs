using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.Abilities;
using GameDevIdiotsProject1.DefaultEcs.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameDevIdiotsProject1.DefaultEcs.Systems
{
	public sealed class PlayerSystem : AEntitySystem<float>
	{
		private readonly GameWindow _window;
		private readonly World _world;

		private MouseState _mouseState;
		private KeyboardState _keyState;

		public PlayerSystem(GameWindow window, World world)
			:base(world.GetEntities()
				.With<PlayerInput>()
				.With<CombatStats>()
				.With<Position>()
				.With<Velocity>()
				.AsSet())
		{
			_window = window;
			_world = world;
		}

		protected override void PreUpdate(float state)
		{
			_mouseState = Mouse.GetState();
			_keyState = Keyboard.GetState();
		}

		protected override void Update(float state, in Entity entity)
		{
			ref Velocity velocity = ref entity.Get<Velocity>();
			ref CombatStats stats = ref entity.Get<CombatStats>();
			var abilities = stats.abilities;
			ref Ability currentAbility = ref stats.currentAbility;

			if (!currentAbility.movementOverride)
			{
				velocity.Value.X = 0;
				velocity.Value.Y = 0;

				if (_keyState.IsKeyDown(Keys.D))
					velocity.Value.X += 1;
				if (_keyState.IsKeyDown(Keys.A))
					velocity.Value.X -= 1;
				if (_keyState.IsKeyDown(Keys.W))
					velocity.Value.Y -= 1;
				if (_keyState.IsKeyDown(Keys.S))
					velocity.Value.Y += 1;
			}

			if (currentAbility.abilityState != AbilityState.PERFORMING)
			{
				foreach (Keys key in _keyState.GetPressedKeys())
				{
					if (abilities.ContainsKey(key.ToString()))
					{
						currentAbility = abilities[key.ToString()];

						if (currentAbility.abilityState == AbilityState.AVAILABLE)
						{
							currentAbility.Start(in entity, in _world);
							break;
						}
					}
				}
			}

			foreach (Ability ability in abilities.Values)
				ability.Update(state, in entity, in _world);
		}
	}
}
