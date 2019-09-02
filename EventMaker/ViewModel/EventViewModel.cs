using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Eventmaker.Common;
using EventMaker.Annotations;
using EventMaker.Handler;
using EventMaker.Model;

namespace EventMaker.ViewModel
{
    public class EventViewModel : INotifyPropertyChanged
    {
        //instancefield
        //private EventCatalogSingleton _eventCatalog;
        private ObservableCollection<Event> _allEvents;
        private int _id;
        private string _name;
        private string _description;
        private string _place;
        private DateTimeOffset _date;
        private TimeSpan _time;

        private Event _selectedEvent;

        private Handler.EventHandler _eventHandler;

        //contructor
        public EventViewModel()
        {
            _allEvents = EventCatalogSingleton.Instance.GetList;
            DateTime dt = System.DateTime.Now;

            _date = new DateTimeOffset(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, new TimeSpan());
            _time = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
            _eventHandler = new Handler.EventHandler(this);
            CreateEventCommand = new RelayCommand(CreateEventMethod);
            DeleteEventCommand = new RelayCommand(DeleteEventMethod);
        }

        public ICommand CreateEventCommand { get; set; }
        public ICommand DeleteEventCommand { get; set; }

        public void CreateEventMethod()
        {
            EventHandler.CreateEvent();
        }

        public void DeleteEventMethod()
        {
            EventHandler.DeleteEvent(SelectedEvent);
        }


        //properties
        public ObservableCollection<Event> AllEvents { get { return _allEvents;} }

        public Event SelectedEvent
        {
            get { return _selectedEvent; }
            set { _selectedEvent = value; }
        }

        public int Id
        {
            get { return _id;}
            set { _id = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string Place
        {
            get { return _place; }
            set { _place = value; }
        }
        public DateTimeOffset Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public TimeSpan Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public EventCatalogSingleton EventCatalogSingleton
        {
            get { return EventCatalogSingleton.Instance; }
        }

        public Handler.EventHandler EventHandler
        {
            get { return _eventHandler; }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
