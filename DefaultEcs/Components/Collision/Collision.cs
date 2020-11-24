using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace GameDevIdiotsProject1.DefaultEcs.Components
{
	public struct Collision {
		public ICollisionActor collisionActor;
		public CollisionActorEntity.Type type;

		public Collision(ICollisionActor aActor, CollisionActorEntity.Type aType) { //?
			collisionActor = aActor;
            type = aType;
        }
	}

	public class CollisionActorEntity : ICollisionActor
	{
		public IShapeF Bounds { get; }
		public Type type;

		public CollisionActorEntity(IShapeF bounds)
		{
			Bounds = bounds;
		}
		
		public void OnCollision(CollisionEventArgs collisionInfo)
		{
			CollisionActorEntity other = (CollisionActorEntity) collisionInfo.Other;
		}

		public enum Type {
			// Take damage
			DamageCollision = 0,

			// Stop moving
			NormalCollision = 1,

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
