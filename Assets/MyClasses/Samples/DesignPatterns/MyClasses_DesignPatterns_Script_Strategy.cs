using System.Collections;
using UnityEngine;

#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_Strategy : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- Strategy - Interface -----

        public interface IMoveable
        {
            void Move();
        }

        public abstract class WeaponBase // : MonoBehaviour
        {
            public abstract void Shoot();
        }

        #endregion

        #region ----- Strategy - Concrete Strategy -----

        public class LinearMoveBehavior : IMoveable
        {
            private Transform _transform;
            private float _speed;

            public LinearMoveBehavior(Transform transform, float speed)
            {
                _transform = transform;
                _speed = speed;
            }

            public void Move()
            {
                _transform.position += _transform.forward * _speed;
            }
        }

        public class AccelerateMoveBehavior : IMoveable
        {
            private Transform _transform;
            private float _accelerateSpeed;
            private float _currentSpeed;

            public AccelerateMoveBehavior(Transform transform, float accelerateSpeed)
            {
                _transform = transform;
                _accelerateSpeed = accelerateSpeed;
                _currentSpeed = 0;
            }

            public void Move()
            {
                _currentSpeed += _accelerateSpeed;
                _transform.position += _transform.forward * _currentSpeed;
            }
        }

        public class Blaster : WeaponBase
        {
            public override void Shoot()
            {
                Debug.Log("Shoot the Blaster - Bang!");

                // spawn projectile
            }
        }

        public class Laucher : WeaponBase
        {
            public override void Shoot()
            {
                Debug.Log("Shoot the Laucher - BOOM!");
            }
        }

        #endregion

        #region ----- Strategy - Context -----


        // [RequireComponent(typeof(Rigidbody))]
        // [RequireComponent(typeof(Collider))]
        public class Projectile // : MonoBehaviour
        {
            private IMoveable _moveBehavior;

            public Projectile(IMoveable newMoveBehavior)
            {
                _moveBehavior = newMoveBehavior;
            }

            void Update()
            {
                _moveBehavior.Move();
            }

            void OnTriggerEnter(Collider other)
            {
                // destroy projectile
            }
        }

        public class WeaponSystem // : MonoBehaviour
        {
            [SerializeField] WeaponBase _weaponSlot1Prefab;
            [SerializeField] WeaponBase _weaponSlot2Prefab;
            [SerializeField] Transform _transformWeaponParent;
            [SerializeField] WeaponBase _currentWeapon;

            void Awake()
            {
                _Equip(_weaponSlot1Prefab);
            }

            void Update()
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    _Equip(_weaponSlot1Prefab);
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _Equip(_weaponSlot2Prefab);
                }
                
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _Shoot();
                }
            }

            public void _Equip(WeaponBase weapon)
            {
                if (_currentWeapon != null)
                {
                    // destroy current weapon
                }
                
                // spawn new weapon
            }

            public void _Shoot()
            {
                _currentWeapon?.Shoot();
            }
        }

        #endregion

        #region ----- Implementation -----

        protected override void _Define()
        {
            _name = "Strategy Pattern";
            _complexity = "★";
            _popularity = "★★★";
            _defination = "Strategy là mẫu Hành Vi cho phép lựa chọn hành vi (thuật toán)"
                        + "\ncủa một tác vụ cụ thể lúc runtime bằng cách tách rời hành vi"
                        + "\n(thuật toán) khỏi đối tượng sử dụng nó.";
            _structure = "- Interface: interface hoặc abstract class chứa phương thức trừu tượng."
                        + "\n- Concrete Strategies: lớp cụ thể triển khai phương thức trừu tượng."
                        + "\n- Context: lớp sử dụng các strategy.";
            _advantages = "- Tính linh hoạt: dễ dàng thay đổi hành vi của một đối tượng"
                        + "\nbằng cách thay đổi lớp strategy của nó."
                        + "\n- Tái sử dụng: lớp strategy có thể tái sử dụng cho nhiều đối tượng"
                        + "\nkhác nhau."
                        + "\n- Khả năng bảo trì: việc tách hành vi của đối tượng khỏi lớp của nó"
                        + "\ngiúp dễ dàng bảo trì và sửa đổi mã hơn.";
            _disadvantages = "- Việc áp dụng sẽ là quá mức cần thiết nếu số lượng trạng thái ít"
                            + "\nhoặc hiếm khi thay đổi.";
        }

        protected override void _Usage()
        {
            _textResult.text = "[Strategy Pattern]";
            _textResult.text += "\n[Difficulty: " + _complexity + "]";
            _textResult.text += "\n[Popularity: " + _popularity + "]";
        }

        #endregion
    }
}