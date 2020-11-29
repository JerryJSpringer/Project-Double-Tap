using DefaultEcs;
using GameDevIdiotsProject1.Abilities;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;
using System.Collections.Generic;

namespace GameDevIdiotsProject1.DefaultEcs.Entities
{
	public static class Player
	{
        #region animation-constants

        private const int WALK_SPRITE_HEIGHT = 80;
		private const int WALK_SPRITE_WIDTH = 80;
        private const double WALK_FRAME_LENGTH = 0.08;

		#endregion

		public static void Create(World world, CollisionComponent collisionComponent, Texture2D texture)
		{
			Entity player = world.CreateEntity();
			player.Set<PlayerInput>(default);

			player.Set(new Position
			{
				Value = new Vector2(0, 0)
			});

			player.Set(new Velocity
			{
				Value = new Vector2(),
				speed = .2f
			});

			player.Set(new RenderInfo {
				sprite = texture,
				bounds = new Rectangle(0, 0, WALK_SPRITE_HEIGHT, WALK_SPRITE_WIDTH),
				color = Color.White,
				flip = false
			});

			// Collision
			CollisionActorEntity actor = new CollisionActorEntity(
				new RectangleF(0, 0, WALK_SPRITE_HEIGHT, WALK_SPRITE_WIDTH), 
					CollisionActorEntity.CollisionType.PlayerCollision,
					ref player
				);
			player.Set(new Collision(actor));
			collisionComponent.Insert(actor);

			
			// Animations
			var AnimationTable = new Dictionary<string, Animation>();

			// CREATE ANIMATIONS
			Animation walkDown = new Animation();
			walkDown.AddFrame(new Rectangle(0, 0, WALK_SPRITE_HEIGHT, WALK_SPRITE_WIDTH), TimeSpan.FromSeconds(WALK_FRAME_LENGTH));
			walkDown.AddFrame(new Rectangle(0 + WALK_SPRITE_WIDTH, 0, WALK_SPRITE_HEIGHT, WALK_SPRITE_WIDTH), TimeSpan.FromSeconds(WALK_FRAME_LENGTH));
			walkDown.AddFrame(new Rectangle(0 + WALK_SPRITE_WIDTH * 2, 0, WALK_SPRITE_HEIGHT, WALK_SPRITE_WIDTH), TimeSpan.FromSeconds(WALK_FRAME_LENGTH));
			walkDown.AddFrame(new Rectangle(0 + WALK_SPRITE_WIDTH * 3, 0, WALK_SPRITE_HEIGHT, WALK_SPRITE_WIDTH), TimeSpan.FromSeconds(WALK_FRAME_LENGTH));
			walkDown.AddFrame(new Rectangle(0 + WALK_SPRITE_WIDTH * 4, 0, WALK_SPRITE_HEIGHT, WALK_SPRITE_WIDTH), TimeSpan.FromSeconds(WALK_FRAME_LENGTH));
			walkDown.AddFrame(new Rectangle(0 + WALK_SPRITE_WIDTH * 5, 0, WALK_SPRITE_HEIGHT, WALK_SPRITE_WIDTH), TimeSpan.FromSeconds(WALK_FRAME_LENGTH));
			walkDown.AddFrame(new Rectangle(0 + WALK_SPRITE_WIDTH * 6, 0, WALK_SPRITE_HEIGHT, WALK_SPRITE_WIDTH), TimeSpan.FromSeconds(WALK_FRAME_LENGTH));
			walkDown.AddFrame(new Rectangle(0 + WALK_SPRITE_WIDTH * 7, 0, WALK_SPRITE_HEIGHT, WALK_SPRITE_WIDTH), TimeSpan.FromSeconds(WALK_FRAME_LENGTH));
			walkDown.AddFrame(new Rectangle(0 + WALK_SPRITE_WIDTH * 8, 0, WALK_SPRITE_HEIGHT, WALK_SPRITE_WIDTH), TimeSpan.FromSeconds(WALK_FRAME_LENGTH));

			// Add to List
			AnimationTable["walk-down"] = walkDown;

			//set animations
			player.Set(new Animate {
				AnimationList = AnimationTable,
				currentAnimation = "walk-down"
			});


			// Abilities
			var abilityTable = new Dictionary<string, Ability>();
			abilityTable[Keys.Space.ToString()] = new DodgeRoll();

			player.Set(new CombatStats
			{
				currentAbility = new DefaultAbility(),
				abilities = abilityTable
			});
		}
	}
}
