using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleMedicamentos.ConsoleApp.Compartilhado;

namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    public class TelaCadastroPaciente : TelaBase, ITelaCadastravel
    {

        private readonly RepositorioPaciente _repositorioPaciente;
        private readonly Notificador _notificador;

        public TelaCadastroPaciente(RepositorioPaciente repositorioPaciente, Notificador notificador) : base("Cadastro Pacientes")
        {

            _repositorioPaciente = repositorioPaciente;
            _notificador = notificador;

        }

        public void Inserir()
        {

            MostrarTitulo("Cadastro de pacientes");

            Paciente novoPaciente = ObterPaciente();

            _repositorioPaciente.Inserir(novoPaciente);

            _notificador.ApresentarMensagem("Paciente cadastrado com sucesso!", TipoMensagem.Sucesso);

        }

        public void Editar()
        {
            MostrarTitulo("Editando Medicamento");

            bool temPacientesCadastrados = VisualizarRegistros("Pesquisando");

            if (temPacientesCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum Medicamento cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroPaciente = ObterNumeroRegistro();

            Paciente pacienteAtualizado = ObterPaciente();

            bool conseguiuEditar = _repositorioPaciente.Editar(numeroPaciente, pacienteAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Paciente editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Paciente");

            bool temMedicamentosRegistrados = VisualizarRegistros("Pesquisando");

            if (temMedicamentosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum Paciente cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroMedicamento = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioPaciente.Excluir(numeroMedicamento);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Paciente excluído com sucesso1", TipoMensagem.Sucesso);
        }

        private Paciente ObterPaciente()
        {
            Console.WriteLine("Digite o nome do paciente: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite telefone do paciente: ");
            string telefone = Console.ReadLine();

            Console.WriteLine("Digite o endereço do paciente: ");
            string endereco = Console.ReadLine();

            return new Paciente(nome, telefone, endereco);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Pacientes");

            List<Paciente> pacientes = _repositorioPaciente.SelecionarTodos();

            if (pacientes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum paciente disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Paciente paciente in pacientes)
                Console.WriteLine(paciente.ToString());

            Console.ReadLine();

            return true;
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do paciente que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioPaciente.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do paciente não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

    }
}
