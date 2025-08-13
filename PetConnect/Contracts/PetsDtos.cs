using System.ComponentModel.DataAnnotations;

public record PetInputDto(
    [Required, StringLength(120)] string NomePet,
    string RacaPet,
    DateOnly DataDeNascimentoPet,
    [StringLength(14)] string CPFTutor,
    [StringLength(40)] string CorPet,
    [StringLength(60)] string EspeciePet
);

public record PetUpdateDto(
    string NomePet,
    string RacaPet,
    DateOnly DataDeNascimentoPet,
    string CPFTutor,
    string CorPet,
    string EspeciePet
);
