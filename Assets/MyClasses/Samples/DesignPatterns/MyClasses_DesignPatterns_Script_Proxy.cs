using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 1006
#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_Proxy : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- Other -----

        public class User
        {
            public string username;
            public string password;
        }

        public interface IUserRepository
        {
            User GetUserByUsername(string username);
        }

        public class UserRepository : IUserRepository
        {
            private readonly Dictionary<string, User> users = new Dictionary<string, User>()
            {
                { "admin", new User { username = "admin", password = "123456" } },
                { "userA", new User { username = "userA", password = "passA" } },
                { "userB", new User { username = "userB", password = "passB" } }
            };

            public User GetUserByUsername(string username)
            {
                if (users.ContainsKey(username))
                {
                    return users[username];
                }

                return null;
            }
        }

        public interface ICacheService
        {
            T Get<T>(string key);
            void Set<T>(string key, T value);
        }

        public class CacheService : ICacheService
        {
            private readonly Dictionary<string, object> cache = new Dictionary<string, object>();

            public T Get<T>(string key)
            {
                if (cache.TryGetValue(key, out object value))
                {
                    return (T)value;
                }

                return default(T);
            }

            public void Set<T>(string key, T value)
            {
                cache[key] = value;
            }
        }

        #endregion

        #region ----- Proxy - Subject -----

        public interface IAuthentication
        {
            bool Login(string username, string password);
        }

        #endregion

        #region ----- Proxy - Real Subject -----

        public class UserAuthentication : IAuthentication
        {
            private readonly IUserRepository userRepository;

            public UserAuthentication(IUserRepository userRepository)
            {
                this.userRepository = userRepository;
            }

            public bool Login(string username, string password)
            {
                User user = userRepository.GetUserByUsername(username);

                if (user != null && user.password == password)
                {
                    return true;
                }

                return false;
            }
        }

        #endregion

        #region ----- Proxy - Proxy -----

        public class AuthenticationProxy : IAuthentication
        {
            private readonly IAuthentication realAuthentication;
            private readonly ICacheService cacheService;

            public AuthenticationProxy(IAuthentication realAuthentication, ICacheService cacheService)
            {
                this.realAuthentication = realAuthentication;
                this.cacheService = cacheService;
            }

            public bool Login(string username, string password)
            {
                string cachedToken = cacheService.Get<string>(username);
                if (cachedToken != null)
                {
                    return true;
                }

                bool loginSuccess = realAuthentication.Login(username, password);
                if (loginSuccess)
                {
                    cacheService.Set(username, _GenerateToken());
                }
                return loginSuccess;
            }

            private string _GenerateToken()
            {
                return Guid.NewGuid().ToString();
            }
        }

        #endregion
        
        #region ----- Implementation -----

        protected override void _Define()
        {
            _name = "Proxy Pattern";
            _complexity = "★★";
            _popularity = "★";
            _defination = "Proxy là mẫu Cấu Trúc cho phép tạo ra một đối tượng thay thế"
                        + "\ncho một đối tượng khác để kiểm soát quyền truy cập hoặc"
                        + "\nthêm chức năng bổ sung.";
            _structure = "- Private Constructor: tránh việc khởi tạo ra 1 thể hiện khác."
                        + "\n- Public Static Instance: cho phép mọi nơi trong chương trình"
                        + "\ncó thể truy cập.";
            _advantages = "- Kiểm soát quyền truy cập: đảm bảo chỉ người dùng hoặc đối tượng được ủy quyền"
                        + "\nmới có thể truy cập vào Real Subject."
                        + "\n- Thêm chức năng: Proxy có thể thêm chức năng mới cho Real Subject"
                        + "\nmà không cần thay đổi bản thân Real Subject."
                        + "\n- Tăng hiệu suất: Proxy có thể cải thiện hiệu suất bằng cách lưu trữ"
                        + "\ntrạng thái của Real Subject."
                        + "\n- Tăng tính an toàn: Proxy có thể bảo vệ Real Subject khỏi các truy cập"
                        + "\nđộc hại hoặc lỗi";
            _disadvantages = "- Việc áp dụng sẽ là quá mức cần thiết nếu số lượng trạng thái ít"
                            + "\nhoặc hiếm khi thay đổi.";
        }

        protected override void _Usage()
        {
            IUserRepository userRepository = new UserRepository();
            IAuthentication authenticationProxy = new AuthenticationProxy(new UserAuthentication(userRepository), new CacheService());
            
            bool isAdminLoginSuccess = authenticationProxy.Login("admin", "123456");
            _textResult.text += "\nuser 'admin' " + (isAdminLoginSuccess ? "has logged in successfully" : "has failed to log in");
            bool isUserALoginSuccess = authenticationProxy.Login("userA", "pass");
            _textResult.text += "\nuser 'userA' " + (isUserALoginSuccess ? "has logged in successfully" : "has failed to log in");
            bool isUserBLoginSuccess = authenticationProxy.Login("userB", "passB");
            _textResult.text += "\nuser 'userB' " + (isUserBLoginSuccess ? "has logged in successfully" : "has failed to log in");
            bool isUserALoginSuccess2 = authenticationProxy.Login("userA", "passA");
            _textResult.text += "\nuser 'userA' " + (isUserALoginSuccess2 ? "has logged in successfully" : "has failed to log in");
        }

        #endregion
    }
}