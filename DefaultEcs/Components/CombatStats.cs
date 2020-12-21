using GameDevIdiotsProject1.Abilities;
using System.Collections.Generic;

namespace GameDevIdiotsProject1.DefaultEcs.Components
{
	public struct CombatStats {
		public float health;
		public int maxHealth;
		public float damage;
		public float speedBonus;

		// The highest priority ability active
		public Ability currentAbility;
		public List<Ability> abilities;
	}
}
