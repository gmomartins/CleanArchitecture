using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Domain.ValueObjects;
using CleanArchitecture.Infrastructure.EntityFrameworkDataAccess;
using CleanArchitecture.Infrastructure.EntityFrameworkDataAccess.Entities;

namespace CleanArchitecture.Infrastructure
{
    public class ContextInitializer
    {
        static bool Inicializado = false;

        public ContextInitializer()
        {

        }

        public void InicializarComDadosFake(Context context)
        {
            if (!Inicializado)
                InicializarContext(context);
        }

        private void InicializarContext(Context context)
        {
            AbrirConta(context, Guid.Parse("55365557-fba0-40e3-b8e1-638ff5dbbc23"), Guid.Parse("a09b81c1-5f55-42ca-98b0-309b27dded52"), "77788899911", "José da silva", "minh@senh@", "123", "456321", "5", 100);

            AbrirConta(context, Guid.Parse("9404ade2-c181-4770-b03a-fb1476826876"), Guid.Parse("857df84e-cb39-4f93-b6f7-dbbdc410d4ed"), "777888555522", "João da silva", "minh@senh@", "123", "785632", "4", 50);

            context.SaveChanges();

            Inicializado = true;
        }

        private void AbrirConta(Context context, Guid clienteId, Guid contaId, Cpf cpf, Nome nome, Senha senha, NumeroAgencia numeroAgencia, NumeroConta numeroConta, DigitoConta digitoConta, Valor saldoInicial)
        {

            context.Clientes.Add(new Cliente()
            {
                Cpf = cpf,
                Nome = nome,
                Senha = new SenhaCriptografada(senha),
                Id = clienteId
            });

            context.Contas.Add(new ContaCorrente()
            {
                ClienteId = clienteId,
                DigitoConta = digitoConta,
                Id = contaId,
                NumeroAgencia = numeroAgencia,
                NumeroConta = numeroConta
            });

            context.Creditos.Add(new Credito()
            {
                ContaId = contaId,
                DataTransacao = DateTime.UtcNow,
                TransacaoId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                Valor = saldoInicial
            });
        }
    }
}
