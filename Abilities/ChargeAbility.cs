using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Util;

namespace GameDevIdiotsProject1.Abilities
{
	abstract class ChargeAbility : Ability
	{
		protected float maxMulti;
		protected float multi;

		public ChargeAbility(Command command) : base (command) { }

		public override bool Start(in Entity entity)
		{
			ref Ability currentAbility = ref entity.Get<CombatStats>().currentAbility;
			if (state == AbilityState.AVAILABLE
				&& ((currentAbility.state != AbilityState.PERFORMING && currentAbility.state != AbilityState.STARTING)
				|| (currentAbility.types.Contains(AbilityType.OVERRIDABLE) && types.Contains(AbilityType.INTERRUPT))))
			{
				currentTime = 0;
				state = AbilityState.CHARGING;
				return true;
			}

			return false;
		}

		public override void Update(float delta, in Entity entity)
		{
			currentTime += delta;
			ref Ability currentAbility = ref entity.Get<CombatStats>().currentAbility;
			if (currentAbility.state == AbilityState.AVAILABLE)
			{
				if (state == AbilityState.COOLDOWN && currentTime > cooldown)
					state = AbilityState.AVAILABLE;
				else if (state == AbilityState.CHARGING && !command.IsPressed())
					End(in entity);
			}
		}

		public override void End(in Entity entity)
		{
			state = AbilityState.COOLDOWN;
			if (currentTime > duration)
			{
				multi = maxMulti;
			}
			else
			{
				multi = (currentTime / duration) * maxMulti;
				if (multi < 1)
					multi = 1;
			}
		}
	}
}
