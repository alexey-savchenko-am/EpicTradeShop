﻿using Microsoft.EntityFrameworkCore;
using Persistence;
using Product.Application.Abstract;
using Product.Domain.Entities.ProductAggregate;

namespace Product.Infrastructure.Data.Repositories;

internal class ProductRepository
    : Repository<ProductAggregate>, IProductRepository
{
	public ProductRepository(DbContext dbContext)
		: base(dbContext)
	{}

    public Task<bool> IsProductExistsAsync(string productName)
    {
        return Set.AnyAsync(product => product.Name == productName);
    }
}
