using FluentResults;

namespace NET_Core_Task.BLL.MediatR.ResultVariations
{
    public class NullResult<T> : Result<T>
    {
        public NullResult()
            : base()
        {
        }
    }
}
