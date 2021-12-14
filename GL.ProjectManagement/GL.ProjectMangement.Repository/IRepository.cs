﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GL.ProjectMangement.Repository
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T Update(T entity);
        T Get(string id);
        T Delete(string id);
        IEnumerable<T> All();

        Task SaveChanges();
    }
}