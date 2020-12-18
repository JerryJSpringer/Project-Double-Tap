using Microsoft.Xna.Framework;

namespace GameDevIdiotsProject1.Util
{
	public static class CameraFactory
	{
		private static Camera _camera;

		public static void Init()
		{
			_camera = new Camera();
		}

		public static void Update(Vector2 position)
		{
			_camera.Update(ref position);
		}

		public static Vector2 GetPosition()
		{
			return _camera.GetPosition();
		}
	}

	internal class Camera
	{
		private Vector2 _position;
		internal Camera()
		{
			_position = new Vector2();
		}

		internal void Update(ref Vector2 position)
		{
			_position = position;
		}

		internal Vector2 GetPosition()
		{
			return _position;
		}
	}
}
