namespace DesafioProjetoHospedagem.Models
{
    public class Suite
    {
        public Suite() { }

        public Suite(string tipoSuite, int capacidade, decimal valorDiaria)
        {
            if (capacidade <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacidade), "Capacidade deve ser maior que zero.");
            if (valorDiaria <= 0)
                throw new ArgumentOutOfRangeException(nameof(valorDiaria), "Valor da diária deve ser maior que zero.");

            TipoSuite = tipoSuite;
            Capacidade = capacidade;
            ValorDiaria = valorDiaria;
        }

        public string TipoSuite { get; set; }
        public int Capacidade { get; set; }
        public decimal ValorDiaria { get; set; }
    }
}