using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccesVertical_Manager
{
    
    class AbsènciesAnualsOperari
    {
        public AbsènciesMensuals[] Absencies = new AbsènciesMensuals[12];
        public string nom {  get; }
        public string cognoms { get; }
        public string vinculacio { get; }
        public AbsènciesAnualsOperari(string nom, string cognoms,string vinculacio, DateTime data,string Motiu)
        {
    
            this.nom = nom;
            this.cognoms = cognoms;
            this.vinculacio = vinculacio;
           for(int i = 0; i < 12; i++)
            {
                Absencies[i] = new AbsènciesMensuals();
            }
           if(data!= null)
            Absencies[data.Month-1].SumarAbsència(Motiu);
            
        }
        public void AfegirAbsencia(DateTime data,string Motiu)
        {
            if (data != null)
                Absencies[data.Month-1].SumarAbsència(Motiu);

        }
    }
}
