using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;

namespace DellMare.Addon
{
    //ELIMINAR LINHA
    public class Menu__1293 : B1Menu
    {

        public Menu__1293()
        {
            MenuUID = "1293";
        }


        [B1Listener(BoEventTypes.et_MENU_CLICK, false)]
        public virtual void OnAfterMenuClick(MenuEvent pVal)
        {
        }
    }
}
