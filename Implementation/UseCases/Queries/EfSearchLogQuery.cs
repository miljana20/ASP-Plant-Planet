using Application.UseCases.DTO;
using Application.UseCases.Queries;
using Application.UseCases.Queries.Searches;
using DataAccess;
using Domain.Entities;
using Implementation.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EfSearchLogQuery : EfUseCase, ISearchLogQuery
    {
        public EfSearchLogQuery(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 33;

        public string Name => "Search log";

        public string Description => "";

        public PagedResponseDto<LogDto> Execute(LogSearch search)
        {
            IQueryable<Log> query = Context.Logs.Where(x => x.IsActive && x.DeletedAt == null);

            if (!string.IsNullOrEmpty(search.Actor))
                query = query.Where(x => x.Actor.ToLower() == search.Actor.ToLower());

            if (!string.IsNullOrEmpty(search.UseCase))
                query = query.Where(x => x.UseCaseName.ToLower() == search.UseCase.ToLower());

            if (search.DateFrom.HasValue)
                query = query.Where(x => x.CreatedAt >= search.DateFrom.Value);

            if (search.DateTo.HasValue)
                query = query.Where(x => x.CreatedAt <= search.DateTo.Value);

            return query.ToPagedResponse<Log, LogDto>(search, x => new LogDto
            {
                Id = x.Id,
                Actor = x.Actor,
                Data = x.UseCaseData,
                Date = x.CreatedAt,
                UseCase = x.UseCaseName
            });
        }
    }
}
