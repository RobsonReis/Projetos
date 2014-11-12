namespace B1WizardBase
{
    using SAPbobsCOM;
    using SAPbouiCOM;
    using System;
    using System.Runtime.InteropServices;
using System.Xml;

    public sealed class B1Connections
    {
        public static string addOnIdentifierStr;
        public static ConnectionType connectType = ConnectionType.SSO;
        public static string connStr = "0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056";
        public static SAPbobsCOM.Company diCompany = null;
        private static bool diRequired;
        public static Application theAppl;
        public static SAPbobsCOM.Company diCompanyTarget = null;
    
        public static void ConectTarget()
        {
            XmlNode note = null;
            string oPathXML = string.Empty;
            string Server = string.Empty;
            string User = string.Empty;
            string PassWord = string.Empty;
            string DataBase = string.Empty;

            string SourceServer = string.Empty;
            string SourceUser = string.Empty;
            string SourcePassWord = string.Empty;
            string SourceDataBase = string.Empty;
            string SourceSAPUser = string.Empty;
            string SourceSAPPassWord = string.Empty;
            string SourceTypeSQL = string.Empty;

            XmlDocument doc = new XmlDocument();

            string sErrMsg = string.Empty;
            int lRetCode = 0;

            #region Get data from Target
            oPathXML = System.Windows.Forms.Application.StartupPath + @"\Config.xml";
            doc.Load(oPathXML);
            note = null;
            note = doc.SelectSingleNode(@"DellMare/Target");
            foreach (XmlNode n in note.ChildNodes)
            {
                if (n.Name == "Server")
                {
                    SourceServer = n.InnerText;
                }
                else if (n.Name == "User")
                {
                    SourceUser = n.InnerText;
                }
                else if (n.Name == "PassWord")
                {
                    SourcePassWord = n.InnerText;
                }
                else if (n.Name == "DataBase")
                {
                    SourceDataBase = n.InnerText;
                }
                else if (n.Name == "TypeSQL")
                {
                    SourceTypeSQL = n.InnerText;
                }
                else if (n.Name == "UserSap")
                {
                    SourceSAPUser = n.InnerText;
                }
                else if (n.Name == "PassWordSap")
                {
                    SourceSAPPassWord = n.InnerText;
                }
            }
            #endregion

            #region SAP Target
            diCompanyTarget = new SAPbobsCOM.Company();
            diCompanyTarget.Server = SourceServer;
            diCompanyTarget.UseTrusted = false;
            diCompanyTarget.DbUserName = SourceUser;
            diCompanyTarget.DbPassword = SourcePassWord;
            diCompanyTarget.CompanyDB = SourceDataBase;
            diCompanyTarget.UserName = SourceSAPUser;
            diCompanyTarget.Password = SourceSAPPassWord;
            if (SourceTypeSQL == "2005")
            {
                diCompanyTarget.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005;
            }
            else if (SourceTypeSQL == "2008")
            {
                diCompanyTarget.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;
            }
            else if (SourceTypeSQL == "2012")
            {
                diCompanyTarget.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
            }
            else
            {
                diCompanyTarget.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL;
            }
            diCompanyTarget.language = SAPbobsCOM.BoSuppLangs.ln_Portuguese_Br;

            lRetCode = diCompanyTarget.Connect();
            sErrMsg = string.Empty;

            if (lRetCode != 0)
            {
                diCompanyTarget.GetLastError(out lRetCode, out sErrMsg);
                B1Connections.theAppl.StatusBar.SetText(string.Format("Erro ao conectar na base Destino - {0}", sErrMsg), BoMessageTime.bmt_Long, BoStatusBarMessageType.smt_Error);
            }
            #endregion
        }

        private B1Connections()
        {
        }

        public static int Init(string connStr, string addOnIdStr, ConnectionType cnxType)
        {
            try
            {
                SboGuiApi api = new SboGuiApiClass {
                    AddonIdentifier = addOnIdentifierStr = addOnIdStr
                };
                api.Connect(connStr);
                theAppl = api.GetApplication(-1);
            }
            catch (COMException exception)
            {
                throw exception;
            }
            connectType = cnxType;
            return Reinit();
        }

        public static int Init(string connStr, string addOnIdStr, bool diRequired)
        {
            try
            {
                SboGuiApi api = new SboGuiApiClass {
                    AddonIdentifier = addOnIdentifierStr = addOnIdStr
                };
                api.Connect(connStr);
                theAppl = api.GetApplication(-1);
            }
            catch (COMException exception)
            {
                throw exception;
            }
            if (diRequired)
            {
                connectType = ConnectionType.SSO;
            }
            else
            {
                connectType = ConnectionType.OnlyUI;
            }
            return Reinit();
        }

        public static int Reinit()
        {
            int num = 0;
            if ((connectType == ConnectionType.SSO) || (connectType == ConnectionType.MultipleAddOns))
            {
                try
                {
                    if (connectType == ConnectionType.MultipleAddOns)
                    {
                        diCompany = (SAPbobsCOM.Company) theAppl.Company.GetDICompany();
                        return num;
                    }
                    if ((diCompany != null) && diCompany.Connected)
                    {
                        diCompany.Disconnect();
                        diCompany = null;
                    }
                    diCompany = new SAPbobsCOM.CompanyClass();
                    string contextCookie = diCompany.GetContextCookie();
                    string connectionContext = theAppl.Company.GetConnectionContext(contextCookie);
                    num = diCompany.SetSboLoginContext(connectionContext);
                    if (num != 0)
                    {
                        return num;
                    }
                    diCompany.AddonIdentifier = addOnIdentifierStr;
                    num = diCompany.Connect();
                    if (num != 0)
                    {
                        return num;
                    }
                }
                catch (COMException exception)
                {
                    throw exception;
                }
            }
            return num;
        }

        public static System.Data.DataTable ExecuteSqlDataTable(string Query)
        {
            try
            {
                System.Data.DataTable oDT = new System.Data.DataTable();
                SAPbobsCOM.Recordset oRS = (SAPbobsCOM.Recordset)diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRS.DoQuery(Query);
                if (!oRS.EoF)
                {
                    oRS.MoveFirst();
                    for (int i = 0; i < oRS.Fields.Count; i++)
                    {
                        oDT.Columns.Add(oRS.Fields.Item(i).Name);
                    }
                    System.Data.DataRow oDR;
                    for (int x = 0; x < oRS.RecordCount; x++)
                    {
                        oDR = oDT.NewRow();

                        for (int i = 0; i < oRS.Fields.Count; i++)
                        {
                            oDR[i] = Convert.ToString(oRS.Fields.Item(i).Value);
                        }
                        oDT.Rows.Add(oDR);
                        oRS.MoveNext();
                    }
                }

                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRS);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                return oDT;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static object ExecuteSqlScalar(string Query)
        {
            try
            {
                object obj = null;
                SAPbobsCOM.Recordset oRS = (SAPbobsCOM.Recordset)diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRS.DoQuery(Query);
                if (!oRS.EoF)
                {
                    obj = oRS.Fields.Item(0).Value;
                }

                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRS);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public enum ConnectionType
        {
            OnlyUI,
            SSO,
            MultipleAddOns
        }
    }
}

