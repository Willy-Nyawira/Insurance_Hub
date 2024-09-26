using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Domain.Entities;

namespace InsuranceHub.Application.RepositoryInterfaces
{
    public interface IInvoiceRepository
    {
        Task CreateInvoiceAsync(Invoice invoice);
        Task<Invoice> GetInvoiceByIdAsync(Guid invoiceId);
        Task<Guid> GetCustomerIdByUsernameAsync(string username);
    }
}
