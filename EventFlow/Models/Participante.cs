﻿using EventFlow.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EventFlow.Models
{
    public class Participante : Pessoa
    {
        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataNascimento]
        public DateOnly DataNascimento { get; set; }

        public ICollection<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();
    }
}
