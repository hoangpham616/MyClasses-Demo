using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 1006
#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_ChainOfResponsibility : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- Chain of Responsibility - Handler -----

        public interface IHandler
        {
            IHandler SetNext(IHandler handler);
            
            string Handle(string request);
        }

        #endregion

        #region ----- Chain of Responsibility - Base Handler -----

        public abstract class AbstractHandler : IHandler
        {
            private IHandler _nextHandler;

            public IHandler SetNext(IHandler handler)
            {
                _nextHandler = handler;
                return handler;
            }
            
            public virtual string Handle(string request)
            {
                if (_nextHandler != null)
                {
                    return _nextHandler.Handle(request);
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region ----- Chain of Responsibility - Concrete Handlers -----

        public class MonkeyHandler : AbstractHandler
        {
            public override string Handle(string request)
            {
                if ((request as string) == "Banana")
                {
                    return $"Monkey: I'll eat the {request.ToString()}.\n";
                }
                else
                {
                    return base.Handle(request);
                }
            }
        }

        public class SquirrelHandler : AbstractHandler
        {
            public override string Handle(string request)
            {
                if (request.ToString() == "Nut")
                {
                    return $"Squirrel: I'll eat the {request.ToString()}.\n";
                }
                else
                {
                    return base.Handle(request);
                }
            }
        }

        public class DogHandler : AbstractHandler
        {
            public override string Handle(string request)
            {
                if (request.ToString() == "MeatBall")
                {
                    return $"Dog: I'll eat the {request.ToString()}.\n";
                }
                else
                {
                    return base.Handle(request);
                }
            }
        }

        #endregion

        #region ----- Implementation -----

        protected override void _Define()
        {
            _name = "Chain of Responsibility Pattern";
            _complexity = "★★";
            _popularity = "★★";
            _defination = "Chain of Responsibility cho phép yêu cầu được truyền vào"
                        + "\ncác trình xử lý (handler) theo dạng chuỗi (chain) cho đến"
                        + "\nkhi yêu cầu được xử lý.";
            _structure = "- Handler: là interface khai báo phương thức SetNext và Handle."
                        + "\n- Base Handler (optional): là abstract class cài đặt các"
                        + "\nphương thức của Handler."
                        + "\n- Concrete Handlers: lớp xử lý yêu cầu, trong trường hợp"
                        + "\nkhông xử lý được sẽ chuyển sang cho Concrete Handler kế.";
            _advantages = "- Nguyên tắc Single Responsibility (SRP): việc xử lý yêu cầu"
                        + "\nđược chia nhỏ ra thành nhiều trình xử lý."
                        + "\n- Nguyên tắc Open/Closed (OCP): có thể thêm các"
                        + "\nConcrete Handler mà không ảnh hưởng mã đang có."
                        + "\n- Có thể điều khiển được thứ tự của các trình xử lý.";
            _disadvantages = "- Một số yêu cầu có thể không được xử lý vì không có trình xử lý"
                            + "\nnào xử lý được.";
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
            MonkeyHandler monkeyHandler = new MonkeyHandler();
            SquirrelHandler squirrelHandler = new SquirrelHandler();
            DogHandler dogHandler = new DogHandler();

            monkeyHandler.SetNext(squirrelHandler).SetNext(dogHandler);

            Debug.Log("--------------------");
            Debug.Log("[Chain: Monkey > Squirrel > Dog]");
            _ClientRequest(monkeyHandler);

            Debug.Log("--------------------");
            Debug.Log("[Subchain: Squirrel > Dog]");
            _ClientRequest(squirrelHandler);
        }

        private void _ClientRequest(AbstractHandler handler)
        {
            List<string> foods = new List<string>() { "Nut", "Banana", "Cup of coffee" };
            foreach (var food in foods)
            {
                Debug.Log($"Client: Who wants a {food}?");
                string result = handler.Handle(food);
                Debug.Log(result != null ? $" -> {result}" : $" -> {food} was left untouched.");
            }
        }

        #endregion
    }
}