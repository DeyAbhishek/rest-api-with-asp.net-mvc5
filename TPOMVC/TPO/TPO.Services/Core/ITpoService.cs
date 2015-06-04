using System.Collections.Generic;

namespace TPO.Services.Core
{
    interface ITpoService<TDto> where TDto : class 
    {
        int Add(TDto dto);
        List<TDto> GetAll();
        TDto Get(int id);
        void Delete(int id);
        void Update(TDto dto);
        void Dispose();
    }
}
