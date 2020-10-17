using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using GameDevIdiotsProject1.DefaultEcs.Components;
using Microsoft.Xna.Framework;

namespace GameDevIdiotsProject1.DefaultEcs.Systems
{
	public sealed class PositionSystem : AEntitySystem<float>
	{
		public PositionSystem(World world, IParallelRunner runner) :
			base(world.GetEntities()
				.With<Position>()
				.With<RenderInfo>()
				.AsSet(), runner)
		{
		}

		protected override void Update(float state, in Entity entity)
		{
			Vector2 position = entity.Get<Position>().Value;
			ref RenderInfo renderInfo = ref entity.Get<RenderInfo>();

			renderInfo.bounds.X = (int)position.X - (renderInfo.bounds.Width / 2);
			renderInfo.bounds.Y = (int)position.Y - (renderInfo.bounds.Height / 2);
		}
	}
}
