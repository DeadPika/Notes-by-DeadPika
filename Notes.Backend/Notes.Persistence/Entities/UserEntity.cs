using AutoMapper;
using Notes.Application.Interfaces;
using Notes.Domain.Models;

namespace Notes.Persistence.Entities
{
    public class UserEntity : IMapWith<User>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string HashPassword { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<RoleEntity> Roles { get; set; } = [];
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserEntity, User>()
                .ForMember(user => user.Id,
                    opt => opt.MapFrom(userEntity => userEntity.Id))
                .ForMember(user => user.Name,
                    opt => opt.MapFrom(userEntity => userEntity.UserName))
                .ForMember(user => user.HashPassword,
                    opt => opt.MapFrom(userEntity => userEntity.HashPassword))
                .ForMember(user => user.Email,
                    opt => opt.MapFrom(userEntity => userEntity.Email));

            profile.CreateMap<User, UserEntity>()
                .ForMember(userEntity => userEntity.Id,
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
