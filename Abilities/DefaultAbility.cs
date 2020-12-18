using DefaultEcs;
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
			state = AbilityState.PERFORMING;
		}

		public override void Update(float delta, in Entity entity)
		{
			currentTime += delta;

			if (state == AbilityState.COOLDOWN && currentTime > cooldown)
				state = AbilityState.AVAILABLE;
			else if (state == AbilityState.PERFORMING && currentTime > duration)
				End(in entity);
		}

		public override void End(in Entity entity)
		{
			currentTime = 0;
			state = AbilityState.COOLDOWN;
		}
	}
}
