using AutoMapper;

namespace Presentation;

public interface IMappableRequest<T>
{
    void Mapping(Profile profile) =>
        profile.CreateMap(typeof(T), GetType());
}
