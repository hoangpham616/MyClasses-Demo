using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_Facade : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- Base Class -----

        public class Product
        {
        }

        public class CartItem
        {
        }

        public class Customer
        {
        }

        #endregion

        #region ----- Facade - Subclasses -----

        public class ProductService
        {
            public List<Product> GetProducts()
            {
                // Lấy danh sách sản phẩm từ cơ sở dữ liệu
                return null;
            }

            public Product GetProductDetails(int productId)
            {
                // Lấy chi tiết sản phẩm từ cơ sở dữ liệu
                return null;
            }
        }

        public class ShoppingCartService
        {
            private List<CartItem> _cartItems;

            public ShoppingCartService()
            {
                _cartItems = new List<CartItem>();
            }

            public void AddProductToCart(int productId, int quantity)
            {
                // Thêm sản phẩm vào giỏ hàng
            }

            public List<CartItem> GetCartItems()
            {
                // Lấy danh sách sản phẩm trong giỏ hàng
                return _cartItems;
            }

            public decimal GetCartTotal()
            {
                // Tính toán tổng số tiền trong giỏ hàng
                return 0;
            }
        }

        public class OrderService
        {
            private ShoppingCartService _shoppingCartService;

            public OrderService(ShoppingCartService shoppingCartService)
            {
                _shoppingCartService = shoppingCartService;
            }

            public void PlaceOrder(Customer customer)
            {
                // Tạo đơn hàng
                // Lưu đơn hàng vào cơ sở dữ liệu
                // Gửi email xác nhận cho khách hàng
            }
        }

        public class CustomerService
        {
            public Customer GetCustomerDetails(int customerId)
            {
                // Lấy chi tiết khách hàng từ cơ sở dữ liệu
                return null;
            }
        }

        #endregion

        #region ----- Facade - Facade -----

        public class OrderFacade
        {
            private ProductService _productService;
            private ShoppingCartService _shoppingCartService;
            private OrderService _orderService;
            private CustomerService _customerService;

            public OrderFacade(ProductService productService, ShoppingCartService shoppingCartService, OrderService orderService, CustomerService customerService)
            {
                _productService = productService;
                _shoppingCartService = shoppingCartService;
                _orderService = orderService;
                _customerService = customerService;
            }

            public string PlaceOrder(int customerId, int productId, int quantity)
            {
                // Lấy chi tiết sản phẩm
                Product product = _productService.GetProductDetails(productId);

                // Thêm sản phẩm vào giỏ hàng
                _shoppingCartService.AddProductToCart(productId, quantity);

                // Lấy chi tiết khách hàng
                Customer customer = _customerService.GetCustomerDetails(customerId);

                // Tạo đơn hàng
                // Lưu đơn hàng vào cơ sở dữ liệu
                // Gửi email xác nhận cho khách hàng
                _orderService.PlaceOrder(customer);

                return "The order has been placed";
            }
        }

        #endregion

        #region ----- Implementation -----

        protected override void _Define()
        {
            _name = "Facade Pattern";
            _complexity = "★";
            _popularity = "★★";
            _defination = "Facade cung cấp một giao diện đơn giản để sử dụng cho một"
                        + "\ntập hợp các giao diện phức tạp, giúp việc truy cập"
                        + "\nvà sử dụng các thành phần của hệ thống dễ dàng hơn.";
            _structure = "- Subclasses: các thành phần riêng lẻ của các hệ thống con"
                        + "\nmà Facade sẽ tương tác."
                        + "\n- Facade: cung cấp các giao diện đơn giản cho người dùng.";
            _advantages = "- Giảm độ phức tạp: đơn giản hóa việc sử dụng hệ thống bằng cách"
                        + "\nche giấu đi các chi tiết phức tạp bên trong."
                        + "\n- Tăng tính linh hoạt: việc thay đổi cách thức hoạt động"
                        + "\nbên trong hệ thống có thể được thực hiện mà không ảnh hưởng"
                        + "\nđến giao diện người dùng.";
            _disadvantages = "- Việc áp dụng sẽ là quá mức cần thiết nếu số lượng trạng thái ít"
                            + "\nhoặc hiếm khi thay đổi.";
        }

        protected override void _Usage()
        {
            ProductService productService = new ProductService();
            ShoppingCartService shoppingCartService = new ShoppingCartService();
            OrderService orderService = new OrderService(shoppingCartService);
            CustomerService customerService = new CustomerService();

            OrderFacade orderFacade = new OrderFacade(productService, shoppingCartService, orderService, customerService);
            int customerId = 123;
            int productId = 456;
            int quantity = 7;
            _textResult.text += "\n" + orderFacade.PlaceOrder(customerId, productId, quantity);
        }

        #endregion
    }
}