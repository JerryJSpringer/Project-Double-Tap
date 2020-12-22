using DefaultEcs;
using GameDevIdiotsProject1.Abilities;
using GameDevIdiotsProject1.DefaultEcs.Components;
using GameDevIdiotsProject1.Graphics;
using GameDevIdiotsProject1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;
using System.Collections.Generic;

namespace GameDevIdiotsProject1.DefaultEcs.Entities
{
	public static class PlayerFactory
	{
		private static Player _player;

		public static float _scale;

		public static void Init(World world, CollisionComponent collisionComponent, Texture2D texture, float scale = 1f)
		{
			_player = new Player(world, collisionComponent, texture, scale);
		}

        public static void Create(Vector2 position)
		{
			_player.Create(ref position);
		}

		public static void Dispose()
		{
			_player.Dispose();
		}
	}

	internal class Player : EntityFactory
	{
		#region animation-constants

		private const int WALK_SPRITE_WIDTH = 16;
		private const int WALK_SPRITE_HEIGHT = 16;
		private const double WALK_FRAME_LENGTH = 0.1;

		private const int DODGE_SPRITE_WIDTH = 16;
		private const int DODGE_SPRITE_HEIGHT = 16;
		private const double DODGE_FRAME_LENGTH = 0.025;

		private const int IDLE_SPRITE_WIDTH = 16;
		private const int IDLE_SPRITE_HEIGHT = 16;
		private const double IDLE_FRAME_LENGTH = 0.2;

		#endregion

		internal Player(World world, CollisionComponent collisionComponent, Texture2D texture, float scale)
			: base(world, collisionComponent, texture, scale) { }

