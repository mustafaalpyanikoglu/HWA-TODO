using AutoMapper;
using Core.Persistence.Repositories;
using MediatR;
using Persistence.Contexts;

namespace Application.Features.Assignment.Queries.GetById;

public class GetByIdAssignmentQuery : IRequest<GetByIdAssignmentResponse>
{
    public int Id { get; set; }

    public class GetByIdAssigmentQueryHandler : IRequestHandler<GetByIdAssignmentQuery, GetByIdAssignmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Models.Assignment, AppDbContext> _assignmentRepository;

        public GetByIdAssigmentQueryHandler(IMapper mapper, IRepository<Domain.Models.Assignment, AppDbContext> assignmentRepository)
        {
            _mapper = mapper;
            _assignmentRepository = assignmentRepository;
        }


        public async Task<GetByIdAssignmentResponse> Handle(GetByIdAssignmentQuery request, CancellationToken cancellationToken)
        {
            var assigment = await _assignmentRepository.GetFirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken: cancellationToken);

            var response = _mapper.Map<GetByIdAssignmentResponse>(assigment);

            return response;
        }
    }
}