using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloRequisicao;
using System;

namespace ControleMedicamentos.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Notificador notificador = new Notificador();
            TelaMenuPrincipal menuPrincipal = new TelaMenuPrincipal(notificador);

            while (true)
            {
                TelaBase telaSelecionada = menuPrincipal.ObterTela();

                if (telaSelecionada is null)
                    return;

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();

                if (telaSelecionada is TelaCadastroRequisicao)
                    GerenciarCadastroRequisicoes(telaSelecionada, opcaoSelecionada);

                else if (telaSelecionada is ITelaCadastravel)
                {
                    ITelaCadastravel telaCadastravel = (ITelaCadastravel)telaSelecionada;

                    if (opcaoSelecionada == "1")
                        telaCadastravel.Inserir();

                    else if (opcaoSelecionada == "2")
                        telaCadastravel.Editar();

                    else if (opcaoSelecionada == "3")
                        telaCadastravel.Excluir();

                    else if (opcaoSelecionada == "4")
                        telaCadastravel.VisualizarRegistros("Tela");
                }
            }
        }

        private static void GerenciarCadastroRequisicoes(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaCadastroRequisicao telaCadastroRequisicao = telaSelecionada as TelaCadastroRequisicao;

            if (telaCadastroRequisicao is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroRequisicao.RegistrarRequisicao();
            else if (opcaoSelecionada == "2")
                telaCadastroRequisicao.VisualizarRequicoes("Tela");
        }
    }
}

