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
				.AsSet()) { }

		protected override void Update(float delta, in Entity entity)
		{
			Vector2 position = entity.Get<Position>().Value;
			ref RenderInfo renderInfo = ref entity.Get<RenderInfo>();

			renderInfo.position.X = position.X;
			renderInfo.position.Y = position.Y;

			if (entity.Has<Collision>())
			{
				ref Collision collision = ref entity.Get<Collision>();

				if (collision.collisionActor.Bounds is RectangleF rectangle)
				{
					rectangle.Position = new Vector2(
						position.X - (rectangle.Width / 2),
						position.Y - (rectangle.Height / 2));

					collision.collisionActor.Bounds.Position = rectangle.Position;
				}
				else if (collision.collisionActor.Bounds is CircleF)
				{
					collision.collisionActor.Bounds.Position = position;
				}
			}
		}
	}
}
