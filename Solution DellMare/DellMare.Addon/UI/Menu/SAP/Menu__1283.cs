using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;

namespace DellMare.Addon
{
    //REMOVER DOCUMENTO
    public class Menu__1283 : B1Menu {
        
        public Menu__1283() {
            MenuUID = "1283";
        }

    
        [B1Listener(BoEventTypes.et_MENU_CLICK, false)]
        public virtual void OnAfterMenuClick(MenuEvent pVal) {
            // ADD YOUR ACTION CODE HERE ...

            Form oForm = B1Connections.theAppl.Forms.ActiveForm;

            switch (oForm.TypeEx)
            {
                case "SYS_FRMSYSFILIAIS":
                    {
                        break;
                    }
                case "SYS_FRMSYSETBCTO":
                    {

                        break;
                    }
            }
        }
    }
}
