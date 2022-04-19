using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesVertical_Manager
{
    class MesdeObra
    {
       public string Expedient { get; }
        private Dictionary<DateTime,float> dates;
       public float SumatoriMensual { get; private set; }
      public  string Tècnic { get; }
      public  bool Doc { get; }
        public bool DocEntregada { get; }
        public DateTime MaxData { get; }
        public bool diaOK { get; }
        public  string Activitat { get; }
      public  string Client { get; }
      public  int UnitatsdObra { get; }
       public  MesdeObra(string Expedient,Dictionary<DateTime,float> dates,string Tècnic,bool Doc,bool DocEntregada,bool diaOK,string Activitat,string Client, int UnitatsdObra,DateTime MaxData) {
            this.Expedient = Expedient;
            this.dates = dates;
            this.Tècnic = Tècnic;
            this.Doc = Doc;
            this.DocEntregada = DocEntregada;
            this.diaOK = diaOK;
            this.Activitat = Activitat;
            this.Client = Client;
            this.UnitatsdObra = UnitatsdObra;
            this.MaxData = MaxData;
            SumatoriMensual = 0;
            foreach (KeyValuePair< DateTime,float> pair in dates)
            {
                SumatoriMensual += pair.Value;
            }

            }
        public void AfegirData(DateTime data,float sumatori) {
            SumatoriMensual += sumatori;
            if (!dates.ContainsKey(data))
                dates.Add(data, sumatori);
            else {
                dates[data] += sumatori;
            }
        }
        public Dictionary<DateTime, float> getDates()
        {
            return dates;

        }

    }
}
