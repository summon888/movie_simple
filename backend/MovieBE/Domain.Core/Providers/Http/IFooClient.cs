using Refit;

namespace Domain.Core.Providers.Http
{
    public interface IFooClient
    {
        [Get("/")]
        Task<object> GetAll();
    }
}
