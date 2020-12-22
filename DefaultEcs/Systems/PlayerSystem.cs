using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.Abilities;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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
				.AsSet()) { }

		protected override void PreUpdate(float delta)
		{
			_mouseState = Mouse.GetState();
			_keyState = Keyboard.GetState();
			_gamePadState = GamePad.GetState(PlayerIndex.One);
			Command.Update(_keyState, _mouseState, _gamePadState);
		}

		protected override void Update(float delta, in Entity entity)
		{
			// Update aiming
			ref Aim aim = ref entity.Get<Aim>();
			aim.Value = CursorFactory.Update(new Vector2(_mouseState.X, +_mouseState.Y)) - entity.Get<Position>().Value;

			Movement.ResetMovement(in entity);

			// Update abilities
			ref List<Ability> abilities = ref entity.Get<CombatStats>().abilities;

			// NOTE: Keep these loop separate, update all -> start all
			foreach (Ability ability in abilities)
				ability.Update(delta, in entity);

			foreach (Ability ability in abilities)
				if (ability.command.IsPressed())
					ability.Start(in entity);
		}
	}
}
