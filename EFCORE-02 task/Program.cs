using System;
using System.Collections.Generic;
using System.Linq;
using EFCORE_02_task.Models;

namespace EFCORE_02_task
{
    internal class Program
    {
        // One shared database context
        static ProjectContext context = new ProjectContext();

        // 0 means nobody is logged in
        static int loggedInUserId = 0;

        static void Main(string[] args)
        {
            bool exitApp = false;

            while (!exitApp)
            {
                Console.WriteLine();
                Console.WriteLine("===== E-Commerce Console App =====");
                Console.WriteLine("1. Register New User");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Add New Category");
                Console.WriteLine("4. Add New Product");
                Console.WriteLine("5. View All Products");
                Console.WriteLine("6. Place an Order");
                Console.WriteLine("7. View My Orders");
                Console.WriteLine("8. View Order Details");
                Console.WriteLine("9. Add a Review for an Order");
                Console.WriteLine("10. View All Reviews for a Product");
                Console.WriteLine("11. Logout");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                int choice;

                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid input. Enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        RegisterUser();
                        break;

                    case 2:
                        Login();
                        break;

                    case 3:
                        AddCategory();
                        break;

                    case 4:
                        AddProduct();
                        break;

                    case 5:
                        ViewAllProducts();
                        break;

                    case 6:
                        PlaceOrder();
                        break;

                    case 7:
                        ViewMyOrders();
                        break;

                    case 8:
                        ViewOrderDetails();
                        break;

                    case 9:
                        AddReview();
                        break;

                    case 10:
                        ViewReviewsForProduct();
                        break;

                    case 11:
                        Logout();
                        break;

                    case 0:
                        exitApp = true;
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        // =====================================================
        // 1. REGISTER NEW USER
        // =====================================================

        static void RegisterUser()
        {
            Console.WriteLine();
            Console.WriteLine("===== Register New User =====");

            Console.Write("Enter user name: ");
            string userName = Console.ReadLine();

            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            bool emailExists = context.users.Any(
                user => user.Email == email
            );

            if (emailExists)
            {
                Console.WriteLine("This email already exists.");
                return;
            }

            User newUser = new User();

            newUser.UserName = userName;
            newUser.Email = email;
            newUser.Password = password;

            context.users.Add(newUser);
            context.SaveChanges();

            Console.WriteLine("User registered successfully.");
            Console.WriteLine("User ID: " + newUser.UserId);
        }

        // =====================================================
        // 2. LOGIN
        // =====================================================

        static void Login()
        {
            Console.WriteLine();
            Console.WriteLine("===== Login =====");

            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            User foundUser = context.users.FirstOrDefault(
                user =>
                    user.Email == email &&
                    user.Password == password
            );

            if (foundUser == null)
            {
                Console.WriteLine("Incorrect email or password.");
                return;
            }

            loggedInUserId = foundUser.UserId;

            Console.WriteLine("Login successful.");
            Console.WriteLine("Welcome " + foundUser.UserName);
        }

        // =====================================================
        // 3. ADD NEW CATEGORY
        // =====================================================

        static void AddCategory()
        {
            Console.WriteLine();
            Console.WriteLine("===== Add New Category =====");

            Console.Write("Enter category name: ");
            string categoryName = Console.ReadLine();

            bool categoryExists = context.categories.Any(
                category =>
                    category.CategoryName == categoryName
            );

            if (categoryExists)
            {
                Console.WriteLine("This category already exists.");
                return;
            }

            Category newCategory = new Category();

            newCategory.CategoryName = categoryName;

            context.categories.Add(newCategory);
            context.SaveChanges();

            Console.WriteLine("Category added successfully.");
            Console.WriteLine(
                "Category ID: " + newCategory.CategoryId
            );
        }

        // =====================================================
        // 4. ADD NEW PRODUCT
        // =====================================================

        static void AddProduct()
        {
            Console.WriteLine();
            Console.WriteLine("===== Add New Product =====");

            List<Category> categoryList =
                context.categories.ToList();

            if (categoryList.Count == 0)
            {
                Console.WriteLine(
                    "There are no categories. Add a category first."
                );

                return;
            }

            Console.WriteLine("Available categories:");

            foreach (Category category in categoryList)
            {
                Console.WriteLine(
                    category.CategoryId +
                    ". " +
                    category.CategoryName
                );
            }

            int categoryId;

            try
            {
                Console.Write("Enter category ID: ");
                categoryId = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid category ID.");
                return;
            }

            Category selectedCategory =
                context.categories.FirstOrDefault(
                    category =>
                        category.CategoryId == categoryId
                );

            if (selectedCategory == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            Console.Write("Enter product name: ");
            string productName = Console.ReadLine();

            double productPrice;

            try
            {
                Console.Write("Enter product price: ");
                productPrice = double.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid product price.");
                return;
            }

            if (productPrice <= 0)
            {
                Console.WriteLine(
                    "Product price must be greater than zero."
                );

                return;
            }

            Product newProduct = new Product();

            newProduct.ProductName = productName;
            newProduct.ProductPrice = productPrice;
            newProduct.CategoryId = categoryId;

            context.products.Add(newProduct);
            context.SaveChanges();

            Console.WriteLine("Product added successfully.");
            Console.WriteLine(
                "Product ID: " + newProduct.ProductId
            );
        }

        // =====================================================
        // 5. VIEW ALL PRODUCTS
        // =====================================================

        static void ViewAllProducts()
        {
            Console.WriteLine();
            Console.WriteLine("===== View All Products =====");

            Console.WriteLine("1. View all products");
            Console.WriteLine("2. Filter products by category");
            Console.Write("Enter choice: ");

            int choice;

            try
            {
                choice = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

            List<Product> productList;

            if (choice == 2)
            {
                List<Category> categoryList =
                    context.categories.ToList();

                if (categoryList.Count == 0)
                {
                    Console.WriteLine("No categories found.");
                    return;
                }

                Console.WriteLine("Available categories:");

                foreach (Category category in categoryList)
                {
                    Console.WriteLine(
                        category.CategoryId +
                        ". " +
                        category.CategoryName
                    );
                }

                int categoryId;

                try
                {
                    Console.Write("Enter category ID: ");
                    categoryId = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid category ID.");
                    return;
                }

                productList = context.products
                    .Where(
                        product =>
                            product.CategoryId == categoryId
                    )
                    .ToList();
            }
            else
            {
                productList = context.products.ToList();
            }

            if (productList.Count == 0)
            {
                Console.WriteLine("No products found.");
                return;
            }

            foreach (Product product in productList)
            {
                Category category =
                    context.categories.FirstOrDefault(
                        item =>
                            item.CategoryId == product.CategoryId
                    );

                Console.WriteLine();
                Console.WriteLine(
                    "Product ID: " + product.ProductId
                );

                Console.WriteLine(
                    "Product name: " + product.ProductName
                );

                Console.WriteLine(
                    "Product price: £" +
                    product.ProductPrice.ToString("F2")
                );

                if (category != null)
                {
                    Console.WriteLine(
                        "Category: " + category.CategoryName
                    );
                }
            }
        }

        // =====================================================
        // 6. PLACE AN ORDER
        // =====================================================

        static void PlaceOrder()
        {
            Console.WriteLine();
            Console.WriteLine("===== Place an Order =====");

            if (loggedInUserId == 0)
            {
                Console.WriteLine(
                    "You must login before placing an order."
                );

                return;
            }

            List<Product> productList =
                context.products.ToList();

            if (productList.Count == 0)
            {
                Console.WriteLine("No products are available.");
                return;
            }

            List<OrderProduct> selectedProducts =
                new List<OrderProduct>();

            bool finishOrder = false;

            while (!finishOrder)
            {
                Console.WriteLine();
                Console.WriteLine("Available products:");

                foreach (Product product in productList)
                {
                    Console.WriteLine(
                        product.ProductId +
                        ". " +
                        product.ProductName +
                        " - £" +
                        product.ProductPrice.ToString("F2")
                    );
                }

                Console.WriteLine("0. Finish order");

                int productId;

                try
                {
                    Console.Write("Enter product ID: ");
                    productId = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid product ID.");
                    continue;
                }

                if (productId == 0)
                {
                    finishOrder = true;
                    continue;
                }

                Product selectedProduct =
                    context.products.FirstOrDefault(
                        product =>
                            product.ProductId == productId
                    );

                if (selectedProduct == null)
                {
                    Console.WriteLine("Product not found.");
                    continue;
                }

                int quantity;

                try
                {
                    Console.Write("Enter quantity: ");
                    quantity = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid quantity.");
                    continue;
                }

                if (quantity <= 0)
                {
                    Console.WriteLine(
                        "Quantity must be greater than zero."
                    );

                    continue;
                }

                OrderProduct existingItem =
                    selectedProducts.FirstOrDefault(
                        item =>
                            item.ProductId == productId
                    );

                if (existingItem != null)
                {
                    existingItem.Quantity =
                        existingItem.Quantity + quantity;

                    Console.WriteLine(
                        "Product quantity updated."
                    );
                }
                else
                {
                    OrderProduct newOrderProduct =
                        new OrderProduct();

                    newOrderProduct.ProductId = productId;
                    newOrderProduct.Quantity = quantity;

                    selectedProducts.Add(newOrderProduct);

                    Console.WriteLine(
                        "Product added to the order."
                    );
                }
            }

            if (selectedProducts.Count == 0)
            {
                Console.WriteLine(
                    "No products were selected."
                );

                return;
            }

            Order newOrder = new Order();

            newOrder.UserId = loggedInUserId;
            newOrder.OrderDated =
                DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            context.orders.Add(newOrder);
            context.SaveChanges();

            foreach (OrderProduct item in selectedProducts)
            {
                item.OrderId = newOrder.OrderId;

                context.ordersProducts.Add(item);
            }

            context.SaveChanges();

            double total = 0;

            foreach (OrderProduct item in selectedProducts)
            {
                Product product =
                    context.products.FirstOrDefault(
                        productItem =>
                            productItem.ProductId ==
                            item.ProductId
                                                            );

                if (product != null)
                {
                    total =
                        total +
                        product.ProductPrice * item.Quantity;
                }
            }

            Console.WriteLine("Order placed successfully.");
            Console.WriteLine(
                "Order ID: " + newOrder.OrderId
            );

            Console.WriteLine(
                "Order total: £" + total.ToString("F2")
            );
        }

        // =====================================================
        // 7. VIEW MY ORDERS
        // =====================================================

        static void ViewMyOrders()
        {
            Console.WriteLine();
            Console.WriteLine("===== My Orders =====");

            if (loggedInUserId == 0)
            {
                Console.WriteLine(
                    "You must login before viewing your orders."
                );

                return;
            }

            List<Order> orderList = context.orders
                .Where(
                    order =>
                        order.UserId == loggedInUserId
                )
                .ToList();

            if (orderList.Count == 0)
            {
                Console.WriteLine("You have no orders.");
                return;
            }

            foreach (Order order in orderList)
            {
                List<OrderProduct> orderProductList =
                    context.ordersProducts
                    .Where(
                        item =>
                            item.OrderId == order.OrderId
                    )
                    .ToList();

                double total = 0;

                foreach (OrderProduct item in orderProductList)
                {
                    Product product =
                        context.products.FirstOrDefault(
                            productItem =>
                                productItem.ProductId ==
                                item.ProductId
                        );

                    if (product != null)
                    {
                        total =
                            total +
                            product.ProductPrice *
                            item.Quantity;
                    }
                }

                Console.WriteLine();
                Console.WriteLine(
                    "Order ID: " + order.OrderId
                );

                Console.WriteLine(
                    "Order date: " + order.OrderDated
                );

                Console.WriteLine(
                    "Order total: £" +
                    total.ToString("F2")
                );
            }
        }

        // =====================================================
        // 8. VIEW ORDER DETAILS
        // =====================================================

        static void ViewOrderDetails()
        {
            Console.WriteLine();
            Console.WriteLine("===== View Order Details =====");

            int orderId;

            try
            {
                Console.Write("Enter order ID: ");
                orderId = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid order ID.");
                return;
            }

            Order selectedOrder =
                context.orders.FirstOrDefault(
                    order => order.OrderId == orderId
                );

            if (selectedOrder == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            List<OrderProduct> orderProductList =
                context.ordersProducts
                .Where(
                    item => item.OrderId == orderId
                )
                .ToList();

            Console.WriteLine();
            Console.WriteLine(
                "Order ID: " + selectedOrder.OrderId
            );

            Console.WriteLine(
                "Order date: " + selectedOrder.OrderDated
            );

            Console.WriteLine("Products:");

            double total = 0;

            foreach (OrderProduct item in orderProductList)
            {
                Product product =
                    context.products.FirstOrDefault(
                        productItem =>
                            productItem.ProductId ==
                            item.ProductId
                    );

                if (product != null)
                {
                    double subtotal =
                        product.ProductPrice * item.Quantity;

                    total = total + subtotal;

                    Console.WriteLine();
                    Console.WriteLine(
                        "Product: " + product.ProductName
                    );

                    Console.WriteLine(
                        "Price: £" +
                        product.ProductPrice.ToString("F2")
                    );

                    Console.WriteLine(
                        "Quantity: " + item.Quantity
                    );

                    Console.WriteLine(
                        "Subtotal: £" +
                        subtotal.ToString("F2")
                    );
                }
            }

            Console.WriteLine();
            Console.WriteLine(
                "Order total: £" + total.ToString("F2")
            );

            Review review =
                context.reviews.FirstOrDefault(
                    reviewItem =>
                        reviewItem.OrderId == orderId
                );

            if (review == null)
            {
                Console.WriteLine(
                    "This order does not have a review."
                );
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(
                    "Rating: " + review.Ratings + "/5"
                );

                Console.WriteLine(
                    "Comment: " + review.Comment
                );
            }
        }

        // =====================================================
        // 9. ADD A REVIEW FOR AN ORDER
        // =====================================================

        static void AddReview()
        {
            Console.WriteLine();
            Console.WriteLine("===== Add Review =====");

            if (loggedInUserId == 0)
            {
                Console.WriteLine(
                    "You must login before adding a review."
                );

                return;
            }

            int orderId;

            try
            {
                Console.Write("Enter order ID: ");
                orderId = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid order ID.");
                return;
            }

            Order selectedOrder =
                context.orders.FirstOrDefault(
                    order => order.OrderId == orderId
                );

            if (selectedOrder == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            if (selectedOrder.UserId != loggedInUserId)
            {
                Console.WriteLine(
                    "This order does not belong to you."
                );

                return;
            }

            Review existingReview =
                context.reviews.FirstOrDefault(
                    review =>
                        review.OrderId == orderId
                );

            if (existingReview != null)
            {
                Console.WriteLine(
                    "This order already has a review."
                );

                return;
            }

            int rating;

            try
            {
                Console.Write("Enter rating from 1 to 5: ");
                rating = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid rating.");
                return;
            }

            if (rating < 1 || rating > 5)
            {
                Console.WriteLine(
                    "Rating must be between 1 and 5."
                );

                return;
            }

            Console.Write("Enter comment: ");
            string comment = Console.ReadLine();

            Review newReview = new Review();

            newReview.Ratings = rating;
            newReview.Comment = comment;
            newReview.OrderId = orderId;

            context.reviews.Add(newReview);
            context.SaveChanges();

            Console.WriteLine("Review added successfully.");
        }

        // =====================================================
        // 10. VIEW REVIEWS FOR A PRODUCT
        // =====================================================

        static void ViewReviewsForProduct()
        {
            Console.WriteLine();
            Console.WriteLine(
                "===== Reviews for a Product ====="
            );

            List<Product> productList =
                context.products.ToList();

            if (productList.Count == 0)
            {
                Console.WriteLine("No products found.");
                return;
            }

            Console.WriteLine("Available products:");

            foreach (Product product in productList)
            {
                Console.WriteLine(
                    product.ProductId +
                    ". " +
                    product.ProductName
                );
            }

            int productId;

            try
            {
                Console.Write("Enter product ID: ");
                productId = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid product ID.");
                return;
            }

            Product selectedProduct =
                context.products.FirstOrDefault(
                    product =>
                        product.ProductId == productId
                );

            if (selectedProduct == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            List<OrderProduct> orderProductList =
                context.ordersProducts
                .Where(
                    item => item.ProductId == productId
                )
                .ToList();

            if (orderProductList.Count == 0)
            {
                Console.WriteLine(
                    "This product has not been ordered."
                );

                return;
            }

            bool orderFound = false;

            foreach (OrderProduct item in orderProductList)
            {
                orderFound = true;

                Review review =
                    context.reviews.FirstOrDefault(
                        reviewItem =>
                            reviewItem.OrderId == item.OrderId
                    );

                Console.WriteLine();
                Console.WriteLine(
                    "Order ID: " + item.OrderId
                );

                if (review == null)
                {
                    Console.WriteLine(
                        "This order has no review."
                    );
                }
                else
                {
                    Console.WriteLine(
                        "Rating: " + review.Ratings + "/5"
                    );

                    Console.WriteLine(
                        "Comment: " + review.Comment
                    );
                }
            }

            if (!orderFound)
            {
                Console.WriteLine(
                    "No orders were found for this product."
                );
            }
        }

        // =====================================================
        // 11. LOGOUT
        // =====================================================

        static void Logout()
        {
            if (loggedInUserId == 0)
            {
                Console.WriteLine(
                    "No user is currently logged in."
                );

                return;
            }

            loggedInUserId = 0;

            Console.WriteLine("Logout successful.");
        }
    }
}