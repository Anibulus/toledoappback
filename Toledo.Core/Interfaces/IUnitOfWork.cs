
using Toledo.Core.Interfaces;

namespace Toledo.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {


        void SaveChanges();

        Task SaveChangesAsync();

    }
}
