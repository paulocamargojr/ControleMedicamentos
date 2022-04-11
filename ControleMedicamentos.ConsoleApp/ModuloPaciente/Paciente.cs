
using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloRequisicao;
using System;

namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    public class Paciente : EntidadeBase
    {

        private readonly string nome;
        private Requisicao requisicao;
        private readonly string telefone;
        private readonly string endereco;

        public Paciente(string nome, string telefone, string endereco)
        {

            this.nome = nome;
            this.telefone = telefone;
            this.endereco = endereco;

        }


        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + nome + Environment.NewLine +
                "Telefone: " + telefone + Environment.NewLine +
                "Endereço: " + endereco + Environment.NewLine;
        }
    }
}
