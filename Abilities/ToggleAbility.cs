using DefaultEcs;
using GameDevIdiotsProject1.Util;

namespace GameDevIdiotsProject1.Abilities
{
	abstract class ToggleAbility : Ability
	{
		private const float DEFAULT_COOLDOWN = 200;
		public ToggleAbility(Command command) : base (command)
		{
			cooldown = DEFAULT_COOLDOWN;

			// To prevent lockout at t < cooldown
			currentTime = cooldown;
		}

		public override void Start(in Entity entity)
		{
			if (currentTime < cooldown)
				return;

			if (state == AbilityState.AVAILABLE)
			{
				state = AbilityState.ACTIVE;
				AddBuff(in entity);
			}
			else if (state == AbilityState.ACTIVE)
			{
				state = AbilityState.AVAILABLE;
				RemoveBuff(in entity);
			}

			End(in entity);
		}

		public override void Update(float delta, in Entity entity)
		{
			currentTime += delta;
		}

		protected abstract void AddBuff(in Entity entity);

		protected abstract void RemoveBuff(in Entity entity);

		public override void End(in Entity entity)
		{
			currentTime = 0;
		}
	}
}
