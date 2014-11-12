using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;

namespace DellMare.Addon
{
    public class Button__140__btnEntrada : B1Item
    {
        public Button__140__btnEntrada()
        {
            FormType = "140";
            ItemUID = "btnEntrada";
        }


        [B1Listener(BoEventTypes.et_CLICK, false)]
        public virtual void OnAfterClick(ItemEvent pVal)
        {
            SAPbouiCOM.Form oForm = (SAPbouiCOM.Form)B1Connections.theAppl.Forms.ActiveForm;
            if (oForm.Mode == BoFormMode.fm_OK_MODE)
            {
                Form__140.RealizaEntradaMercadoria();
                Form__140.EnableButton();
            }
            else
            {
                B1Connections.theAppl.StatusBar.SetText("Atualize os dados da entrega antes de executar a entrada de mercadoria!", BoMessageTime.bmt_Medium, BoStatusBarMessageType.smt_Error);
            }
           
        }


    }
}
