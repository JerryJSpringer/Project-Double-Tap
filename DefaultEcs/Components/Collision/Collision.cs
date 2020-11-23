using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System.Collections.Generic;
using System.Numerics;

namespace GameDevIdiotsProject1.DefaultEcs.Components
{
	public struct Collision {
		public ICollisionActor collisionActor;
	}

	public class CollisionActorEntity : ICollisionActor
	{
		public IShapeF Bounds { get; }
		public Vector2 Velocity;
		public List<CollisionEventArgs> collisions;

		public CollisionActorEntity(IShapeF bounds)
		{
			Bounds = bounds;
		}

		public void OnCollision(CollisionEventArgs collisionInfo)
		{
			collisions.Add(collisionInfo);
		}
	}
}
