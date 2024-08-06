using MediatR;

namespace NET_Core_Task.BLL.Behavior
{
    public interface IValidatableRequest<out TResponse> : IRequest<TResponse>
    {
    }
}
