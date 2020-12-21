using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Util;

namespace GameDevIdiotsProject1.Abilities
{
	abstract class DefaultAbility : Ability
	{
		public DefaultAbility(Command command) : base(command) { }

		public override bool Start(in Entity entity)
		{
			ref Ability currentAbility = ref entity.Get<CombatStats>().currentAbility;
			if (state == AbilityState.AVAILABLE
				&& ((currentAbility.state != AbilityState.PERFORMING && currentAbility.state != AbilityState.STARTING)
				|| (currentAbility.types.Contains(AbilityType.OVERRIDABLE) && types.Contains(AbilityType.INTERRUPT))))
			{
				currentTime = 0;
				state = AbilityState.STARTING;
				currentAbility = this;

				ref Animate animate = ref entity.Get<Animate>();
				if (startAnimation != null)
				{
					animate.AnimationList[startAnimation].Reset();
					animate.currentAnimation = startAnimation;
				}
				return true;
			}

			return false;
		}

		public override void Update(float delta, in Entity entity)
		{
			currentTime += delta;
			ref Animate animate = ref entity.Get<Animate>();

			if (state == AbilityState.COOLDOWN && currentTime > cooldown)
			{
				state = AbilityState.AVAILABLE;
			}
			else if (state == AbilityState.STARTING && currentTime > startup)
			{
				currentTime = 0;
				state = AbilityState.PERFORMING;
				if (updateAnimation != null)
				{
					animate.AnimationList[updateAnimation].Reset();
					animate.currentAnimation = updateAnimation;
				}
			}
			else if (state == AbilityState.PERFORMING && currentTime > duration)
			{
				End(in entity);
			}
		}

		public override void End(in Entity entity)
		{
			currentTime = 0;
			state = AbilityState.COOLDOWN;

			ref Animate animate = ref entity.Get<Animate>();
			if (endAnimation != null)
			{
				animate.AnimationList[endAnimation].Reset();
				animate.currentAnimation = endAnimation;
			}
		}
	}
}
