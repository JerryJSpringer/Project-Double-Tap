using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.DefaultEcs.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

namespace GameDevIdiotsProject1.DefaultEcs.Systems
{
	public sealed class DebugSystem : AComponentSystem<float, Collision>
	{
		private readonly SpriteBatch _batch;
		private readonly OrthographicCamera _camera;

		public DebugSystem(World world, SpriteBatch batch, OrthographicCamera camera)
			: base(world)
		{
			_batch = batch;
			_camera = camera;
		}

		protected override void PreUpdate(float delta)
		{
			_batch.Begin(transformMatrix: _camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
		}

		protected override void Update(float delta, ref Collision component)
		{
			if (component.collisionActor.Bounds is RectangleF rectangle)
				_batch.DrawRectangle(rectangle, Color.Red, 3, 1);
			else if (component.collisionActor.Bounds is CircleF circle)
				_batch.DrawCircle(circle, 10, Color.Red, 3, 1);
		}

		protected override void PostUpdate(float delta)
		{
			_batch.End();
		}
	}
}
