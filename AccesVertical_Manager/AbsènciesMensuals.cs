using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccesVertical_Manager
{
   


    public class AbsènciesMensuals
    {
        public int vacances { get; set; } = 0;
        public int absencia { get; set; } = 0;
        public int curs { get; set; } = 0;
        public int baixa { get; set; } = 0;
        public int metge { get; set; } = 0;
        public  AbsènciesMensuals()
        {
            vacances = 0; absencia = 0;curs = 0;baixa = 0;

        }
        public void SumarAbsència(string Motiu)
        {
          
            switch (Motiu)
            {
                case "VACANCES": vacances++;
                    break;
                case "ABSÈNCIA":
                    absencia++;
                    break;
                case "":
                    absencia++;
                    break;
                case "CURS":
                    curs++;
                    break;
                case "BAIXA":
                    baixa++;
                    break;
                case "METGE":
                    metge++;
                    break;

            }


        }
    }
}
