using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Util;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.ViewportAdapters;

namespace GameDevIdiotsProject1.DefaultEcs.Systems
{
	public sealed class CameraSystem : AEntitySystem<float>
	{

		private readonly int _center;
        private readonly OrthographicCamera _camera;
		private readonly TiledMap _tiledMap;
		private readonly BoxingViewportAdapter _viewportAdapter;

		public CameraSystem(World world, OrthographicCamera camera, TiledMap tiledMap, BoxingViewportAdapter viewportAdapter, int gamesize) :
			base(world.GetEntities()
				.With<PlayerInput>()
				.With<RenderInfo>()
				.AsSet())		
		{
			_camera = camera;
			_tiledMap = tiledMap;
			_viewportAdapter = viewportAdapter;
			_center = gamesize / 2;
		}

		protected override void Update(float delta, in Entity entity)
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

			CameraFactory.Update(Vector2.Transform(charPosition, _camera.GetViewMatrix()) 
				+ new Vector2(_viewportAdapter.Viewport.X, _viewportAdapter.Viewport.Y));

			_camera.LookAt(cameraPosition);
			
		}
	}
}
