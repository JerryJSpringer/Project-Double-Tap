using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GameDevIdiotsProject1.DefaultEcs.Components
{
	public struct RenderInfo {
		public Texture2D sprite;
		public Rectangle bounds;
		public IShapeF collisionBounds;
		public Vector2 position;
		public Color color;
		public bool flip;
	}
}
