using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShvnFbrk
{
    class ConClass
    {
        public string ConnectString;
        public void Connection_Options(string DS_NAME,
            string INIT_CATALOG, string LOD_ID, string PAS_ID)
        {
            ConnectString = "Data Source="
                + DS_NAME + ";" + "Initial Catalog="
                + INIT_CATALOG + ";" + "Persist Security Info=True;User ID="
                + LOD_ID + ";Password=\"" + PAS_ID + "\"";
        }
    }
}
