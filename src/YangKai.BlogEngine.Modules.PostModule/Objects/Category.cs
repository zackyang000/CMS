using System;
using System.Collections.Generic;
using System.Linq;
using AtomLab.Domain;
using AtomLab.Domain.Infrastructure;

namespace YangKai.BlogEngine.Modules.PostModule.Objects
{
    /// <summary>
    /// 分类信息.
    /// </summary>
    public class Category : Entity<Guid>,
                            IEventHandler<EntityCreatingEvent<Category>>
    {
        #region constructor

        public Category()
            : base(Guid.NewGuid())
        {
            Name = string.Empty;
            Url = string.Empty;
            Description = string.Empty;
            OrderId = 255;
            IsDeleted = false;
            CreateDate = DateTime.Now;
        }

        #endregion

        #region property

        /// <summary>
        /// 主键.
        /// </summary>
        public Guid CategoryId { get; private set; }

        /// <summary>
        /// 分类名称.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用于显示分类信息的URL地址.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 分类描述.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 排列顺序,默认为255.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 是否已被删除.(禁用)
        /// </summary>
        public bool IsDeleted { get; private set; }

        /// <summary>
        /// 创建时间.
        /// </summary>
        public DateTime CreateDate { get; private set; }

        #endregion

        #region relation schema

        /// <summary>
        /// 所包含的文章列表.
        /// </summary>
        public List<Post> Posts { get; set; }

        /// <summary>
        /// 所属分组信息.
        /// </summary>
        public Group Group { get; set; }

        #endregion

        #region handler

        #region IEventHandler<EntityCreatingEvent<Category>> Members

        public void Handle(EntityCreatingEvent<Category> e)
        {
            //TODO:Domain中应当去掉Repository
            var isExist = InstanceLocator.Current.GetInstance<Repository<Category,Guid>>().Exist(p => p.Url == e.EntityCreating.Url);

            if (isExist)
            {
                throw new DomainException(string.Format("别名{0}已存在.", e.EntityCreating.Url));
            }
        }

        #endregion

        #endregion
    }
}