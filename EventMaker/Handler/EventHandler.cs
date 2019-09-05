using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventMaker.Converter;
using EventMaker.Model;
using EventMaker.View;
using EventMaker.ViewModel;

namespace EventMaker.Handler
{
    public class EventHandler
    {
        private EventViewModel EVM;

        public EventHandler(EventViewModel evm)
        {
            EVM = evm;
        }

        public void CreateEvent()
        {
            EVM.EventCatalogSingleton.Add(new Event(EVM.Id, EVM.Name, EVM.Description, EVM.Place, DateTimeConverter.DateTimeOffsetAndTimeSetToDateTime(EVM.Date, EVM.Time)));
        }
        
        public void DeleteEvent(Event e)
        {
            EVM.EventCatalogSingleton.Remove(e);
        }
    }
}
