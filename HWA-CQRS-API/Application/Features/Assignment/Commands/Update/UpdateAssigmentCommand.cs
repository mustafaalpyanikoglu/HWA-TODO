using AutoMapper;
using Core.Persistence.Repositories;
using MediatR;
using Persistence.Contexts;

namespace Application.Features.Assignment.Commands.Update;

public class UpdateAssigmentCommand : IRequest<UpdatedAssigmentResponse>
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }

    public class UpdateAssigmentCommandHandler (IMapper mapper, IRepository<Domain.Models.Assignment, AppDbContext> assignmentRepository)
        : IRequestHandler<UpdateAssigmentCommand, UpdatedAssigmentResponse>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IRepository<Domain.Models.Assignment, AppDbContext> _assignmentRepository = assignmentRepository;
        
        public async Task<UpdatedAssigmentResponse> Handle(UpdateAssigmentCommand request, CancellationToken cancellationToken)
        {
            var assignment = await _assignmentRepository.FindAsync(request.Id);
            if (assignment == null)
            {
                throw new Exception("Assignment not found");
            }

            assignment = _mapper.Map(request, assignment);
            await _assignmentRepository.UpdateAsync(assignment);
            var response = _mapper.Map<UpdatedAssigmentResponse>(assignment);
            return response;
        }
    }
}