		internal void Create(ref Vector2 position)
		{
			Entity player = _world.CreateEntity();
			player.Set<PlayerInput>(default);

			player.Set(new Aim
			{
				Value = new Vector2(0, 0)
			});

			player.Set(new Position
			{
				Value = position
			});

			player.Set(new Velocity
			{
				Value = new Vector2(),
				speed = .2f
			});

			player.Set(new RenderInfo
			{
				sprite = _texture,
				bounds = new Rectangle(0, 0, WALK_SPRITE_WIDTH, WALK_SPRITE_HEIGHT),
				color = Color.White,
				flip = false,
				scale = _scale
			});

			// Collision
			CollisionActorEntity actor = new CollisionActorEntity(
				new RectangleF(0, 0, 16 * _scale, 16 * _scale),
					CollisionActorEntity.CollisionType.PlayerCollision,
					ref player
				);
			player.Set(new Collision(actor));
			_collisionComponent.Insert(actor);


			// Abilities
			var abilities = new List<Ability>()
			{
				new DodgeRoll(new KeyCommand(Keys.Space)),
				new Gun(new MouseCommand(MouseButton.LEFT_BUTTON)),
				new Bow(new MouseCommand(MouseButton.RIGHT_BUTTON)),
				new Movement(new KeyCommand(Keys.W), MovementDirection.UP),
				new Movement(new KeyCommand(Keys.A), MovementDirection.LEFT),
				new Movement(new KeyCommand(Keys.S), MovementDirection.DOWN),
				new Movement(new KeyCommand(Keys.D), MovementDirection.RIGHT),
				new Dash(new KeyCommand(Keys.W), MovementDirection.UP),
				new Dash(new KeyCommand(Keys.A), MovementDirection.LEFT),
				new Dash(new KeyCommand(Keys.S), MovementDirection.DOWN),
				new Dash(new KeyCommand(Keys.D), MovementDirection.RIGHT),
				new SpeedBuff(new KeyCommand(Keys.E))
			};

			player.Set(new CombatStats
			{
				currentAbility = abilities[0],
				abilities = abilities
			});

			#region create-animations
			// Animations
			var AnimationTable = new Dictionary<string, Animation>();

			// CREATE ANIMATIONS (should probably make a function that automates this)
			Animation walkDown = new Animation(loop: true);
			walkDown.AddFrame(new Rectangle(0, 16, WALK_SPRITE_WIDTH, WALK_SPRITE_HEIGHT), TimeSpan.FromSeconds(WALK_FRAME_LENGTH));
			walkDown.AddFrame(new Rectangle(WALK_SPRITE_WIDTH, 16, WALK_SPRITE_WIDTH, WALK_SPRITE_HEIGHT), TimeSpan.FromSeconds(WALK_FRAME_LENGTH));
			walkDown.AddFrame(new Rectangle(WALK_SPRITE_WIDTH * 2, 16, WALK_SPRITE_WIDTH, WALK_SPRITE_HEIGHT), TimeSpan.FromSeconds(WALK_FRAME_LENGTH));
			walkDown.AddFrame(new Rectangle(WALK_SPRITE_WIDTH * 3, 16, WALK_SPRITE_WIDTH, WALK_SPRITE_HEIGHT), TimeSpan.FromSeconds(WALK_FRAME_LENGTH));
			walkDown.AddFrame(new Rectangle(WALK_SPRITE_WIDTH * 4, 16, WALK_SPRITE_WIDTH, WALK_SPRITE_HEIGHT), TimeSpan.FromSeconds(WALK_FRAME_LENGTH));
			walkDown.AddFrame(new Rectangle(WALK_SPRITE_WIDTH * 5, 16, WALK_SPRITE_WIDTH, WALK_SPRITE_HEIGHT), TimeSpan.FromSeconds(WALK_FRAME_LENGTH));

			// Dodge Roll Animation
			Animation dodgeRoll = new Animation(DodgeRoll.DURATION);
			//dodgeRoll.AddFrame(new Rectangle(0, 32, DODGE_SPRITE_WIDTH, DODGE_SPRITE_WIDTH), TimeSpan.FromSeconds(DODGE_FRAME_LENGTH));
			//dodgeRoll.AddFrame(new Rectangle(DODGE_SPRITE_WIDTH, 32, DODGE_SPRITE_WIDTH, DODGE_SPRITE_HEIGHT), TimeSpan.FromSeconds(DODGE_FRAME_LENGTH));
			dodgeRoll.AddFrame(new Rectangle(DODGE_SPRITE_WIDTH * 2, 32, DODGE_SPRITE_WIDTH, DODGE_SPRITE_HEIGHT));
			dodgeRoll.AddFrame(new Rectangle(DODGE_SPRITE_WIDTH * 3, 32, DODGE_SPRITE_WIDTH, DODGE_SPRITE_HEIGHT), TimeSpan.FromSeconds(.05f));
			dodgeRoll.AddFrame(new Rectangle(DODGE_SPRITE_WIDTH * 4, 32, DODGE_SPRITE_WIDTH, DODGE_SPRITE_HEIGHT), TimeSpan.FromSeconds(.05f));
			dodgeRoll.AddFrame(new Rectangle(DODGE_SPRITE_WIDTH * 5, 32, DODGE_SPRITE_WIDTH, DODGE_SPRITE_HEIGHT), TimeSpan.FromSeconds(.05f));
			dodgeRoll.AddFrame(new Rectangle(DODGE_SPRITE_WIDTH * 6, 32, DODGE_SPRITE_WIDTH, DODGE_SPRITE_WIDTH), TimeSpan.FromSeconds(DODGE_FRAME_LENGTH));
			dodgeRoll.AddFrame(new Rectangle(DODGE_SPRITE_WIDTH * 7, 32, DODGE_SPRITE_WIDTH, DODGE_SPRITE_HEIGHT), TimeSpan.FromSeconds(DODGE_FRAME_LENGTH));
			//dodgeRoll.AddFrame(new Rectangle(DODGE_SPRITE_WIDTH * 8, 32, DODGE_SPRITE_WIDTH, DODGE_SPRITE_HEIGHT), TimeSpan.FromSeconds(DODGE_FRAME_LENGTH));
			dodgeRoll.AddFrame(new Rectangle(DODGE_SPRITE_WIDTH * 9, 32, DODGE_SPRITE_WIDTH, DODGE_SPRITE_HEIGHT), TimeSpan.FromSeconds(DODGE_FRAME_LENGTH));
			dodgeRoll.AddFrame(new Rectangle(DODGE_SPRITE_WIDTH * 10, 32, DODGE_SPRITE_WIDTH, DODGE_SPRITE_HEIGHT), TimeSpan.FromSeconds(DODGE_FRAME_LENGTH));
			dodgeRoll.AddFrame(new Rectangle(DODGE_SPRITE_WIDTH * 11, 32, DODGE_SPRITE_WIDTH, DODGE_SPRITE_HEIGHT), TimeSpan.FromSeconds(DODGE_FRAME_LENGTH));
			dodgeRoll.AddFrame(new Rectangle(DODGE_SPRITE_WIDTH * 12, 32, DODGE_SPRITE_WIDTH, DODGE_SPRITE_WIDTH), TimeSpan.FromSeconds(DODGE_FRAME_LENGTH));
			dodgeRoll.AddFrame(new Rectangle(DODGE_SPRITE_WIDTH * 13, 32, DODGE_SPRITE_WIDTH, DODGE_SPRITE_HEIGHT), TimeSpan.FromSeconds(DODGE_FRAME_LENGTH));
			//dodgeRoll.AddFrame(new Rectangle(DODGE_SPRITE_WIDTH * 14, 32, DODGE_SPRITE_WIDTH, DODGE_SPRITE_HEIGHT), TimeSpan.FromSeconds(DODGE_FRAME_LENGTH));
			//dodgeRoll.AddFrame(new Rectangle(DODGE_SPRITE_WIDTH * 15, 32, DODGE_SPRITE_WIDTH, DODGE_SPRITE_HEIGHT), TimeSpan.FromSeconds(DODGE_FRAME_LENGTH));

			//Idle Animation
			Animation idle = new Animation(loop: true);
			idle.AddFrame(new Rectangle(0, 0, IDLE_SPRITE_WIDTH, IDLE_SPRITE_HEIGHT), TimeSpan.FromSeconds(IDLE_FRAME_LENGTH));
			idle.AddFrame(new Rectangle(IDLE_SPRITE_WIDTH, 0, IDLE_SPRITE_WIDTH, IDLE_SPRITE_HEIGHT), TimeSpan.FromSeconds(IDLE_FRAME_LENGTH));

			// Add to List
			AnimationTable["walk"] = walkDown;
			AnimationTable["dodge-roll"] = dodgeRoll;
			AnimationTable["idle"] = idle;

			//set animations
			player.Set(new Animate
			{
				AnimationList = AnimationTable,
				currentAnimation = "idle"
			});
			#endregion
		}
	}
}
