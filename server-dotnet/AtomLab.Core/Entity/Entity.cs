using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AtomLab.Core
{
    public class Entity : IEntity
    {
        public Entity()
        {
            CreateIp = Auth.Ip;
            CreateAddress = Auth.Address;
            CreateDate = DateTime.Now;
        }

        public string CreateUser { get; set; }
        public string CreateIp { get; set; }
        public string CreateAddress { get; set; }
        public DateTime CreateDate { get; set; }

        public string LastEditIp { get; set; }
        public string LastEditAddress { get; set; }
        public string LastEditUser { get; set; }
        public DateTime? LastEditDate { get; set; }

        public int? OrderId { get; set; }
        public bool IsDeleted { get; set; }
        public string Remark { get; set; }
    }
}