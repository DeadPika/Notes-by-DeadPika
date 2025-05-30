using AutoMapper;
using Notes.Application.Interfaces;
using Notes.Persistence.Repositories.Notes.Commands.CreateNote;
using System.ComponentModel.DataAnnotations;

namespace Notes.WebApi.Contracts
{
    public class CreateNoteDto : IMapWith<CreateNoteDto>
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateNoteDto, CreateNoteCommand>()
                .ForMember(noteCommand => noteCommand.Title,
                    opt => opt.MapFrom(noteDto => noteDto.Title))
                .ForMember(noteCommand => noteCommand.Details,
                    opt => opt.MapFrom(noteDto => noteDto.Details));
        }
    }
}   
