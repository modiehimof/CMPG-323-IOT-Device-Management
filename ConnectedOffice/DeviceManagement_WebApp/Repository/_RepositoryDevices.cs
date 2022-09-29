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
    public class _RepositoryDevices
    {
        private readonly ConnectedOfficeContext _context;

        public _RepositoryDevices(ConnectedOfficeContext context)
        {
            _context = context;
        }

        public async Task<List<Device>> allDevices()
        {
            var connectedOfficeContext = _context.Device.Include(d => d.Category).Include(d => d.Zone);
            return await connectedOfficeContext.ToListAsync();
        }

        public Task<Device> DeviceDetails(Guid? id)
        {
            return _context.Device
                .Include(d => d.Category)
                .Include(d => d.Zone)
                .FirstOrDefaultAsync(m => m.DeviceId == id);
        }

        public async void DeviceCreate(Device device)
        {
            device.DeviceId = Guid.NewGuid();
            _context.Add(device);
            await _context.SaveChangesAsync();
        }

        public async void DeviceEdit(Guid id, Device device)
        {
            try
            {
                _context.Update(device);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(device.DeviceId))
                {
                    throw;
                }
            }
        }

        public async Task<Device> delete(Guid? id)
        {
             var device = await _context.Device
                .Include(d => d.Category)
                .Include(d => d.Zone)
                .FirstOrDefaultAsync(m => m.DeviceId == id);
            return device;
        }

        public async Task<Device> DeviceDelete(Guid id)
        {
            var device = await _context.Device.FindAsync(id);
            _context.Device.Remove(device);
            await _context.SaveChangesAsync();
            return device;
        }

        private bool DeviceExists(Guid id)
        {
            return _context.Device.Any(e => e.DeviceId == id);
        }

        public async Task<Device> DeviceGetOne(Guid? id)
        {
            return await _context.Device.FindAsync(id);
        }
    }
}