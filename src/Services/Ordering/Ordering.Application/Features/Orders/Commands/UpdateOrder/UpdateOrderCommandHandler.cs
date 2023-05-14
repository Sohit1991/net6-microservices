using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;
        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        //Making "void" request handlers return Task instead of Unit in mediatr 12
        //        Breaking changes include:

        //Depending directly on IServiceProvider to resolve services
        //Making "void" request handlers return Task instead of Unit
        //IRequest does not inherit IRequest<Unit> instead IBaseRequest
        //Consolidating the MediatR.Extensions.Microsoft.DependencyInjection package into the main MediatR package
        //Rolling back stricter generic constraints in various behavior interfaces
        //Various behavior registrations now inside of the IServiceCollection extension
        //Overloads of AddMediatR consolidated into single configuration object
        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            // Update the order
            var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);
            if (orderToUpdate == null)
            {
                _logger.LogError("Orderdoes not exist in db");
                throw new NotFoundException(nameof(Order), request.Id);
            }
            _mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));

            await _orderRepository.UpdateAsync(orderToUpdate!);

            _logger.LogInformation($"Order {orderToUpdate!.Id} is successfully updated");
            //return Unit.Value;
        }


    }
}
