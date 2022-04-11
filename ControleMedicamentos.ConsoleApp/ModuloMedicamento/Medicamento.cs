using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ControleMedicamentos.ConsoleApp.ModuloMedicamento
{

	public class Medicamento : EntidadeBase
	{

		private readonly string nome;
		private readonly string descricao;
		private int quantidade;

		public Medicamento(string nome, string descricao, int quantidade)
		{

			this.nome = nome;
			this.descricao = descricao;
			this.quantidade = quantidade;

		}

		public bool temEstoque()
        {

			if(quantidade == 0)
				return true;
            else
				return false;

        }

		public override string ToString()
		{
			return "Id: " + id + Environment.NewLine +
				"Nome: " + nome + Environment.NewLine +
				"Descrição: " + descricao + Environment.NewLine +
				"Quantidade: " + quantidade + Environment.NewLine;
		}

        public void Retirar(int qtdCaixas)
        {
            quantidade -= qtdCaixas;
        }
    }
}
