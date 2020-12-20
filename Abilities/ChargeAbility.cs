using DefaultEcs;
using GameDevIdiotsProject1.Util;

namespace GameDevIdiotsProject1.Abilities
{
	abstract class ChargeAbility : Ability
	{
		protected float maxMulti;
		protected float multi;

		public ChargeAbility(Command command) : base (command) { }

		public override void Start(in Entity entity)
		{
			state = AbilityState.CHARGING;
			currentTime = 0;
		}

		public override void Update(float delta, in Entity entity)
		{
			currentTime += delta;
			if (state == AbilityState.COOLDOWN && currentTime > cooldown)
				state = AbilityState.AVAILABLE;
			else if (state == AbilityState.CHARGING && !command.IsPressed())
				End(in entity);
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
