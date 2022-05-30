using Descarta2.Context;
using Descarta2.Models;
using Microsoft.EntityFrameworkCore;

namespace Descarta2.Repository
{
    public class DiffRepository
    {
        //Creating instance of DatabaseContext
        private readonly DiffContext _context;

        public DiffRepository(DiffContext diffContext)
        {
            _context = diffContext;
        }
        //Method to add or update item in base
        public async Task<bool> AddOrUpdate(DiffItem item)
        {
            try
            {
                DiffItem foundItem = await Select(item.Id, item.Position);

                if (foundItem == null)
                {
                    _context.Add(item);
                }
                else
                {
                    foundItem.Data = item.Data;
                }

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }
        //Method for saving changes in database
        public void Save()
        {
            _context.SaveChanges();
        }
        //Method for selecting data from database
        public async Task<DiffItem> Select(int id, string position)
        {
            return await _context.DiffItems.
                                  Where(n => n.Id == id && n.Position == position).
                                  FirstOrDefaultAsync();
        }
    }
}
