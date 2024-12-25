using System;
using Debug = UnityEngine.Debug;

#pragma warning disable 1006
#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_Adapter : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- Object Adapter - Service (Adaptee) -----

        public class LegacyLine
        {
            public void DrawLine(int x1, int y1, int x2, int y2)
            {
                Debug.Log($"Draw a line with the starting point ({x1}, {y1}), the ending point ({x2}, {y2})");
            }
        }

        public class LegacyRectangle
        {
            public void DrawRectangle(int left, int top, int width, int height)
            {
                Debug.Log($"Draw a rectangle with the left top point ({left}, {top}), width {width}, height {height}");
            }
        }

        #endregion

        #region ----- Object Adapter - Adapter -----

        public interface IShape
        {
            void Draw(int x1, int y1, int x2, int y2);
        }

        public class LineAdapter : IShape
        {
            private LegacyLine _legacyLine;

            public LineAdapter(LegacyLine legacyLine)
            {
                _legacyLine = legacyLine;
            }

            public void Draw(int x1, int y1, int x2, int y2)
            {
                _legacyLine.DrawLine(x1, y1, x2, y2);
            }
        }

        public class RectangleAdapter : IShape
        {
            private LegacyRectangle _legacyRectangle;

            public RectangleAdapter(LegacyRectangle legacyRectangle)
            {
                _legacyRectangle = legacyRectangle;
            }

            public void Draw(int x1, int y1, int x2, int y2)
            {
                int left = Math.Min(x1, x2);
                int top = Math.Min(y1, y2);
                int width = Math.Abs(x1 - x2);
                int height = Math.Abs(y1 - y2);
                _legacyRectangle.DrawRectangle(left, top, width, height);
            }
        }

        #endregion
        
        #region ----- Object Adapter - Client / Implementation -----

        protected override void _Define()
        {
            _name = "Adapter Pattern";
            _complexity = "★";
            _popularity = "★★★";
            _defination = "- Adapter là mẫu Cấu Trúc giúp 2 giao diện không tương thích có thể"
                        + "\nlàm việc với nhau."
                        + "\n- Có 2 cách triển khai là Object Adapter và Apdater Class, trong "
                        + "\nđó Object Adapter tốt hơn vì một Adapter có thể hoạt động được với"
                        + "\nnhiều Adaptee.";
            _structure = "- Object Adapter:"
                        + "\n+ Client: đối tượng muốn sử dụng giao diện của đối tượng khác."
                        + "\n+ Service/Adaptee: đối tượng có giao diện không tương thích"
                        + "\nvới Client."
                        + "\n+ Adapter: lớp trung gian giúp chuyển đổi các yêu cầu từ Client"
                        + "\nsang giao diện của Service và ngược lại.";
            _advantages = "- Nguyên tắc Single Responsibility (SRP): tách interface hoặc"
                        + "\ncác đoạn code chuyển đổi dữ liệu khỏi logic nghiệp vụ chính của"
                        + "\nchương trình."
                        + "\n- Nguyên tắc Open/Closed (OCP): có thể thêm các Adapter mới mà"
                        + "\nkhông ảnh hưởng đến mã hiện tại."
                        + "\n- Giúp các lớp không tương thích có thể làm việc được với nhau.";
            _disadvantages = "- Độ phức tạp tăng lên vì phải thêm các interface và Adapter."
                            + "\n- Đôi khi việc sửa Service cho phù hợp với các thành phần khác"
                            + "\nsẽ đơn giản hơn.";
        }

        protected override void _Usage()
        {
            LegacyLine legacyLine = new LegacyLine();
            legacyLine.DrawLine(0, 0, 10, 10);
            
            LegacyRectangle legacyRectangle = new LegacyRectangle();
            legacyRectangle.DrawRectangle(5, 5, 10, 5);

            LineAdapter lineAdapter = new LineAdapter(legacyLine);
            lineAdapter.Draw(0, 0, 10, 10);
            
            RectangleAdapter rectangleAdapter = new RectangleAdapter(legacyRectangle);
            rectangleAdapter.Draw(5, 5, 15, 10);
        }

        #endregion
    }
}