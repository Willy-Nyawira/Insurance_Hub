using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Application.RepositoryInterfaces;
using InsuranceHub.Domain.Entities;
using InsuranceHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InsuranceHub.Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateInvoiceAsync(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task<Invoice> GetInvoiceByIdAsync(Guid invoiceId)
        {
            return await _context.Invoices.FindAsync(invoiceId);
        }
        public async Task<Guid> GetCustomerIdByUsernameAsync(string username)
        {
            // Logic to retrieve customer ID from the database based on the username
            var customer = await _context.Customers
                .Where(c => c.Username == username)
                .FirstOrDefaultAsync();

            return customer?.Id ?? Guid.Empty; // Return Guid.Empty if not found
        }
        public async Task<Invoice> GetInvoiceByCheckoutRequestIdAsync(string checkoutRequestId)
        {
            return await _context.Invoices
                .Where(i => i.CheckoutRequestId == checkoutRequestId)
                .FirstOrDefaultAsync();
        }

        // Implementation of UpdateInvoiceAsync
        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();
        }
    }
}
