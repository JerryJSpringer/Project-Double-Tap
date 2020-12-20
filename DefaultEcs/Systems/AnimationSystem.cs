using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Graphics;
namespace GameDevIdiotsProject1.DefaultEcs.Systems {
	public sealed class AnimationSystem : AEntitySystem<float> {
		public AnimationSystem(World world)
			: base(world.GetEntities()
				.With<Animate>()
				.With<RenderInfo>()
				.AsSet()) { }

		protected override void Update(float delta, in Entity entity) {

			ref RenderInfo renderInfo = ref entity.Get<RenderInfo>();
			ref Animate animateInfo = ref entity.Get<Animate>();
			Velocity velocity;

			if (entity.Has<Velocity>())
			{
				velocity = entity.Get<Velocity>();

				// Flip
				if (velocity.Value.X < 0)
					renderInfo.flip = true;
				else if (velocity.Value.X > 0)
					renderInfo.flip = false;
			}

			Animation currentAnimation = animateInfo.AnimationList[animateInfo.currentAnimation];
			currentAnimation.Update(delta / 1000);

			// Update renderInfo
			renderInfo.bounds = currentAnimation.CurrentRectangle;
		}
	}
}
