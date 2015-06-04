using System.Collections.Generic;
using TPO.Common.DTOs;

namespace TPO.Services.Core
{
    public interface IMessageService
    {
        IEnumerable<MessageDto> GetMessagesByPrefix(string prefix);
        MessageDto GetMessageByName(string name);
    }
}
