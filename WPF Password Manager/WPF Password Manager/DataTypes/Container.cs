using System.Collections.Generic;

namespace WPF_Password_Manager.DataTypes
{
    public class Container : DataObject
    {
        public int Count { get { return list.Count; } }
        private List<Container> list;
        private int _selected;
        public int Selected { get { return _selected; } set { _selected = value; } }
        public Container(int i,string t)
        {
            ID = i;
            Title = t;
            list = new List<Container>();
        }
        
        public Container(int i, string t, string d)
        {
            ID = i;
            Title = t;
            Data = d;
        }

        public Container()
        {
            ID = 0;
            Title = "Main";
            list = new List<Container>();
        }

        private Container _perspective;
        public Container Perspective { get { return _perspective; } }
        public void IntoPerspective(int index)
        {
            if (list.Count - 1 >= index)
            {
                _perspective = list[index];
            }
        }

        //Because classes are reference variables
        //Seeting the parent container, only lists a reference
        //to the space in memory of that particular container
        private Container _parent;
        public Container Parent { get { return _parent; } }
        public void SetParent(Container container)
        {
            _parent = container;
        }

        public void Add(Container container)
        {
            list.Add(container);
            container.SetParent(this);
        }

        public void Remove(Container container)
        {
            if (list.Contains(container))
            {
                list.Remove(container);
            }
        }

        public void RemoveAt(int index)
        {
            if (list.Count - 1 >= index)
            {
                list.RemoveAt(index);
            }
        }
        
        public bool TitleCheck(string title)
        {
            foreach(var k in list)
            {
                if (k.Title == title)
                {
                    return false;
                }
                
            }
            return true;
        }
        
        public void EvaluteID()
        {
            int i = 1;
            foreach (var item in list)
            {
                item.ID = i++;
            }
        }

        public Container Copy()
        {
            Container output;
            if (string.IsNullOrEmpty(Data))
            {
                output = new Container(ID, Title);
                foreach(var item in list)
                {
                    output.Add(item);
                }
                output.Selected = Selected;
                if (_perspective != null)
                {
                    output.IntoPerspective(_perspective.ID);
                }
                output.SetParent(_parent);
                return output;
            }
            output = new Container(ID, Title, Data);
            output.SetParent(_parent);
            return output;

        }

        public List<Container> GetList()
        {
            return list;
        }
    }
}