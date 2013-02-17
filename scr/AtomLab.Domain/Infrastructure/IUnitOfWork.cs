using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtomLab.Domain.Infrastructure
{
    /// <summary>
    /// Contract for UnitOfWork pattern. For more
    /// references see http://martinfowler.com/eaaCatalog/unitOfWork.html or
    /// http://msdn.microsoft.com/en-us/magazine/dd882510.aspx
    /// In this solution sample Unit Of Work is implemented out-of-box in 
    /// ADO.NET Entity Framework persistence engine. But for academic
    /// purposed and for mantein PI ( Persistence Ignorant ) in Domain 
    /// this pattern is implemented.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commit all changes made in  a container.
        /// </summary>
        ///<remarks>
        /// If entity have fixed properties and optimistic concurrency problem exists 
        /// exception is thrown
        ///</remarks>
        void Commit();

        /// <summary>
        /// Commit all changes made in  a container.
        /// </summary>
        ///<remarks>
        /// If entity have fixed properties and optimistic concurrency problem exists 
        /// client changes are refereshed
        ///</remarks>
        void CommitAndRefreshChanges();

        /// <summary>
        /// Rollback changes not stored in databse at 
        /// this moment. See references of UnitOfWork pattern
        /// </summary>
        void RollbackChanges();
    }
}
