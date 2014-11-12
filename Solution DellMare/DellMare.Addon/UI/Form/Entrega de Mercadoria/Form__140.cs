using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;

namespace DellMare.Addon
{
    public class Form__140 : B1Form
    {
        public static string UniqueID;

        public Form__140()
        {
            FormType = "140";

        }

        public static void CriaButton(ItemEvent pVal)
        {
            Form oForm = B1Connections.theAppl.Forms.Item(pVal.FormUID);

            oForm.Freeze(true);

            Item oItem = oForm.Items.Add("btnEntrada", BoFormItemTypes.it_BUTTON);
            oItem.Left = oForm.Items.Item("10000329").Left;
            oItem.Top = oForm.Items.Item("10000329").Top - 25;
            oItem.Width = 100;
            ((Button)oItem.Specific).Caption = "Entrada Mercadoria";
            oItem.Visible = false;

            oForm.Freeze(false);
        }

        public static void EnableButton()
        {
            try
            {
                Form oForm = B1Connections.theAppl.Forms.ActiveForm;
                oForm.Items.Item("btnEntrada").Visible = oForm.DataSources.DBDataSources.Item("ODLN").GetValue("U_EntPendente", 0).ToString().Trim().Equals("S");
            }
            catch
            {
            }

        }

        public static void RealizaEntradaMercadoria()
        {
            Form oForm = B1Connections.theAppl.Forms.ActiveForm;
            string strSql = string.Empty;

            try
            {
                strSql = string.Format(@"select OWHS.WhsCode from  ODLN 
                                                inner join DLN1 on ODLN.DocEntry = DLN1.DocEntry
                                                inner join OWHS on ODLN.BPLId = OWHS.BPLid and OWHS.Nettable='N' and OWHS.DropShip='N'
                                         Where ODLN.DocEntry = {0}
	                                           AND DLN1.Usage in (select Id From OUSG Where OUSG.U_EntMercadoria = 'S')",
                                         Convert.ToInt32(oForm.DataSources.DBDataSources.Item("ODLN").GetValue("DocEntry", 0).ToString()));
                System.Data.DataTable dt = B1Connections.ExecuteSqlDataTable(strSql);
                if (dt.Rows.Count > 0)
                {
                    Documents docDelivery = (Documents)B1Connections.diCompany.GetBusinessObject(BoObjectTypes.oDeliveryNotes);
                    docDelivery.GetByKey(Convert.ToInt32(oForm.DataSources.DBDataSources.Item("ODLN").GetValue("DocEntry", 0).ToString()));

                    Documents docInventory = (Documents)B1Connections.diCompany.GetBusinessObject(BoObjectTypes.oInventoryGenEntry);
                    docInventory.BPL_IDAssignedToInvoice = docDelivery.BPL_IDAssignedToInvoice;
                    docInventory.TaxDate = docDelivery.TaxDate;
                    docInventory.Comments = docDelivery.Comments;
                    docInventory.UserFields.Fields.Item("U_DocEntrega").Value = docDelivery.DocEntry;

                    for (int i = 0; i < docDelivery.Lines.Count; i++)
                    {
                        if (i > 0) docInventory.Lines.Add();
                        docDelivery.Lines.SetCurrentLine(i);
                        docInventory.Lines.ItemCode = docDelivery.Lines.ItemCode;
                        docInventory.Lines.Quantity = docDelivery.Lines.Quantity;
                        docInventory.Lines.UnitPrice = docDelivery.Lines.UnitPrice;
                        docInventory.Lines.WarehouseCode = dt.Rows[0][0].ToString();

                        for (int x = 0; x < docDelivery.Lines.BatchNumbers.Count; x++)
                        {
                            docDelivery.Lines.BatchNumbers.SetCurrentLine(x);
                            docInventory.Lines.BatchNumbers.Quantity = docDelivery.Lines.BatchNumbers.Quantity;
                            docInventory.Lines.BatchNumbers.BatchNumber = docDelivery.Lines.BatchNumbers.BatchNumber;
                            docInventory.Lines.BatchNumbers.Add();
                        }
                    }

                    int iErro = docInventory.Add();
                    string sErro = string.Empty;
                    if (iErro != 0)
                    {
                        B1Connections.diCompany.GetLastError(out iErro, out sErro);
                        throw new Exception(sErro);
                    }

                    string sDocNew = string.Empty;
                    B1Connections.diCompany.GetNewObjectCode(out sDocNew);

                    strSql = string.Format("Update ODLN set U_EntPendente = 'N' Where DocEntry = {0}", oForm.DataSources.DBDataSources.Item("ODLN").GetValue("DocEntry", 0).ToString());
                    B1Connections.ExecuteSqlDataTable(strSql);

                    oForm.Items.Item("btnEntrada").Visible = false;
                }
            }
            catch (Exception ex)
            {
                strSql = string.Format("Update ODLN set U_EntPendente = 'S' Where DocEntry = {0}", oForm.DataSources.DBDataSources.Item("ODLN").GetValue("DocEntry", 0).ToString());
                B1Connections.ExecuteSqlDataTable(strSql);
                oForm.Items.Item("btnEntrada").Visible = true;
                B1Connections.theAppl.MessageBox(string.Format("Erro ao tentar gerar entrada de mercadoria. {0}", ex.Message), 1, "OK", "", "");
            }

        }


        [B1Listener(BoEventTypes.et_FORM_LOAD, false)]
        public virtual void OnFormLoad(ItemEvent pVal)
        {
            CriaButton(pVal);
        }


        [B1Listener(BoEventTypes.et_FORM_DATA_LOAD, false)]
        public virtual void OnAfterFormDataLoad(BusinessObjectInfo pVal)
        {
            EnableButton();
        }

        [B1Listener(BoEventTypes.et_FORM_DATA_UPDATE, false)]
        public virtual void OnAfterFormDataUpdate(BusinessObjectInfo pVal)
        {
            EnableButton();
        }

        [B1Listener(BoEventTypes.et_FORM_DATA_ADD, false)]
        public virtual void OnAfterFormDataAdd(BusinessObjectInfo pVal)
        {
            if (pVal.ActionSuccess)
            {
                RealizaEntradaMercadoria();
                EnableButton();
            }
        }




    }
}
