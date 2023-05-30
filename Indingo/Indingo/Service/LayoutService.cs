using Indingo.DAL;
using Indingo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Indingo.Service
{
    public class LayoutService
    {
        private readonly AppDbContext _contex;

        public LayoutService(AppDbContext contex)
        {
            _contex = contex;
        }
        public async Task<Dictionary<string, string>> GetSettings()
        {
            Dictionary<string, string?> settings = await _contex.Settings.ToDictionaryAsync(s => s.Key, s => s.Value);
            return settings;
        }

    }
}
