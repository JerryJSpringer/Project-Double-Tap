using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.DefaultEcs.Components;
using Microsoft.Xna.Framework;

namespace GameDevIdiotsProject1.DefaultEcs.Systems
{
	public sealed class VelocitySystem : AEntitySystem<float>
	{
		public VelocitySystem(World world)
			:base(world.GetEntities()
				 .With<Velocity>()
				 .With<Position>()
				 .AsSet()) { }

		protected override void Update(float delta, in Entity entity)
		{
			ref Velocity velocity = ref entity.Get<Velocity>();
			ref Position position = ref entity.Get<Position>();

			Vector2 dir = velocity.Value;
			float speed = velocity.speed;

			// Get unit direction vector if non-zero
			if (dir.X != 0 || dir.Y != 0)
				dir.Normalize();

			if (entity.Has<CombatStats>())
				speed *= 1 + entity.Get<CombatStats>().speedBonus;

			// Position * direction * delta
			position.Value.X += dir.X * speed * delta;
			position.Value.Y += dir.Y * speed * delta;
		}
	}
}