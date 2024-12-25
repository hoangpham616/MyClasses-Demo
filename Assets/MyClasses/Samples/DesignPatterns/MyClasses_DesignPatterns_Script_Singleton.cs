using System.Collections;
using UnityEngine;

#pragma warning disable 1006
#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_Singleton : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- Singleton -----

        public sealed class Singleton
        {
            private Singleton()
            {
            }

            private static Singleton _instance = null;  
            public static Singleton Instance
            {  
                get
                { 
                    if (_instance == null)
                    {  
                        _instance = new Singleton();  
                    }  
                    return _instance;  
                }  
            }

            private int _count = 0;

            public string Count()
            {
                _count += 1;
                return _count.ToString();
            }
        } 

        public sealed class SafetySingleton
        {
            private SafetySingleton()
            {
            }

            private static readonly object _singletonLock = new object();  
            private static SafetySingleton _instance = null;  
            public static SafetySingleton Instance
            {  
                get
                {  
                    lock (_singletonLock)
                    {  
                        if (_instance == null)
                        {  
                            _instance = new SafetySingleton();  
                        }  
                        return _instance;  
                    }  
                }  
            }  
        } 

        #endregion

        #region ----- Implementation -----

        protected override void _Define()
        {
            _name = "Singleton Pattern";
            _complexity = "★";
            _popularity = "★★";
            _defination = "Singleton là mẫu Sáng Tạo đảm bảo chỉ có một thể hiện"
                        + "\n(instance) duy nhất của một lớp nào đó tồn tại trong toàn bộ"
                        + "\nchương trình.";
            _structure = "- Private Constructor: tránh việc khởi tạo ra 1 thể hiện khác."
                        + "\n- Public Static Instance: cho phép mọi nơi trong chương trình"
                        + "\ncó thể truy cập.";
            _advantages = "- Tính nhất quán: việc chỉ có một instance duy nhất đảm bảo tất cả"
                        + "\ncác thao tác trên lớp đó đều sử dụng cùng một dữ liệu."
                        + "\n- Giảm bộ nhớ sử dụng: việc chỉ có một instance duy nhất giúp"
                        + "\ntiết kiệm bộ nhớ, đặc biệt khi lớp đó chứa dữ liệu nặng."
                        + "\n- Tăng hiệu quả truy cập: việc truy cập dữ liệu thông qua một"
                        + "\ninstance duy nhất giúp tăng hiệu quả truy cập, đặc biệt khi"
                        + "\nlớp đó được sử dụng thường xuyên.";
            _disadvantages = "- Vi phạm nguyên tắc Single Responsibility (SRP): giải quyết"
                            + "\nnhiều vấn đề trên cùng một thời điểm."
                            + "\n- Thể thể hiện thiết kế kém (bad design): các thành phần"
                            + "\ncủa chương trình biết quá nhiều về nhau."
                            + "\n- Khó kiểm thử: không thể kế thừa cũng như tạo thêm instance.";
        }

        protected override void _Usage()
        {
            Singleton.Instance.Count();
            Singleton.Instance.Count();
            Singleton.Instance.Count();
            Singleton.Instance.Count();
            Singleton.Instance.Count();
            Singleton.Instance.Count();
            Singleton.Instance.Count();
        }

        #endregion
    }
}