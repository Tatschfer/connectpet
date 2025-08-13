using System.ComponentModel.DataAnnotations;

public record TutorInputDto(
    [Required, StringLength(150)] string NomeTutor,
    [Required, StringLength(14)] string CPFTutor,
    DateOnly DataDeNascimentoTutor,
    [StringLength(250)] string EnderecoTutor,
    [StringLength(20)] string TelefoneTutor,
    [EmailAddress, StringLength(200)] string EmailTutor
);

public record TutorUpdateDto(
    string NomeTutor,
    string CPFTutor,
    DateOnly DataDeNascimentoTutor,
    string EnderecoTutor,
    string TelefoneTutor,
    string EmailTutor
);
