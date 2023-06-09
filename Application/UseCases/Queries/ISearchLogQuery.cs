﻿using Application.UseCases.DTO;
using Application.UseCases.Queries.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries
{
    public interface ISearchLogQuery : IQuery<LogSearch, PagedResponseDto<LogDto>>
    {
    }
}
