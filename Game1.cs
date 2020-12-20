using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.DefaultEcs.Entities;
using GameDevIdiotsProject1.DefaultEcs.Systems;
using GameDevIdiotsProject1.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;

namespace GameDevIdiotsProject1
{
	public class Game1 : Game
	{
		// Graphics
		private const int GAME_SIZE = 500;
		private readonly GraphicsDeviceManager _graphics;
		private OrthographicCamera _camera;
		private SpriteBatch _batch;

		// Collision
		private CollisionComponent _collisionComponent;

		// World
		private World _world;
		private ISystem<float> _system;

		// Tiled
		private TiledMap _tiledMap;
		private TiledMapRenderer _tiledMapRenderer;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);

			Window.AllowUserResizing = true;

			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			// Set the buffer back width
			_graphics.PreferredBackBufferHeight = GAME_SIZE;
			_graphics.PreferredBackBufferWidth = GAME_SIZE;
			_graphics.ApplyChanges();

			// Init camera factory
			CameraFactory.Init();

			// Update graphics devices
			var viewportAdapter = new BoxingViewportAdapter(Window, _graphics.GraphicsDevice, GAME_SIZE, GAME_SIZE);
			_batch = new SpriteBatch(viewportAdapter.GraphicsDevice);

			//load tiledmap resources
			_tiledMap = Content.Load<TiledMap>("samplemap");
			_tiledMapRenderer = new TiledMapRenderer(viewportAdapter.GraphicsDevice, _tiledMap);
			_camera = new OrthographicCamera(viewportAdapter);

			// Set up world and systems
			_world = new World(1000);

			_system = new SequentialSystem<float>(
					new PlayerSystem(_world),
					new VelocitySystem(_world),
					new PositionSystem(_world),
					new AnimationSystem(_world),
					new CameraSystem(_world, _camera, _tiledMap, viewportAdapter, GAME_SIZE),
					new DrawSystem(_world, _batch, _camera),
					new DebugSystem(_world, _batch, _camera)); 

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_collisionComponent = new CollisionComponent(
				new RectangleF(0, 0, _tiledMap.WidthInPixels, _tiledMap.HeightInPixels));

			PlayerFactory.Init(_world, _collisionComponent, Content.Load<Texture2D>("gamedev"), 2f);
			PlayerFactory.Create(new Vector2());

			EnemyFactory.Init(_world, _collisionComponent, Content.Load<Texture2D>("gamedev"), 4f);
			EnemyFactory.Create(new Vector2(100, 100));

			BulletFactory.Init(_world, _collisionComponent, Content.Load<Texture2D>("gamedev"), 2f);
		}

		protected override void Update(GameTime gameTime)
		{
			// Update graphics device
			_graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

			// Update tilemap renderer
			_tiledMapRenderer.Update(gameTime);
			_tiledMapRenderer.Draw(viewMatrix: _camera.GetViewMatrix());

			// Update ECS systems
			_system.Update((float) gameTime.ElapsedGameTime.TotalMilliseconds);

			// Update collision
			_collisionComponent.Update(gameTime);

			// Base call
			base.Update(gameTime);
		}

		protected override void Dispose(bool disposing)
		{
			// Instance systems
			_world.Dispose();
			_system.Dispose();
			_batch.Dispose();
			_graphics.Dispose();

			// Factories
			PlayerFactory.Dispose();
			EnemyFactory.Dispose();
			BulletFactory.Dispose();

			// Base call
			base.Dispose(disposing);
		}
	}
}
