using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.DefaultEcs.Entities;
using GameDevIdiotsProject1.Util;

namespace GameDevIdiotsProject1.Abilities
{
	class Bow : ChargeAbility
	{
		private const int COOLDOWN = 200;
		private const int MAX_CHARGE = 500;
		private const int MAX_MULTI = 4;
		private const float BULLET_SPEED = .25f;

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
