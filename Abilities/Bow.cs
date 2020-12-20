using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.DefaultEcs.Entities;
using GameDevIdiotsProject1.Util;

namespace GameDevIdiotsProject1.Abilities
{
	class Bow : ChargeAbility
	{
		public static readonly float COOLDOWN = 200;
		public static readonly float MAX_CHARGE = 500;
		public static readonly float MAX_MULTI = 4;
		public static readonly float BULLET_SPEED = .25f;

		public Bow(Command command) : base(command)
		{
			cooldown = COOLDOWN;
			duration = MAX_CHARGE;
			maxMulti = MAX_MULTI;

			types.AddRange(new AbilityType[]
			{
				AbilityType.CHARGE,
				AbilityType.OVERRIDABLE
			});
		}

		public override void End(in Entity entity)
		{
			base.End(in entity);
			BulletFactory.Create(entity.Get<Position>().Value, entity.Get<Aim>().Value, BULLET_SPEED * multi);
		}
	}
}
