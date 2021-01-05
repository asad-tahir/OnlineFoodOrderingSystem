using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using OnlineFoodOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;

namespace OnlineFoodOrderingSystem.ViewModels
{
    public class PlaceOrderViewModel
    {
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public List<CartItem> Cart { get; set; }

        public static decimal GetTotalAmount(List<CartItem> cart)
        {

            decimal totalPrice = 0;
            foreach(var item in cart)
            {
                totalPrice = totalPrice + (item.Price * item.Qty);
            }

            return Convert.ToDecimal(totalPrice);
        }
        public static string PlaceOrder(PlaceOrderViewModel placeOrderViewModel, string customerId)
        {
            string ApiLoginID = "36Sq2bdWP";
            string ApiTransactionKey = "6W5V46uYd8A6hnjS";

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var creditCard = new creditCardType
            {
                cardNumber = placeOrderViewModel.CardNumber,
                expirationDate = placeOrderViewModel.ExpirationDate
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };
            var TotalAmount = PlaceOrderViewModel.GetTotalAmount(placeOrderViewModel.Cart);
            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authOnlyTransaction.ToString(),    // authorize only
                amount = TotalAmount,
                payment = paymentType
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the controller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            // validate response
            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse.messages != null)
                    {
                        using (var _context = new ApplicationDbContext())
                        {
                            var transaction = new Transaction
                            {
                                Amount = TotalAmount,
                                CustomerId = customerId
                            };
                            _context.Transactions.Add(transaction);
                            _context.SaveChanges();
                            var order = new Order
                            {
                                CustomerId = transaction.CustomerId,
                                OrderDate = transaction.TransactionDate,
                                Status = OrderStatus.Pending,
                                TransactionId = transaction.Id
                            };
                            _context.Orders.Add(order);
                            _context.SaveChanges();
                            foreach (var cartItem in placeOrderViewModel.Cart)
                            {
                                var orderItem = new OrderItem
                                {
                                    ItemId = cartItem.Id,
                                    OrderId = order.Id,
                                    Qty = cartItem.Qty,
                                    Price = Convert.ToDecimal(cartItem.Price * cartItem.Qty)
                                };
                                _context.OrderItems.Add(orderItem);
                                _context.SaveChanges();
                            }

                        }
                        return "Success!";
                        //Console.WriteLine("Successfully created transaction with Transaction ID: " + response.transactionResponse.transId);
                        //Console.WriteLine("Response Code: " + response.transactionResponse.responseCode);
                        //Console.WriteLine("Message Code: " + response.transactionResponse.messages[0].code);
                        //Console.WriteLine("Description: " + response.transactionResponse.messages[0].description);
                        //Console.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
                    }
                    else
                    {
                        //Console.WriteLine("Failed Transaction.");
                        if (response.transactionResponse.errors != null)
                        {
                            return "Error!";
                            //Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                            //Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                        }
                    }
                }
                else
                {
                    //Console.WriteLine("Failed Transaction.");
                    if (response.transactionResponse != null && response.transactionResponse.errors != null)
                    {
                        return "Error!";
                        //Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                        //Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                    }
                    else
                    {
                        return "Error!";
                        //Console.WriteLine("Error Code: " + response.messages.message[0].code);
                        //Console.WriteLine("Error message: " + response.messages.message[0].text);
                    }
                }
            }
            else
            {
                return "Error!";
                //Console.WriteLine("Null Response.");
            }
            return "Error!";
        }
    }
    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public byte Qty { get; set; }
    }
}