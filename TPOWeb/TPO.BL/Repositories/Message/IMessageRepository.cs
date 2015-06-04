using System.Collections.Generic;
using TPO.Domain;
using TPO.Domain.DTO;

namespace TPO.BL.Repositories.Message
{
    public interface IMessageRepository
    {
        IEnumerable<MessageDTO> GetMessagesByPrefix(string prefix);
        MessageDTO GetMessageByName(string name);
    }
}