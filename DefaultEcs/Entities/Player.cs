using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

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
			player.Set(new RenderInfo {
				sprite = texture,
				bounds = new Rectangle(40, 0, 44, 52),
				position = new Vector2(0, 0),
				color = Color.White,
				flip = false
			});

			
			// Animations
			var AnimationTable = new Dictionary<string, Animation>();

			// CREATE ANIMATIONS
			Animation walkDown = new Animation();
			walkDown.AddFrame(new Rectangle(848, 0, 44, 52), TimeSpan.FromSeconds(0.25));
			walkDown.AddFrame(new Rectangle(936, 0, 44, 52), TimeSpan.FromSeconds(0.25));
			walkDown.AddFrame(new Rectangle(980, 0, 44, 52), TimeSpan.FromSeconds(0.25));

			// Add to List
			AnimationTable["walk-down"] = walkDown;

			//set animations
			player.Set(new Animate {
				AnimationList = AnimationTable,
				currentAnimation = "walk-down"
			});
			
		}
	}
}
