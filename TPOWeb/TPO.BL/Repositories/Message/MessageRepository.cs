using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPO.DL.Models;
using TPO.Domain.DTO;

namespace TPO.BL.Repositories.Message
{
    public class MessageRepository : IMessageRepository
    {
        #region Variables
        #endregion // Variables

        #region Properties
        #endregion // Properties

        #region Constructors
        #endregion // Constructors

        public static void AddResourceFile(string applicationName, string fileName, int priority)
        {
            TPO.DL.Repositories.MessageRepository.AddResourceFile(applicationName, fileName, priority);
        }

        public static bool RemoveResourceFile(string applicationName, string fileName)
        {
            return TPO.DL.Repositories.MessageRepository.RemoveResourceFile(applicationName, fileName);
        }

        public IEnumerable<MessageDTO> GetMessagesByPrefix(string prefix)
        {
            TPO.DL.Repositories.MessageRepository dlMessageRepository = new TPO.DL.Repositories.MessageRepository();

            return CreateList(dlMessageRepository.GetMessagesByPrefix(prefix));
        }

        public MessageDTO GetMessageByName(string name)
        {
            TPO.DL.Repositories.MessageRepository dlMessageRepository = new TPO.DL.Repositories.MessageRepository();

            return MapToDTO(dlMessageRepository.GetMessageByName(name));
        }

        public static string GetStringValue(string name)
        {
            TPO.DL.Repositories.MessageRepository dlMessageRepository = new TPO.DL.Repositories.MessageRepository();

            return dlMessageRepository.GetMessageStringByName(name);
        }

        private static List<MessageDTO> CreateList(IEnumerable<IMessage> items)
        {
            return items.Select(MapToDTO).ToList();
        }

        private static MessageDTO MapToDTO(IMessage dlo)
        {
            //TODO: Add mapper
            var dto = new MessageDTO()
            {
                Name = dlo.Name,
                Value = dlo.Value
            };
            return dto;
        }


    }
}
