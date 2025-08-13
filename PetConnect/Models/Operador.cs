using System;
using System.Collections.Generic;

namespace PetConnect.Models;

public partial class Operador
{
    public int IdOperador { get; set; }

    public string Nome { get; set; } = null!;

    public string CpfOperador { get; set; } = null!;

    public string CnpjOperador { get; set; } = null!;

    public string EmailOperador { get; set; } = null!;

    public string? TelefoneOperador { get; set; }

    public string CpfTutor { get; set; } = null!;

    public virtual Tutor CpfTutorNavigation { get; set; } = null!;
}
