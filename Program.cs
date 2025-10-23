using System;
using System.Text;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using DesafioProjetoHospedagem.Models;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = new CultureInfo("pt-BR");
CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");

// Sistema de Hospedagem - Menu Interativo
List<Pessoa> hospedes = new();
Suite? suite = null;
int diasReservados = 0;

Console.WriteLine("Sistema de Hospedagem - Menu Interativo");
bool sair = false;
while (!sair)
{
    Console.WriteLine("\nMenu:");
    Console.WriteLine("1) Cadastrar suíte");
    Console.WriteLine("2) Adicionar hóspedes");
    Console.WriteLine("3) Definir dias da reserva");
    Console.WriteLine("4) Exibir resumo e calcular valor");
    Console.WriteLine("5) Limpar dados");
    Console.WriteLine("0) Sair");
    Console.Write("Escolha: ");
    var opc = Console.ReadLine();

    switch (opc)
    {
        case "1":
            try
            {
                var tipo = Prompt("Tipo da suíte: ");
                int capacidade = PromptInt("Capacidade da suíte (maior que zero): ", min: 1);
                decimal valorDiaria = PromptDecimal("Valor da diária (R$): ", min: 0.01m);
                suite = new Suite(tipo, capacidade, valorDiaria);
                Console.WriteLine($"Suíte cadastrada: {suite.TipoSuite} (capacidade {suite.Capacidade}, diária {suite.ValorDiaria.ToString("C")})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cadastrar suíte: {ex.Message}");
            }
            break;

        case "2":
            string addMais;
            do
            {
                string nome = Prompt("Nome do hóspede: ");
                string sobrenome = Prompt("Sobrenome do hóspede (opcional): ", allowEmpty: true);
                hospedes.Add(string.IsNullOrWhiteSpace(sobrenome) ? new Pessoa(nome) : new Pessoa(nome, sobrenome));
                Console.WriteLine($"Adicionado: {hospedes.Last().NomeCompleto}");
                addMais = Prompt("Adicionar outro? (s/n): ").Trim().ToLower();
            } while (addMais == "s");
            Console.WriteLine($"Total de hóspedes cadastrados: {hospedes.Count}");
            break;

        case "3":
            diasReservados = PromptInt("Dias da reserva (maior que zero): ", min: 1);
            Console.WriteLine($"Dias definidos: {diasReservados}");
            break;

        case "4":
            try
            {
                if (suite == null)
                {
                    Console.WriteLine("Cadastre a suíte antes de calcular.");
                    break;
                }
                if (hospedes.Count == 0)
                {
                    Console.WriteLine("Adicione pelo menos um hóspede antes de calcular.");
                    break;
                }
                var reserva = new Reserva(diasReservados);
                reserva.CadastrarSuite(suite);
                reserva.CadastrarHospedes(hospedes);
                var valor = reserva.CalcularValorDiaria();

                Console.WriteLine("\nResumo da Reserva");
                Console.WriteLine($"Suíte: {suite.TipoSuite} (capacidade {suite.Capacidade})");
                Console.WriteLine($"Hóspedes ({reserva.ObterQuantidadeHospedes()}): {string.Join(", ", hospedes.Select(h => h.NomeCompleto))}");
                Console.WriteLine($"Dias reservados: {reserva.DiasReservados}");
                Console.WriteLine($"Valor total da estadia: {valor.ToString("C")}");
                if (reserva.DiasReservados >= 10) Console.WriteLine("Desconto de 10% aplicado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao calcular: {ex.Message}");
            }
            break;

        case "5":
            hospedes.Clear();
            suite = null;
            diasReservados = 0;
            Console.WriteLine("Dados limpos.");
            break;

        case "0":
            sair = true;
            break;

        default:
            Console.WriteLine("Opção inválida.");
            break;
    }
}

// Funções auxiliares (comentários principais)
string Prompt(string mensagem, bool allowEmpty = false)
{
    while (true)
    {
        Console.Write(mensagem);
        var s = Console.ReadLine() ?? "";
        if (allowEmpty || !string.IsNullOrWhiteSpace(s)) return s.Trim();
        Console.WriteLine("Entrada obrigatória. Tente novamente.");
    }
}

int PromptInt(string mensagem, int min = int.MinValue, int max = int.MaxValue)
{
    while (true)
    {
        Console.Write(mensagem);
        var s = Console.ReadLine();
        if (int.TryParse(s, out var v) && v >= min && v <= max) return v;
        Console.WriteLine("Valor inválido. Tente novamente.");
    }
}

decimal PromptDecimal(string mensagem, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
{
    while (true)
    {
        Console.Write(mensagem);
        var s = Console.ReadLine();
        if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.CurrentCulture, out var v) && v >= min && v <= max) return v;
        Console.WriteLine("Valor inválido. Tente novamente.");
    }
}