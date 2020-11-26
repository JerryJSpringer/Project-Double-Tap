using DefaultEcs;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;

namespace GameDevIdiotsProject1.DefaultEcs.Components
{
	public struct Collision
	{
		public ICollisionActor collisionActor;

		public Collision(ICollisionActor aActor) {
			collisionActor = aActor;
        }
	}

	public class CollisionActorEntity : ICollisionActor
	{
		public IShapeF Bounds { get; }
		public CollisionType Type { get; }
		public Entity Entity { get; }

		public CollisionActorEntity(IShapeF bounds, CollisionType type, ref Entity entity)
		{
			Bounds = bounds;
			Type = type;
			Entity = entity;
		}
		
		public void OnCollision(CollisionEventArgs collisionInfo)
		{
			CollisionActorEntity other = (CollisionActorEntity) collisionInfo.Other;
			switch (other.Type) {
				case CollisionType.NormalCollision:
					break;

				case CollisionType.DamageCollision:
					break;

				case CollisionType.ItemPickupCollision:
					break;

				case CollisionType.PlayerCollision:
					break;

				case CollisionType.MonsterCollision:
					ref Vector2 position = ref Entity.Get<Position>().Value;
					position -= collisionInfo.PenetrationVector;
					break;
			}
		}

		public enum CollisionType 
		{
			// Stop moving
			NormalCollision = 0,

			// Take damage
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
