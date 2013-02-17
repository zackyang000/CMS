using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace AtomLab.Domain.Infrastructure
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        #region IUnitOfWork Members

        /// <summary>
        /// Commit all changes made in a container.
        /// </summary>
        ///<remarks>
        /// If entity have fixed properties and optimistic concurrency problem exists 
        /// exception is thrown
        ///</remarks>
        public void Commit()
        {
            base.SaveChanges();
        }

        /// <summary>
        /// Commit all changes made in a container.
        /// </summary>
        ///<remarks>
        /// If entity have fixed properties and optimistic concurrency problem exists 
        /// client changes are refereshed
        ///</remarks>
        [Obsolete("直接使用Commit方法,EF中已集成事务功能.")]
        public void CommitAndRefreshChanges()
        {
            throw new NotSupportedException("直接使用Commit方法,EF中已集成事务功能.");
        }

        /// <summary>
        /// Rollback changes not stored in databse at 
        /// this moment. See references of UnitOfWork pattern
        /// </summary>
        [Obsolete("直接使用Commit方法,EF中已集成事务功能.")]
        public void RollbackChanges()
        {
            throw new NotSupportedException("直接使用Commit方法,EF中已集成事务功能.");
        }

        #endregion
    }
}