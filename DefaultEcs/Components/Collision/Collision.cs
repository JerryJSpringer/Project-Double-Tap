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

	public class CollisionActorEntity : ICollisionActor
	{
		public IShapeF Bounds { get; }
		public Type type;

		//counter to help debug collisions
		private static int _counter;

		public CollisionActorEntity(IShapeF bounds, Type type)
		{
			Bounds = bounds;
			this.type = type;
		}
		
		public void OnCollision(CollisionEventArgs collisionInfo)
		{
			CollisionActorEntity other = (CollisionActorEntity) collisionInfo.Other;

			//for debugging collision - change project to Console Application to see output
			Console.WriteLine("Collide " + _counter++);
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
