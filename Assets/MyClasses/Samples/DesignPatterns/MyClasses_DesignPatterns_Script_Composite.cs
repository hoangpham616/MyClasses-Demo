using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 1006
#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_Composite : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- Composite - Component -----

        public interface ICharacter
        {
            string Attack(ICharacter target);
            string Defend();
            string Heal(ICharacter target);
        }

        #endregion

        #region ----- Composite - Leaf -----

        public class Hero : ICharacter
        {
            public string Attack(ICharacter target)
            {
                return "Hero is attacking!";
            }

            public string Defend()
            {
                return "Hero is defending!";
            }

            public string Heal(ICharacter target)
            {
                return "Hero is healing!";
            }
        }

        public class Warrior : ICharacter
        {
            public string Attack(ICharacter target)
            {
                return "Warrior is attacking!";
            }

            public string Defend()
            {
                return "Warrior is defending!";
            }

            public string Heal(ICharacter target)
            {
                return string.Empty;
            }
        }

        public class Priest : ICharacter
        {
            public string Attack(ICharacter target)
            {
                return string.Empty;
            }

            public string Defend()
            {
                return "Priest is defending!";
            }

            public string Heal(ICharacter target)
            {
                return "Priest is healing!";
            }
        }

        #endregion

        #region ------ Composite - Composite -----

        public class GroupCharacter : ICharacter
        {
            private List<ICharacter> _members = new List<ICharacter>();

            public void AddMember(ICharacter member)
            {
                _members.Add(member);
            }

            public void RemoveMember(ICharacter member)
            {
                _members.Remove(member);
            }

            public List<ICharacter> GetMembers()
            {
                return _members;
            }

            public string Attack(ICharacter target)
            {
                string result = string.Empty;
                foreach (ICharacter member in _members)
                {
                    if (result.Length > 0)
                    {
                        result += " & ";
                    }
                    result += member.Attack(target);
                }
                return result;
            }

            public string Defend()
            {
                string result = string.Empty;
                foreach (ICharacter member in _members)
                {
                    if (result.Length > 0)
                    {
                        result += " & ";
                    }
                    result += member.Defend();
                }
                return result;
            }

            public string Heal(ICharacter target)
            {
                string result = string.Empty;
                foreach (ICharacter member in _members)
                {
                    if (result.Length > 0)
                    {
                        result += " & ";
                    }
                    result += member.Heal(target);
                }
                return result;
            }
        }

        #endregion

        #region ----- Implementation -----

        protected override void _Define()
        {
            _name = "Composite Pattern";
            _complexity = "★★";
            _popularity = "★★";
            _defination = "Composite (còn gọi là Tổ Hợp) cho phép tạo ra cấu trúc cây phân"
                        + "\ncấp, trong đó các đối tượng ở các cấp độ khác nhau của cây đều"
                        + "\ncó thể được đối xử giống nhau.";
            _structure = "- Component: interface hoặc abstract class chứa phương thức chung"
                        + "\ncho tất cả đối tượng trong cây phân cấp."
                        + "\n- Leaf: đối tượng đơn lẻ hiện thực Component."
                        + "\n- Composite: chứa danh sách nhóm đối tượng con hiện thực Component"
                        + "\ntheo dạng hoạt động tập thể, thường sẽ bổ sung một số phương thức"
                        + "\nđể thêm, xoá, truy cập các đối tượng con.";
            _advantages = "- Tính linh hoạt: cho phép tạo ra các cấu trúc cây phân cấp phức tạp"
                        + "\nmột cách dễ dàng."
                        + "\n- Tính dễ mở rộng: dễ dàng thêm các tính năng mới vào hệ thống mà"
                        + "\nkhông cần thay đổi cấu trúc hiện có của cây phân cấp."
                        + "\n- Tính rõ ràng: việc chia nhỏ hệ thống phức tạp thành các thành phần"
                        + "\nnhỏ hơn giúp dễ hiểu và dễ quản lý hơn.";
            _disadvantages = "- Việc áp dụng sẽ là quá mức cần thiết nếu số lượng trạng thái ít"
                            + "\nhoặc hiếm khi thay đổi.";
        }

        protected override void _Usage()
        {
            GroupCharacter allMember = new GroupCharacter();
            allMember.AddMember(new Hero());
            GroupCharacter group1 = new GroupCharacter();
            group1.AddMember(new Warrior());
            group1.AddMember(new Warrior());
            group1.AddMember(new Priest());
            allMember.AddMember(group1);
            allMember.Attack(null);
            group1.Heal(null);
        }

        #endregion
    }
}