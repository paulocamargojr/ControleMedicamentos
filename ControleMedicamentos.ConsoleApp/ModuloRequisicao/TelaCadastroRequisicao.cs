using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleMedicamentos.ConsoleApp.ModuloPaciente;

namespace ControleMedicamentos.ConsoleApp.ModuloRequisicao
{
    public class TelaCadastroRequisicao : TelaBase
    {

        private readonly Notificador _notificador;
        private readonly IRepositorio<Requisicao> _repositorioRequisicao;
        private readonly IRepositorio<Medicamento> _repositorioMedicamento;
        private readonly IRepositorio<Paciente> _repositorioPaciente;
        private readonly TelaCadastroMedicamento _telaCadastroMedicamento;
        private readonly TelaCadastroPaciente _telaCadastroPaciente;

        public TelaCadastroRequisicao(RepositorioRequisicao repositorioRequisicao, Notificador notificador,
            IRepositorio<Medicamento> repositorioMedicamento,
            IRepositorio<Paciente> repositorioPaciente,
            TelaCadastroMedicamento telaCadastroMedicamento,
            TelaCadastroPaciente telaCadastroPaciente) : base("Cadastro Requisição")
        {

            _repositorioRequisicao = repositorioRequisicao;
            _repositorioMedicamento = repositorioMedicamento;
            _repositorioPaciente = repositorioPaciente;
            _telaCadastroMedicamento = telaCadastroMedicamento;
            _telaCadastroPaciente = telaCadastroPaciente;
            _notificador = notificador;

        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para solicitar requisição");
            Console.WriteLine("Digite 2 para visualizar as requisições");
            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;

        }

        public void RegistrarRequisicao()
        {
            MostrarTitulo("Inserindo novo Requisição");

            Paciente pacienteSelecionado = ObtemPaciente();

            if (pacienteSelecionado == null)
            {
                _notificador.ApresentarMensagem("Nenhum paciente selecionado", TipoMensagem.Erro);
                return;
            }

            Medicamento medicamentoSelecionado = ObtemMedicamento();

            if (medicamentoSelecionado.temEstoque())
            {
                _notificador.ApresentarMensagem("Medicamento esgotado", TipoMensagem.Erro);
                return;
            }

            Console.WriteLine("Digite a quantidade de caixas: ");
            int qtdCaixas = Convert.ToInt32(Console.ReadLine());

            Requisicao requisicao = ObtemRequisicao(pacienteSelecionado, medicamentoSelecionado);
            requisicao.atualizarEstoque(qtdCaixas);

            requisicao.aprovada = true;

            string statusValidacao = _repositorioRequisicao.Inserir(requisicao);

            if (statusValidacao == "REGISTRO_VALIDO")
                _notificador.ApresentarMensagem("A requisicao foi completada com sucesso!", TipoMensagem.Sucesso);
            else
                _notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        private Paciente ObtemPaciente()
        {
            bool temPacientesDisponiveis = _telaCadastroPaciente.VisualizarRegistros("Pesquisando");

            if (!temPacientesDisponiveis)
            {
                _notificador.ApresentarMensagem("Não há nenhum paciente disponível para cadastrar requisicao.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do id do paciente que está realizando a requisição: ");
            int numeroPacienteRequisicao = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Paciente pacienteSelecionado = _repositorioPaciente.SelecionarRegistro(numeroPacienteRequisicao);

            return pacienteSelecionado;
        }

        private Medicamento ObtemMedicamento()
        {
            bool temEstoqueMedicamento = _telaCadastroMedicamento.VisualizarRegistros("Pesquisando");

            if (!temEstoqueMedicamento)
            {
                _notificador.ApresentarMensagem("Sem estoque de medicamentos", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do medicamento que está sendo solicitado: ");
            int numeroMedicamentoRequisicao = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Medicamento medicamentoRequisitado = _repositorioMedicamento.SelecionarRegistro(numeroMedicamentoRequisicao);

            return medicamentoRequisitado;
        }

        private Requisicao ObtemRequisicao(Paciente paciente, Medicamento medicamento)
        {
            Requisicao novaRequisicao = new Requisicao(paciente, medicamento);

            return novaRequisicao;
        }

        public bool VisualizarRequicoes(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Requisições");

            List<Requisicao> requisicoes = _repositorioRequisicao.SelecionarTodos();

            if (requisicoes.Count == 0)
            {
                _notificador.ApresentarMensagem("Não há nenhum empréstimo disponível", TipoMensagem.Atencao);
                return false;
            }

            foreach (Requisicao requisicao in requisicoes)
                Console.WriteLine(requisicao.ToString());

            Console.ReadLine();

            return true;
        }

    }
}
