using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;

namespace DellMare.Addon
{
    //INSERIR LINHA
    public class Menu__1292 : B1Menu {
        
        public Menu__1292() {
            MenuUID = "1292";
        }

    
        [B1Listener(BoEventTypes.et_MENU_CLICK, false)]
        public virtual void OnAfterMenuClick(MenuEvent pVal) {
            // ADD YOUR ACTION CODE HERE ...
          
        }
    }
}
