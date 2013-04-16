using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UFC.Helpers
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]

    public class HtmlPropertiesAttribute : Attribute
    {

        public string CssClass
        {

            get;

            set;

        }

        public int MaxLength
        {

            get;

            set;

        }

        public string Size
        {

            get;

            set;

        }

        public IDictionary<string, object> HtmlAttributes()
        {

            //Todo: we could use TypeDescriptor to get the dictionary of properties and their values

            IDictionary<string, object> htmlatts = new Dictionary<string, object>();

            if (MaxLength != 0)
            {

                htmlatts.Add("MaxLength", MaxLength);

            }

            if (Size != "")
            {

                htmlatts.Add("style", Size);

            }

            if (CssClass != "")
            {

                htmlatts.Add("class", CssClass);

            }

            return htmlatts;

        }

    }

}