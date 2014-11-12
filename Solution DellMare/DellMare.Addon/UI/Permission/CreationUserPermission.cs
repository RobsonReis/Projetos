using System;
using System.Collections.Generic;
using System.Text;
using B1WizardBase;
using SAPbobsCOM;


namespace DellMare.Addon
{
    class CreationUserPermission
    {
        public CreationUserPermission()
        {
        }

        public void CriaPermissionMain()
        {
            CriaNivel1Permissoes("SPS Addon", "");
            CriaChildPermissoes("SPS Addon", "SPS_CADINV", "SPS_1", "Cadastro de Inventário");
         }

        public void CriaNivel1Permissoes(string Nivel1, string Parent)
        {
            SAPbobsCOM.UserPermissionTree oUserPermission = (SAPbobsCOM.UserPermissionTree)B1Connections.diCompany.GetBusinessObject(BoObjectTypes.oUserPermissionTree);
            int lErrCode;
            string sErrMsg = "";

            if (!oUserPermission.GetByKey(Nivel1))
            {
                B1Connections.theAppl.StatusBar.SetText("Criando Permissão para Formulários NPK Inventário Addon..", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

                oUserPermission.PermissionID = Nivel1;
                oUserPermission.Name = Nivel1;
                oUserPermission.Options = SAPbobsCOM.BoUPTOptions.bou_FullNone;
                oUserPermission.ParentID = Parent;

                oUserPermission.Add();
                B1Connections.diCompany.GetLastError(out lErrCode, out sErrMsg);

                if (lErrCode != 0)
                {
                    throw new Exception(sErrMsg);
                }
            }
        }

        public void CriaChildPermissoes(string Nivel1, string PermissionID, string FormType, string Titulo)
        {
            SAPbobsCOM.UserPermissionTree oUserPermission = (SAPbobsCOM.UserPermissionTree)B1Connections.diCompany.GetBusinessObject(BoObjectTypes.oUserPermissionTree);
            int lErrCode;
            string sErrMsg = "";

            if (!oUserPermission.GetByKey(PermissionID))
            {
                //For level 2 and up you must set the object's father unique ID
                oUserPermission.PermissionID = PermissionID;
                oUserPermission.Name = Titulo;
                oUserPermission.Options = BoUPTOptions.bou_FullNone;

                //oUserPermission.Levels = 2;
                oUserPermission.ParentID = Nivel1;
                //this object manages forms
                oUserPermission.UserPermissionForms.FormType = FormType;

                oUserPermission.Add();
                B1Connections.diCompany.GetLastError(out lErrCode, out sErrMsg);
                if (lErrCode != 0)
                {
                    throw new Exception(sErrMsg);
                }
            }
        }
    }
}

