using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloPaciente;
using ControleMedicamentos.ConsoleApp.ModuloMedicamento;

namespace ControleMedicamentos.ConsoleApp.ModuloRequisicao
{
    public class Requisicao : EntidadeBase
    {

        private Paciente paciente;
        private Medicamento medicamento;
        public bool aprovada;
        //public bool aprovado { get => aprovado; }
        public DateTime dataRequisicao;
        public string Status { get => aprovada ? "Aberto" : "Fechado"; }

        public Requisicao(Paciente paciente, Medicamento medicamento)
        {
            this.paciente = paciente;
            this.medicamento = medicamento;

            EntregarMedicamentos();
        }

        public void EntregarMedicamentos()
        {
            if (aprovada)
            {
                dataRequisicao = DateTime.Today;
                //this.medicamento.Diminuir();
            }
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Paciente: " + paciente + Environment.NewLine +
                "Medicamento: " + medicamento + Environment.NewLine +
                "Status de aprovação: " + aprovada + Environment.NewLine +
                "Data de requisição:: " + dataRequisicao.ToShortDateString() + Environment.NewLine;
        }

        internal void atualizarEstoque(int qtdCaixas)
        {
            medicamento.Retirar(qtdCaixas);
        }
    }
}
