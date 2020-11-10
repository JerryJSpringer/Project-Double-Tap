using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.DefaultEcs.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GameDevIdiotsProject1.DefaultEcs.Systems
{
	public sealed class DrawSystem : AComponentSystem<float, RenderInfo>
	{
		private readonly SpriteBatch _batch;
		private readonly OrthographicCamera _camera;

		public DrawSystem(SpriteBatch batch, World world, OrthographicCamera camera)
			:base(world)
		{
			_batch = batch;
			_camera = camera;
		}

		protected override void PreUpdate(float state)
		{
			_batch.Begin(transformMatrix: _camera.GetViewMatrix());
		}

		protected override void Update(float state, ref RenderInfo component)
		{
			_batch.Draw(component.sprite, component.position, component.bounds, component.color, 0f, new Vector2(0,0), 1f, (component.flip) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);

		}

		protected override void PostUpdate(float state)
		{
			_batch.End();
		}
	}
}
