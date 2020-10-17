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

		#endregion

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			IsFixedTimeStep = true;
			TargetElapsedTime = TimeSpan.FromSeconds(1 / 60f);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			_batch = new SpriteBatch(GraphicsDevice);
			_renderTarget = new RenderTarget2D(GraphicsDevice, 320, 320);

			
			_world = new World(1000);

			
			_runner = new DefaultParallelRunner(Environment.ProcessorCount);
			_system = new SequentialSystem<float>(
				new PlayerSystem(Window, _world),
				new VelocitySystem(_world, _runner),
				new PositionSystem(_world, _runner),
				new DrawSystem(_batch, _world));

			base.Initialize();
		}

		protected override void LoadContent()
		{
			Player.Create(_world, Content.Load<Texture2D>("hero"));
		}

		#region game

		protected override void Update(GameTime gameTime)
		{
			// Render to RenderTarget
			GraphicsDevice.SetRenderTarget(_renderTarget);
			GraphicsDevice.Clear(Color.White);

			_system.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);

			// Render to back buffer
			GraphicsDevice.SetRenderTarget(null);

			_batch.Begin(samplerState: SamplerState.PointClamp);
			_batch.Draw(_renderTarget, 
				new Rectangle(0, 0,
					GraphicsDevice.DisplayMode.Width,
					GraphicsDevice.DisplayMode.Height),
				Color.White);
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
	}
}
