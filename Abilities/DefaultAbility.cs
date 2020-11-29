using DefaultEcs;

namespace GameDevIdiotsProject1.Abilities
{
	class DefaultAbility : Ability
	{
		public override void Start(in Entity entity, in World world)
		{
			currentTime = 0;
			abilityState = AbilityState.PERFORMING;
		}

		public override void Update(float state, in Entity entity, in World world)
		{
			currentTime += state;
				
			if (currentTime > duration)
			{
				if (abilityState == AbilityState.COOLDOWN)
					abilityState = AbilityState.AVAILABLE;
				else if (abilityState == AbilityState.PERFORMING)
					End(in entity, in world);
			}
		}

		public override void End(in Entity entity, in World world)
		{
			currentTime = 0;
			abilityState = AbilityState.COOLDOWN;
		}
	}
}
