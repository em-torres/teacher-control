using AutoMapper;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;

namespace TeacherControl.Domain.AutoMapperProfiles.AssignmentProfiles
{
    public class AssignmentCommentProfile : Profile
    {
        public AssignmentCommentProfile()
        {
            CreateMap<AssignmentCommentDTO, AssignmentComment>();

            CreateMap<AssignmentComment, AssignmentCommentDTO>()
                .ForMember(i => i.Author, i => i.MapFrom(m => m.CreatedBy))
                .ForMember(i => i.Downvote, i => i.MapFrom(m => m.Downvotes.Count + 1))
                .ForMember(i => i.Upvote, i => i.MapFrom(m => m.Upvotes.Count + 1));
        }
    }
}
