using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Interfaces
{
    public interface IAccountRepository
    {
        public void AddAccount(Account account);
        public void DeleteAccount(Account account);
        public void UpdateAccount(Account account);
        public Account GetAccount(string email);
        public bool CheckPassword(string email, string password);
    }
}
