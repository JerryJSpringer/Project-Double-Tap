using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace GameDevIdiotsProject1.DefaultEcs.Entities
{
	public static class BulletFactory
	{
		private static Bullet _bullet;

		public static void Init(World world, CollisionComponent collisionComponent, Texture2D texture, float scale = 1f)
		{
			_bullet = new Bullet(world, collisionComponent, texture, scale);
		}

		public static void Create(Vector2 position, Vector2 direction, float speed)
		{
			_bullet.Create(position, direction, speed);
		}
	}

	internal class Bullet : EntityFactory
	{
		internal Bullet(World world, CollisionComponent collisionComponent, Texture2D texture, float scale) 
			: base(world, collisionComponent, texture, scale)
		{
		}

		internal void Create(Vector2 position, Vector2 direction, float speed)
		{
			Entity bullet = _world.CreateEntity();

			bullet.Set(new Position
			{
				Value = position
			});

			bullet.Set(new Velocity
			{
				Value = direction,
				speed = speed
			});

			bullet.Set(new RenderInfo
			{
				sprite = _texture,
				bounds = new Rectangle(0, 0, 4, 4),
				color = Color.White,
				flip = false,
				scale = _scale
			});

			CollisionActorEntity actor = new CollisionActorEntity(
				new CircleF(new Point2(), 4 * _scale),
				CollisionActorEntity.CollisionType.DamageCollision,
				ref bullet);
			bullet.Set(new Collision(actor));
			_collisionComponent.Insert(actor);
		}
	}
}
