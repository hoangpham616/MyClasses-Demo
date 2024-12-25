#pragma warning disable 1006
#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_Bridge : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- Bridge - Abstraction -----

        public interface IAttack
        {
            string Attack();
        }

        public interface IDefend
        {
            string Defend();
        }

        public abstract class CharacterAbstraction : IAttack, IDefend
        {
            protected IAttack _attackBehavior;
            protected IDefend _defendBehavior;

            protected CharacterAbstraction(IAttack attackBehavior, IDefend defendBehavior)
            {
                _attackBehavior = attackBehavior;
                _defendBehavior = defendBehavior;
            }

            public string Attack()
            {
                return _attackBehavior.Attack();
            }

            public string Defend()
            {
                return _defendBehavior.Defend();
            }
        }

        public class WarriorAbstraction : CharacterAbstraction
        {
            public WarriorAbstraction(IAttack attackBehavior, IDefend defendBehavior) : base(attackBehavior, defendBehavior)
            {
            }
        }

        public class MageAbstraction : CharacterAbstraction
        {
            public MageAbstraction(IAttack attackBehavior, IDefend defendBehavior) : base(attackBehavior, defendBehavior)
            {
            }
        }

        public class ArcherAbstraction : CharacterAbstraction
        {
            public ArcherAbstraction(IAttack attackBehavior, IDefend defendBehavior) : base(attackBehavior, defendBehavior)
            {
            }
        }

        #endregion

        #region ----- Bridge - Implementation -----

        public class SwordAttack : IAttack
        {
            public string Attack()
            {
                return "attacking with a sword!";
            }
        }

        public class SpellAttack : IAttack
        {
            public string Attack()
            {
                return "attacking with a magic spell!";
            }
        }

        public class ArrowAttack : IAttack
        {
            public string Attack()
            {
                return "attacking with arrows!";
            }
        }

        public class ShieldDefend : IDefend
        {
            public string Defend()
            {
                return "defending with a shield!";
            }
        }

        public class MagicShieldDefend : IDefend
        {
            public string Defend()
            {
                return "defending with a magic shield!";
            }
        }

        public class DodgeDefend : IDefend
        {
            public string Defend()
            {
                return "dodging attack!";
            }
        }

        #endregion

        #region ------ Bridge - Bridge (optional) -----

        public class WarriorBridge : WarriorAbstraction
        {
            public WarriorBridge() : base(new SwordAttack(), new ShieldDefend())
            {
            }
        }

        public class MageBridge : MageAbstraction
        {
            public MageBridge() : base(new SpellAttack(), new MagicShieldDefend())
            {
            }
        }

        public class ArcherBridge : ArcherAbstraction
        {
            public ArcherBridge() : base(new ArrowAttack(), new DodgeDefend())
            {
            }
        }

        #endregion

        #region ----- Implementation -----

        protected override void _Define()
        {
            _name = "Bridge Pattern";
            _complexity = "★★★";
            _popularity = "★";
            _defination = "Bridge cho phép tách biệt phần khái niệm (abstraction) khỏi"
                        + "\nphần triển khai (implementation) của nó, giúp hai phần này"
                        + "\ncó thể thay đổi độc lập mà không ảnh hưởng đến nhau.";
            _structure = "- Abstraction: định nghĩa các giao diện cơ bản của hệ thống"
                        + "\nmà không phụ thuộc vào chi tiết triển khai."
                        + "\n- Implementation: triển khai cụ thể các chức năng của hệ thống."
                        + "\n- Bridge (optional): tham chiếu đến cả Abstraction và Implementation,"
                        + "\nthực hiện việc kết nối và chuyển đổi các yêu cầu giữa chúng.";
            _advantages = "- Tăng tính linh hoạt: gúp hệ thống dễ dàng thích nghi với những thay đổi"
                        + "\nmới mà không cần phải sửa đổi toàn bộ hệ thống."
                        + "\n- Tái sử dụng: các thành phần Abstraction và Implementation có thể"
                        + "\nđược tái sử dụng cho nhiều dự án khác nhau."
                        + "\n- Dễ bảo trì: việc tách biệt Abstraction và Implementation giúp dễ dàng"
                        + "\nsửa đổi và bảo trì từng phần mà không ảnh hưởng đến phần còn lại.";
            _disadvantages = "- Việc áp dụng sẽ là quá mức cần thiết nếu số lượng trạng thái ít"
                            + "\nhoặc hiếm khi thay đổi.";
        }

        protected override void _Usage()
        {
            WarriorAbstraction warriorAbstraction = new WarriorBridge();
            _textResult.text += "\nWarrior is " + warriorAbstraction.Attack();

            ArcherAbstraction archerAbstraction = new ArcherAbstraction(new ArrowAttack(), new DodgeDefend());
            _textResult.text += "\nArcher is " + archerAbstraction.Attack();

            MageAbstraction mageAbstraction = new MageBridge();
            _textResult.text += "\nMage is " + mageAbstraction.Defend();
        }

        #endregion
    }
}