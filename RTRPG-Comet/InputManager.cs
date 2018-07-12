using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Comet
{
    class InputManager
    {
        static InputManager instance = new InputManager();

        MouseState lastMouse;
        MouseState currentMouse;
        KeyboardState lastKeyboard;
        KeyboardState currentKeyboard;
        GamePadState lastGamepad1;
        GamePadState currentGamepad1;

        public Player[] players;

        Keys key1_alliedCharacter1 = Keys.Up;
        Keys key1_alliedCharacter2 = Keys.Left;
        Keys key1_alliedCharacter3 = Keys.Down;
        Keys key1_alliedCharacter4 = Keys.Right;
        Keys key1_skill1 =           Keys.Q;
        Keys key1_skill2 =           Keys.W;
        Keys key1_skill3 =           Keys.E;
        Keys key1_skill4 =           Keys.R;
        Keys key1_enemyCharacter1 =  Keys.U;
        Keys key1_enemyCharacter2 =  Keys.I;
        Keys key1_enemyCharacter3 =  Keys.O;
        Keys key1_enemyCharacter4 =  Keys.P;
        Keys key1_joinGame =         Keys.Space;

        Buttons button_character1 =     Buttons.A;
        Buttons button_character2 =     Buttons.B;
        Buttons button_character3 =     Buttons.X;
        Buttons button_character4 =     Buttons.Y;
        Buttons button_skill1 =         Buttons.DPadDown;
        Buttons button_skill2 =         Buttons.DPadLeft;
        Buttons button_skill3 =         Buttons.DPadRight;
        Buttons button_skill4 =         Buttons.DPadUp;
        Buttons button_sideModifier =   Buttons.LeftTrigger;
        Buttons button_joinGame =       Buttons.Start;

        public bool alliedCharacter1 { get; set; }
        public bool alliedCharacter2 { get; set; }
        public bool alliedCharacter3 { get; set; }
        public bool alliedCharacter4 { get; set; }
        public bool skill1 { get; set; }
        public bool skill2 { get; set; }
        public bool skill3 { get; set; }
        public bool skill4 { get; set; }
        public bool enemyCharacter1 { get; set; }
        public bool enemyCharacter2 { get; set; }
        public bool enemyCharacter3 { get; set; }
        public bool enemyCharacter4 { get; set; }
        public bool sideModifier { get; set; }

        public InputManager()
        {
            currentMouse = Mouse.GetState();
            currentKeyboard = Keyboard.GetState();
            currentGamepad1 = GamePad.GetState(0);
        }

        public void Initialize()
        {
            players = new Player[6];
            players[0] = new KeyboardPlayer(
                             Keys.Left,
                             Keys.Down,
                             Keys.Right,
                             Keys.Up,
                             Keys.Q,
                             Keys.W,
                             Keys.E,
                             Keys.R,
                             Keys.A,
                             Keys.S,
                             Keys.D,
                             Keys.F,
                             Keys.Space);
            players[1] = new KeyboardPlayer(
                             Keys.NumPad1,
                             Keys.NumPad2,
                             Keys.NumPad3,
                             Keys.NumPad5,
                             Keys.U,
                             Keys.I,
                             Keys.O,
                             Keys.P,
                             Keys.J,
                             Keys.K,
                             Keys.L,
                             Keys.OemSemicolon,
                             Keys.NumPad7);
            players[2] = new GamepadPlayer(
                             Buttons.Y,
                             Buttons.X,
                             Buttons.A,
                             Buttons.B,
                             Buttons.DPadDown,
                             Buttons.DPadLeft,
                             Buttons.DPadRight,
                             Buttons.DPadUp,
                             Buttons.LeftTrigger,
                             Buttons.Start);
        }

        public void Update()
        {
            lastMouse = currentMouse;
            lastKeyboard = currentKeyboard;
            lastGamepad1 = currentGamepad1;

            currentMouse = Mouse.GetState();
            currentKeyboard = Keyboard.GetState();
            currentGamepad1 = GamePad.GetState(0);

            foreach(Player p in players)
            {
                if (p != null)
                    p.Update();
            }
        }

        public Vector2 GetCursorPosition()
        {
            return currentMouse.Position.ToVector2();
        }

        public bool Any()
        {
            if (currentKeyboard.GetPressedKeys().Length > 0 && lastKeyboard.GetPressedKeys().Length == 0)
                return true;
            if (IsInput(button_character1))
                return true;
            return false;
        }

        public bool IsInput(Keys key)
        {
            if (currentKeyboard.IsKeyDown(key) && lastKeyboard.IsKeyUp(key))
                return true;
            return false;
        }

        public bool IsInputDown(Keys key)
        {
            if (currentKeyboard.IsKeyDown(key))
                return true;
            return false;
        }

        public bool IsInput(Buttons button)
        {
            if (currentGamepad1.IsButtonDown(button) && lastGamepad1.IsButtonUp(button))
                return true;
            return false;
        }

        public bool IsInputDown(Buttons button)
        {
            if (currentGamepad1.IsButtonDown(button))
                return true;
            return false;
        }

        public static InputManager GetInstance()
        {
            if (instance == null)
                return new InputManager();
            return instance;
        }
    }
}
