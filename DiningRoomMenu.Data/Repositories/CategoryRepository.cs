﻿using System.Linq;
using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Contracts.Repositories;

namespace DiningRoomMenu.Data.Repositories
{
    class CategoryRepository : RepositoryBase<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(DiningRoomMenuDbContext context)
            : base(context) { }

        public CategoryEntity Get(string name)
        {
            return context.Categories.SingleOrDefault(category => category.Name == name);
        }
    }
}
