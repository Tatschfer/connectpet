using System;
using System.Collections.Generic;

namespace PetConnect.Models;

public partial class Pet
{
    public int IdPet { get; set; }

    public string Nome { get; set; } = null!;

    public DateOnly DataDeNascimento { get; set; }

    public string? Cor { get; set; }

    public string Raca { get; set; } = null!;

    public string Especie { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public virtual Tutor CpfNavigation { get; set; } = null!;
}
