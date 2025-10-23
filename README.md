# Sistema de Hospedagem (Desafio DIO .NET)

Este projeto implementa um sistema de hospedagem simples em C#, seguindo o desafio da trilha .NET (Explorando a linguagem C#) da DIO.

## Sobre o Projeto
- Modelos centrais: `Pessoa`, `Suite` e `Reserva`.
- Regras de negócio implementadas conforme o enunciado.
- Aplicação de console com menu interativo para cadastro e cálculo.
- Cultura `pt-BR` aplicada para formatação de moeda e números.

![Diagrama de classe hotel](diagrama_classe_hotel.png)

## Funcionalidades
- Cadastrar suíte (tipo, capacidade e valor da diária).
- Adicionar hóspedes (nome e sobrenome opcional).
- Definir dias da reserva.
- Exibir resumo e calcular o valor total da estadia, com desconto quando aplicável.
- Limpar dados para iniciar novo cadastro.

## Regras e Validações Implementadas
- Capacidade da suíte não pode ser menor que a quantidade de hóspedes.
- `ObterQuantidadeHospedes()` retorna o total de hóspedes cadastrados.
- `CalcularValorDiaria()` retorna `DiasReservados x ValorDiaria`.
- Desconto de 10% aplicado quando `DiasReservados >= 10`.
- Validações adicionais de robustez:
  - Suite obrigatória para cadastrar hóspedes e calcular valor.
  - `DiasReservados` deve ser maior que zero.
  - Construtor de `Suite` valida `capacidade > 0` e `valorDiaria > 0`.
  - `Pessoa.NomeCompleto` evita espaços extras quando `Sobrenome` está vazio/nulo.

## Estrutura das Classes
- `Pessoa`: `Nome`, `Sobrenome` e `NomeCompleto` (em maiúsculas, sem espaços extras).
- `Suite`: `TipoSuite`, `Capacidade`, `ValorDiaria` (com validações de argumentos).
- `Reserva`: `Hospedes`, `Suite`, `DiasReservados`, métodos `CadastrarHospedes`, `CadastrarSuite`, `ObterQuantidadeHospedes` e `CalcularValorDiaria`.

## Como Executar
Pré-requisitos: .NET SDK instalado.

No diretório do projeto, execute:
```bash
dotnet run
```
Siga as opções do menu no console:
- `1) Cadastrar suíte`
- `2) Adicionar hóspedes`
- `3) Definir dias da reserva`
- `4) Exibir resumo e calcular valor`
- `5) Limpar dados`
- `0) Sair`

## Exemplo de Saída (Resumo)
```
Suíte: Premium (capacidade 2)
Hóspedes (2): HÓSPEDE 1, HÓSPEDE 2
Dias reservados: 5
Valor total da estadia: R$ 150,00
```
Desconto aplicado quando `DiasReservados >= 10` (ex.: 10 dias, diária R$ 30,00 → total R$ 270,00).

## Erros Comuns Tratados
- Capacidade da suíte menor que a quantidade de hóspedes.
- Suite não cadastrada para cadastrar hóspedes ou calcular valor.
- `DiasReservados` menor ou igual a zero.
- `valorDiaria` ou `capacidade` inválidos ao criar a suíte.
- Entradas inválidas no console (novas tentativas até receber valores válidos).

## Arquivos Principais
- `Models/Pessoa.cs`: modelo de hóspede e formatação de nome completo.
- `Models/Suite.cs`: modelo de suíte com validação de argumentos.
- `Models/Reserva.cs`: regras da reserva, validações e cálculo do valor.
- `Program.cs`: menu interativo, validações de entrada e resumo.

## Próximos Passos (Opcional)
- Limite de cadastro de hóspedes com base na suíte durante a inclusão.
- Melhorias nas mensagens e internacionalização.
- Testes unitários para regras críticas (xUnit/NUnit).

