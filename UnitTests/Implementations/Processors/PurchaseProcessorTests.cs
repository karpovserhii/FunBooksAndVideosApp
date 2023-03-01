using Models;
using Models.Enums;
using Models.Implementations.Processors;
using Models.Interfaces.Services;
using Moq;
using NUnit.Framework;
using System;

namespace UnitTests.Implementations.Processors
{
	[TestFixture]
	public class PurchaseProcessorTests
	{
		private Mock<IMembershipActivationService> _membershipActivationServiceMock;
		private Mock<IShippingService> _shippingServiceMock;
		private PurchaseProcessor _processor;

		[SetUp]
		public void SetUp()
		{
			_membershipActivationServiceMock = new Mock<IMembershipActivationService>();
			_shippingServiceMock = new Mock<IShippingService>();
			_processor = new PurchaseProcessor(_membershipActivationServiceMock.Object, _shippingServiceMock.Object);
		}

		[Test]
		public void ProcessPurchase_WithNullPurchase_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => _processor.ProcessPurchase(null));
		}

		[Test]
		public void ProcessPurchase_WithNoOrders_ThrowsInvalidOperationException()
		{
			var purchase = new Purchase
			{
				Orders = new List<OrderPosition>()
			};
			Assert.Throws<ArgumentException>(() => _processor.ProcessPurchase(purchase));
		}

		[Test]
		public void ProcessPurchase_WithVideoClubMembershipActivations_AddsMembershipActivationStep()
		{
			var purchase = new Purchase
			{
				CustomerId = -1,
				Orders = new List<OrderPosition>
			{
				new OrderPosition { OrderItem = new OrderItem { OrderItemType = OrderItemType.VideoClubMembership, ExpirationDate = DateTime.Now.Date } }
			}
			};
			_membershipActivationServiceMock.Setup(s => s.ActivateMembership(purchase)).Returns(new MembershipStatus() { VideoClubMembershiExpirationDate = DateTime.Now.Date });

			var result = _processor.ProcessPurchase(purchase);

			Assert.IsTrue(result.Success);
			Assert.IsInstanceOf<MembershipActivationStep>(_processor.RootStep.Steps.First());
			Assert.IsTrue(result.MembershipStatus.VideoClubMembershiExpirationDate == purchase.Orders.First().OrderItem.ExpirationDate);
		}


		[Test]
		public void ProcessPurchase_WithBookClubMembershipActivations_AddsMembershipActivationStep()
		{
			var purchase = new Purchase
			{
				CustomerId = -1,
				Orders = new List<OrderPosition>
			{
				new OrderPosition { OrderItem = new OrderItem { OrderItemType = OrderItemType.BookClubMembeship,  ExpirationDate = DateTime.Now.Date }  }
			}
			};
			_membershipActivationServiceMock.Setup(s => s.ActivateMembership(purchase)).Returns(new MembershipStatus() { BookClubMembershiExpirationDate = DateTime.Now.Date });

			var result = _processor.ProcessPurchase(purchase);

			Assert.IsTrue(result.Success);
			Assert.IsInstanceOf<MembershipActivationStep>(_processor.RootStep.Steps.First());
			Assert.IsTrue(result.MembershipStatus.BookClubMembershiExpirationDate == purchase.Orders.First().OrderItem.ExpirationDate);
		}

		[Test]
		public void ProcessPurchase_WithPremiumMembershipActivations_AddsMembershipActivationStep()
		{
			var purchase = new Purchase
			{

				CustomerId = -1,
				Orders = new List<OrderPosition>
			{
				new OrderPosition { OrderItem = new OrderItem { OrderItemType = OrderItemType.Premium , ExpirationDate = DateTime.Now.Date} }
			}
			};
			_membershipActivationServiceMock.Setup(s => s.ActivateMembership(purchase)).Returns(new MembershipStatus() { PremiumMembershiExpirationDate = DateTime.Now.Date });

			var result = _processor.ProcessPurchase(purchase);

			Assert.IsTrue(result.Success);
			Assert.IsInstanceOf<MembershipActivationStep>(_processor.RootStep.Steps.First());
			Assert.IsTrue(result.MembershipStatus.PremiumMembershiExpirationDate == purchase.Orders.First().OrderItem.ExpirationDate);
		}

		[Test]
		public void ProcessPurchase_WithBookOrders_AddsShippingActivationStep()
		{
			var purchase = new Purchase
			{
				CustomerId = -1,
				Orders = new List<OrderPosition>
			{
				new OrderPosition { OrderItem = new OrderItem { OrderItemType = OrderItemType.Book, NameOfItem = "book1", Price = 10 } }
			}
			};
			_shippingServiceMock.Setup(s => s.GenerateShippingSlip(purchase.CustomerId, purchase.Orders.First())).Returns(new ShippingSlip() { Customer = new Customer { Id = purchase.CustomerId }, ShippingItem = purchase.Orders.First().OrderItem });

			var result = _processor.ProcessPurchase(purchase);

			Assert.IsTrue(result.Success);
			Assert.IsInstanceOf<ShippingActivationStep>(_processor.RootStep.Steps.First());
			Assert.IsTrue(result.ShippingSlips.Count == 1 && result.ShippingSlips.First().ShippingItem.NameOfItem == purchase.Orders.First().OrderItem.NameOfItem);
		}

		[Test]
		public void ProcessPurchase_WithVideoOrders_AddsShippingActivationStep()
		{
			var purchase = new Purchase
			{
				CustomerId = -1,
				Orders = new List<OrderPosition>
			{
				new OrderPosition { OrderItem = new OrderItem { OrderItemType = OrderItemType.Video  , NameOfItem = "video1", Price = 10} }
			}
			};
			_shippingServiceMock.Setup(s => s.GenerateShippingSlip(purchase.CustomerId, purchase.Orders.First())).Returns(new ShippingSlip() { Customer = new Customer { Id = purchase.CustomerId }, ShippingItem = purchase.Orders.First().OrderItem });

			var result = _processor.ProcessPurchase(purchase);

			Assert.IsTrue(result.Success);
			Assert.IsInstanceOf<ShippingActivationStep>(_processor.RootStep.Steps.First());
			Assert.IsTrue(result.ShippingSlips.Count == 1 && result.ShippingSlips.First().ShippingItem.NameOfItem == purchase.Orders.First().OrderItem.NameOfItem);

		}


		[Test]
		public void ProcessPurchase_WithVideoAndBookAndPremiumOrders_AddsShippingActivationStepAndMembershipActivation()
		{
			var purchase = new Purchase
			{
				CustomerId = -1,
				Orders = new List<OrderPosition>
			{
				new OrderPosition { OrderItem = new OrderItem { OrderItemType = OrderItemType.Video  , NameOfItem = "video1", Price = 10} },
				new OrderPosition { OrderItem = new OrderItem { OrderItemType = OrderItemType.Book, NameOfItem = "book1", Price = 10 } },
				new OrderPosition { OrderItem = new OrderItem { OrderItemType = OrderItemType.Premium , ExpirationDate = DateTime.Now.Date} }

			}
			};
			_shippingServiceMock.Setup(s => s.GenerateShippingSlip(purchase.CustomerId, purchase.Orders[0])).Returns(new ShippingSlip() { Customer = new Customer { Id = purchase.CustomerId }, ShippingItem = purchase.Orders[0].OrderItem });
			_shippingServiceMock.Setup(s => s.GenerateShippingSlip(purchase.CustomerId, purchase.Orders[1])).Returns(new ShippingSlip() { Customer = new Customer { Id = purchase.CustomerId }, ShippingItem = purchase.Orders[1].OrderItem });

			_membershipActivationServiceMock.Setup(s => s.ActivateMembership(purchase)).Returns(new MembershipStatus() { PremiumMembershiExpirationDate = DateTime.Now.Date });



			var result = _processor.ProcessPurchase(purchase);
			Assert.IsInstanceOf<MembershipActivationStep>(_processor.RootStep.Steps[0]);
			Assert.IsInstanceOf<ShippingActivationStep>(_processor.RootStep.Steps[1]);
			Assert.IsTrue(result.Success);
			Assert.IsTrue(result.MembershipStatus.PremiumMembershiExpirationDate == purchase.Orders[2].OrderItem.ExpirationDate);
			Assert.IsTrue(result.ShippingSlips.Count == 2);
			Assert.IsTrue(result.ShippingSlips[0].ShippingItem.NameOfItem == purchase.Orders[0].OrderItem.NameOfItem);
			Assert.IsTrue(result.ShippingSlips[1].ShippingItem.NameOfItem == purchase.Orders[1].OrderItem.NameOfItem);

		}
		[Test]
		public void ProcessPurchase_WithVideoOrdersFailedGenerateShippingSlipErrorIsPresented()
		{
			var purchase = new Purchase
			{
				CustomerId = -1,
				Orders = new List<OrderPosition>
			{
				new OrderPosition { OrderItem = new OrderItem { OrderItemType = OrderItemType.Video  , NameOfItem = "video1", Price = 10} }
			}
			};
			_shippingServiceMock.Setup(s => s.GenerateShippingSlip(purchase.CustomerId, purchase.Orders.First())).Returns(() => null);

			var result = _processor.ProcessPurchase(purchase);


			Assert.IsInstanceOf<ShippingActivationStep>(_processor.RootStep.Steps.First());
			Assert.IsFalse(result.Success);
			Assert.IsTrue(result.ErrorMessages.Count == 1 && result.ErrorMessages.First() == $"Failed generate Shipping Slip for item: [{purchase.Orders.First().OrderItem.NameOfItem}]");
		}
	}
}
