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
		public static void Create(World world, CollisionComponent collisionComponent, Texture2D texture)
		{
			Entity wall = world.CreateEntity();
			wall.Set(new Position
			{
				Value = new Vector2(40, 40)
			});

			CollisionActorEntity actor = new CollisionActorEntity(new RectangleF(0, 0, 44, 52), CollisionActorEntity.Type.MonsterCollision);
			wall.Set(new Collision(actor));
			collisionComponent.Insert(actor);

			wall.Set(new RenderInfo
			{
				sprite = texture,
				bounds = new Rectangle(40, 0, 44, 52),
				position = new Vector2(0, 0),
				color = Color.White,
				flip = false
			});
		}
	}
}
