using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.TourRequests;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using TouristAgency.Users;
using TouristAgency.Util;

namespace TouristAgency.Tours.ComplexTourRequestFeatures.Domain
{
    public class ComplexTourRequest : INotifyPropertyChanged, ISerializable
    {
        private int _id;
        private string _name;
        private List<TourRequest> _components;

        public event PropertyChangedEventHandler PropertyChanged;

        public ComplexTourRequest() 
        {
            _id = -1;
            _components = new List<TourRequest>();
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public int ID
        {
            get => _id;
            set
            {
                if (_id != value) 
                {
                    value = _id;
                    OnPropertyChanged("ID");
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if(value != _name) 
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public List<TourRequest> Components
        {
            get => _components;
            set
            {
                if (_components != value) 
                {
                    _components = value;
                    OnPropertyChanged("Components");
                }
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                _id.ToString(),
                _name
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _id = Convert.ToInt32(values[0]);
            _name = values[1];
        }
    }
}
