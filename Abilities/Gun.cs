using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.DefaultEcs.Entities;
using GameDevIdiotsProject1.Util;

namespace GameDevIdiotsProject1.Abilities
{
	class Gun : DefaultAbility
	{
		public static readonly float COOLDOWN = 200;
		public static readonly float DURATION = 0;
		public static readonly float BULLET_SPEED = .5f;

		public Gun(Command command) : base(command)
		{
			cooldown = COOLDOWN;
			duration = DURATION;

			types.AddRange(new AbilityType[]
			{
				AbilityType.OVERRIDABLE
			});
		}

		public override void Start(in Entity entity)
		{
			base.Start(in entity);
			BulletFactory.Create(entity.Get<Position>().Value, entity.Get<Aim>().Value, BULLET_SPEED);
		}
	}
}
