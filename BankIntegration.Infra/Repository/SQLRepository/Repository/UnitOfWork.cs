﻿using BankIntegration.Infra.Repository.SQLRepository.Persistance;
using BankIntegration.Infra.Repository.SQLRepository.RepositoryInterface;
using SOS.Infrastructure.Repository.Interface;

namespace BankIntegration.Infra.Repository.SQLRepository.Repository;

public class UnitOfWork : IUnitOfWork , IDisposable
{
    
    public IProductRepository ProductRepository { get; }
    public IProductApiKeyRepository ProductApiKeyRepository { get; }
    public IPeopleRepository PeopleRepository { get; }

    private readonly ApplicationDbContext _context;
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        ProductRepository = new ProductRepository(_context);
        ProductApiKeyRepository = new ProductApiKeyRepository(_context);
        PeopleRepository = new PeopleRepository(_context);
    }



    public async Task<bool> CompleteASync()
    {
        var result = await _context.SaveChangesAsync();
        return true ? result > 0 : false;
    }

    public void Dispose()
    {
        _context.DisposeAsync();
    }
}