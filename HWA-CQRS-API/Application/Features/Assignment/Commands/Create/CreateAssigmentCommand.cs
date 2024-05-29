using AutoMapper;
using Core.Persistence.Repositories;
using MediatR;
using Persistence.Contexts;

namespace Application.Features.Assignment.Commands.Create;

public class CreateAssigmentCommand : IRequest<CreatedAssigmentResponse>
{
    //This is the user who is creating the task
    //May be can be read from the token!
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }

    public class CreateAssigmentCommandHandler : IRequestHandler<CreateAssigmentCommand, CreatedAssigmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Models.Assignment, AppDbContext> _assignmentRepository;

        public CreateAssigmentCommandHandler(IMapper mapper, IRepository<Domain.Models.Assignment, AppDbContext> assignmentRepository)
        {
            _mapper = mapper;
            _assignmentRepository = assignmentRepository;
        }


        public async Task<CreatedAssigmentResponse> Handle(CreateAssigmentCommand request, CancellationToken cancellationToken)
        {
            var assignment = _mapper.Map<Domain.Models.Assignment>(request);

            await _assignmentRepository.AddAsync(assignment);
            var response = _mapper.Map<CreatedAssigmentResponse>(assignment);

            return response;
        }
    }
}