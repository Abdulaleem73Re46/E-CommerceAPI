




                using System.Text.Json;
using AutoMapper;
using Core.Contracts;
using Core.Entities;
                using Core.Enum.OrderStatus;
                using Core.Enum.PaymentMethod;
                using Core.Enum.PaymentStatus;
                using Core.Enum.TransactionType;
                using Core.Shared.DataTransferObjects;
                using Core.Shared.Externals;
                using Service.Contracts;
                namespace Service;




                public sealed class MockPaymentService : IPaymentGateway
                {
                private readonly IRepositoryManager _repository;
                private readonly IMapper _mapper;
                public MockPaymentService(IRepositoryManager repository, IMapper mapper)
                {
                _repository = repository;
                _mapper = mapper;

                }
                // =========================
                // CREATE PAYMENT (MOCK)
                // =========================
                public async Task<PaymentDto> CreatePaymentIntentAsync(Guid orderId, string
                idempotencyKey)
                {
                var existingRecord =
                await _repository.IdempotencyRecordRepository.GetByKeyAsync(idempotencyKey);
                if (existingRecord != null)
                {
                return JsonSerializer.Deserialize<PaymentDto>(existingRecord.ResponseJson)!;
                }
                var order = await _repository.OrderRepository.GetByIdAsync(orderId, true);
                if (order == null)
                throw new Exception($"Order {orderId} not found");
                if (order.Status == OrderStatus.Paid)
                throw new Exception("Order already paid");

                var payment = new Payment
                {
                PaymentId = Guid.NewGuid(),
                OrderId = orderId,
                Amount = order.TotalPrice,
                PayStatus = PaymentStatus.Pending,
                PayMethod = PaymentMethods.Stripe,
                IdempotencyKey = idempotencyKey,
                CreatedAt = DateTime.UtcNow
                };

                await _repository.PaymentRepository.AddAsync(payment);
                await _repository.SaveAsync();
                var dto = _mapper.Map<PaymentDto>(payment);

                var idempotencyrecord = new IdempotencyRecord
                {
                Id = Guid.NewGuid(),
                Key = idempotencyKey,
                ResponseJson = JsonSerializer.Serialize(dto),
                CreatedAt = DateTime.UtcNow
                };
                _repository.IdempotencyRecordRepository.Add(idempotencyrecord);
                await _repository.SaveAsync();
                return dto;

                
                }
                // =========================
                // CONFIRM PAYMENT (MOCK SUCCESS)
                // =========================
                public async Task ConfirmPaymentAsync(string externalPaymentId)
                {
                var payment = await _repository.PaymentRepository
                .GetPaymentAsync(Guid.Parse(externalPaymentId), true);

                if (payment == null)
                throw new Exception("Payment not found");
                if (payment.PayStatus != PaymentStatus.Pending)
                return;

                var order = await _repository.OrderRepository
                .GetOrderWithItemsAsync(payment.OrderId);

                if (order == null)
                throw new Exception("Order not found");

                foreach (var item in order.OrderItems)
                {
                var product = await _repository.ProductRepository
                .GetProductAsync(item.ProductId, true);
                if (product.StockQuantity < item.Quantity)
                throw new Exception("Stock exceeded");
                product.StockQuantity -= item.Quantity;
                _repository.ProductRepository.UpdateProduct(product);
                }
                payment.PayStatus = PaymentStatus.Success;
                payment.PaidAt = DateTime.UtcNow;
                var transaction = new PaymentTransaction
                {
                Id = Guid.NewGuid(),
                PaymentId = payment.PaymentId,
                type = TransactionType.Capture,
                Amount = payment.Amount,
                ExternalTransactionId = externalPaymentId,
                CreatedAt = DateTime.UtcNow
                };
                _repository.PaymentTransactionRepository.AddAsync(transaction);
                order.Status = OrderStatus.Confirmed;
                _repository.OrderRepository.UpdateOrder(order);
                var cart = await _repository.CartRepository.GetByUserIdAsync(order.UserId);
                if (cart != null)
                _repository.CartRepository.DeleteItem(cart);
                await _repository.SaveAsync();
                }
                // =========================
                // FAIL PAYMENT (MOCK)
                // =========================
                public async Task FailPaymentAsync(string externalPaymentId)
                {
                var payment = await _repository.PaymentRepository.GetPaymentAsync(Guid.Parse(externalPaymentId), true);
                if (payment == null || payment.PayStatus != PaymentStatus.Pending)
                return;
                payment.PayStatus = PaymentStatus.Failed;
                var transaction = new PaymentTransaction
                {
                Id = Guid.NewGuid(),
                PaymentId = payment.PaymentId,
                type = TransactionType.Failed,
                Amount = payment.Amount,
                ExternalTransactionId = externalPaymentId,
                CreatedAt = DateTime.UtcNow
                };
                _repository.PaymentTransactionRepository.AddAsync(transaction);
                await _repository.SaveAsync();
                }
                // =========================
                // REFUND (MOCK)
                // =========================
                public async Task RefundPaymentAsync(Guid paymentId, decimal? amount = null)
                {
                var payment = await _repository.PaymentRepository
                .GetPaymentAsync(paymentId, true);
                if (payment == null || payment.PayStatus != PaymentStatus.Success)
                throw new Exception("Cannot refund");
                var refundAmount = amount ?? payment.Amount;
                var transaction = new PaymentTransaction
                {
                Id = Guid.NewGuid(),
                PaymentId = payment.PaymentId,
                type = TransactionType.Refund,
                Amount = refundAmount,
                ExternalTransactionId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow
                };_repository.PaymentTransactionRepository.AddAsync(transaction);
                if (refundAmount >= payment.Amount)
                {
                payment.PayStatus = PaymentStatus.Refunded;
                var order = await _repository.OrderRepository
                .GetByIdAsync(payment.OrderId, true);
                if (order != null)
                order.Status = OrderStatus.Refunded;
                }
                await _repository.SaveAsync();
                }

    public async Task<PaymentDto> GetPaymentAsync(Guid paymentId)
    {
       var payment=await _repository.PaymentRepository.GetPaymentAsync(paymentId,trackChanges:true);
       if(payment is null)throw new KeyNotFoundException("No Payment ");

        return _mapper.Map<PaymentDto>(payment);

    }
}