using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using TPO.DL.Models;

namespace TPO.DL.Repositories
{
    public class MessageRepository
    {
        #region Variables
        #endregion // Variables

        #region Properties
        #endregion // Properties

        #region Constructors
        #endregion // Constructors

        #region Public Methods

        private static Dictionary<string, ResourceFile> _resourceFiles = new Dictionary<string, ResourceFile>();

        private class ResourceFile
        {
            public string ApplicationName { get; set; }
            public string FileName { get; set; }
            public int Priority { get; set; }
            public ResourceManager ResourceManager { get; set; }
        }

        public static void AddResourceFile(string applicationName, string fileName, int priority)
        {
            if (string.IsNullOrEmpty(applicationName))
                throw new ArgumentNullException("applicationName can not be null or empty");

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName can not be null or empty");

            _resourceFiles.Add
                (
                    applicationName + "." + fileName,
                    new ResourceFile()
                    {
                        ApplicationName = applicationName,
                        FileName = fileName,
                        Priority = priority,
                        ResourceManager = ResourceManager.CreateFileBasedResourceManager(fileName, applicationName, null)
                        //            ResourceManager =  new ResourceManager(applicationName + "." + fileName, assembly)
                    }
                );
        }

        public static bool RemoveResourceFile(string applicationName, string fileName)
        {
            if (string.IsNullOrEmpty(applicationName))
                throw new ArgumentNullException("applicationName can not be null or empty");

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName can not be null or empty");

            return _resourceFiles.Remove(applicationName + "." + fileName);

        }

        public IEnumerable<IMessage> GetMessagesByPrefix(string prefix)
        {
            List<IMessage> list = new List<IMessage>();
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
                        new DL.Models.Message()
                        {
                            Name = match.Key.ToString(),
                            Value = match.Value.ToString()
                        }
                        );
                }
            }
            return list;
        }

        public IMessage GetMessageByName(string name)
        {
            IMessage returnValue = null;
            string value = GetMessageStringByName(name);
            if (!string.IsNullOrEmpty(value))
                returnValue = new DL.Models.Message()
                {
                    Name = name,
                    Value = value
                };

            return returnValue;
        }

        public string GetMessageStringByName(string name)
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

        #endregion // Public methods

    }
}
