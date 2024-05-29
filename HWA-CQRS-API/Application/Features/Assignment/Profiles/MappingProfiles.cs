using Application.Features.Assignment.Commands.Create;
using Application.Features.Assignment.Commands.Delete;
using Application.Features.Assignment.Commands.Update;
using Application.Features.Assignment.Queries.GetById;
using AutoMapper;

namespace Application.Features.Assignment.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Domain.Models.Assignment, GetByIdAssignmentQuery>().ReverseMap();
        CreateMap<Domain.Models.Assignment, GetByIdAssignmentResponse>().ReverseMap();

        CreateMap<Domain.Models.Assignment, GetByIdAssignmentResponse>().ReverseMap();

        CreateMap<Domain.Models.Assignment, CreatedAssigmentResponse>().ReverseMap();
        CreateMap<Domain.Models.Assignment, CreateAssigmentCommand>().ReverseMap();

        CreateMap<Domain.Models.Assignment, UpdatedAssigmentResponse>().ReverseMap();
        CreateMap<Domain.Models.Assignment, UpdateAssigmentCommand>().ReverseMap();

        CreateMap<Domain.Models.Assignment, DeletedAssigmentResponse>().ReverseMap();
        CreateMap<Domain.Models.Assignment, DeleteAssigmentCommand>().ReverseMap();
    }
}