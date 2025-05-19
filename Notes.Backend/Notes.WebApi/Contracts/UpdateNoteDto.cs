using AutoMapper;
using Notes.Application.Interfaces;
using Notes.Persistence.Repositories.Notes.Commands.UpdateNote;

namespace Notes.WebApi.Contracts
{
    public class UpdateNoteDto : IMapWith<UpdateNoteCommand>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateNoteDto, UpdateNoteCommand>()
                .ForMember(noteCommand => noteCommand.Id,
                    opt => opt.MapFrom(noteDto => noteDto.Id))
                .ForMember(noteCommand => noteCommand.Title,
                    opt => opt.MapFrom(noteDto => noteDto.Title))
                .ForMember(noteCommand => noteCommand.Details,
                    opt => opt.MapFrom(noteDto => noteDto.Details));
        }
    }
}
