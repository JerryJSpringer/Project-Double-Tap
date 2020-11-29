using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.DefaultEcs.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using System;

namespace GameDevIdiotsProject1.DefaultEcs.Systems
{
	public sealed class CameraSystem : AEntitySystem<float>
	{

		private readonly int _center;
        private readonly OrthographicCamera _camera;
		private readonly TiledMap _tiledMap;

		public CameraSystem(World world, OrthographicCamera camera, TiledMap tiledMap, int gamesize) :
			base(world.GetEntities()
				.With<PlayerInput>()
				.With<RenderInfo>()
				.AsSet())		
		{
			_camera = camera;
			_tiledMap = tiledMap;
			_center = gamesize / 2;
		}

		protected override void Update(float state, in Entity entity)
		{
			Vector2 pos = new Vector2();

			pos.X = (entity.Get<Position>().Value.X <= _center) ? _center
				: (entity.Get<Position>().Value.X >= _tiledMap.WidthInPixels - _center) ? _tiledMap.WidthInPixels - _center
				: entity.Get<Position>().Value.X;

			pos.Y = (entity.Get<Position>().Value.Y <= _center) ? _center
				: (entity.Get<Position>().Value.Y >= _tiledMap.HeightInPixels - _center) ? _tiledMap.HeightInPixels - _center
				: entity.Get<Position>().Value.Y;

			_camera.LookAt(pos);
			
		}
	}
}
