using Application.Features.Assignment.Commands.Create;
using Application.Features.Assignment.Commands.Delete;
using Application.Features.Assignment.Commands.Update;
using Application.Features.Assignment.Queries.GetById;
using Application.Features.Assignment.Queries.GetList;
using AutoMapper;
using Domain.Models;

namespace Application.Profiles;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        #region Assignment mapping extensions

        CreateMap<Assignment, GetByIdAssignmentQuery>().ReverseMap();
        CreateMap<Assignment, GetByIdAssignmentResponse>().ReverseMap();
        CreateMap<Assignment, GetByIdAssignmentResponse>().ReverseMap();
        CreateMap<Assignment, CreatedAssigmentResponse>().ReverseMap();
        CreateMap<Assignment, CreateAssigmentCommand>().ReverseMap();
        CreateMap<Assignment, UpdatedAssigmentResponse>().ReverseMap();
        CreateMap<Assignment, UpdateAssigmentCommand>().ReverseMap();
        CreateMap<Assignment, DeletedAssigmentResponse>().ReverseMap();
        CreateMap<Assignment, DeleteAssigmentCommand>().ReverseMap();

        CreateMap<Assignment, GetByIdAssignmentQuery>().ReverseMap();
        CreateMap<Assignment, GetListAssigmentItemDto>().ReverseMap();
        #endregion
    }
}