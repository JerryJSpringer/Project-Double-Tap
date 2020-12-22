using DefaultEcs;
using DefaultEcs.System;
using GameDevIdiotsProject1.DefaultEcs.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GameDevIdiotsProject1.DefaultEcs.Systems
{
	public sealed class DrawSystem : AComponentSystem<float, RenderInfo>
	{
		private readonly SpriteBatch _batch;
		private readonly OrthographicCamera _camera;

		public DrawSystem(World world, SpriteBatch batch, OrthographicCamera camera)
			:base(world)
		{
			_batch = batch;
			_camera = camera;
		}

		protected override void PreUpdate(float delta)
		{
			_batch.Begin(transformMatrix: _camera.GetViewMatrix(), samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.FrontToBack) ;
		}

		protected override void Update(float delta, ref RenderInfo component)
		{
			_batch.Draw(
				component.sprite, 
				component.position, 
				component.bounds, 
				component.color, 
				0f, 
				new Vector2(0,0), 
				component.scale, 
				(component.flip) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 
				component.layerDepth
			);
		}

		protected override void PostUpdate(float delta)
		{
			_batch.End();
		}
	}
}
