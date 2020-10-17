using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.DefaultEcs.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameDevIdiotsProject1.DefaultEcs.Systems
{
	public sealed class PlayerSystem : AEntitySystem<float>
	{
		private readonly GameWindow _window;

		private MouseState _mouseState;
		private KeyboardState _keyState;

		public PlayerSystem(GameWindow window, World world)
			:base(world.GetEntities()
				.With<PlayerInput>()
				.With<Velocity>()
				.AsSet())
		{
			_window = window;
		}

		protected override void PreUpdate(float state)
		{
			_mouseState = Mouse.GetState();
			_keyState = Keyboard.GetState();
		}

		protected override void Update(float state, in Entity entity)
		{
			ref Velocity velocity = ref entity.Get<Velocity>();

			velocity.Value.X = 0;
			velocity.Value.Y = 0;
			float speed = velocity.speed;

			if (_keyState.IsKeyDown(Keys.D))
				velocity.Value.X += speed;
			if (_keyState.IsKeyDown(Keys.A))
				velocity.Value.X -= speed;
			if (_keyState.IsKeyDown(Keys.W))
				velocity.Value.Y -= speed;
			if (_keyState.IsKeyDown(Keys.S))
				velocity.Value.Y += speed;
		}
	}
}
