using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.Domain.Enum
{
    public class Enums
    {
        public enum PlanoTipo
        {
            Enfermaria = 1,
            Apartamento = 2,
            Vip = 3,
        }

        public enum AssociadoStatus
        {
            Ativo = 1,
            Suspenso = 2,
            Inativo = 3,
        }

        public enum EmpresaPorte
        {
            Pequeno = 'P',
            Medior = 'M',
            Grande = 'G'
        }

        public enum BoletoStatus
        {
            Pendente = 'P',
            Atrasado = 'A',
            Quitado = 'Q'
        }

        public enum ConveniadoTipo
        {
            Clinica = 'C',
            Hospital = 'H',
            Laboratorio = 'Q'
        }

        public enum ConsultaStatus
        {
            AguardandoAgendamento = 'A',
            Agendada = 'S',
            Cancelada = 'C',
            Realizada = 'R'
        }

        public enum ExameStatus
        {
            AguardandoAutorizacao = 'A',
            Autorizado = 'S',
            Cancelado = 'C',
            Realizado = 'R'
        }
    }
}
