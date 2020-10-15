using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using Microsoft.Xna.Framework;

namespace GameDevIdiotsProject1.DefaultEcs.Entities
{
	public static class Player
	{
		public static void Create(World world)
		{
			Entity player = world.CreateEntity();
			player.Set<PlayerInput>(default);
			player.Set<Velocity>(default);
			player.Set<Position>(default);
			player.Set(new RenderInfo
			{
				color = Color.White,
				sprite = new Rectangle(0, 0, 100, 100)
			});
		}
	}
}
