using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 1006
#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_Iterator : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- Iterator 1 -----

        #region ----- Iterator 1 - Other -----

        public class Profile
        {
            public string Name { get; set; }
            public string Email { get; set; }
        }

        #endregion

        #region ----- Iterator 1 - Iterator -----

        public interface IProfileIterator
        {
            abstract bool HasMore();
            abstract Profile GetNext();
        }

        #endregion

        #region ----- Iterator 1 - Concrete Iterator -----

        public class FacebookIterator : IProfileIterator
        {
            private readonly Facebook _facebook;
            private readonly string _profileId;
            private readonly string _type;

            private int _currentPosition;
            private List<Profile> _cache;

            public FacebookIterator(Facebook facebook, string profileId, string type)
            {
                _facebook = facebook;
                _profileId = profileId;
                _type = type;
                _LazyInit();
            }

            private void _LazyInit()
            {
                if (_cache == null)
                {
                    _cache = _facebook.SocialGraphRequest(_profileId, _type);
                }
            }

            public Profile GetNext()
            {
                if (HasMore())
                {
                    var result = _cache[_currentPosition];
                    _currentPosition++;
                    return result;
                }

                return null;
            }

            public bool HasMore()
            {
                return _currentPosition < _cache.Count;
            }
        }

        #endregion

        #region ----- Iterator 1 - Collection -----

        public interface ISocialNetwork
        {
            IProfileIterator CreateFriendsIterator(string profileId);
            IProfileIterator CreateCoworkersIterator(string profileId);
        }

        #endregion

        #region ----- Iterator 1 - Concrete Collection -----

        public class Facebook : ISocialNetwork
        {
            public IProfileIterator CreateFriendsIterator(string profileId)
            {
                return new FacebookIterator(this, profileId, "friends");
            }

            public IProfileIterator CreateCoworkersIterator(string profileId)
            {
                return new FacebookIterator(this, profileId, "coworkers");
            }

            public List<Profile> SocialGraphRequest(string profileId, string type)
            {
                List<Profile> profiles = new List<Profile>();
                if (type == "coworkers")
                {
                    profiles.Add(new Profile { Name = "Coworker 1", Email = "coworker1@example.com" });
                    profiles.Add(new Profile { Name = "Coworker 3", Email = "coworker3@example.com" });
                    profiles.Add(new Profile { Name = "Coworker 6", Email = "coworker3@example.com" });
                }
                else
                {
                    profiles.Add(new Profile { Name = "Friend 1", Email = "friend1@example.com" });
                    profiles.Add(new Profile { Name = "Friend 2", Email = "friend2@example.com" });
                    profiles.Add(new Profile { Name = "Friend 4", Email = "friend4@example.com" });
                }
                return profiles;
            }
        }

        #endregion

        #endregion

        #region ----- Iterator 2 -----

        #region ----- Iterator 2 - Iterator -----

        public abstract class Iterator2 : IEnumerator
        {
            object IEnumerator.Current => Current();

            public abstract object Current();
            public abstract bool MoveNext(); // HasMore() + GetNext()
            public abstract void Reset();
        }

        #endregion

        #region ----- Iterator 2 - Concrete Iterator -----

        public class AlphabeticalOrderIterator2 : Iterator2
        {
            private WordsCollection2 _collection;
            
            private int _position = -1;
            
            private bool _reverse = false;

            public AlphabeticalOrderIterator2(WordsCollection2 collection, bool reverse = false)
            {
                _collection = collection;
                _reverse = reverse;

                if (reverse)
                {
                    _position = collection.GetItems().Count;
                }
            }
            
            public override object Current()
            {
                return _collection.GetItems()[_position];
            }
            
            public override bool MoveNext()
            {
                int updatedPosition = _position + (_reverse ? -1 : 1);

                if (updatedPosition >= 0 && updatedPosition < _collection.GetItems().Count)
                {
                    _position = updatedPosition;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
            public override void Reset()
            {
                _position = _reverse ? _collection.GetItems().Count - 1 : 0;
            }
        }

        #endregion

        #region ----- Iterator 2 - Collection -----

        public abstract class ICollection2 : IEnumerable
        {
            public abstract IEnumerator GetEnumerator(); // CreateIterator()
        }

        #endregion

        #region ----- Iterator 2 - Concrete Collection -----

        public class WordsCollection2 : ICollection2
        {
            private List<string> _collection = new List<string>();
            private bool _isReverse = false;
            
            public override IEnumerator GetEnumerator()
            {
                return new AlphabeticalOrderIterator2(this, _isReverse);
            }
            
            public void Reverse()
            {
                _isReverse = !_isReverse;
            }
            
            public List<string> GetItems()
            {
                return _collection;
            }
            
            public void AddItem(string item)
            {
                _collection.Add(item);
            }
        }

        #endregion

        #endregion

        #region ----- Implementation -----

        protected override void _Define()
        {
            _name = "Iterator / Cursor Pattern";
            _complexity = "★★";
            _popularity = "★★★";
            _defination = "Iterator cho phép duyệt qua các phần tử của bộ sưu tập (collection)"
                        + "\nmà không để lộ chi tiết bên trong của nó (danh sách, ngăn xếp...).";
            _structure = "- Iterator : interface hoặc abstract class chứa các phương thức để"
                        + "\ntruy xuất tập hợp, thường là HasMore và GetNext."
                        + "\n- Concrete Iterator: hiện thực các phương thức của Iterator, giữ"
                        + "\nchỉ mục (index) để duyệt qua các phần tử."
                        + "\n- Collection: khai báo một hoặc nhiều phương thức để nhận được"
                        + "\ncác Iterator tương thích."
                        + "\n- Concrete Collection: trả về các phiên bản của Concrete Iterator.";
            _advantages = "- Nguyên tắc Single Responsibility (SRP): tách phần duyệt phần tử của"
                        + "\ncác bộ sưu tập thành các đối tượng riêng lẻ."
                        + "\n- Nguyên tắc Open/Closed (OCP): có thể thêm các bộ sưu tập và"
                        + "\nConcrete Iterator mới mà không ảnh hưởng mã đang có."
                        + "\n- Có thể truy cập song song trên cùng một tập hợp vì mỗi Concrete"
                        + "\nIterator có chứa trạng thái riêng của nó."
                        + "\n- Có thể trì hoãn và tiếp tục việc duyệt phần tử nếu muốn.";
            _disadvantages = "- Việc áp dụng sẽ là quá mức cần thiết nếu ứng dụng chỉ sử dụng các"
                            + "\nbộ sưu tập đơn giản."
                            + "\n- Hiệu suất có thể kém hơn so với việc duyệt các phần tử trong bộ"
                            + "\nsưu tập một cách trực tiếp.";
        }

        protected override void _Usage()
        {
            Facebook collection1 = new Facebook();
            IProfileIterator iterator1 = collection1.CreateFriendsIterator("profile1");

            Debug.Log("--------------------");
            Debug.Log("[1. Straight traversal]");
            while (iterator1.HasMore())
            {
                Profile profile = iterator1.GetNext();
                Debug.Log(profile.Name + " " + profile.Email);
            }

            WordsCollection2 collection2 = new WordsCollection2();
            collection2.AddItem("Phạm");
            collection2.AddItem("Minh");
            collection2.AddItem("Hoàng");
            IEnumerator iterator2 = collection2.GetEnumerator();

            Debug.Log("--------------------");
            Debug.Log("[2. Straight traversal]");
            while (iterator2.MoveNext())
            {
                Debug.Log(iterator2.Current);
            }

            Debug.Log("--------------------");
            Debug.Log("[2. Reverse traversal]");
            collection2.Reverse();
            foreach (var element in collection2)
            {
                Debug.Log(element);
            }
        }

        #endregion
    }
}