#pragma warning disable 1006
#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_AbstractFactory : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- Abstract Factory - Product -----

        public interface IButton
        {
            string Click();
        }

        public interface ILabel
        {
            string Display();
        }

        #endregion

        #region ----- Abstract Factory - Concrete Product -----

        public class WinButton : IButton
        {
            public string Click()
            {
                return "Clicking a Windows button";
            }
        }

        public class MacButton : IButton
        {
            public string Click()
            {
                return "Clicking a Mac button";
            }
        }

        public class WinLabel : ILabel
        {
            public string Display()
            {
                return "Displaying a Windows label";
            }
        }

        public class MacLabel : ILabel
        {
            public string Display()
            {
                return "Displaying a Mac label";
            }
        }

        #endregion

        #region ----- Abstract Factory - Abstract Factory -----

        public abstract class GUIFactory
        {
            public abstract IButton CreateButton();
            public abstract ILabel CreateLabel();
        }

        #endregion

        #region ----- Abstract Factory - Concrete Factory -----

        public class WinGUIFactory : GUIFactory
        {
            public override IButton CreateButton()
            {
                return new WinButton();
            }

            public override ILabel CreateLabel()
            {
                return new WinLabel();
            }
        }

        public class MacGUIFactory : GUIFactory
        {
            public override IButton CreateButton()
            {
                return new MacButton();
            }

            public override ILabel CreateLabel()
            {
                return new MacLabel();
            }
        }

        #endregion
        
        #region ----- Implementation -----

        protected override void _Define()
        {
            _name = "Abstract Factory Pattern";
            _complexity = "★★";
            _popularity = "★★★";
            _defination = "Abstract Factory nhằm tạo ra các tập hợp đối tượng có liên quan"
                                                    + "\ntừ các họ sản phẩm khác nhau.";
            _structure = "- Abstract Product: khuôn mẫu (interface) hoặc lớp trừu tượng"
                        + "\n(abstract class) của tập hợp các đối tượng riêng biệt nhưng có"
                        + "\nliên quan với nhau."
                        + "\n- Concrete Product: các lớp cụ thể hiện thực Abstract Product."
                        + "\n- Abstract Factory: khuôn mẫu (interface) hoặc lớp trừu tượng"
                        + "\n(abstract class) có chứa phương thức để tạo Abstract Product."
                        + "\n- Concrete Factory: các lớp cụ thể hiện thực Abstract Factory.";
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
            GUIFactory factory = new WinGUIFactory();
            IButton button = factory.CreateButton();
            _textResult.text += "\n" + button.Click();

            ILabel label = factory.CreateLabel();
            _textResult.text += "\n" + label.Display();

            factory = new MacGUIFactory();
            button = factory.CreateButton();
            _textResult.text += "\n" + button.Click();

            label = factory.CreateLabel();
            _textResult.text += "\n" + label.Display();
        }

        #endregion
    }
}