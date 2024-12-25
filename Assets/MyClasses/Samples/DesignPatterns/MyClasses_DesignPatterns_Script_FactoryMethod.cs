using System;

#pragma warning disable 1006
#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_FactoryMethod : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- Factory Method - Product -----

        public interface IShape
        {
            string Draw();
        }

        #endregion

        #region ----- Factory Method - Concrete Product -----

        public class Circle : IShape
        {
            public string Draw()
            {
                return "Drawing a circle";
            }
        }

        public class Square : IShape
        {
            public string Draw()
            {
                return "Drawing a square";
            }
        }

        #endregion

        #region ----- Factory Method - Creator -----

        public class ShapeFactory
        {
            public static IShape Create(string shape)
            {
                if (shape == "Circle")
                {
                    return new Circle();
                }
                else if (shape == "Square")
                {
                    return new Square();
                }
                else
                {
                    throw new ArgumentException("Invalid shape type: " + shape);
                }
            }
        }

        #endregion
        
        #region ----- Adapter - Client / Implementation -----

        protected override void _Define()
        {
            _name = "Factory Method Pattern";
            _complexity = "★";
            _popularity = "★★★";
            _defination = "Factory Method cung cấp phương thức tĩnh (static method) để tạo"
                        + "\nđối tượng mà không cần chỉ ra lớp chính xác.";
            _structure = "- Product: khuôn mẫu (interface) của các đối tượng sẽ được tạo."
                        + "\n- Concrete Product: các lớp hiện thực Product."
                        + "\n- Creator: lớp chứa phương thức Create để sinh ra các đối tượng.";
            _advantages = "- Tính linh hoạt: việc tạo đối tượng trở nên nhanh và dễ dàng hơn,"
                        + "\nkhi cần thay đổi cách tạo sẽ không ảnh hưởng đến những nơi khác."
                        + "\n- Tái sử dụng: phương thức Create của Creator có thể được gọi"
                        + "\nở nhiều nơi trong dự án."
                        + "\n- Dễ bảo trì: việc sửa đổi và bảo trì dễ dàng hơn vì mã đã được"
                        + "\ntập trung ở một chỗ.";
            _disadvantages = "- Việc áp dụng sẽ là quá mức cần thiết nếu số lượng trạng thái ít"
                            + "\nhoặc hiếm khi thay đổi.";
        }

        protected override void _Usage()
        {
            IShape circle = ShapeFactory.Create("Circle");
            _textResult.text += "\n" + circle.Draw();

            IShape square = ShapeFactory.Create("Square");
            _textResult.text += "\n" + square.Draw();
        }

        #endregion
    }
}