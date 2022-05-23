namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder;

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using Ordering.Application.Interfaces.Infrastructure;
using Ordering.Application.Interfaces.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
{
    private readonly IOrderRepository orderRepository;
    private readonly IEmailService emailService;
    private readonly IMapper mapper;
    private readonly ILogger<CheckoutOrderCommandHandler> logger;

    public CheckoutOrderCommandHandler(
            IOrderRepository orderRepository,
            IEmailService emailService,
            IMapper mapper,
            ILogger<CheckoutOrderCommandHandler> logger)
    {
        this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = mapper.Map<Order>(request);
        var newOrder = await this.orderRepository.AddAsync(orderEntity);

        this.logger.LogInformation($"Order {newOrder.Id} is successfully created.");

        await SendMail(newOrder);

        return newOrder.Id;
    }

    private async Task SendMail(Order newOrder)
    {
        var email = new Email()
        {
            To = "someMail@gmail.com",
            Body = "Order was created.",
            Subject = "Order was created."
        };

        try
        {
            await this.emailService.SendEmailAsync(email);
        }
        catch (Exception ex)
        {
            this.logger.LogError($"Order {newOrder.Id} failed due to an error with the mail service: {ex.Message}");
            throw;
        }
    }
}
