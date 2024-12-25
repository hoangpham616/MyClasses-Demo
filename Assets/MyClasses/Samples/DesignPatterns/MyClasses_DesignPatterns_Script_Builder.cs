#pragma warning disable 1006
#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_Builder : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- Builder - Product -----

        public interface ICharacter
        {
            string name { get; }
            string weapon { get; }
            int maxHealth { get; }
            int maxMana { get; }
            int attack { get; }
            int defense { get; }
        }

        public class Character : ICharacter
        {
            public string name { get; private set; }
            public string weapon { get; private set; }
            public int maxHealth { get; private set; }
            public int maxMana { get; private set; }
            public int attack { get; private set; }
            public int defense { get; private set; }

            public Character(string name, string weapon, int maxHealth, int maxMana, int attack, int defense)
            {
                this.name = name;
                this.weapon = weapon;
                this.maxHealth = maxHealth;
                this.maxMana = maxMana;
                this.attack = attack;
                this.defense = defense;
            }
            
            public string Info()
            {
                return string.Format("[name={0}; weapon={1}; maxHealth={2}; maxMana={3}; attack={4}; defense={5}]", name, weapon, maxHealth, maxMana, attack, defense);
            }
        }

        #endregion

        #region ----- Builder - Builder -----

        public interface ICharacterBuilder
        {
            ICharacterBuilder SetName(string name);
            ICharacterBuilder SetWeapon(string weapon);
            ICharacterBuilder SetMaxHeath(int maxHealth);
            ICharacterBuilder SetMaxMana(int maxMana);
            ICharacterBuilder SetAttack(int attack);
            ICharacterBuilder SetDefense(int defense);
            ICharacter Build();
        }

        public class WarriorBuilder : ICharacterBuilder
        {
            private string _name;
            private string _weapon = "axe";
            private int _maxHealth;
            private int _maxMana;
            private int _attack;
            private int _defense;

            public ICharacterBuilder SetName(string name)
            {
                _name = name;
                return this;
            }

            public ICharacterBuilder SetWeapon(string weapon)
            {
                _weapon = weapon;
                return this;
            }

            public ICharacterBuilder SetMaxHeath(int maxHealth)
            {
                _maxHealth = maxHealth;
                return this;
            }

            public ICharacterBuilder SetMaxMana(int maxMana)
            {
                _maxMana = maxMana;
                return this;
            }

            public ICharacterBuilder SetAttack(int attack)
            {
                _attack = attack;
                return this;
            }

            public ICharacterBuilder SetDefense(int defense)
            {
                _defense = defense;
                return this;
            }

            public ICharacter Build()
            {
                return new Character(_name, _weapon, _maxHealth, _maxMana, _attack, _defense);
            }
        }

        #endregion

        #region ----- Implementation -----

        protected override void _Define()
        {
            _name = "Builder Pattern";
            _complexity = "★★";
            _popularity = "★★★";
            _defination = "Builder giúp tách biệt quá trình khởi tạo một đối tượng phức tạp thành"
                        + "\ncác bước nhỏ hơn.";
            _structure = "- Product: đối tượng cuối cùng được tạo ra."
                        + "\n- Builder: lớp chịu trách nhiệm tạo đối tượng."
                        + "\n- Director (optional): lớp chịu trách nhiệm điều phối quá trình xây dựng.";
            _advantages = "- Tính dễ đọc: việc chia nhỏ quá trình khởi tạo giúp code dễ đọc và bảo trì."
                        + "\n- Tính linh hoạt: có thể thay đổi thứ tự hoặc bỏ qua một số bước khi xây dựng.";
            _disadvantages = "- Việc áp dụng sẽ là quá mức cần thiết nếu số lượng trạng thái ít"
                            + "\nhoặc hiếm khi thay đổi.";
        }

        protected override void _Usage()
        {
            Character warrior = new Character("warrior", "axe", 100, 100, 15, 25);
            _textResult.text += "\n" + warrior.Info();

            Character warriorByBuilder = (Character)new WarriorBuilder()
                                                    .SetName("warrior")
                                                    .SetWeapon("great sword")
                                                    .SetAttack(30)
                                                    .SetDefense(10)
                                                    .SetMaxHeath(100)
                                                    .Build();
            _textResult.text += "\n" + warriorByBuilder.Info();
        }

        #endregion
    }
}