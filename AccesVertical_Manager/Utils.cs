using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccesVertical_Manager
{
    class Utils
    {

        public static void InsertColumns(DataGridView dataGV, int mida)
        {
            DataGridViewImageColumn img = new DataGridViewImageColumn();

            img.HeaderText = "Image";
            img.Name = "Image";
            img.Width = mida;
            dataGV.RowTemplate.Height = mida;
            if (dataGV.Columns["Image"] == null)
                dataGV.Columns.Insert(0, img);


        }
        public static int[] PreselectedIndex(DataGridView DtgView)
        {

            int saveRow = DtgView.FirstDisplayedScrollingRowIndex;

            int selectedRowCount = DtgView.Rows.GetRowCount(DataGridViewElementStates.Selected);
            int[] infoRowsSelScroll = new int[selectedRowCount + 1];
            if (selectedRowCount > 0)
            {
                
                for (int i = 0; i < selectedRowCount; i++)
                {


                    infoRowsSelScroll[i] = DtgView.SelectedRows[i].Index;
                    
                }
            }
            infoRowsSelScroll[selectedRowCount] = saveRow;
            return infoRowsSelScroll;

        }
       public static void PostselectedIndex(DataGridView DtgView, int[] infoRowsSelScroll)
        {
            Boolean row0 = false;
            for (int i = 0; i < infoRowsSelScroll.Length - 1; i++)
            {

                if (i < DtgView.Rows.GetRowCount(DataGridViewElementStates.Displayed))
                { if (infoRowsSelScroll[i] == 0) row0 = true;
                            DtgView.Rows[infoRowsSelScroll[i]].Selected = true;

                }
            }
           
            if (infoRowsSelScroll.Length > 1)
                DtgView.Rows[0].Selected = row0;
            int saveRow = infoRowsSelScroll[infoRowsSelScroll.Length - 1];
            if (saveRow >= 0 && saveRow < DtgView.Rows.Count)
                DtgView.FirstDisplayedScrollingRowIndex = saveRow;
           
        }

   

    public static void PostselectedIndexOnlyOneRow(DataGridView DtgView, int[] infoRowsSelScroll)
    {
            if (infoRowsSelScroll.Length > 1)
            {
                {
                    if (DtgView.Rows.Count > 1)
                        DtgView.Rows[infoRowsSelScroll[0]].Selected = true;
                }
            }
        int saveRow = infoRowsSelScroll[infoRowsSelScroll.Length - 1];
        if (saveRow >= 0 && saveRow < DtgView.Rows.Count)
            DtgView.FirstDisplayedScrollingRowIndex = saveRow;

    }

}

}
