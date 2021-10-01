using Domain;
using DomainServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFInfrastructure
{
    class DBAccountRepository : IAccountRepository
    {
        private readonly FysioDbContext _context;
        public DBAccountRepository(FysioDbContext context)
        {
            _context = context;
        }

        public void AddAccount(Account account)
        {
            _context.Add(account);
            _context.SaveChanges();
        }

        public bool CheckPassword(string email, string password)
        {
            Account acc = _context.Accounts.Where(p => p.Email == email).FirstOrDefault();
            return acc?.Password == password;
        }

        public void DeleteAccount(Account account)
        {
            _context.Remove(account);
            _context.SaveChanges();
        }

        public Account GetAccount(string email)
        {
            return _context.Accounts.Where(p => p.Email == email).FirstOrDefault();
        }

        public void UpdateAccount(Account account)
        {
            Account old = _context.Accounts.Where(p => p.Email == account.Email).FirstOrDefault();
            old = account;
            _context.SaveChanges();
        }
    }
}
