using GameDevIdiotsProject1.Abilities;
using System.Collections.Generic;

namespace GameDevIdiotsProject1.DefaultEcs.Components
{
	public struct CombatStats {
		public int health;
		public int maxHealth;
		public int damage;

		public List<Ability> abilities;
		public Ability currentAbility;
	}
}
