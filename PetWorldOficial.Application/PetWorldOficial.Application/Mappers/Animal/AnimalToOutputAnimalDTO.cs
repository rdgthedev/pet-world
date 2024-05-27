using AutoMapper;
using PetWorldOficial.Application.DTOs.Animal.Output;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Application.Mappers;

public class AnimalToOutputAnimalDTO : Profile
{
    public AnimalToOutputAnimalDTO()
    {
        CreateMap<Animal, OutputAnimalDTO>()
            .ConstructUsing(c =>
                new OutputAnimalDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    Gender = c.Gender.ToString(),
                    Species = c.Species,
                    Race = c.Race,
                    UserId = c.UserId
                });

        CreateMap<OutputAnimalDTO, Animal>()
            .ConstructUsing(c =>
                new Animal(
                    c.Name,
                    c.Species,
                    c.Race,
                    c.BirthDate,
                    (EGender)Enum.Parse(typeof(EGender), c.Gender),
                    c.UserId));
    }
}