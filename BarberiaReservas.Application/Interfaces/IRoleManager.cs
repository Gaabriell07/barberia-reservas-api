using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberiaReservas.Application.Interfaces;

public interface IRoleManager
{
    bool IsValidRole(string role);
    Task<bool> PromoteToAdminAsync(int userId);
    Task<bool> DemoteToClientAsync(int userId);
}
