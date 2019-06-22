using Newtonsoft.Json;
using SimonsSearch.Service.DataModels;
using SimonsSearch.Service.Interfaces;
using System.IO;
using System.Reflection;

namespace SimonsSearch.Service
{
    public class SearchRepository : ISearchRepository
    {
        private static DataFile _fileData; 

        public DataFile LoadData()
        {
            //_fileData is static property so if it is not null
            //do not load data from disk
            if (_fileData != null)
            {
                return _fileData;
            }

            var fileData = LoadFileFromDisk(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/DataFile/sv_lsm_data.json");

            _fileData = JsonConvert.DeserializeObject<DataFile>(fileData);

            return _fileData;
        }

        private string LoadFileFromDisk(string filePath)
        {
            var fileData = string.Empty;

            using (StreamReader reader = new StreamReader(filePath))
            {
                fileData = reader.ReadToEnd();
            }

            return fileData;

        }
    }
}
