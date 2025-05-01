using AutoMapper;
using Notes.Application.Interfaces;
using Notes.Domain.Models;

namespace Notes.Persistence.Entities
{
    public class UserEntity : IMapWith<User>
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string HashPassword { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserEntity>()
                .ForMember(userEntity => userEntity.UserId,
                    opt => opt.MapFrom(user => user.Id))
                .ForMember(userEntity => userEntity.UserName,
                    opt => opt.MapFrom(user => user.Name))
                .ForMember(userEntity => userEntity.HashPassword,
                    opt => opt.MapFrom(user => user.HashPassword))
                .ForMember(noteCommand => noteCommand.Email,
                    opt => opt.MapFrom(noteDto => noteDto.Email));
        }
    }
}
