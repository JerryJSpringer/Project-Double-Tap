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
		private SpriteBatch _batch;
		private World _world;

		private DefaultParallelRunner _runner;
		private ISystem<float> _system;

		private Texture2D _square;

		#endregion

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			_batch = new SpriteBatch(GraphicsDevice);
			_square = Content.Load<Texture2D>("square");

			_world = new World(1000);

			_runner = new DefaultParallelRunner(Environment.ProcessorCount);
			_system = new SequentialSystem<float>(
				new PlayerSystem(Window, _world),
				new VelocitySystem(_world, _runner),
				new PositionSystem(_world, _runner),
				new DrawSystem(_batch, _square, _world));

			_world.Subscribe(this);

			base.Initialize();
		}

		protected override void LoadContent()
		{
			Player.Create(_world);
		}

		#region game

		protected override void Update(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.White);
			_system.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);

			base.Update(gameTime);
		}

		protected override void Dispose(bool disposing)
		{
			_runner.Dispose();
			_world.Dispose();
			_system.Dispose();
			_square.Dispose();
			_batch.Dispose();
			_graphics.Dispose();
			base.Dispose(disposing);
		}

		#endregion
	}
}
