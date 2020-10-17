using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevIdiotsProject1.DefaultEcs.Components
{
	public struct RenderInfo {
		public Texture2D sprite;
		public Rectangle bounds;
		public Color color;
	}
}
