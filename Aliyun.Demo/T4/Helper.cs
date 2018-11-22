using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
namespace System.Xml.Linq
{
    public static class Helper
    {
        public static XElement GetXElement<T>(this T value)
        {
            var meta = typeof(T);
            var lst= meta.GetProperties(Reflection.BindingFlags.Public | Reflection.BindingFlags.Instance).ToList();
            var lstEl= lst.Select(x => new XElement(x.Name, x.GetValue(value))).ToList();
            var el = new XElement(meta.Name, lstEl);
            return el;
        }

        public static T GetInstance<T>(this XElement el)
        {
            var meta = typeof(T);
            var lst = meta.GetProperties(Reflection.BindingFlags.Public | Reflection.BindingFlags.Instance).ToList();
            T value = System.Activator.CreateInstance<T>();
            
            lst.ForEach(x=> {

                
                x.SetValue(value, el.Element(x.Name).Value);
            });
            return value;
        }

    }

   
}

namespace System.Web.UI
{
    public static class Helper {

        public static T GetValue<T>(this System.Web.UI.IDataItemContainer container)
        {
            return (T)container.DataItem;
        }
    }

}