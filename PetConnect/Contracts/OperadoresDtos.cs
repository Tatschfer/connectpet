using System.ComponentModel.DataAnnotations;

public record OperadorCreateDto(
    [Required, StringLength(150)] string NomeOperador,
    [StringLength(14)] string CPFOperador,
    [StringLength(18)] string CNPJOperador,
    [EmailAddress, StringLength(200)] string EmailOperador,
    [StringLength(20)] string TelefoneOperador
);

public record OperadorUpdateDto(
    [StringLength(150)] string NomeOperador,
    [StringLength(14)] string CPFOperador,
    [StringLength(18)] string CNPJOperador,
    [EmailAddress, StringLength(200)] string EmailOperador,
    [StringLength(20)] string TelefoneOperador
);
