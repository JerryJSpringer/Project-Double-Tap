using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Util;
using System.Collections.Generic;

namespace GameDevIdiotsProject1.Abilities
{
	public abstract class Ability
	{
		protected const string DEFAULT_IDLE_ANIMATION = "idle";
		protected float currentTime;
		protected float startup;
		protected float cooldown;
		public float duration;
		protected string startAnimation;
		protected string updateAnimation;
		protected string endAnimation;
		public Command command;
		public AbilityState state;
		public List<AbilityType> types;

		public Ability(Command command)
		{
			this.command = command;
			types = new List<AbilityType>();
			state = AbilityState.AVAILABLE;
		}

		public abstract bool Start(in Entity entity);
		public abstract void Update(float delta, in Entity entity);
		public abstract void End(in Entity entity);
	}

	public enum AbilityState
	{
		AVAILABLE,
		PERFORMING,
		CHARGING,
		COOLDOWN,
		ACTIVE,
		STARTING
	}

	public enum AbilityType
	{
		MOVEMENTOVERRIDE = 1,
		CHARGE = 2,
		INTERRUPT = 4,
		OVERRIDABLE = 8,
		INSTANT = 16
	}
}
