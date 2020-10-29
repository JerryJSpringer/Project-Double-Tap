using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Graphics;

namespace GameDevIdiotsProject1.DefaultEcs.Systems {
	public sealed class AnimationSystem : AEntitySystem<float> {
		public AnimationSystem(World world)
			: base(world.GetEntities()
				.With<Animate>()
				.With<RenderInfo>()
				.AsSet()) {
		}

		protected override void Update(float state, in Entity entity) {

			ref RenderInfo renderInfo = ref entity.Get<RenderInfo>();
			ref Animate animateInfo = ref entity.Get<Animate>();

			Animation currentAnimation = animateInfo.AnimationList[animateInfo.currentAnimation];

			// update animation
			currentAnimation.Update(state / 1000);

			//update renderInfo
			renderInfo.bounds = currentAnimation.CurrentRectangle;
		}
	}
}
