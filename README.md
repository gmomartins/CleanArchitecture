# Clean Architecture

## O que utilizei

.NET Core 2.1

xUnit

Autenticação JWT

Entity Framework Core (InMemory)

Swagger

Padrões:

1. Modelo Rico

2. [Objeto sempre Válido] (http://codebetter.com/gregyoung/2009/05/22/always-valid/)


## Como o projeto está dividido

Projeto "CleanArchitecture.Domain" contém as entidades, value objects, etc.

Projeto "CleanArchitecture.Application" contém os UseCases do projeto, onde cada use case representa uma funcionalidade no sistema e também as interfaces utilizadas no sistema (Ports).

Projeto "CleanArchitecture.Infrastructure" cotém as implementações do repositórios (Adpaters).

Projeto "CleanArchitecture.Framework" contém classes de suporte para o funcionamento do sistema, por exemplo: Geração de Token, Tratamento de exceptions, etc.

Projeto "CleanArchitecture.Autenticacao.Api" é o microserviço de autenticação, possibilita autenticar um cliente.

Projeto "CleanArchitecture.Contas.Api" é o microserviço de contas, permite abrir uma conta, fazer um crédito, fazer um débito, fazer uma transferência e obter detalhes de uma conta.

## Segurança

O microserviço de contas está protegido por uma autenticação de token que utiliza o padrão JWT e além disso os use cases CreditarUseCase, DebitarUseCase, DetalharContaUseCase e TransferirUseCase validam se o usuário logado é o proprietário da conta.

## Como testar

Os 2 micro serviços estão com o swagger configurado basta rodar os projetos.

Eu já deixei criado 2 clientes e 2 contas para realizar testes, permitindo testar o microserviço de contas sem criar uma nova conta corrente.

###### Cliente 1: ######

clienteId:55365557-fba0-40e3-b8e1-638ff5dbbc23

cpf: 77788899911

nome: José da silva

senha: minh@senh@

###### Cliente 2: ######

clienteId:9404ade2-c181-4770-b03a-fb1476826876

cpf: 777888555522

nome: João da silva

senha: minh@senh@

###### Conta Corrente 1 (Pertence ao Cliente 1) com saldo de 100 reais: ######

contaId: a09b81c1-5f55-42ca-98b0-309b27dded52

numeroAgencia:123

numeroConta:456321

digitoConta: 5

###### Conta Corrente 2 (Pertence ao Cliente 2) com saldo de 50 reais: ######

contaId: 857df84e-cb39-4f93-b6f7-dbbdc410d4ed

numeroAgencia:123

numeroConta:785632

digitoConta: 4


