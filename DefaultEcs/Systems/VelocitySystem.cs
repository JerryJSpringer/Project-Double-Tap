using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using GameDevIdiotsProject1.DefaultEcs.Components;

namespace GameDevIdiotsProject1.DefaultEcs.Systems
{
	public sealed class VelocitySystem : AEntitySystem<float>
	{
		public VelocitySystem(World world, IParallelRunner runner)
			:base(world.GetEntities()
				.With<Velocity>()
				.With<Position>()
				.AsSet(), runner)
		{
		}

		protected override void Update(float state, in Entity entity)
		{
			ref Velocity velocity = ref entity.Get<Velocity>();
			ref Position position = ref entity.Get<Position>();

			position.Value.X += velocity.Value.X * state;
			position.Value.Y += velocity.Value.Y * state;
		}
	}
}
