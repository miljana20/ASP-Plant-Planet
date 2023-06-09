using Application.Exceptions;
using Application.UseCases;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCaseHandling
{
    public class QueryHandler : IQueryHandler
    {
        public TResult HandleQuery<TSearch, TResult>(IQuery<TSearch,TResult> query, TSearch search)
            where TResult : class
        {
            return query.Execute(search);
        }
    }
}
