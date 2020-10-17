using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevIdiotsProject1.DefaultEcs.Entities
{
	public static class Player
	{
		public static void Create(World world, Texture2D texture)
		{
			Entity player = world.CreateEntity();
			player.Set<PlayerInput>(default);
			player.Set<Position>(default);
			player.Set(new Velocity
			{
				Value = new Vector2(),
				speed = .2f
			});
			player.Set(new RenderInfo
			{
				sprite = texture,
				bounds = new Rectangle(0, 0, 16, 16),
				color = Color.White
			});
		}
	}
}
