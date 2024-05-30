using AutoMapper;
using Core.Persistence.Repositories;
using MediatR;
using Persistence.Contexts;

namespace Application.Features.Assignment.Commands.Delete;

public class DeleteAssigmentCommand : IRequest<DeletedAssigmentResponse>
{
    public int Id { get; set; }


    public class DeleteAssigmentCommandHandler (IMapper mapper, IRepository<Domain.Models.Assignment, AppDbContext> assignmentRepository)
        : IRequestHandler<DeleteAssigmentCommand, DeletedAssigmentResponse>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IRepository<Domain.Models.Assignment, AppDbContext> _assignmentRepository = assignmentRepository;
        
        public async Task<DeletedAssigmentResponse> Handle(DeleteAssigmentCommand request, CancellationToken cancellationToken)
        {
            var assignment = await _assignmentRepository.FindAsync(request.Id);
            if (assignment == null)
            {
                throw new Exception("Assignment not found");
            }

            await _assignmentRepository.DeleteAsync(assignment);
            var response = _mapper.Map<DeletedAssigmentResponse>(assignment);
            return response;
        }
    }
}