using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.DefaultEcs.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;

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
			ref Vector2 position = ref entity.Get<Position>().Value;
			ref RenderInfo renderInfo = ref entity.Get<RenderInfo>();
			ref Collision collision = ref entity.Get<Collision>();

			//move npc for testing purposes
			if (!entity.Has<PlayerInput>()) {
				position.X += .02f * state;
			}

			renderInfo.position.X = position.X - (renderInfo.bounds.Width / 2);
			renderInfo.position.Y = position.Y - (renderInfo.bounds.Height / 2);

			//collision bounds should match render bounds for now
			collision.collisionActor.Bounds.Position = renderInfo.position;
			
			//send collision bounds to RenderInfo for debugging purposes
			renderInfo.collisionBounds = collision.collisionActor.Bounds;
			
		}
	}
}
