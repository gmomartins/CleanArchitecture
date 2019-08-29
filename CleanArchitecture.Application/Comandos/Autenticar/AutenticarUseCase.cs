using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Repositorios;
using CleanArchitecture.Application.Seguranca;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Comandos.Autenticar
{
    public class AutenticarUseCase : IAutenticarUseCase
    {
        private readonly IClienteRepository clienteRepository;
        private readonly ITokenProvider tokenProvider;

        public AutenticarUseCase(IClienteRepository clienteRepository, ITokenProvider tokenProvider)
        {
            this.clienteRepository = clienteRepository;
            this.tokenProvider = tokenProvider;
        }

        public async Task<AutenticarResult> Execute(Cpf cpf, Senha senha)
        {
            var usuario = await this.clienteRepository.ObterPorCpf(cpf);

            if (usuario == null)
                throw new UsuarioOuSenhaInvalidosException();

            SenhaCriptografada senhaCriptografada = new SenhaCriptografada(senha);

            if (!usuario.Senha.Equals(senhaCriptografada))
                throw new UsuarioOuSenhaInvalidosException();

            return new AutenticarResult()
            {
                TokenAcesso = tokenProvider.GerarToken(cpf, usuario.Id),
                Usuario = cpf
            };
        }
    }
}
