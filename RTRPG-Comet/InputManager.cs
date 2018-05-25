using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Comet
{
    class InputManager
    {
        static InputManager instance = GetInstance();

        MouseState lastMouse;
        MouseState currentMouse;
        KeyboardState lastKeyboard;
        KeyboardState currentKeyboard;
        GamePadState lastGamepad1;
        GamePadState currentGamepad1;

        Keys key_alliedCharacter1 = Keys.D1;
        Keys key_alliedCharacter2 = Keys.D2;
        Keys key_alliedCharacter3 = Keys.D3;
        Keys key_alliedCharacter4 = Keys.D4;
        Keys key_skill1 = Keys.Q;
        Keys key_skill2 = Keys.W;
        Keys key_skill3 = Keys.E;
        Keys key_skill4 = Keys.R;
        Keys key_enemyCharacter1 = Keys.U;
        Keys key_enemyCharacter2 = Keys.I;
        Keys key_enemyCharacter3 = Keys.O;
        Keys key_enemyCharacter4 = Keys.P;

        Buttons button_character1 = Buttons.A;
        Buttons button_character2 = Buttons.B;
        Buttons button_character3 = Buttons.X;
        Buttons button_character4 = Buttons.Y;
        Buttons button_skill1 = Buttons.DPadDown;
        Buttons button_skill2 = Buttons.DPadLeft;
        Buttons button_skill3 = Buttons.DPadRight;
        Buttons button_skill4 = Buttons.DPadUp;
        Buttons button_sideModifier = Buttons.LeftTrigger;

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

        public InputManager()
        {
            currentMouse = Mouse.GetState();
            currentKeyboard = Keyboard.GetState();
            currentGamepad1 = GamePad.GetState(0);
        }

        public void Update()
        {
            lastMouse = currentMouse;
            lastKeyboard = currentKeyboard;
            lastGamepad1 = currentGamepad1;

            currentMouse = Mouse.GetState();
            currentKeyboard = Keyboard.GetState();
            currentGamepad1 = GamePad.GetState(0);

            alliedCharacter1 = IsKey(key_alliedCharacter1) || IsButton(button_character1);
            alliedCharacter2 = IsKey(key_alliedCharacter2) || IsButton(button_character2);
            alliedCharacter3 = IsKey(key_alliedCharacter3) || IsButton(button_character3);
            alliedCharacter4 = IsKey(key_alliedCharacter4) || IsButton(button_character4);
            skill1 = IsKey(key_skill1) || IsButton(button_skill1);
            skill2 = IsKey(key_skill1) || IsButton(button_skill1);
            skill3 = IsKey(key_skill1) || IsButton(button_skill1);
            skill4 = IsKey(key_skill1) || IsButton(button_skill1);
            enemyCharacter1 = IsKey(key_enemyCharacter1) || (IsButton(button_character1) && IsButton(button_sideModifier));
            enemyCharacter2 = IsKey(key_enemyCharacter1) || (IsButton(button_character1) && IsButton(button_sideModifier));
            enemyCharacter3 = IsKey(key_enemyCharacter1) || (IsButton(button_character1) && IsButton(button_sideModifier));
            enemyCharacter4 = IsKey(key_enemyCharacter1) || (IsButton(button_character1) && IsButton(button_sideModifier));
        }

        public Vector2 GetCursorPosition()
        {
            return currentMouse.Position.ToVector2();
        }

        public bool AnyKeyPress()
        {
            if (currentKeyboard.GetPressedKeys().Length > 0 && lastKeyboard.GetPressedKeys().Length == 0)
                return true;
            return false;
        }

        bool IsKey(Keys key)
        {
            if (currentKeyboard.IsKeyDown(key) && lastKeyboard.IsKeyUp(key))
                return true;
            return false;
        }

        bool IsKeyDown(Keys key)
        {
            if (currentKeyboard.IsKeyDown(key))
                return true;
            return false;
        }

        bool IsButton(Buttons button)
        {
            if (currentGamepad1.IsButtonDown(button) && lastGamepad1.IsButtonUp(button))
                return true;
            return false;
        }

        bool IsButtonDown(Buttons button)
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
