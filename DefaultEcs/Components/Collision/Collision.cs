using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;

namespace GameDevIdiotsProject1.DefaultEcs.Components
{
	public struct Collision {
		public ICollisionActor collisionActor;

		public Collision(ICollisionActor aActor) { //?
			collisionActor = aActor;
        }
	}

	/// <summary>
	/// Used to 
	/// </summary>
	public class CollisionActorEntity : ICollisionActor
	{
		public IShapeF Bounds { get; }
		public Type type;

		public CollisionActorEntity(IShapeF bounds, Type type)
		{
			Bounds = bounds;
			this.type = type;
		}
		
		public void OnCollision(CollisionEventArgs collisionInfo)
		{
			CollisionActorEntity other = (CollisionActorEntity) collisionInfo.Other;
			Console.WriteLine(other.type);
		}

		public enum Type {
			// Take damage
			NormalCollision = 0,

			// Stop moving
			DamageCollision = 1,

			// Pickup item
			ItemPickupCollision = 2,

			// Load new level
			LoadingZoneCollision = 4,

			// Player collision
			PlayerCollision = 8,

			// Monster collision
			MonsterCollision = 16
		}
	}
}
