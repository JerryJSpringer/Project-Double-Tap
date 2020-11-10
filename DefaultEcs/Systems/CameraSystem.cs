using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.DefaultEcs.Components;
using MonoGame.Extended;

namespace GameDevIdiotsProject1.DefaultEcs.Systems
{
	public sealed class CameraSystem : AEntitySystem<float>
	{
		private readonly OrthographicCamera _camera;

		public CameraSystem(World world, OrthographicCamera camera) :
			base(world.GetEntities()
				.With<PlayerInput>()
				.With<RenderInfo>()
				.AsSet())		
		{
			_camera = camera;
		}

		protected override void Update(float state, in Entity entity)
		{
			_camera.LookAt(entity.Get<Position>().Value);
		}
	}
}
