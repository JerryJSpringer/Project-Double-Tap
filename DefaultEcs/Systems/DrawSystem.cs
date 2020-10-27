using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using GameDevIdiotsProject1.DefaultEcs.Components;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevIdiotsProject1.DefaultEcs.Systems
{
	public sealed class DrawSystem : AComponentSystem<float, RenderInfo>
	{
		private readonly SpriteBatch _batch;

		public DrawSystem(SpriteBatch batch, World world)
			:base(world)
		{
			_batch = batch;
		}

		protected override void PreUpdate(float state)
		{
			_batch.Begin();
		}

		protected override void Update(float state, ref RenderInfo component)
		{
			_batch.Draw(component.sprite, component.position, component.bounds, component.color);
		}

		protected override void PostUpdate(float state)
		{
			_batch.End();
		}
	}
}
