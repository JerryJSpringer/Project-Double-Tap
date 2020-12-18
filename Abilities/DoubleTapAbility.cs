using DefaultEcs;
using GameDevIdiotsProject1.Util;

namespace GameDevIdiotsProject1.Abilities
{
	abstract class DoubleTapAbility : DefaultAbility
	{
		private const float DEFAULT_MAX_WINDOW = 200;
		private float maxWindow;
		private bool _released;
		
		public DoubleTapAbility(Command command) : base (command)
		{
			maxWindow = DEFAULT_MAX_WINDOW;
		}

		public override void Start(in Entity entity)
		{
			if (state == AbilityState.AVAILABLE && currentTime < maxWindow)
			{
				if (_released)
					Action(in entity);
			} 
			else if (currentTime > maxWindow)
			{
				_released = false;
				currentTime = 0;
			}
		}

		protected virtual void Action(in Entity entity)
		{
			base.Start(in entity);
		}

		public override void Update(float delta, in Entity entity)
		{
			base.Update(delta, entity);
			if (!command.IsPressed())
				_released = true;
		}

		public override void End(in Entity entity)
		{
			base.End(entity);
			_released = false;
		}
	}
}
