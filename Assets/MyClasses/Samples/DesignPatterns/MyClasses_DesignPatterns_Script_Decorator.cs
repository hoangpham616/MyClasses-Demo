using UnityEngine;

#pragma warning disable 1006
#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_Decorator : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- Decorator - Component -----

        public interface ICard
        {
            int GetValue();
            void Play();
        }

        public abstract class Card : ICard
        {
            protected int _value;
        
            public Card(int value)
            {
                _value = value;
            }

            public virtual int GetValue()
            {
                return _value;
            }

            public abstract void Play();
        }

        #endregion

        #region ----- Decorator - Concrete Components -----

        public class AttackCard : Card
        {
            public AttackCard(int value) : base(value)
            {
            }

            public override void Play()
            {
                Debug.Log($"Attack for {GetValue()} points!");
            }
        }

        public class DefendCard : Card
        {
            public DefendCard(int value) : base(value)
            {
            }

            public override void Play()
            {
                Debug.Log($"Defend for {GetValue()} points!");
            }
        }

        #endregion

        #region ----- Decorator - Decorator -----

        public abstract class Decorator : ICard
        {
            private ICard _decoratorCard;
        
            public Decorator(ICard card)
            {
                _decoratorCard = card;
            }

            public virtual int GetValue()
            {
                return _decoratorCard.GetValue();
            }

            public virtual void Play()
            {
                _decoratorCard.Play();
            }
        }

        #endregion

        #region ----- Decorator - Concrete Decorators -----

        public class DoubleDamageDecorator : Decorator
        {
            public DoubleDamageDecorator(ICard card) : base(card)
            {
            }

            public override void Play()
            {
                base.Play();
                Debug.Log($"Double damage applied, dealing another {base.GetValue()} points!");
            }
        }

        public class HealingDecorator : Decorator
        {
            private int _healingAmount;

            public HealingDecorator(ICard card, int healingAmount) : base(card)
            {
                _healingAmount = healingAmount;
            }

            public override void Play()
            {
                base.Play();
                Debug.Log($"Healed for {_healingAmount} points!");
            }
        }

        public class PoisonDecorator : Decorator
        {
            private int _poisonDamage;

            public PoisonDecorator(ICard card, int poisonDamage) : base(card)
            {
                _poisonDamage = poisonDamage;
            }

            public override void Play()
            {
                base.Play();
                Debug.Log($"Poisoned for {_poisonDamage} points!");
            }
        }

        #endregion

        #region ----- Implementation -----

        protected override void _Define()
        {
            _name = "Decorator / Wrapper Pattern";
            _complexity = "★★";
            _popularity = "★★";
            _defination = "Decorator cho phép gắn thêm hành vi mới vào một đối tượng"
                        + "\nbằng cách cho đối tượng đó được bao bọc (wrap) bởi các"
                        + "\nđối tượng có chứa hành vi mới.";
            _structure = "- Component: là interface khai báo các phương thức cơ bản của đối tượng."
                        + "\n- Concrete Components: lớp triển khai các phương thức của Component."
                        + "\n- Decorator: là abstract class dùng để duy trì một tham"
                        + "\nchiếu của đối tượng Component và đồng thời cài đặt các"
                        + "\nphương thức của Component interface."
                        + "\n- Concrete Decorators / Wrappers: lớp hiện thực các phương"
                        + "\nthức của Decorator, đồng thời cài đặt các tính năng mới"
                        + "\ncho Component.";
            _advantages = "- Nguyên tắc Single Responsibility (SRP): chia nhỏ nhiều"
                        + "\nbiến thể hành vi của đối tượng thành nhiều lớp nhỏ hơn."
                        + "\n- Mở rộng hành vi đối tượng mà không cần lớp con (subclass) mới."
                        + "\n- Có thể thêm hoặc xoá tính năng đối tượng lúc runtime."
                        + "\n- Có thể kết hợp nhiều hành vi bằng cách bao bọc đối tượng"
                        + "\nvào nhiều Wrapper.";
            _disadvantages = "- Khó để xoá một Wrapper cụ thể khỏi ngăn xếp (stack)."
                            + "\nKhó để triển khai phương thức của một Wrapper mà không"
                            + "\nphụ thuộc vào thứ tự trong ngăn xếp.";
            _note = "- Chain of Responsibility và Decorator có cấu trúc rất giống nhau,"
                    + "\ncả 2 đều dựa vào đệ quy để truyền việc thực thi qua loạt các đối tượng."
                    + "\n- Khác biệt lớn nhất là:"
                    + "\n+ Chain of Responsibility có thể thực hiện các xử lý độc lập"
                    + "\nvà ngừng chuyển yêu cầu bất cứ lúc nào."
                    + "\n+ Decorator có thể mở rộng hành vi của đối tượng trong khi"
                    + "\nvẫn giữ cho nó nhất quán với giao diện cơ sở và không được"
                    + "\nphép phá vỡ quy trình xử lý yêu cầu.";
        }

        protected override void _Usage()
        {
            ICard attackCard = new AttackCard(5);
            attackCard.Play();

            attackCard = new DoubleDamageDecorator(attackCard);
            attackCard = new PoisonDecorator(attackCard, 1);
            attackCard = new HealingDecorator(attackCard, 3);
            attackCard.Play();
        }

        #endregion
    }
}