using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMaker.Model
{
    public class EventCatalogSingleton
    {
        //instancefields
        private ObservableCollection<Event> ObservableCollection;
        private static EventCatalogSingleton _instance;

        //constructor
        private EventCatalogSingleton()
        {
            ObservableCollection = new ObservableCollection<Event>();
            LoadEventsAsync();
        }

        //properties
        public static EventCatalogSingleton Instance
        {
            get
            {
                if (_instance == null) _instance = new EventCatalogSingleton();
                return _instance;

                //return (_instance ?? new EventCatalogSingleton());
            }
        }

        public ObservableCollection<Event> GetList
        {
            get { return ObservableCollection; }
        }

        //methods
        public void Add(Event e)
        {
            ObservableCollection.Add(e);
            PersistencyService.SaveEventsAsJsonAsync(ObservableCollection);
        }

        public void Remove(Event e)
        {
            ObservableCollection.Remove(e);
            PersistencyService.SaveEventsAsJsonAsync(ObservableCollection);
        }




        public async void LoadEventsAsync()
        {
            List<Event> loadedEvents = await PersistencyService.LoadEventsFromJsonAsync();

            if (loadedEvents != null) foreach (Event e in loadedEvents)
            {
                ObservableCollection.Add(e);
            }
        }

        
        
    }
}
