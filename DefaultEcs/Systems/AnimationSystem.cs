using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Graphics;
using Microsoft.Xna.Framework;

namespace GameDevIdiotsProject1.DefaultEcs.Systems {
	public sealed class AnimationSystem : AEntitySystem<float> {
		public AnimationSystem(World world)
			: base(world.GetEntities()
				.With<Animate>()
				.With<Velocity>()
				.With<RenderInfo>()
				.AsSet()) {
		}

		protected override void Update(float state, in Entity entity) {

			ref RenderInfo renderInfo = ref entity.Get<RenderInfo>();
			ref Velocity velInfo = ref entity.Get<Velocity>();
			ref Animate animateInfo = ref entity.Get<Animate>();

			Animation currentAnimation = animateInfo.AnimationList[animateInfo.currentAnimation];

			if (velInfo.Value.X < 0)
				renderInfo.flip = true;
			else if (velInfo.Value.X > 0)
				renderInfo.flip = false;

			// update animation
			if (!velInfo.Value.Equals(new Vector2(0,0)))
				currentAnimation.Update(state / 1000);

			//update renderInfo
			renderInfo.bounds = currentAnimation.CurrentRectangle;
		}
	}
}
