using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.UI.Popups;
using Newtonsoft.Json;

namespace EventMaker.Model
{
    class PersistencyService
    {
        private static string jsonFileName = "EventsAsJson1.dat";
        private static string xmlFileName = "EventsAsXml1.dat";

        public static async void SaveEventsAsJsonAsync(ObservableCollection<Event> Events)
        {
            string EventsJsonString = JsonConvert.SerializeObject(Events);
            SerializeEventsFileAsync(EventsJsonString, jsonFileName);
        }

        public static async Task<List<Event>> LoadEventsFromJsonAsync()
        {
            string EventsJsonString = await DeSerializeEventsFileAsync(jsonFileName);
            if (EventsJsonString != null)
                return (List<Event>)JsonConvert.DeserializeObject(EventsJsonString, typeof(List<Event>));
            return null;
        }

        public static async void SaveEventsAsXmlAsync(ObservableCollection<Event> Events)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(Events.GetType());
            StringWriter textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, Events);
            SerializeEventsFileAsync(textWriter.ToString(), xmlFileName);
        }

        public static async Task<ObservableCollection<Event>> LoadEventsFromXmlAsync()
        {
            string EventsXmlString = await DeSerializeEventsFileAsync(xmlFileName);
            if (EventsXmlString != null)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Event>));
                return (ObservableCollection<Event>)xmlSerializer.Deserialize(new StringReader(EventsXmlString));
            }
            return null;
        }


        public static async void SerializeEventsFileAsync(string EventsString, string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, EventsString);
        }

        public static async Task<string> DeSerializeEventsFileAsync(String fileName)
        {
            try
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(localFile);
            }
            catch (FileNotFoundException ex)
            {

                MessageDialogHelper.Show("Loading for the first time? Try Adding and Save some Events before you are trying to Load Events!", "File not found!");
                return null;
            }
        }

        private class MessageDialogHelper
        {
            public static async void Show(string content, string title)
            {
                MessageDialog messageDialog = new MessageDialog(content, title);
                await messageDialog.ShowAsync();
            }
        }



    }
}
