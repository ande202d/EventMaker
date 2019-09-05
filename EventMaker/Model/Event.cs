using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMaker.Model
{
    public class Event
    {
        //instance fields
        private int _id;
        private string _name;
        private string _description;
        private string _place;
        private DateTime _dateTime;

        //contructors
        //public Event()
        //{

        //}

        public Event(int Id, string Name, string Descreption, string Place, DateTime DT)
        {
            _id = Id;
            _name = Name;
            _description = Descreption;
            _place = Place;
            _dateTime = DT;
        }


        //methods
        public override string ToString()
        {
            return $"{_name}, {_id}, {_description}, {_place}, {_dateTime}";
        }

        //properties
        public string Id { get {return $"{_id}";} }
        public string Name { get {return _name;} }
        public string Descreption { get {return _description;} }
        public string Place { get {return _place;} }

        public string DateTime
        {
            get
            {
                return $"d. {_dateTime.Day}/{_dateTime.Month}/{_dateTime.Year} kl: {_dateTime.Hour}:{_dateTime.Minute}";
                //return _dateTime.ToString();
            }
        }
    }
}
