using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesVertical_Manager
{
    class DiesExtres
    {
      public  int[] CalendariExtres = new int[12];
        
      public  DiesExtres(DateTime data, int extres) {
           
            for(int i=0;i< CalendariExtres.Length; i++)
            {
                CalendariExtres[i] = 0;
            }
            AfegirData(data, extres);
        }

        public void AfegirData(DateTime data, int extres )
        {
            
                CalendariExtres[data.Month-1]+= extres;
                    
                   
            
        }

    }
}
