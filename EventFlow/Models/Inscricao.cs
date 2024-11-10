﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EventFlow.Enums;

namespace EventFlow.Models
{
    public class Inscricao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EventoId { get; set; }

        [Required]
        public int ParticipanteId { get; set; }

        [Required]
        [Display(Name = "Data de Inscrição")]
        public DateTime DataInscricao { get; set; }

        [Required]
        [Display(Name = "Status de Pagamento")]
        public StatusPagamento StatusPagamento { get; set; }

        [Required]
        [Display(Name = "Método de Pagamento")]
        public MetodoPagamento MetodoPagamento { get; set; }

        [ForeignKey("EventoId")]
        public Evento? Evento { get; set; }

        [ForeignKey("ParticipanteId")]
        public Participante? Participante { get; set; }
    }
}
