using AutoMapper;
using Core.Persistence.Repositories;
using MediatR;
using Persistence.Contexts;

namespace Application.Features.Assignment.Queries.GetList;

public class GetListAssigmentQuery : IRequest<List<GetListAssigmentItemDto>>
{
    public class GetListAssigmentQueryHandler : IRequestHandler<GetListAssigmentQuery, List<GetListAssigmentItemDto>>
    {
        private readonly Mapper _mapper;
        private readonly IRepository<Domain.Models.Assignment, AppDbContext> _assignmentRepository;

        public GetListAssigmentQueryHandler(Mapper mapper, IRepository<Domain.Models.Assignment, AppDbContext> assignmentRepository)
        {
            _mapper = mapper;
            _assignmentRepository = assignmentRepository;
        }
        
        public async Task<List<GetListAssigmentItemDto>> Handle(GetListAssigmentQuery request, CancellationToken cancellationToken)
        {
            var assignments = await _assignmentRepository.GetListAsync(cancellationToken: cancellationToken);

            var response = _mapper.Map<List<GetListAssigmentItemDto>>(assignments);

            return response;
        }
    }
}