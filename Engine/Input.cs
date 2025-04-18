using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace GraphicalEngine
{
    public enum KeyCode
    {
        A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
        ArrowUp, ArrowDown, ArrowLeft, ArrowRight,
        Space, Enter, Escape, Shift, Control, Alt, Tilde, Tab, Backspace, Delete,
        Pause, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12,
        Num0, Num1, Num2, Num3, Num4, Num5, Num6, Num7, Num8, Num9,
        Numpad0, Numpad1, Numpad2, Numpad3, Numpad4, Numpad5, Numpad6, Numpad7, Numpad8, Numpad9,
        NumpadAdd, NumpadSubtract, NumpadMultiply, NumpadDivide, NumpadEnter,
        LShift, RShift, LControl, RControl, LAlt, RAlt, LSystem, RSystem, Menu,
        LBracket, RBracket, Semicolon, Comma, Period, Quote, Slash, Backslash, Grave,
        Equals, Hyphen
    }

    public enum MouseButton
    {
        Left, Right, Middle, XButton1, XButton2
    }

    public static class Input
    {
        private static RenderWindow? window;
        private static Dictionary<KeyCode, bool> keyStates = new();
        private static Dictionary<MouseButton, bool> mouseStates = new();
        private static Vector2i mousePosition;

        internal static void Initialize(RenderWindow renderWindow)
        {
            window = renderWindow;

            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                keyStates[keyCode] = false;
            }

            foreach (MouseButton mouseButton in Enum.GetValues(typeof(MouseButton)))
            {
                mouseStates[mouseButton] = false;
            }
        }

        internal static void HandleInputs()
        {
            if (window == null) return;

            try
            {
                mousePosition = Mouse.GetPosition(window);
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    Keyboard.Key? sfmlKey = TryToSFMLKey(keyCode);
                    if (sfmlKey != null)
                    {
                        keyStates[keyCode] = Keyboard.IsKeyPressed(sfmlKey.Value);
                    }
                }


                foreach (MouseButton mouseButton in Enum.GetValues(typeof(MouseButton)))
                {
                    mouseStates[mouseButton] = Mouse.IsButtonPressed(ToSFMLMouseButton(mouseButton));
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("[Input]: " + ex);
            }
        }

        public static Vector2 GetMousePosition()
        {
            return V2Convert.FromSFML(mousePosition).OnlyPositive();
        }

        public static bool IsKeyDown(KeyCode keyCode)
        {
            return keyStates[keyCode];
        }

        public static bool IsMouseButtonDown(MouseButton mouseButton)
        {
            return mouseStates[mouseButton];
        }

        public static KeyCode? GetAnyKeyDown()
        {
            if (window == null) return null;

            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                try
                {
                    if (Keyboard.IsKeyPressed(ToSFMLKey(keyCode)))
                    {
                        return keyCode;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Debug.LogError("[INPUT] GetAnyKeyDown() issued an ArgumentOutOfRangeException.");
                }
            }

            return null;
        }

        public static MouseButton? GetAnyMouseButtonDown()
        {
            if (window == null) return null;

            foreach (MouseButton mouseButton in Enum.GetValues(typeof(MouseButton)))
            {
                if (Mouse.IsButtonPressed(ToSFMLMouseButton(mouseButton)))
                {
                    return mouseButton;
                }
            }

            return null;
        }

        private static Keyboard.Key? TryToSFMLKey(KeyCode keyCode)
        {
            try
            {
                return ToSFMLKey(keyCode);
            }
            catch (ArgumentOutOfRangeException)
            {
                Debug.LogError($"[INPUT] Unmapped KeyCode: {keyCode}");
                return null;
            }
        }

        private static Keyboard.Key ToSFMLKey(KeyCode keyCode)
        {
            return keyCode switch
            {
                KeyCode.A => Keyboard.Key.A,
                KeyCode.B => Keyboard.Key.B,
                KeyCode.C => Keyboard.Key.C,
                KeyCode.D => Keyboard.Key.D,
                KeyCode.E => Keyboard.Key.E,
                KeyCode.F => Keyboard.Key.F,
                KeyCode.G => Keyboard.Key.G,
                KeyCode.H => Keyboard.Key.H,
                KeyCode.I => Keyboard.Key.I,
                KeyCode.J => Keyboard.Key.J,
                KeyCode.K => Keyboard.Key.K,
                KeyCode.L => Keyboard.Key.L,
                KeyCode.M => Keyboard.Key.M,
                KeyCode.N => Keyboard.Key.N,
                KeyCode.O => Keyboard.Key.O,
                KeyCode.P => Keyboard.Key.P,
                KeyCode.Q => Keyboard.Key.Q,
                KeyCode.R => Keyboard.Key.R,
                KeyCode.S => Keyboard.Key.S,
                KeyCode.T => Keyboard.Key.T,
                KeyCode.U => Keyboard.Key.U,
                KeyCode.V => Keyboard.Key.V,
                KeyCode.W => Keyboard.Key.W,
                KeyCode.X => Keyboard.Key.X,
                KeyCode.Y => Keyboard.Key.Y,
                KeyCode.Z => Keyboard.Key.Z,
                KeyCode.ArrowUp => Keyboard.Key.Up,
                KeyCode.ArrowDown => Keyboard.Key.Down,
                KeyCode.ArrowLeft => Keyboard.Key.Left,
                KeyCode.ArrowRight => Keyboard.Key.Right,
                KeyCode.Space => Keyboard.Key.Space,
                KeyCode.Enter => Keyboard.Key.Enter,
                KeyCode.Escape => Keyboard.Key.Escape,
                KeyCode.Shift => Keyboard.Key.LShift,
                KeyCode.Control => Keyboard.Key.LControl,
                KeyCode.Alt => Keyboard.Key.LAlt,
                KeyCode.Tilde => Keyboard.Key.Grave,
                KeyCode.Tab => Keyboard.Key.Tab,
                KeyCode.Backspace => Keyboard.Key.Backspace,
                KeyCode.Delete => Keyboard.Key.Delete,
                KeyCode.Pause => Keyboard.Key.Pause,
                KeyCode.F1 => Keyboard.Key.F1,
                KeyCode.F2 => Keyboard.Key.F2,
                KeyCode.F3 => Keyboard.Key.F3,
                KeyCode.F4 => Keyboard.Key.F4,
                KeyCode.F5 => Keyboard.Key.F5,
                KeyCode.F6 => Keyboard.Key.F6,
                KeyCode.F7 => Keyboard.Key.F7,
                KeyCode.F8 => Keyboard.Key.F8,
                KeyCode.F9 => Keyboard.Key.F9,
                KeyCode.F10 => Keyboard.Key.F10,
                KeyCode.F11 => Keyboard.Key.F11,
                KeyCode.F12 => Keyboard.Key.F12,
                KeyCode.Num0 => Keyboard.Key.Num0,
                KeyCode.Num1 => Keyboard.Key.Num1,
                KeyCode.Num2 => Keyboard.Key.Num2,
                KeyCode.Num3 => Keyboard.Key.Num3,
                KeyCode.Num4 => Keyboard.Key.Num4,
                KeyCode.Num5 => Keyboard.Key.Num5,
                KeyCode.Num6 => Keyboard.Key.Num6,
                KeyCode.Num7 => Keyboard.Key.Num7,
                KeyCode.Num8 => Keyboard.Key.Num8,
                KeyCode.Num9 => Keyboard.Key.Num9,
                KeyCode.Numpad0 => Keyboard.Key.Numpad0,
                KeyCode.Numpad1 => Keyboard.Key.Numpad1,
                KeyCode.Numpad2 => Keyboard.Key.Numpad2,
                KeyCode.Numpad3 => Keyboard.Key.Numpad3,
                KeyCode.Numpad4 => Keyboard.Key.Numpad4,
                KeyCode.Numpad5 => Keyboard.Key.Numpad5,
                KeyCode.Numpad6 => Keyboard.Key.Numpad6,
                KeyCode.Numpad7 => Keyboard.Key.Numpad7,
                KeyCode.Numpad8 => Keyboard.Key.Numpad8,
                KeyCode.Numpad9 => Keyboard.Key.Numpad9,
                KeyCode.LShift => Keyboard.Key.LShift,
                KeyCode.RShift => Keyboard.Key.RShift,
                KeyCode.LControl => Keyboard.Key.LControl,
                KeyCode.RControl => Keyboard.Key.RControl,
                KeyCode.LAlt => Keyboard.Key.LAlt,
                KeyCode.RAlt => Keyboard.Key.RAlt,
                KeyCode.LSystem => Keyboard.Key.LSystem,
                KeyCode.RSystem => Keyboard.Key.RSystem,
                KeyCode.Menu => Keyboard.Key.Menu,
                KeyCode.LBracket => Keyboard.Key.LBracket,
                KeyCode.RBracket => Keyboard.Key.RBracket,
                KeyCode.Semicolon => Keyboard.Key.Semicolon,
                KeyCode.Comma => Keyboard.Key.Comma,
                KeyCode.Period => Keyboard.Key.Period,
                KeyCode.Slash => Keyboard.Key.Slash,
                KeyCode.Backslash => Keyboard.Key.Backslash,
                KeyCode.Grave => Keyboard.Key.Grave,
                KeyCode.Equals => Keyboard.Key.Equal,
                KeyCode.Hyphen => Keyboard.Key.Hyphen,
                KeyCode.NumpadAdd => Keyboard.Key.Add,
                KeyCode.NumpadSubtract => Keyboard.Key.Subtract,
                KeyCode.NumpadMultiply => Keyboard.Key.Multiply,
                KeyCode.NumpadDivide => Keyboard.Key.Divide,
                KeyCode.NumpadEnter => Keyboard.Key.Enter,
                KeyCode.Quote => Keyboard.Key.Apostrophe,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static Mouse.Button ToSFMLMouseButton(MouseButton mouseButton)
        {
            return mouseButton switch
            {
                MouseButton.Left => Mouse.Button.Left,
                MouseButton.Right => Mouse.Button.Right,
                MouseButton.Middle => Mouse.Button.Middle,
                MouseButton.XButton1 => Mouse.Button.XButton1,
                MouseButton.XButton2 => Mouse.Button.XButton2,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
