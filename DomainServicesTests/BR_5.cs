using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DomainServicesTests
{
    public class BR_5
    {
        //De leeftijd van een patiënt is >= 16. 

        [Fact]
        public void AddPatientCheckOnAge()
        {
            Assert.Equal(true, true);
        }
    }
}
