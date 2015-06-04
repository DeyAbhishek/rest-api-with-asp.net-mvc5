using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using TPO.Common.DTOs;
using TPO.Services.Core;

namespace TPO.Services.Application
{
    public class MessageService : IMessageService
    {
        #region Variables
        private static Dictionary<string, ResourceFile> _resourceFiles = new Dictionary<string, ResourceFile>();
        #endregion

        #region Inner Classes
        private class ResourceFile
        {
            public string ApplicationName { get; set; }
            public string FileName { get; set; }
            public int Priority { get; set; }
            public ResourceManager ResourceManager { get; set; }

            public Assembly Assembly { get; set; }
        }
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        public static void AddResourceFile(string nameSpace, string fileName, Assembly assembly, int priority)
        {
            if (string.IsNullOrEmpty(nameSpace))
                throw new ArgumentNullException("nameSpace can not be null or empty");

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName can not be null or empty");

            if (assembly == null)
                throw new ArgumentNullException("assembly can not be null");

            _resourceFiles.Add
                (
                    nameSpace + "." + fileName,
                    new ResourceFile()
                    {
                        ApplicationName = nameSpace,
                        FileName =  fileName,
                        Assembly = assembly,
                        Priority = priority,
                        ResourceManager =  new ResourceManager(nameSpace + "." + fileName, assembly)
                    }
                );
        }

        public IEnumerable<MessageDto> GetMessagesByPrefix(string prefix)
        {
            List<MessageDto> list = new List<MessageDto>();
            foreach (ResourceFile resourceFile in _resourceFiles.Values.OrderBy(v => v.Priority))
            {
                List<DictionaryEntry> matches =
                    resourceFile.ResourceManager
                        .GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, true)
                        .OfType<DictionaryEntry>()
                        .Where(e => e.Key.ToString().StartsWith(prefix))
                        .ToList();
                foreach (DictionaryEntry match in matches)
                {
                    list.Add(
                        new MessageDto()
                        {
                            Name = match.Key.ToString(),
                            Value = match.Value.ToString()
                        }
                        );
                }
            }
            return list;
        }

        public MessageDto GetMessageByName(string name)
        {
            MessageDto returnValue = null;
            string value = GetMessageStringByName(name);
            if (!string.IsNullOrEmpty(value))
                returnValue = new MessageDto()
                {
                    Name = name,
                    Value = value
                };

            return returnValue;
        }

        public static string GetStringValue(string name)
        {
            return GetMessageStringByName(name);
        }

        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods

        private static string GetMessageStringByName(string name)
        {
            string returnValue = null;
            foreach (ResourceFile resourceFile in _resourceFiles.Values.OrderBy(v => v.Priority))
            {
                returnValue = resourceFile.ResourceManager.GetString(name);
                if (!string.IsNullOrEmpty(returnValue))
                    break;
            }
            return returnValue;
        }
        #endregion

        #region Events
        #endregion

        #region Event Handlers
        #endregion
    }
}
