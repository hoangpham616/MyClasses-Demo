using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_Flyweight : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- Flyweight - Flyweight -----

        public class TreeType
        {
            public string name; // intrinsic
            public Color color; // intrinsic
            public Sprite sprite; // intrinsic
        }

        #endregion

        #region ----- Flyweight - Flyweight Factory -----

        public class TreeTypeManager
        {
            private Dictionary<string, TreeType> _dictionaryTreeType = new Dictionary<string, TreeType>();

            public TreeTypeManager(params TreeType[] args)
            {
                foreach (var item in args)
                {
                    _dictionaryTreeType[item.name] = item;
                }
            }

            public TreeType GetTreeType(string name)
            {
                if (_dictionaryTreeType.ContainsKey(name))
                {
                    return _dictionaryTreeType[name];
                }

                return null;
            }
        }

        #endregion

        #region ----- State - Context -----

        public class Tree
        {
            public Vector3 position; // extrinsic
            public TreeType type;

            public void PrintInfo()
            {
                Debug.Log($"{type.name} is located at ({position.x}, {position.y}, {position.z})");
            }
        }

        #endregion

        #region ----- Implementation -----

        protected override void _Define()
        {
            _name = "Flyweight Pattern";
            _complexity = "★★★";
            _popularity = "★";
            _defination = "Flyweight giúp tiết RAM bằng cách chia sẻ các phần trạng thái chung"
                        + "\ncho nhiều đối tượng.";
            _structure = "- Flyweight: lớp chứa các trạng thái có thể chia sẻ cho nhiều đối tượng,"
                        + "\ncác trạng thái này gọi là nội tại (intrinsic)."
                        + "\n- Flyweight Factory: chứa một nhóm các Flyweight, khi nơi nào đó yêu"
                        + "\ncầu thì Factory sẽ trả về Flyweight theo tiêu chí phù hợp hoặc tạo"
                        + "\nmới nếu cần."
                        + "\n- Context: lớp chứa các trạng thái độc nhất của đối tượng, các trạng"
                        + "\nthái này còn gọi là ngoại tại (extrinsic)";
            _advantages = "- Đối tượng trong chương trình càng nhiều thì sẽ càng tiết kiệm RAM.";
            _disadvantages = "- Đánh đổi về mặt sử dụng CPU khi các Flyweight bị truy cập nhiều lần."
                            + "\n- Mã trở nên phức tạp hơn.";
            _note = "- Trong Unity Flyweight thường sẽ dùng ScriptableObject.";
        }

        protected override void _Usage()
        {
            TreeTypeManager treeTypeManager = new TreeTypeManager(
                new TreeType { name = "Apple", color = Color.red, sprite = null },
                new TreeType { name = "Mango", color = Color.yellow, sprite = null },
                new TreeType { name = "Blueberry", color = Color.blue, sprite = null }
            );

            Tree tree1 = new Tree() { position = new Vector3(Random.Range(1, 10), 0, Random.Range(1, 10)), type = treeTypeManager.GetTreeType("Apple") };
            tree1.PrintInfo();
            Tree tree2 = new Tree() { position = new Vector3(Random.Range(1, 10), 0, Random.Range(1, 10)), type = treeTypeManager.GetTreeType("Blueberry") };
            tree2.PrintInfo();
            Tree tree3 = new Tree() { position = new Vector3(Random.Range(1, 10), 0, Random.Range(1, 10)), type = treeTypeManager.GetTreeType("Mango") };
            tree3.PrintInfo();
            Tree tree4 = new Tree() { position = new Vector3(Random.Range(1, 10), 0, Random.Range(1, 10)), type = treeTypeManager.GetTreeType("Blueberry") };
            tree4.PrintInfo();
            Tree tree5 = new Tree() { position = new Vector3(Random.Range(1, 10), 0, Random.Range(1, 10)), type = treeTypeManager.GetTreeType("Blueberry") };
            tree5.PrintInfo();
            Tree tree6 = new Tree() { position = new Vector3(Random.Range(1, 10), 0, Random.Range(1, 10)), type = treeTypeManager.GetTreeType("Apple") };
            tree6.PrintInfo();
        }

        #endregion
    }
}