using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services
{
    public interface ILockerService
    {
        Task<bool> OpenLockerAsync();
    }

}
