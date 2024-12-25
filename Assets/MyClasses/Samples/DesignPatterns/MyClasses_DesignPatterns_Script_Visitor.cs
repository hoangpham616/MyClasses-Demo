using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_Visitor : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- Visitor - Element -----

        public interface Shape
        {
            void Draw();
            void Move(int x, int y);
            void Accept(ExportVisitor visitor);
        }

        #endregion

        #region ----- Visitor - Concrete Element -----

        public class Dot : Shape
        {
            public void Draw()
            {
                throw new System.NotImplementedException();
            }

            public void Move(int x, int y)
            {
                throw new System.NotImplementedException();
            }

            public void Accept(ExportVisitor visitor)
            {
                visitor.VisitDot(this);
            }
        }

        public class Circle : Shape
        {
            public void Draw()
            {
                throw new System.NotImplementedException();
            }

            public void Move(int x, int y)
            {
                throw new System.NotImplementedException();
            }

            public void Accept(ExportVisitor visitor)
            {
                visitor.VisitCircle(this);
            }
        }

        public class Rectangle : Shape
        {
            public void Draw()
            {
                throw new System.NotImplementedException();
            }

            public void Move(int x, int y)
            {
                throw new System.NotImplementedException();
            }

            public void Accept(ExportVisitor visitor)
            {
                visitor.VisitRectangle(this);
            }
        }

        public class CompoundShape : Shape
        {
            public void Draw()
            {
                throw new System.NotImplementedException();
            }

            public void Move(int x, int y)
            {
                throw new System.NotImplementedException();
            }

            public void Accept(ExportVisitor visitor)
            {
                visitor.VisitCompoundShape(this);
            }
        }

        #endregion

        #region ----- Visitor - Visitor -----

        public interface ExportVisitor
        {
            void VisitDot(Dot d);
            void VisitCircle(Circle c);
            void VisitRectangle(Rectangle r);
            void VisitCompoundShape(CompoundShape cs);
        }

        #endregion

        #region ----- Visitor - Concrete Visitor -----

        public class XMLExportVisitor : ExportVisitor
        {
            public void VisitDot(Dot d)
            {
                Debug.Log("Export the dot's ID and center coordinates");
            }

            public void VisitCircle(Circle c)
            {
                Debug.Log("Export the circle's ID, center coordinates and radius");
            }

            public void VisitRectangle(Rectangle r)
            {
                Debug.Log("Export the rectangle's ID, left-top coordinates, width and height");
            }

            public void VisitCompoundShape(CompoundShape cs)
            {
                Debug.Log("Export the shape's ID as well as the list of its children's IDs");
            }
        }

        #endregion


        #region ----- Implementation -----

        protected override void _Define()
        {
            _name = "Visitor / Double Dispatch Pattern";
            _complexity = "★★★";
            _popularity = "★";
            _defination = "Visitor là mẫu Hành Vi cho phép định nghĩa các thao tác trên"
                        + "\nmột tập hợp các đối tượng không đồng nhất mà không làm thay đổi"
                        + "\nđịnh nghĩa về lớp của các đối tượng đó.";
            _structure = "- Element Interface: interface khai báo phương thức Accept để"
                        + "\nlàm việc với Visistor."
                        + "\n- Concrete Element: lớp cụ thể triển khai Element."
                        + "\n- Visitor Interface: interface khai báo tập các phương thức"
                        + "\nđể làm việc với phương thức Accept của các Concrete Element."
                        + "\n- Concrete Visitor: lớp cụ thể triển khai Visitor.";
            _advantages = "- Nguyên tắc Single Responsibility (SRP): chuyển tất cả phiên bản"
                        + "\ncủa một hành vi vào cùng một lớp."
                        + "\n- Nguyên tắc Open/Closed (OCP): có thể thêm các Visitor mới mà"
                        + "\nkhông ảnh hưởng đến mã hiện tại."
                        + "\n- Hỗ trợ tốt cho cấu trúc cây phức tạp: cho phép xử lý các"
                        + "\nphần tử trong cấu trúc cây theo nhiều cách khác nhau.";
            _disadvantages = "- Phải cập nhật lại Visitor khi có Element được thêm hoặc xoá."
                            + "\n- Các Visitor có thể thiếu quyền truy cập cần thiết vào các"
                            + "\ntrường riêng tư (private field) và phương thức riêng tư"
                            + "\n(private method) của các Concrete Element.";
        }

        protected override void _Usage()
        {
            List<Shape> shapes = new List<Shape>();
            shapes.Add(new Dot());
            shapes.Add(new Rectangle());
            shapes.Add(new CompoundShape());
            shapes.Add(new Circle());
            _Export(shapes);
        }

        private void _Export(List<Shape> shapes)
        {
            var exportVisitor = new XMLExportVisitor();
            foreach (var shape in shapes)
            {
                shape.Accept(exportVisitor);
            }
        }

        #endregion
    }
}