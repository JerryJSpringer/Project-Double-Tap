using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.DefaultEcs.Entities;
using GameDevIdiotsProject1.DefaultEcs.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

//TILED IMPORTS
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

using System;

namespace GameDevIdiotsProject1
{
	public class Game1 : Game
	{
		#region Fields 

		private readonly GraphicsDeviceManager _graphics;
		private CollisionComponent _collisionComponent;
		private RenderTarget2D _renderTarget;
		private SpriteBatch _batch;
		private World _world;

		private ISystem<float> _system;

		private const int GAME_SIZE = 500;
		private int renderSize = 500;
		private int xpad = 0;
		private int ypad = 0;

		//Tiled Fields
		private TiledMap _tiledMap;
		private TiledMapRenderer _tiledMapRenderer;

		//Camera
		private OrthographicCamera _camera;

		#endregion

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			IsFixedTimeStep = true;
			TargetElapsedTime = TimeSpan.FromSeconds(1 / 60f);

			Window.AllowUserResizing = true;
			Window.ClientSizeChanged += OnResize;

			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			_graphics.PreferredBackBufferHeight = renderSize;
			_graphics.PreferredBackBufferWidth = renderSize;
			_graphics.ApplyChanges();

			_batch = new SpriteBatch(GraphicsDevice);
			_renderTarget = new RenderTarget2D(GraphicsDevice, GAME_SIZE, GAME_SIZE);

			_world = new World(1000);
			_camera = new OrthographicCamera(GraphicsDevice);

			_system = new SequentialSystem<float>(
				new PlayerSystem(Window, _world),
				new VelocitySystem(_world),
				new PositionSystem(_world),
				new AnimationSystem(_world),
				new CameraSystem(_world, _camera),
				new DrawSystem(_batch, _world, _camera));

			base.Initialize();
		}

		protected override void LoadContent()
		{
			//load tiledmap resources
			_tiledMap = Content.Load<TiledMap>("samplemap");
			_tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

			_collisionComponent = new CollisionComponent(
				new RectangleF(0, 0, _tiledMap.Width, _tiledMap.Height));

			Player.Create(_world, 
				_collisionComponent,
				Content.Load<Texture2D>("gremlin"));
		}

		#region game

		protected override void Update(GameTime gameTime)
		{

			float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

			// Render to RenderTarget
			GraphicsDevice.SetRenderTarget(_renderTarget);
			GraphicsDevice.Clear(Color.White);

			//Update tiled map first so sprites render on top
			_tiledMapRenderer.Update(gameTime);
			_tiledMapRenderer.Draw(_camera.GetViewMatrix());

			//Update Systems so they will draw to the RenderTarget (rather than the screen)
			_system.Update(time);
			

			// Render to back buffer
			GraphicsDevice.SetRenderTarget(null);

			// Set SamplerState to PointClamp so pixels aren't blurry when scaled up
			_batch.Begin(samplerState: SamplerState.PointClamp);

			//Draw rendertarget to screen
			_batch.Draw(_renderTarget, new Rectangle(xpad, ypad, renderSize, renderSize), Color.White);

			_batch.End();

			base.Update(gameTime);
		}

		protected override void Dispose(bool disposing)
		{
			_world.Dispose();
			_system.Dispose();
			_batch.Dispose();
			_graphics.Dispose();
			base.Dispose(disposing);
		}

		#endregion

		#region window

		public void OnResize(Object sender, EventArgs e)
		{
			int x = Window.ClientBounds.Width;
			int y = Window.ClientBounds.Height;

			renderSize = x;
			xpad = 0;
			ypad = 0;

			if (x < y)
			{
				ypad = (y - x) / 2;
			}
			else if (y < x)
			{
				renderSize = y;
				xpad = (x - y) / 2;
			}

			_graphics.PreferredBackBufferHeight = y;
			_graphics.PreferredBackBufferWidth = x;
			_graphics.ApplyChanges();
		}

		#endregion
	}
}
