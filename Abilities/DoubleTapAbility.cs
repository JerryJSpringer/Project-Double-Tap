using DefaultEcs;
using GameDevIdiotsProject1.Util;

namespace GameDevIdiotsProject1.Abilities
{
	abstract class DoubleTapAbility : DefaultAbility
	{
		private const float DEFAULT_MAX_WINDOW = 200;
		private readonly float _maxWindow;
		private bool _released;
		
		public DoubleTapAbility(Command command, float maxWindow = DEFAULT_MAX_WINDOW) : base (command)
		{
			_maxWindow = maxWindow;
		}

		public override void Start(in Entity entity)
		{
			if (state == AbilityState.AVAILABLE && currentTime < _maxWindow)
			{
				if (_released)
					Action(in entity);
			} 
			else if (currentTime > _maxWindow)
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
