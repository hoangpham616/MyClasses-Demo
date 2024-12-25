using UnityEngine;

#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_Prototype : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- Prototype - Interface -----

        public abstract class Object
        {
            public string name;
            public Vector2 position;

            public Object Instantiate()
            {
                return Clone();
            }

            public Object Copy()
            {
                return Clone();
            }

            public abstract Object Clone();
        }

        #endregion

        #region ----- Prototype - Concrete Components -----

        public class Character : Object
        {
            public int health;
            public float speed;

            public override Object Clone()
            {
                Character clone = new Character();
                clone.name = name;
                clone.health = health;
                clone.speed = speed;
                return clone;
            }

            public void PrintInfo()
            {
                Debug.Log($"[name={name}; health={health}; speed={speed}; position=({position.x},{position.y})]");
            }
        }
        
        public class Item : Object
        {
            public string effect;

            public override Object Clone()
            {
                Item clone = new Item();
                clone.name = name;
                clone.effect = effect;
                return clone;
            }

            public void PrintInfo()
            {
                Debug.Log($"[name={name}; effect={effect}; position=({position.x},{position.y})]");
            }
        }

        #endregion
        
        #region ----- Implementation -----

        protected override void _Define()
        {
            _name = "Prototype Pattern";
            _complexity = "★";
            _popularity = "★★";
            _defination = "Prototype là mẫu Sáng Tạo cho phép tạo ra bản sao của các đối tượng"
                        + "\nhiện có mà không phụ thuộc vào lớp của chúng.";
            _structure = "- Interface (optional): interface hoặc abstract class chứa phương thức"
                        + "\ntrừu tượng Clone."
                        + "\n- Concrete Components: lớp cụ thể triển khai phương thức Clone.";
            _advantages = "- Tính linh hoạt: có thể sao chép bất kỳ đối tượng nào mà không"
                        + "\ncần biết chi tiết về lớp của nó."
                        + "\n- Tái sử dụng: có thể sử dụng bản sao được tạo ra để tạo ra các"
                        + "\nđối tượng mới hoặc sửa đổi trạng thái của các đối tượng hiện có."
                        + "\n- Tăng hiệu suất: sao chép đối tượng thường nhanh hơn so với tạo mới."
                        + "\n- Tiết kiệm bộ nhớ: sao chép đối tượng thường tiết kiệm bộ nhớ hơn"
                        + "\nso với việc tạo mới từ đầu, đặc biệt là với các đối tượng phức tạp"
                        + "\ncó nhiều dữ liệu.";
            _disadvantages = "- Việc nhân bản các đối tượng có tham chiếu vòng (circle reference)"
                            + "\ncó thể rất phức tạp.";
        }

        protected override void _Usage()
        {
            Character hero = new Character();
            hero.name = "Hero";
            hero.position = new Vector2(0, 0);
            hero.health = 100;
            hero.speed = 1.5f;
            hero.ToString();

            Character enemy = (Character)hero.Clone();
            enemy.name = "Goblin";
            enemy.position = new Vector3(10, 0, 0);
            enemy.health = 50;
            enemy.ToString();
            
            Item cocaCan = new Item();
            cocaCan.name = "Coca Can";
            cocaCan.effect = "Delicious";
            cocaCan.ToString();

            Item pepsiCan = (Item)cocaCan.Clone();
            pepsiCan.name = "Pepsi Can";
            pepsiCan.ToString();
        }

        #endregion
    }
}