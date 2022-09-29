using System.Security.AccessControl;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;

namespace DeviceManagement_WebApp.Repository
{
    public class _RepositoryCategories
    {
        private readonly ConnectedOfficeContext _context;

        public _RepositoryCategories(ConnectedOfficeContext context)
        {
            _context = context;
        }

        public Task<List<Category>> allCategories()
        {
            return _context.Category.ToListAsync();
        }

        public Task<Category> categoryDetails(Guid? id)
        {
            return _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
        }

        public async void categoryCreate(Category category)
        {
            category.CategoryId = Guid.NewGuid();
            _context.Add(category);
            await _context.SaveChangesAsync();
        }

        public async void CategoryEdit(Guid id, Category category)
        {
            
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException a)
            {
                if (!CategoryExists(category.CategoryId))
                {
                    throw;
                }
                
            }
        }

        public async Task<Category> categoryDelete(Guid? id)
        {
            var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }

        public Task<Category> CategoryGetOne(Guid? id)
        {
            return _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
        }
    }
}