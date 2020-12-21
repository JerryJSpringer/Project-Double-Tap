using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Util;

namespace GameDevIdiotsProject1.Abilities
{
	class SpeedBuff : ToggleAbility
	{
		public static readonly float SPEED_BUFF = 0.2f;

		public SpeedBuff(Command command) : base (command)
		{
			types.AddRange(new AbilityType[]
			{
				AbilityType.INSTANT
			});
		}

		protected override void AddBuff(in Entity entity)
		{
			ref CombatStats combatStats = ref entity.Get<CombatStats>();
			combatStats.speedBonus += SPEED_BUFF;
		}

		protected override void RemoveBuff(in Entity entity)
		{
			ref CombatStats combatStats = ref entity.Get<CombatStats>();
			combatStats.speedBonus -= SPEED_BUFF;
		}
	}
}
