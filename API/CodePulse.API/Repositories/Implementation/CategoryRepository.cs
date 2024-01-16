using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCateogry = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCateogry is null)
            {
                return null;
            }

            dbContext.Categories.Remove(existingCateogry);
            await dbContext.SaveChangesAsync();
            return existingCateogry;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var exisitingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

            if (exisitingCategory != null)
            {
                dbContext.Entry(exisitingCategory).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync();
                return category;

            }
            return null;
        }


    }
}
