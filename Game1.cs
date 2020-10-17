using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using GameDevIdiotsProject1.DefaultEcs.Entities;
using GameDevIdiotsProject1.DefaultEcs.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameDevIdiotsProject1
{
	public class Game1 : Game
	{
		#region Fields 

		private readonly GraphicsDeviceManager _graphics;
		private RenderTarget2D _renderTarget;
		private SpriteBatch _batch;
		private World _world;

		private DefaultParallelRunner _runner;
		private ISystem<float> _system;

		private const int GAME_SIZE = 500;
		private int renderSize = 500;
		private int xpad = 0;
		private int ypad = 0;

		#endregion

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			IsFixedTimeStep = true;
			TargetElapsedTime = TimeSpan.FromSeconds(1 / 60f);

			Window.AllowUserResizing = true;
			Window.ClientSizeChanged += onResize;

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

			
			_runner = new DefaultParallelRunner(Environment.ProcessorCount);
			_system = new SequentialSystem<float>(
				new PlayerSystem(Window, _world),
				new VelocitySystem(_world, _runner),
				new PositionSystem(_world, _runner),
				new AnimationSystem(_world, _runner),
				new DrawSystem(_batch, _world));

			base.Initialize();
		}

		protected override void LoadContent()
		{
			Player.Create(_world, Content.Load<Texture2D>("spritesheet"));
		}

		#region game

		protected override void Update(GameTime gameTime)
		{
			// Render to RenderTarget
			GraphicsDevice.SetRenderTarget(_renderTarget);
			GraphicsDevice.Clear(Color.White);

			//Update Systems so they will draw to the RenderTarget (rather than the screen)
			_system.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);

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
			_runner.Dispose();
			_world.Dispose();
			_system.Dispose();
			_batch.Dispose();
			_graphics.Dispose();
			base.Dispose(disposing);
		}

		#endregion

		#region window

		public void onResize(Object sender, EventArgs e)
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
