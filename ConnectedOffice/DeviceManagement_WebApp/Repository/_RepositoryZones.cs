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
    public class _RepositoryZones
    {
        private readonly ConnectedOfficeContext _context;

        public _RepositoryZones(ConnectedOfficeContext context)
        {
            _context = context;
        }

        public Task<List<Zone>> allZones()
        {
            return _context.Zone.ToListAsync();
        }

        public Task<Zone> ZoneDetails(Guid? id)
        {
            return _context.Zone
                .FirstOrDefaultAsync(m => m.ZoneId == id);
        }

        public async void zoneCreate(Zone zone)
        {
            zone.ZoneId = Guid.NewGuid();
            _context.Add(zone);
            await _context.SaveChangesAsync();
        }

        public async void ZoneEdit(Guid id, Zone zone)
        {
            
            try
            {
                _context.Update(zone);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException a)
            {
                if (!ZoneExists(zone.ZoneId))
                {
                    throw;
                }
                
            }
        }

        public async Task<Zone> ZoneDelete(Guid? id)
        {
            var zone = await _context.Zone.FindAsync(id);
            _context.Zone.Remove(zone);
            await _context.SaveChangesAsync();
            return zone;
        }

        private bool ZoneExists(Guid id)
        {
            return _context.Zone.Any(e => e.ZoneId == id);
        }

        public Task<Zone> ZoneGetOne(Guid? id)
        {
            return _context.Zone
                .FirstOrDefaultAsync(m => m.ZoneId == id);
        }
    }
}