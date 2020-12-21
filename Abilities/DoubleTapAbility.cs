using DefaultEcs;
using GameDevIdiotsProject1.Util;

namespace GameDevIdiotsProject1.Abilities
{
	abstract class DoubleTapAbility : DefaultAbility
	{
		/// <summary>
		/// Default window for consecutive key presses.
		/// </summary>
		private const float DEFAULT_MAX_WINDOW = 200;
		private const float DEFAULT_MIN_WINDOW = 50;

		/// <summary>
		/// The window between consecutive key presses.
		/// </summary>
		private readonly float _maxWindow;
		private readonly float _minWindow;

		private bool _released;
		
		public DoubleTapAbility(Command command, float maxWindow = DEFAULT_MAX_WINDOW, float minWindow = DEFAULT_MIN_WINDOW) : base (command)
		{
			_maxWindow = maxWindow;
			_minWindow = minWindow;
		}

		public override bool Start(in Entity entity)
		{
			if (state == AbilityState.AVAILABLE && _minWindow < currentTime && currentTime < _maxWindow)
			{
				if (_released)
					Action(in entity);
			}
			else if (currentTime > _maxWindow)
			{
				_released = false;
				currentTime = 0;
			}

			return true;
		}

		protected virtual void Action(in Entity entity)
		{
			currentTime = 0;
			state = AbilityState.STARTING;
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
