﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlympiaGymApi.Core
{
    public interface IUnitOfWork
    {
        Task Complete();
    }
}
