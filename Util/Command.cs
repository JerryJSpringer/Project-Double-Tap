using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameDevIdiotsProject1.Util
{
	public abstract class Command
	{
		private protected static KeyboardState _keystate;
		private protected static MouseState _mouseState;
		private protected static GamePadState _gamePadState;

		public static void Update(KeyboardState keyState, MouseState mouseState, GamePadState gamePadState)
		{
			_keystate = keyState;
			_mouseState = mouseState;
			_gamePadState = gamePadState;
		}

		public abstract bool IsPressed();

		protected internal bool CheckButton(ButtonState button)
		{
			return button == ButtonState.Pressed;
		}
	}

	public class KeyCommand : Command
	{
		private readonly Keys _key;
		public KeyCommand(Keys key)
		{
			_key = key;
		}

		public override bool IsPressed()
		{
			return _keystate.IsKeyDown(_key);
		}
	}

	public class GamePadThumbStickCommad : Command
	{
		private static readonly float DEADZONE = .5f;
		private readonly GamePadThumbStick _thumbStick;
		private Vector2 _direction;

		public GamePadThumbStickCommad(GamePadThumbStick thumbStick)
		{
			_thumbStick = thumbStick;
			_direction = new Vector2();
		}

		public override bool IsPressed()
		{
			return _thumbStick switch
			{
				GamePadThumbStick.LEFT_STICK => CheckStick(_gamePadState.ThumbSticks.Left),
				GamePadThumbStick.RIGHT_STICK => CheckStick(_gamePadState.ThumbSticks.Right),
				_ => false
			};
		}

		public Vector2 GetDirection()
		{
			return _direction;
		}

		private bool CheckStick(Vector2 position)
		{
			bool value = position.Length() > DEADZONE;
			_direction = value ? position : new Vector2();
			return value;
		}
	}

	public enum GamePadThumbStick
	{
		LEFT_STICK,
		RIGHT_STICK
	}

	public class GamePadButtonCommand : Command
	{
		private readonly GamePadButton _button;
		public GamePadButtonCommand(GamePadButton button)
		{
			_button = button;
		}

		public override bool IsPressed()
		{
			return _button switch
			{
				GamePadButton.A => CheckButton(_gamePadState.Buttons.A),
				GamePadButton.B => CheckButton(_gamePadState.Buttons.B),
				GamePadButton.BACK => CheckButton(_gamePadState.Buttons.Back),
				GamePadButton.GUIDE_BUTTON => CheckButton(_gamePadState.Buttons.BigButton),
				GamePadButton.LEFT_SHOULDER => CheckButton(_gamePadState.Buttons.LeftShoulder),
				GamePadButton.LEFT_STICK => CheckButton(_gamePadState.Buttons.LeftStick),
				GamePadButton.RIGHT_SHOULDER => CheckButton(_gamePadState.Buttons.RightShoulder),
				GamePadButton.RIGHT_STICK => CheckButton(_gamePadState.Buttons.RightStick),
				GamePadButton.START => CheckButton(_gamePadState.Buttons.Start),
				GamePadButton.X => CheckButton(_gamePadState.Buttons.X),
				GamePadButton.Y => CheckButton(_gamePadState.Buttons.Y),
				GamePadButton.UP => CheckButton(_gamePadState.DPad.Up),
				GamePadButton.DOWN => CheckButton(_gamePadState.DPad.Down),
				GamePadButton.LEFT => CheckButton(_gamePadState.DPad.Left),
				GamePadButton.RIGHT => CheckButton(_gamePadState.DPad.Right),
				_ => false,
			};
		}
	}

	public enum GamePadButton
	{
		A,
		B,
		BACK,
		GUIDE_BUTTON,
		LEFT_SHOULDER,
		LEFT_STICK,
		RIGHT_SHOULDER,
		RIGHT_STICK,
		START,
		X,
		Y,
		UP,
		DOWN,
		LEFT,
		RIGHT
	}

	public class MouseCommand : Command
	{
		private static int scrollValue;
		private readonly MouseButton _button;
		public MouseCommand(MouseButton button)
		{
			_button = button;
		}

		public override bool IsPressed()
		{
			bool value;
			switch (_button)
			{
				case MouseButton.LEFT_BUTTON:
					return CheckButton(_mouseState.LeftButton);
				case MouseButton.RIGHT_BUTTON:
					return CheckButton(_mouseState.RightButton);
				case MouseButton.MIDDLE_BUTTON:
					return CheckButton(_mouseState.MiddleButton);
				case MouseButton.SCROLL_UP:
					value = scrollValue > _mouseState.ScrollWheelValue;
					scrollValue = _mouseState.ScrollWheelValue;
					return value;
				case MouseButton.SCROLL_DOWN:
					value = scrollValue < _mouseState.ScrollWheelValue;
					scrollValue = _mouseState.ScrollWheelValue;
					return value;
				default:
					return false;
			}
		}
	}

	public enum MouseButton
	{
		LEFT_BUTTON,
		RIGHT_BUTTON,
		MIDDLE_BUTTON,
		SCROLL_UP,
		SCROLL_DOWN
	}

	public enum MovementDirection
	{
		LEFT,
		RIGHT,
		UP,
		DOWN
	}
}
