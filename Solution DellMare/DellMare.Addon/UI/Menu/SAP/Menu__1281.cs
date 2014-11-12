using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;
using System.Data.SqlClient;

namespace DellMare.Addon
{
   
    //PROCURAR
    public class Menu__1281 : B1Menu {
        
        public Menu__1281() {
            MenuUID = "1281";
        }

        [B1Listener(BoEventTypes.et_MENU_CLICK, false)]
        public virtual void OnAfterMenuClick(MenuEvent pVal) {
            // ADD YOUR ACTION CODE HERE ...

            Form oForm = B1Connections.theAppl.Forms.ActiveForm;

            switch (oForm.TypeEx)
            {
                case "140":
                    {
                       Form__140.EnableButton();
                       break;
                    }
            }
        }
    }
}
