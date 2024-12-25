using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_Command : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- Command - Abstract Command -----

        public interface ICommand
        {
            void Execute();
            void Undo();
        }

        #endregion

        #region ----- Command - Concrete Commands -----

        public class AttackCommand : ICommand
        {
            private readonly Player _player;
            private readonly Player _enemy;

            public AttackCommand(Player player, Player enemy)
            {
                _player = player;
                _enemy = enemy;
            }

            public void Execute()
            {
                _player.Attack(_enemy);
            }

            public void Undo()
            {
                _enemy.Heal(_player.attack);
            }
        }

        public class MoveCommand : ICommand
        {
            private readonly Player _player;
            private readonly Vector3 _newPosition;
            private Vector3 _previousPosition;

            public MoveCommand(Player player, Vector2 newPosition)
            {
                _player = player;
                _newPosition = newPosition;
                _previousPosition = _player.position;
            }

            public void Execute()
            {
                _player.MoveTo(_newPosition);
            }

            public void Undo()
            {
                _player.MoveTo(_previousPosition);
            }
        }

        #endregion

        #region ----- Command - Invoker -----

        public class PlayerController
        {
            private Player _player;
            private Stack<ICommand> _stackCommand = new Stack<ICommand>();

            public PlayerController(Player player)
            {
                _player = player;
            }

            public void MoveTo(Vector2 position)
            {
                ICommand command = new MoveCommand(_player, position);
                command.Execute();
                _stackCommand.Push(command);

                Debug.Log(string.Format("{0} move to ({1},{2})", _player.name, position.x , position.y));
            }

            public void Attack(Player enemy)
            {
                ICommand command = new AttackCommand(_player, enemy);
                command.Execute();
                _stackCommand.Push(command);

                Debug.Log(string.Format("{0} attack {1}", _player.name, enemy.name));
            }

            public void Undo()
            {
                if (_stackCommand.Count > 0)
                {
                    ICommand command = _stackCommand.Pop();
                    command.Undo();

                    Debug.Log(string.Format("{0} undo {1}", _player.name, command.GetType().Name));
                }

                Debug.Log(string.Format("{0} can not undo anymore", _player.name));
            }
        }

        #endregion

        #region ----- Command - Receiver -----

        public class Player
        {
            public string name;
            public int health;
            public int attack;
            public Vector2 position;

            public void Attack(Player enemy)
            {
                enemy.health -= attack;
            }

            public void MoveTo(Vector2 newPosition)
            {
                position = newPosition;
            }

            public void Heal(int amount)
            {
                health += amount;
            }

            public void PrintInfo()
            {
                Debug.Log(string.Format("[name={0}; health={1}; attack={2}; position=({3},{4})]", name, health, attack, position.x, position.y));
            }
        }
        
        #endregion

        #region ----- Command - Client / Implementation -----

        protected override void _Define()
        {
            _name = "Command Pattern";
            _complexity = "★";
            _popularity = "★★★";
            _defination = "Command tách biệt yêu cầu (command) khỏi người xử lý (receiver).";
            _structure = "- Abstract Command: interface hoặc abstract class chứa phương thức"
                        + "\ntrừu tượng Execute."
                        + "\n- Concrete Commands: triển khai các hành động cụ thể."
                        + "\n- Invoker: gọi Command và truyền cho Receiver."
                        + "\n- Receiver: xử lý Command được truyển bởi Invoker."
                        + "\n- Client: tạo các Command và Invoker, cấu hình chúng.";
            _advantages = "- Nguyên tắc Single Responsibility (SRP): tách lớp gọi thao tác"
                        + "\nra khỏi lớp hiện thực thao tác."
                        + "\n- Nguyên tắc Open/Closed (OCP): có thể thêm Command mới mà"
                        + "\nkhông ảnh hưởng phần Client."
                        + "\n- Có thể hiện thực hoàn tác (undo) / làm lại (redo)."
                        + "\n- Có thể trì hoãn (delay) việc thực hiện các thao tác."
                        + "\n- Có thể kết hợp (combo) nhiều lệnh với nhau.";
            _disadvantages = "- Mã trở nên phức tạp hơn vì sinh ra thêm phần gửi và phần nhận.";
            _note = "- Ví dụ:"
                    + "\n+ Client là tôi, Command là vẽ hình tam giác,"
                    + "\nInvoker là não, Receiver là đôi bàn tay."
                    + "\n+ Nếu Receiver bàn tay đang mắc cầm ly nước để uống"
                    + "\nthì Command vẽ hình sẽ phải chờ đến lúc Receiver đôi"
                    + "\nbàn tay xử lý xong.";
        }

        protected override void _Usage()
        {
            Player hero = new Player()
            {
                name = "Hero",
                health = 100,
                attack = 20,
                position = Vector2.zero,
            };
            Player goblin = new Player()
            {
                name = "Goblin",
                health = 50,
                attack = 5,
                position = new Vector2(100, 0),
            };
            PlayerController heroController = new PlayerController(hero);

            hero.PrintInfo();
            goblin.PrintInfo();

            heroController.MoveTo(new Vector2(0, 0));
            hero.PrintInfo();
            goblin.PrintInfo();

            heroController.Attack(goblin);
            hero.PrintInfo();
            goblin.PrintInfo();

            heroController.Undo();
            hero.PrintInfo();
            goblin.PrintInfo();

            heroController.Attack(goblin);
            hero.PrintInfo();
            goblin.PrintInfo();

            heroController.Undo();
            hero.PrintInfo();
            goblin.PrintInfo();

            heroController.Undo();
            hero.PrintInfo();
            goblin.PrintInfo();

            heroController.Undo();
            hero.PrintInfo();
            goblin.PrintInfo();
        }

        #endregion
    }
}