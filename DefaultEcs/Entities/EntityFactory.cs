using DefaultEcs;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collisions;

namespace GameDevIdiotsProject1.DefaultEcs.Entities
{
	public class EntityFactory
	{
		protected readonly World _world;
		protected readonly CollisionComponent _collisionComponent;
		protected readonly Texture2D _texture;
		protected readonly float _scale;

		public EntityFactory(World world, CollisionComponent collisionComponent, Texture2D texture, float scale)
		{
			_world = world;
			_collisionComponent = collisionComponent;
			_texture = texture;
			_scale = scale;
		}
	}
}
