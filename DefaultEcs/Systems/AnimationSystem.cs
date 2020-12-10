using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.Abilities;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Graphics;
using Microsoft.Xna.Framework;

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
			Velocity velInfo;
			AbilityState abilityState;

			//attempt to retrieve a Velocity Component on the entity
			try {
				velInfo = entity.Get<Velocity>();
				if (velInfo.Value.X < 0)
					renderInfo.flip = true;
				else if (velInfo.Value.X > 0)
					renderInfo.flip = false;

				//attempt to retrieve CombatStats component on entity
				try {
					abilityState = entity.Get<CombatStats>().currentAbility.abilityState;
					if (abilityState != AbilityState.PERFORMING)
						// update animation
						if (velInfo.Value.Equals(new Vector2(0, 0)))
							animateInfo.currentAnimation = "idle";
						else
							animateInfo.currentAnimation = "walk";
				}
				catch {
					// update animation
					if (velInfo.Value.Equals(new Vector2(0, 0)))
						animateInfo.currentAnimation = "idle";
					else
						animateInfo.currentAnimation = "walk";
				}	
			}
			catch {
				animateInfo.currentAnimation = "idle";
            }


			Animation currentAnimation = animateInfo.AnimationList[animateInfo.currentAnimation];
			currentAnimation.Update(state / 1000);



			//update renderInfo
			renderInfo.bounds = currentAnimation.CurrentRectangle;
		}
	}
}
