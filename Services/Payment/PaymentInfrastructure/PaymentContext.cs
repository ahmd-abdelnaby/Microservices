using Microsoft.EntityFrameworkCore;
using PaymentDomain.Entities;
using PaymentDomain.Interfaces;

namespace PaymentInfrastructure
{
    public class PaymentContext : DbContext, IPaymentContext
    {

        public PaymentContext(DbContextOptions<PaymentContext> options): base(options)
        {
        }
        public virtual DbSet<Payment> Payments { get; set; }
    }
        
    }
