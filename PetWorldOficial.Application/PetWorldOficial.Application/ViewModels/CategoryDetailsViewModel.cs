using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Application.ViewModels
{
    public record CategoryDetailsViewModel(
        int Id,
        string Title,
        string Type);
}
