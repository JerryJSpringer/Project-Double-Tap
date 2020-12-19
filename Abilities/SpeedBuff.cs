using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Util;

namespace GameDevIdiotsProject1.Abilities
{
	class SpeedBuff : ToggleAbility
	{
		private const float SPEED_BUFF = 1.5f;

		public SpeedBuff(Command command) : base (command)
		{
			types.AddRange(new AbilityType[]
			{
				AbilityType.INSTANT
			});
		}

		protected override void AddBuff(in Entity entity)
		{
			ref float speed = ref entity.Get<Velocity>().speed;
			speed *= SPEED_BUFF;
		}

		protected override void RemoveBuff(in Entity entity)
		{
			ref float speed = ref entity.Get<Velocity>().speed;
			speed /= SPEED_BUFF;
		}
	}
}
