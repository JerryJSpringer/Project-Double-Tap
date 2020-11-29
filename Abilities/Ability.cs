using DefaultEcs;

namespace GameDevIdiotsProject1.Abilities
{
	public abstract class Ability
	{
		public float currentTime;
		public float cooldown;
		public float duration;
		public bool movementOverride;
		public AbilityState abilityState;

		public abstract void Start(in Entity entity, in World world);
		public abstract void Update(float state, in Entity entity, in World world);
		public abstract void End(in Entity entity, in World world);
	}

	public enum AbilityState
	{
		AVAILABLE = 0,
		PERFORMING = 1,
		COOLDOWN = 2
	}
}
