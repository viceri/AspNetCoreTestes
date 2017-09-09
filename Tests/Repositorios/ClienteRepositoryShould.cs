﻿using Domain.Models;
using Infra.Repositories.Contexts;
using Repositories;
using System.Linq;
using Xunit;

namespace Tests.Repositorios
{
    public class ClienteRepositoryShould
    {
        
        [Fact]
        [Trait("Integration", "")]
        [Trait("Repositorios", "")]
        public async void CriarObterAtualizarExcluirCliente()
        {
            // O certo é ter um teste por método!
            using (var context = new LojaContext(ContextOptions<LojaContext>.GetOptions()))
            {
                var repo = new ClienteRepository(context);
                var cliente = new Cliente("Fernando");
                await repo.Insert(cliente);

                Assert.NotEqual(default(int), cliente.Id);

                var clienteObter = await repo.GetById(cliente.Id);

                Assert.Equal("Fernando", clienteObter.Nome);

                cliente.UpdateInfo("Barbieri");

                await repo.Update(cliente);

                var lista = await repo.GetAllBy(c => c.Nome.StartsWith("Barb"));

                var clienteAtualizado = lista.FirstOrDefault();

                Assert.NotNull(clienteAtualizado.Nome);

                Assert.Equal("Barbieri", clienteAtualizado.Nome);
                
                await repo.Delete(cliente);

                var clienteExcluido = await repo.GetById(cliente.Id);

                Assert.Null(clienteExcluido);

            }
        }
    }
}
