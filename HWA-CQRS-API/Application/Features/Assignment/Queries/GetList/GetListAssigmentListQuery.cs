using AutoMapper;
using Core.Persistence.Repositories;
using MediatR;
using Persistence.Contexts;

namespace Application.Features.Assignment.Queries.GetList;

public class GetListAssigmentListQuery : IRequest<List<GetListAssigmentListItemDto>>
{
    public class GetListAssigmentListQueryHandler : IRequestHandler<GetListAssigmentListQuery, List<GetListAssigmentListItemDto>>
    {
        private readonly Mapper _mapper;
        private readonly IRepository<Domain.Models.Assignment, AppDbContext> _assignmentRepository;

        public GetListAssigmentListQueryHandler(Mapper mapper, IRepository<Domain.Models.Assignment, AppDbContext> assignmentRepository)
        {
            _mapper = mapper;
            _assignmentRepository = assignmentRepository;
        }


        public async Task<List<GetListAssigmentListItemDto>> Handle(GetListAssigmentListQuery request, CancellationToken cancellationToken)
        {
            var assignments = await _assignmentRepository.GetListAsync(cancellationToken: cancellationToken);

            var response = _mapper.Map<List<GetListAssigmentListItemDto>>(assignments);

            return response;
        }
    }
}