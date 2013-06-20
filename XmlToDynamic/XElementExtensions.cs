using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XmlToDynamic
{
    public static class XElementExtensions
    {
        //private static List<string> KnownLists;

        public static dynamic ToDynamic(this XElement element)
        {
            if (element.HasElements)
            {
                var uniqueElements = element.Elements().Where(el => element.Elements().Count(el2 => el2.Name.LocalName.Equals(el.Name.LocalName)) == 1);
                var repeatedElements = element.Elements().Except(uniqueElements);

                var item = new ExpandoObject();

                foreach (var repeatedElementGroup in repeatedElements.GroupBy(re => re.Name.LocalName).OrderBy(el => el.Key))
                {
                    var list = new List<dynamic>();
                    foreach (var repeatedElement in repeatedElementGroup)
                        list.Add(ToDynamic(repeatedElement));
                    ((IDictionary<string, object>)item).Add(repeatedElementGroup.Key + "s", list);
                }

                foreach (var uniqueElement in uniqueElements.OrderBy(el => el.Name.LocalName))
                {
                    ((IDictionary<string, object>)item).Add(uniqueElement.Name.LocalName, ToDynamic(uniqueElement));
                }
                return item;
            }
            else
            {
                return element.Value;
            }
        }
    }
}
