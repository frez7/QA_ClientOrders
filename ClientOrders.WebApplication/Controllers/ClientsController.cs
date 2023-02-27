using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClientOrders.Data.Database;
using ClientOrders.Data.Models.Entities;
using ClientOrders.Data.Repository;

namespace ClientOrders.WebApplication.Controllers
{
    public class ClientsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<Order> _orderRepository;

        public ClientsController(AppDbContext context, IRepository<Client> clientRepository, IRepository<Order> orderRepository)
        {
            _context = context;
            _clientRepository = clientRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(_clientRepository.GetAll());
        }
        public async Task<IActionResult> Orders(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            ViewData["ClientName"] = $"{client.FirstName} {client.SecondName}";
            var orders = await _context.Orders.Where(z => z.ClientID == id).ToListAsync();

            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            var orders = await _orderRepository.GetByClientIdAsync(id);
            ViewData["ClientName"] = $"{client.FirstName} {client.SecondName}";
            client.OrderAmount = orders.ToList().Count;
            _context.SaveChanges();

            return View(client);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,SecondName,PhoneNum,Id")] Client client)
        {
            if (ModelState.IsValid)
            {
                _clientRepository.Add(client);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["ClientName"] = $"{client.FirstName} {client.SecondName}";
            _clientRepository.Update(client);
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,SecondName,PhoneNum,DateAdd,Id")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _clientRepository.Update(client);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clients == null)
            {
                return Problem("Entity set 'AppDbContext.Clients'  is null.");
            }
            var client = await _context.Clients.FindAsync(id);
            var orders = await _orderRepository.GetOrdersByClientIdAsync(id);
            if (client != null)
            {
                _clientRepository.Delete(client);
                foreach (var order in orders)
                {
                    _orderRepository.Delete(order);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
          return _context.Clients.Any(e => e.Id == id);
        }
    }
}
