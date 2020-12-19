using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Util;

namespace GameDevIdiotsProject1.Abilities
{
	abstract class DefaultAbility : Ability
	{
		public DefaultAbility(Command command) : base(command)
		{
		}

		public override void Start(in Entity entity)
		{
			currentTime = 0;
			state = AbilityState.STARTING;
		}

		public override void Update(float delta, in Entity entity)
		{
			currentTime += delta;
			ref Animate animation = ref entity.Get<Animate>();

			if (state == AbilityState.COOLDOWN && currentTime > cooldown)
			{
				state = AbilityState.AVAILABLE;
			}
			else if (state == AbilityState.STARTING && currentTime > startup)
			{
				state = AbilityState.PERFORMING;
				if (startAnimation != null)
					animation.currentAnimation = startAnimation;
			}
			else if (state == AbilityState.PERFORMING)
			{
				if (currentTime > duration)
					End(in entity);
				else if (updateAnimation != null)
					animation.currentAnimation = updateAnimation;
			}
		}

		public override void End(in Entity entity)
		{
			currentTime = 0;
			state = AbilityState.COOLDOWN;

			if (endAnimation != null)
			{
				ref string animation = ref entity.Get<Animate>().currentAnimation;
				animation = endAnimation;
			}
		}
	}
}
