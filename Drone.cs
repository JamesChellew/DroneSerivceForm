using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneSerivceForm
{
    internal class Drone
    {
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
                _name = name;
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
