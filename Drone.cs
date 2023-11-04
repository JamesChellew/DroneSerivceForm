using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneSerivceForm
{
    internal class Drone
    {
        // 6.1 Create a separate class file to hold the data items of the Drone. Use separate getter and setter methods,
        // ensure the attributes are private and the accessor methods are public. Add a display method that returns a string
        // for Client Name and Service Cost. Add suitable code to the Client Name and Service Problem accessor methods so
        // the data is formatted as Title case or Sentence case. Save the class as “Drone.cs”.
        private string _name;
        private string _model;
        private string _issue;
        private double _cost;
        private int _tag;

        public string GetName()
        {
            return _name;
        }
        public void SetName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                TextInfo ti = new CultureInfo("en-AU", false).TextInfo;
                _name = ti.ToTitleCase(name);
            }
            else
            {
                _name = "Unspecified";
            }
        }

        public string GetModel()
        {
            return _model;
        }
        public void SetModel(string model)
        {
            if (!string.IsNullOrEmpty(model))
            {
                _model = model;
            }
            else
            {
                _model = "Unspecified";
            }
        }

        public string GetIssue()
        {
            return _issue;
        }
        public void SetIssue(string issue) 
        {
            if (!string.IsNullOrEmpty(_issue))
            {
                _issue = issue;
            }
            else
            {
                _issue = "Unspecified";
            }
        }

        public double GetCost()
        {
            return _cost;
        }
        public void SetCost(double cost)
        {
            _cost = cost;
        }

        public int GetTag()
        {
            return _tag;
        }
        public void SetTag(int tag)
        {
            _tag = tag;
        }
    }
}
