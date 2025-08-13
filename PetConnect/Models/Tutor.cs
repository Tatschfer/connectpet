using System;
using System.Collections.Generic;

namespace PetConnect.Models;

public partial class Tutor
{
    public int IdTutor { get; set; }

    public string NomeTutor { get; set; } = null!;

    public string CpfTutor { get; set; } = null!;

    public DateOnly DataDeNascimentoTutor { get; set; }

    public string TelefoneTutor { get; set; }

    public string EnderecoTutor { get; set; }

    public string EmailTutor { get; set; }

    public virtual ICollection<Operador> Operadores { get; set; } = new List<Operador>();

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet   >();
}
