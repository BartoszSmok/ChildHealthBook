using ChildHealthBook.Common.WebDtos.ChildDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Child.API.DAL
{
    public interface IChildRepository
    {
        Task<IEnumerable<ChildReadDto>> GetAllChildren();
        Task AddNewChild(ChildCreateDto childCreateDto);
    }
}
