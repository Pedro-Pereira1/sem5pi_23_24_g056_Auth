using DDDSample1.Domain.Shared;
using RobDroneGo.Infrastructure;

namespace RobDroneGoAuth.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RobDroneGoAuthContext _context;

        public UnitOfWork(RobDroneGoAuthContext context)
        {
            this._context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await this._context.SaveChangesAsync();
        }

    }
}