﻿using Exam_system.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_system.UI.Contracts
{
    public interface IincludeNavigation<T> : IRepository<T> where T : BaseModel
    {
        IQueryable<T> CollectionInclude();
    }
}
