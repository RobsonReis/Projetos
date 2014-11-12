using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using B1WizardBase;

namespace DellMare.Addon
{
    public class Menus
    {

        public void CriarMenus()
        {
            CreationUserMenu menu;

            menu = new CreationUserMenu("DellMare", "43537", "MenuDellMare", SAPbouiCOM.BoMenuType.mt_POPUP, 20, "");
            menu.Add();
            menu = new CreationUserMenu("Assistente de Pagamento", "MenuDellMare", "mnuAssistentePagto", SAPbouiCOM.BoMenuType.mt_STRING, 1, "");
            menu.Add();
           
        }
    }
}
