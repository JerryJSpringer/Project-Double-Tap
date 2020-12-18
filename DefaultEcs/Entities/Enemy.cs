using DefaultEcs;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;
using System.Collections.Generic;

namespace GameDevIdiotsProject1.DefaultEcs.Entities
{
	public static class Enemy
	{
		#region animation-constants

		private const int IDLE_SPRITE_WIDTH = 16;
		private const int IDLE_SPRITE_HEIGHT = 16;
		private const double IDLE_FRAME_LENGTH = 0.2;

		#endregion

		public static float _scale { get; private set; }

		public static void Create(World world, CollisionComponent collisionComponent, Texture2D texture, float scale = 1f)
		{
			_scale = scale;
			Entity enemy = world.CreateEntity();

			enemy.Set(new Position
			{
				Value = new Vector2(100, 100)
			});

			enemy.Set(new RenderInfo 
			{
				sprite = texture,
				bounds = new Rectangle(0, 0, 16, 16),
				color = Color.White,
				flip = false,
				scale = _scale
			});

			CollisionActorEntity actor = new CollisionActorEntity(
				new CircleF(new Point2(), 8 * _scale),
				CollisionActorEntity.CollisionType.MonsterCollision,
				ref enemy);
			enemy.Set(new Collision(actor));
			collisionComponent.Insert(actor);

			#region create-animations
			// Animations
			var AnimationTable = new Dictionary<string, Animation>();

			//Idle Animation
			Animation idle = new Animation();
			idle.Loop = true;
			idle.AddFrame(new Rectangle(0, 0, IDLE_SPRITE_WIDTH, IDLE_SPRITE_HEIGHT), TimeSpan.FromSeconds(IDLE_FRAME_LENGTH));
			idle.AddFrame(new Rectangle(IDLE_SPRITE_WIDTH, 0, IDLE_SPRITE_WIDTH, IDLE_SPRITE_HEIGHT), TimeSpan.FromSeconds(IDLE_FRAME_LENGTH));
			
			//add to list
			AnimationTable["idle"] = idle;

			//set animations
			enemy.Set(new Animate {
				AnimationList = AnimationTable,
				currentAnimation = "idle"
			});

			#endregion
		}
	}
}