using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace GameDevIdiotsProject1.DefaultEcs.Entities
{
	public static class Wall
	{
		public static float _scale { get; private set; }

		public static void Create(World world, CollisionComponent collisionComponent, Texture2D texture, float scale = 1f)
		{
			_scale = scale;
			Entity wall = world.CreateEntity();

			wall.Set(new Position
			{
				Value = new Vector2(100, 100)
			});

			wall.Set(new RenderInfo {
				sprite = texture,
				bounds = new Rectangle(0, 0, 10, 15),
				color = Color.White,
				flip = false,
				scale = _scale
			});

			CollisionActorEntity actor = new CollisionActorEntity(
				new CircleF(new Point2(), 8 * _scale),
				CollisionActorEntity.CollisionType.MonsterCollision,
				ref wall);
			wall.Set(new Collision(actor));
			collisionComponent.Insert(actor);
		}
	}
}