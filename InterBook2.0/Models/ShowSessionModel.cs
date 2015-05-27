using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using InterBook2._0.Models;
using InterBook2._0.BLL;

namespace Website.Models
{
    public class ShowSessionModel : ModelBase
    {
        public bool HasSession
        {
            get
            {
                return SessionManager.Current.Util != null && SessionManager.Current.Util.IdU > 0;
            }
        }

        // Prop static
        public static readonly string boxTemplate = "<table id='hor-zebra'><thead><tr><th colspan='3'>{0}</th></tr></thead><tbody>{1}</tbody></table>";
        public static readonly string lineTemplate = "<tr><td class='key'>{0}<td><td>{1}<td></tr>";
        // Prop
        public List<string> Templates { get; set; }
        private HttpContextBase Context { get; set; }

        // Constructeur
        public ShowSessionModel(HttpContextBase context)
        {
            Context = context;

            Templates = new List<string>();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            PropertyInfo[] pi = SessionManager.Current.GetType().GetProperties();
            foreach (var item in GetPropertInfos(SessionManager.Current))
            {
                if (dic.Keys.Any(x => x == item.Key))
                {
                    if (!dic[item.Key].Contains(item.Value))
                        dic[item.Key] += item.Value;
                }
                else
                    dic.Add(item.Key, item.Value);
            }

            foreach (var item in dic.Where(x => !x.Key.ToLower().Contains("ref_")))
            {
                Templates.Add(string.Format(boxTemplate, item.Key, item.Value));
            }
        }

        

        // Méthode
        private IEnumerable<KeyValue> GetPropertInfos(object o, string parent = null)
        {
            if (o != null)
            {
                Type t = o.GetType();
                PropertyInfo[] props = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prp in props)
                {
                    if (prp.PropertyType.Module.ScopeName != "CommonLanguageRuntimeLibrary" && prp.PropertyType.Module.ScopeName != "SubSonic.dll" && o != null)
                    {
                        foreach (var info in GetPropertInfos(prp.GetValue(o, null), t.Name))
                            yield return info;
                    }
                    else if (prp.DeclaringType.ToString() == ((MemberInfo)prp).DeclaringType.FullName && prp.DeclaringType.ToString() != "System.Web.HttpApplicationState")
                    {
                        var value = prp.GetValue(o, null);
                        IList il = value as IList;
                        if (il != null && il.Count > 0)
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (var item in il)
                            {
                                sb.Append(item).Append(", ");
                            }
                            sb.Length -= 2;
                            value = sb;
                        }

                        var stringValue = (value != null) ? value.ToString() : "";

                        if (t.Name != prp.Name)
                            yield return new KeyValue { Key = t.Name, Value = string.Format(lineTemplate, prp.Name, stringValue) };
                    }
                }
            }
        }

        internal class KeyValue
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
    }
}