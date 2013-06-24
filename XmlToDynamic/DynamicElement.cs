using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlToDynamic
{
    public class DynamicElement : DynamicObject
    {
        private IDictionary<string, string> attributes = new Dictionary<string, string>();

        internal IDictionary<string, dynamic> SubElements = new Dictionary<string, dynamic>();

        public string this[string key]
        {
            get { return attributes[key]; }
            set { attributes[key] = value; }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = SubElements[binder.Name];
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            SubElements[binder.Name] = value as dynamic;
            return true;
        }

        public override string ToString()
        {
            return Value;
        }

        public string Value { get; set; }
    }
}