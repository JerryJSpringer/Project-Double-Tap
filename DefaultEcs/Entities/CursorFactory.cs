using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.DefaultEcs.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collisions;

namespace GameDevIdiotsProject1.Util
{
	public static class CursorFactory
	{
		private static Cursor _cursor;
		private static Matrix _inverseViewMatrix;
		private static Vector2 _offset;

		public static void Init(World world, CollisionComponent collisionComponent, Texture2D texture, float scale = 1f)
		{
			_cursor = new Cursor(world, collisionComponent, texture, scale);
		}

		public static void Create()
		{
			_cursor.Create();
		}

		public static Vector2 Update(Vector2 mousePosition)
		{
			return _cursor.Update(Vector2.Transform(mousePosition - _offset, _inverseViewMatrix));
		}

		public static void UpdateCamera(Matrix inverseViewMatrix, Vector2 offset)
		{
			_inverseViewMatrix = inverseViewMatrix;
			_offset = offset;
		}

		public static void Dispose()
		{
			_cursor.Dispose();
		}
	}

	internal class Cursor : EntityFactory
	{
		private Entity _entity;

		internal Cursor(World world, CollisionComponent collisionComponent, Texture2D texture, float scale)
			: base(world, collisionComponent, texture, scale) { }

		internal void Create()
		{

			_entity = _world.CreateEntity();

			_entity.Set(new Position
			{
				Value = new Vector2()
			});

			_entity.Set(new RenderInfo
			{
				sprite = _texture,
				bounds = new Rectangle(0, 0, 16, 16),
				color = Color.White,
				flip = false,
				scale = _scale
			});
		}

		internal Vector2 Update(Vector2 position)
		{
			return _entity.Get<Position>().Value = position;
		}

		internal Vector2 GetPosition()
		{
			return _entity.Get<Position>().Value;
		}
	}
}
