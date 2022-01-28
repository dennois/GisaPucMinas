using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Integration
{
    public interface IMessageria
    {
        public Task<KeyValuePair<string, string>> RecuperarMensagem();

        public Task<KeyValuePair<string,string>> RecuperarConsultaMensagemFila();

        public Task<KeyValuePair<string, string>> RecuperarEspecialidadeMensagemFila();

        public Task<KeyValuePair<string, string>> RecuperarPrestadorMensagemFila();

        public Task<KeyValuePair<string, string>> RecuperarConveniadoMensagem();

        public Task<KeyValuePair<string, string>> RecuperarExameMensagem();

        public Task<KeyValuePair<string, string>> RecuperarUsuarioMensagem();

        public Task<KeyValuePair<string, string>> RecuperarFluxoMensagem();
    }
}
