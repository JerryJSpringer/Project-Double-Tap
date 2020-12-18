using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Util;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;

namespace GameDevIdiotsProject1.DefaultEcs.Systems
{
	public sealed class CameraSystem : AEntitySystem<float>
	{

		private readonly int _center;
        private readonly OrthographicCamera _camera;
		private readonly TiledMap _tiledMap;
		private readonly GameWindow _window;

		public CameraSystem(World world, OrthographicCamera camera, TiledMap tiledMap, GameWindow window, int gamesize) :
			base(world.GetEntities()
				.With<PlayerInput>()
				.With<RenderInfo>()
				.AsSet())		
		{
			_camera = camera;
			_tiledMap = tiledMap;
			_window = window;
			_center = gamesize / 2;
		}

		protected override void Update(float state, in Entity entity)
		{
			Vector2 charPosition = entity.Get<Position>().Value;

			Vector2 cameraPosition = new Vector2
			{
				X = (charPosition.X <= _center) ? _center
				: (charPosition.X >= _tiledMap.WidthInPixels - _center) ? _tiledMap.WidthInPixels - _center
				: charPosition.X,

				Y = (charPosition.Y <= _center) ? _center
				: (charPosition.Y >= _tiledMap.HeightInPixels - _center) ? _tiledMap.HeightInPixels - _center
				: charPosition.Y
			};

			int width = _window.ClientBounds.Width / 2;
			int height = _window.ClientBounds.Height / 2;

			CameraFactory.Update(new Vector2
			{
				X = (charPosition.X <= width) ? charPosition.X
				: (charPosition.X >= _tiledMap.WidthInPixels - width) ? charPosition.X - _tiledMap.WidthInPixels + 2 * width
				: width,

				Y = (charPosition.Y <= height) ? charPosition.Y
				: (charPosition.Y >= _tiledMap.HeightInPixels - height) ? charPosition.Y - _tiledMap.HeightInPixels + 2 * height
				: height
			});

			_camera.LookAt(cameraPosition);
			
		}
	}
}
