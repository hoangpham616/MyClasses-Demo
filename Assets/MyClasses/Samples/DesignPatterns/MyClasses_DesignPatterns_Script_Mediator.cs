using Debug = UnityEngine.Debug;

#pragma warning disable 1006
#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_Mediator : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- Mediator - Mediator -----

        public interface IMediator
        {
            void Notify(object sender, string evt);
        }

        #endregion

        #region ----- Mediator - Concrete Mediator -----

        public class ChatDialog : IMediator
        {
            private TextField _textFieldChat;
            private Button _buttonSend;

            public ChatDialog(TextField component1, Button component2)
            {
                _textFieldChat = component1;
                _textFieldChat.SetMediator(this);
                _buttonSend = component2;
                _buttonSend.SetMediator(this);
            } 

            public void Notify(object sender, string evt)
            {
                Debug.Log("ChatDialog reacts on " + evt);

                if (sender == _textFieldChat && evt == "OnTextChanged")
                {
                    _buttonSend.SetEnabled(_textFieldChat.GetText().Length > 0);
                }
                else if (sender == _textFieldChat && evt == "OnTextChanged")
                {
                    if (_textFieldChat.GetText().Length > 0)
                    {
                        string chat = _textFieldChat.GetText();
                        // send chat to server
                        _textFieldChat.ClearText();
                        _buttonSend.SetEnabled(false);
                    }
                }
            }
        }

        #endregion

        #region ----- Mediator - Component -----

        public class BaseComponent
        {
            protected IMediator _mediator;

            public BaseComponent(IMediator mediator = null)
            {
                _mediator = mediator;
            }

            public void SetMediator(IMediator mediator)
            {
                _mediator = mediator;
            }
        }

        public class TextField : BaseComponent
        {
            private string _text = string.Empty;

            public void ClearText()
            {
                Debug.Log("TextField.ClearText()");

                _text = string.Empty;
            }

            public string GetText()
            {
                return _text;
            }

            public void OnTextChanged(string text)
            {
                Debug.Log("TextField.OnTextChanged()");
                
                _text = text;
                _mediator.Notify(this, "OnTextChanged");
            }
        }

        public class Button : BaseComponent
        {
            public void SetEnabled(bool isEnable)
            {
                Debug.Log("Button.SetEnabled(): " + isEnable);
            }

            public void OnClick()
            {
                Debug.Log("Button.OnClick()");

                _mediator.Notify(this, "OnClick");
            }
        }

        #endregion
        
        #region ----- Implementation -----

        protected override void _Define()
        {
            _name = "Mediator Pattern";
            _complexity = "★★";
            _popularity = "★★";
            _defination = "Mediator là mẫu Hành Vi giúp giảm sự ghép nối giữa các thành phần"
                        + "\ncủa chương trình bằng cách làm cho chúng giao tiếp gián tiếp qua"
                        + "\nmột đối tượng trung gian.";
            _structure = "- Components: các lớp chứa logic nghiệp vụ liên quan với nhau,"
                        + "\nmỗi Component đều giữ tham chiếu đến Concrete Mediator quản lý nó."
                        + "- Mediator: interface khai báo các phương thức để giao tiếp với"
                        + "\ncác Component, thường thì chỉ cần một phương thức Notify."
                        + "\n- Concrete Mediator: đóng gói mối quan hệ giữa các Component,"
                        + "\nthường sẽ giữ tham chiếu của tất cả Component mà nó quản lý.";
            _advantages = "- Nguyên tắc Single Responsibility (SRP): trích xuất sự giao tiếp"
                        + "\ngiữa các Component vào một nơi, giúp dễ hiểu và dễ bảo trì hơn."
                        + "\n- Nguyên tắc Open/Closed (OCP): có thể thêm các Concrete Mediator"
                        + "\nmới mà không ảnh hưởng đến mã hiện tại."
                        + "\n- Giảm sự ghép nối giữa các đối tượng của chương trình."
                        + "\n- Có thể tái sử dụng các Component dễ dàng hơn.";
            _disadvantages = "- Qua thời gian thì Mediator có thể trở thành God Object.";
        }

        protected override void _Usage()
        {
            TextField textField = new TextField();
            Button button = new Button();
            new ChatDialog(textField, button);

            textField.OnTextChanged("Hoàng");
            textField.OnTextChanged("Hoàng ơi!");
            button.OnClick();
        }

        #endregion
    }
}