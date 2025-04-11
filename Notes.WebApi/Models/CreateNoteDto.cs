using AutoMapper;
using Notes.Application.Common.Mapping;
using Notes.Application.Notes.Commands.CreateNote;

namespace Notes.WebApi.Models
{
    public class CreateNoteDto : IMapWith<CreateNoteDto>
    {
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateNoteDto, CreateNoteCommand>()
                .ForMember(noteCommand => noteCommand.Title,
                    opt => opt.MapFrom(noteDto => noteDto.Title.Length))
                .ForMember(noteCommand => noteCommand.Details,
                    opt => opt.MapFrom(noteDto => noteDto.Details));
        }
    }
}
