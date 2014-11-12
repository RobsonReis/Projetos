using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;
using System.Data.SqlClient;

namespace DellMare.Addon
{

    //INSERIR DOCUMENTO
    public class Menu__1282 : B1Menu {
        
        public Menu__1282() {
            MenuUID = "1282";
        }

    
        [B1Listener(BoEventTypes.et_MENU_CLICK, false)]
        public virtual void OnAfterMenuClick(MenuEvent pVal) {
            // ADD YOUR ACTION CODE HERE ...

            Form oForm = B1Connections.theAppl.Forms.ActiveForm;
            oForm.Freeze(true);
            switch (oForm.TypeEx)
            {
                case "140":
                    {
                        Form__140.EnableButton();
                        break;
                    }
            }

            oForm.Freeze(false);
        }
    }
}
