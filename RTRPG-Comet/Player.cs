using Microsoft.Xna.Framework.Input;

namespace Comet
{
    abstract class Player
    {
        protected InputManager manager = InputManager.GetInstance();
        string name;

        public InputStatus inputs;
        public SelectionState selectState = SelectionState.User;

        public abstract void Update();
    }

    class GamepadPlayer : Player
    {
        Buttons input_alliedCharacter1 { get; set; }
        Buttons input_alliedCharacter2 { get; set; }
        Buttons input_alliedCharacter3 { get; set; }
        Buttons input_alliedCharacter4 { get; set; }
        Buttons input_skill1 { get; set; }
        Buttons input_skill2 { get; set; }
        Buttons input_skill3 { get; set; }
        Buttons input_skill4 { get; set; }
        Buttons input_sideModifier { get; set; }
        Buttons input_joinGame { get; set; }

        public GamepadPlayer(
            Buttons button_alliedCharacter1,
            Buttons button_alliedCharacter2,
            Buttons button_alliedCharacter3,
            Buttons button_alliedCharacter4,
            Buttons button_skill1,
            Buttons button_skill2,
            Buttons button_skill3,
            Buttons button_skill4,
            Buttons button_sideModifier,
            Buttons button_joinGame)
        {
            input_alliedCharacter1 = button_alliedCharacter1;
            input_alliedCharacter2 = button_alliedCharacter2;
            input_alliedCharacter3 = button_alliedCharacter3;
            input_alliedCharacter4 = button_alliedCharacter4;
            input_skill1           = button_skill1;
            input_skill2           = button_skill2;
            input_skill3           = button_skill3;
            input_skill4           = button_skill4;
            input_sideModifier     = button_sideModifier;
            input_joinGame =         button_joinGame;
        }

        public override void Update()
        {
            inputs.alliedCharacter1 = manager.IsInput(input_alliedCharacter1) && !manager.IsInputDown(input_sideModifier);
            inputs.alliedCharacter2 = manager.IsInput(input_alliedCharacter2) && !manager.IsInputDown(input_sideModifier);
            inputs.alliedCharacter3 = manager.IsInput(input_alliedCharacter3) && !manager.IsInputDown(input_sideModifier);
            inputs.alliedCharacter4 = manager.IsInput(input_alliedCharacter4) && !manager.IsInputDown(input_sideModifier);
            inputs.skill1 =           manager.IsInput(input_skill1);
            inputs.skill2 =           manager.IsInput(input_skill2);
            inputs.skill3 =           manager.IsInput(input_skill3);
            inputs.skill4 =           manager.IsInput(input_skill4);
            inputs.enemyCharacter1 =  manager.IsInput(input_alliedCharacter1) && manager.IsInputDown(input_sideModifier);
            inputs.enemyCharacter2 =  manager.IsInput(input_alliedCharacter2) && manager.IsInputDown(input_sideModifier);
            inputs.enemyCharacter3 =  manager.IsInput(input_alliedCharacter3) && manager.IsInputDown(input_sideModifier);
            inputs.enemyCharacter4 =  manager.IsInput(input_alliedCharacter4) && manager.IsInputDown(input_sideModifier);
        }
    }

    class KeyboardPlayer : Player
    {
        Keys input_alliedCharacter1 { get; set; }
        Keys input_alliedCharacter2 { get; set; }
        Keys input_alliedCharacter3 { get; set; }
        Keys input_alliedCharacter4 { get; set; }
        Keys input_skill1 { get; set; }
        Keys input_skill2 { get; set; }
        Keys input_skill3 { get; set; }
        Keys input_skill4 { get; set; }
        Keys input_enemyCharacter1 { get; set; }
        Keys input_enemyCharacter2 { get; set; }
        Keys input_enemyCharacter3 { get; set; }
        Keys input_enemyCharacter4 { get; set; }
        Keys input_joinGame { get; set; }

        public KeyboardPlayer(
            Keys keys_alliedCharacter1,
            Keys keys_alliedCharacter2,
            Keys keys_alliedCharacter3,
            Keys keys_alliedCharacter4,
            Keys keys_skill1,
            Keys keys_skill2,
            Keys keys_skill3,
            Keys keys_skill4,
            Keys keys_enemyCharacter1,
            Keys keys_enemyCharacter2,
            Keys keys_enemyCharacter3,
            Keys keys_enemyCharacter4,
            Keys keys_joinGame)
        {
            input_alliedCharacter1 = keys_alliedCharacter1;
            input_alliedCharacter2 = keys_alliedCharacter2;
            input_alliedCharacter3 = keys_alliedCharacter3;
            input_alliedCharacter4 = keys_alliedCharacter4;
            input_skill1 =           keys_skill1;
            input_skill2 =           keys_skill2;
            input_skill3 =           keys_skill3;
            input_skill4 =           keys_skill4;
            input_enemyCharacter1 =  keys_enemyCharacter1;
            input_enemyCharacter2 =  keys_enemyCharacter2;
            input_enemyCharacter3 =  keys_enemyCharacter3;
            input_enemyCharacter4 =  keys_enemyCharacter4;
            input_joinGame =     keys_joinGame;
        }

        public override void Update()
        {
            inputs.alliedCharacter1 = manager.IsInput(input_alliedCharacter1);
            inputs.alliedCharacter2 = manager.IsInput(input_alliedCharacter2);
            inputs.alliedCharacter3 = manager.IsInput(input_alliedCharacter3);
            inputs.alliedCharacter4 = manager.IsInput(input_alliedCharacter4);
            inputs.skill1 = manager.IsInput(input_skill1);
            inputs.skill2 = manager.IsInput(input_skill2);
            inputs.skill3 = manager.IsInput(input_skill3);
            inputs.skill4 = manager.IsInput(input_skill4);
            inputs.enemyCharacter1 = manager.IsInput(input_enemyCharacter1);
            inputs.enemyCharacter2 = manager.IsInput(input_enemyCharacter2);
            inputs.enemyCharacter3 = manager.IsInput(input_enemyCharacter3);
            inputs.enemyCharacter4 = manager.IsInput(input_enemyCharacter4);
            inputs.joinGame = manager.IsInput(input_joinGame);
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public struct InputStatus
    {
        /// <summary> </summary>
        public bool alliedCharacter1 { get; set; }
        /// <summary> </summary>
        public bool alliedCharacter2 { get; set; }
        /// <summary> </summary>
        public bool alliedCharacter3 { get; set; }
        /// <summary> </summary>
        public bool alliedCharacter4 { get; set; }
        /// <summary> </summary>
        public bool skill1 { get; set; }
        /// <summary> </summary>
        public bool skill2 { get; set; }
        /// <summary> </summary>
        public bool skill3 { get; set; }
        /// <summary> </summary>
        public bool skill4 { get; set; }
        /// <summary> </summary>
        public bool enemyCharacter1 { get; set; }
        /// <summary> </summary>
        public bool enemyCharacter2 { get; set; }
        /// <summary> </summary>
        public bool enemyCharacter3 { get; set; }
        /// <summary> </summary>
        public bool enemyCharacter4 { get; set; }
        /// <summary> </summary>
        public bool sideModifier { get; set; }
        /// <summary> </summary>
        public bool joinGame { get; set; }
    }
}
