using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.DefaultEcs.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameDevIdiotsProject1.DefaultEcs.Systems
{
	public sealed class PositionSystem : AEntitySystem<float>
	{
		public PositionSystem(World world) :
			base(world.GetEntities()
				.With<Position>()
				.With<RenderInfo>()
				.With<Collision>()
				.AsSet())
		{
		}

		protected override void Update(float state, in Entity entity)
		{
			Vector2 position = entity.Get<Position>().Value;
			ref RenderInfo renderInfo = ref entity.Get<RenderInfo>();
			ref Collision collision = ref entity.Get<Collision>();

			renderInfo.position.X = position.X - (renderInfo.bounds.Width / 2);
			renderInfo.position.Y = position.Y - (renderInfo.bounds.Height / 2);

			IShapeF shape = collision.collisionActor.Bounds;
			shape.Position = new Vector2(
				position.X - (renderInfo.bounds.Width / 2),
				position.Y - (renderInfo.bounds.Height / 2));
		}
	}
}
