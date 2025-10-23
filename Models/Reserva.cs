namespace DesafioProjetoHospedagem.Models
{
    public class Reserva
    {
        public List<Pessoa> Hospedes { get; set; }
        public Suite Suite { get; set; }
        public int DiasReservados { get; set; }

        public Reserva() { }

        public Reserva(int diasReservados)
        {
            DiasReservados = diasReservados;
        }

        public void CadastrarHospedes(List<Pessoa> hospedes)
        {
            // Valida capacidade da suíte antes de cadastrar hóspedes
            if (Suite == null)
                throw new InvalidOperationException("Suite não cadastrada para validar capacidade.");
            if (hospedes == null)
                throw new ArgumentNullException(nameof(hospedes));
            // Verifica se capacidade suporta a quantidade de hóspedes
            if (hospedes.Count <= Suite.Capacidade)
            {
                Hospedes = hospedes;
            }
            else
            {
                throw new ArgumentException("Capacidade da suíte menor que a quantidade de hóspedes.");
            }
        }

        public void CadastrarSuite(Suite suite)
        {
            if (suite == null)
                throw new ArgumentNullException(nameof(suite));
            Suite = suite;
        }

        public int ObterQuantidadeHospedes()
        {
            // Retorna a quantidade de hóspedes cadastrados
            return Hospedes?.Count ?? 0;
        }

        public decimal CalcularValorDiaria()
        {
            // Calcula o total: dias reservados x valor da diária, com desconto quando aplicável
            if (Suite == null)
                throw new InvalidOperationException("Suite não cadastrada para calcular valor.");
            if (DiasReservados <= 0)
                throw new ArgumentOutOfRangeException(nameof(DiasReservados), "DiasReservados deve ser maior que zero.");

            decimal valor = DiasReservados * Suite.ValorDiaria;

            // Desconto de 10% para reservas com 10 dias ou mais
            if (DiasReservados >= 10)
            {
                valor *= 0.9m; // aplica 10% de desconto
            }

            return valor;
        }
    }
}