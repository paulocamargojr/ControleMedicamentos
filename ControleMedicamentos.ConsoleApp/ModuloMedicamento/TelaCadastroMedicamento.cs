using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ControleMedicamentos.ConsoleApp.ModuloMedicamento
{

    public class TelaCadastroMedicamento : TelaBase, ITelaCadastravel
    {

        private readonly RepositorioMedicamentos _repositorioMedicamento;
        private readonly Notificador _notificador;

        public TelaCadastroMedicamento(RepositorioMedicamentos repositorioMedicamento, Notificador notificador) : base("Cadastro Medicamentos")
        {

            _repositorioMedicamento = repositorioMedicamento;
            _notificador = notificador;

        }

        public void Inserir()
        {

            MostrarTitulo("Cadastro de medicamentos");

            Medicamento novoMedicamento = ObterMedicamento();

            _repositorioMedicamento.Inserir(novoMedicamento);

            _notificador.ApresentarMensagem("Medicamento cadastrado com sucesso!", TipoMensagem.Sucesso);

        }

        public void Editar()
        {
            MostrarTitulo("Editando Medicamento");

            bool temFornecedoresCadastrados = VisualizarRegistros("Pesquisando");

            if (temFornecedoresCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum Medicamento cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroMedicamento = ObterNumeroRegistro();

            Medicamento fornecedorAtualizado = ObterMedicamento();

            bool conseguiuEditar = _repositorioMedicamento.Editar(numeroMedicamento, fornecedorAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Medicamento editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Medicamento");

            bool temMedicamentosRegistrados = VisualizarRegistros("Pesquisando");

            if (temMedicamentosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum Medicamento cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroMedicamento = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioMedicamento.Excluir(numeroMedicamento);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Medicamento excluído com sucesso1", TipoMensagem.Sucesso);
        }

        private Medicamento ObterMedicamento()
        {
            Console.WriteLine("Digite o nome do medicamento: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite a descrição do medicamento: ");
            string descricao = Console.ReadLine();

            Console.WriteLine("Digite a quantidade do medicamento: ");
            int quantidade = Convert.ToInt32(Console.ReadLine());

            return new Medicamento(nome, descricao, quantidade);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Medicamentos");

            List<Medicamento> medicamentos = _repositorioMedicamento.SelecionarTodos();

            if (medicamentos.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum medicamento disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Medicamento medicamento in medicamentos)
                Console.WriteLine(medicamento.ToString());

            Console.ReadLine();

            return true;
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do medicamento que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioMedicamento.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do medicamento não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

    }
}
