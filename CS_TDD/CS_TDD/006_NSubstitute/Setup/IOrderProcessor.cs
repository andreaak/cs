using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_TDD._006_NSubstitute.Setup
{
    public interface IOrderProcessor
    {
        void ProcessOrder(int orderId, Action<bool> orderProcessed);
    }

    public interface IEvents
    {
        void RaiseOrderProcessed(int orderId);
    }

    public interface ICart
    {
        int OrderId { get; set; }
    }

    public class OrderPlacedCommand
    {
        IOrderProcessor orderProcessor;
        IEvents events;
        public OrderPlacedCommand(IOrderProcessor orderProcessor, IEvents events)
        {
            this.orderProcessor = orderProcessor;
            this.events = events;
        }
        public void Execute(ICart cart)
        {
            orderProcessor.ProcessOrder(
                cart.OrderId,
                wasOk =>
                {
                    if (wasOk)
                    {
                        events.RaiseOrderProcessed(cart.OrderId);
                    }
                }
            );
        }
    }
}
