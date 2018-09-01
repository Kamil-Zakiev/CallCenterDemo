using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.Requests.Dto;

namespace Domain.Interfaces.Requests
{
    public interface IRequestCreationService
    {
        void Create(NewRequestFormDto requestFormDto);
    }
}
