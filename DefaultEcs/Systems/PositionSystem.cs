using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using GameDevIdiotsProject1.DefaultEcs.Components;
using Microsoft.Xna.Framework;

namespace GameDevIdiotsProject1.DefaultEcs.Systems
{
	public sealed class PositionSystem : AEntitySystem<float>
	{
		public PositionSystem(World world) :
			base(world.GetEntities()
				.With<Position>()
				.With<RenderInfo>()
				.AsSet())
		{
		}

		protected override void Update(float state, in Entity entity)
		{
			Vector2 position = entity.Get<Position>().Value;
			ref RenderInfo renderInfo = ref entity.Get<RenderInfo>();

			renderInfo.position.X = (int)position.X - (renderInfo.bounds.Width / 2);
			renderInfo.position.Y = (int)position.Y - (renderInfo.bounds.Height / 2);
		}
	}
}
