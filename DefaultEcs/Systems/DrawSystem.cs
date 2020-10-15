using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.DefaultEcs.Components;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevIdiotsProject1.DefaultEcs.Systems
{
	public sealed class DrawSystem : AComponentSystem<float, RenderInfo>
	{
		private readonly SpriteBatch _batch;
		private readonly Texture2D _square;

		public DrawSystem(SpriteBatch batch, Texture2D square, World world)
			:base(world)
		{
			_batch = batch;
			_square = square;
		}

		protected override void PreUpdate(float state)
		{
			_batch.Begin();
		}

		protected override void Update(float state, ref RenderInfo component)
		{
			_batch.Draw(_square, component.sprite, component.color);
		}

		protected override void PostUpdate(float state)
		{
			_batch.End();
		}
	}
}
