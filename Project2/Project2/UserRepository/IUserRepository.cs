﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UserRepository
{
    public interface IUserRepository<TEntity>
    {
        public IEnumerable<TEntity> GetAll();
        public IEnumerable<TEntity> FindByCondition(Func<TEntity, bool> expression);
        public TEntity Get(string Id);
        public TEntity GetByName(string Email);
        public void Add(TEntity Entity);
        public void Update(TEntity Entity);
        public void Delete(string Id);
    }
}
