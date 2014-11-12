using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using B1WizardBase;

namespace DellMare.Addon
{    
    /// <summary>
    /// Classe de criação das tabelas e campos de usuário
    /// </summary>
    public class DB
    {
        public DB()
        {
        }

        #region Metodos
        public void CriaMetaDados()
        {
            try
            {
                #region CRIATE TABLES
                string[,] vv;
                B1Connections.diCompany.StartTransaction();

                #region Campos CRM Web
                AddUserField("OCLG", "webuser", "Usuário CRM", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 100, null, null, null);
                AddUserField("OCLG", "OprId", "Id da Oportunidade", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 100, null, null, null);
                AddUserField("OCLG", "OprLine", "Linha da Oportunidade", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 100, null, null, null);
                AddUserField("OCLG", "priority", "Prioridade", SAPbobsCOM.BoFieldTypes.db_Numeric, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, null);
                AddUserField("OCRD", "webuser", "Usuário CRM", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 100, null, null, null);
                vv = new string[8, 2];
                vv[0, 0] = "1";
                vv[0, 1] = "Segunda";
                vv[1, 0] = "2";
                vv[1, 1] = "Terça";
                vv[2, 0] = "3";
                vv[2, 1] = "Quarta";
                vv[3, 0] = "4";
                vv[3, 1] = "Quinta";
                vv[4, 0] = "5";
                vv[4, 1] = "Sexta";
                vv[5, 0] = "6";
                vv[5, 1] = "Sábado";
                vv[6, 0] = "7";
                vv[6, 1] = "Domingo";
                vv[7, 0] = "-1";
                vv[7, 1] = "Nennhum";
                AddUserField("OCRD", "Dia_Semana", "Dia da Semana", SAPbobsCOM.BoFieldTypes.db_Numeric, SAPbobsCOM.BoFldSubTypes.st_None, 2, vv, "-1", null);
                AddUserField("OCRD", "Hora", "Hora", SAPbobsCOM.BoFieldTypes.db_Date, SAPbobsCOM.BoFldSubTypes.st_Time, 4, null, null, null);
                AddUserField("ORDR", "webuser", "Usuário CRM", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 100, null, null, null);
                AddUserField("ORDR", "BasePriceOriginal", "Cotação Origem", SAPbobsCOM.BoFieldTypes.db_Numeric, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, null);
                vv = new string[2, 2];
                vv[0, 0] = "S";
                vv[0, 1] = "Sim";
                vv[1, 0] = "N";
                vv[1, 1] = "Não";
                AddUserField("ORDR", "Leasing", "Leasing", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1, vv, "N", null);
                AddUserField("ORDR", "PercLucro", "Percentual de Lucro", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Percentage, 10, null, null, null);
                AddUserField("ORDR", "ValorLucro", "Valor do Lucro", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Price, 10, null, null, null);
                AddUserField("RDR1", "PriceOriginal", "Preço Original", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Price, 10, null, null, null);
                AddUserField("RDR1", "Custo", "Custo", SAPbobsCOM.BoFieldTypes.db_Float, SAPbobsCOM.BoFldSubTypes.st_Price, 10, null, null, null);
                AddUserField("RDR1", "BaseLinePriceOrig", "Base Line Original", SAPbobsCOM.BoFieldTypes.db_Numeric, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, null);
                AddUserField("OOPR", "webuser", "Usuário CRM", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 100, null, null, null);
                #endregion

                #region Entrada de Mercadoria por Entrega
                AddUserField("ODLN", "EntPendente", "Entrada Pendente", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1, vv, "N", null);
                AddUserField("OUSG", "EntMercadoria", "Entrada de Mercadoria", SAPbobsCOM.BoFieldTypes.db_Alpha, SAPbobsCOM.BoFldSubTypes.st_None, 1, vv, "N", null);
                AddUserField("OIGN", "DocEntrega", "Numero da Entrega", SAPbobsCOM.BoFieldTypes.db_Numeric, SAPbobsCOM.BoFldSubTypes.st_None, 10, null, null, null);
                #endregion

                B1Connections.diCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                #endregion



            }
            catch (Exception ex)
            {
                if (B1Connections.diCompany.InTransaction) B1Connections.diCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                throw ex;
            }
        }

        /// <summary>
        /// Verifica se a tabela já existe na base de dados do B1
        /// </summary>
        /// <param name="TBName">Nome da Tabela</param>
        /// <returns>bool - true/false indicando se a tabela existe ou não</returns>
        /// 
        public bool ExisteTB(string TBName)
        {
            SAPbobsCOM.UserTablesMD oUserTable;
            oUserTable = (SAPbobsCOM.UserTablesMD)B1Connections.diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);
            //UserTablesMD oUserTable = new UserTablesMD(ref oDiCompany);            
            bool ret = oUserTable.GetByKey(TBName);
            int errCode;string errMsg;
            B1Connections.diCompany.GetLastError(out errCode, out errMsg);

            TBName = null;
            errMsg = null;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserTable);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            
            return (ret);
        }

        /// <summary>
        /// Criação dos campos de usuários
        /// </summary>
        /// <param name="NomeTabela">Tabela</param>
        /// <param name="NomeCampo">Come do campo</param>
        /// <param name="DescCampo">Descrição do campo</param>
        /// <param name="Tipo">Tipo do Campo</param>
        /// <param name="SubTipo">Sub Tipo</param>
        /// <param name="Tamanho">Tamanho</param>
        /// <returns>bool - true/false indicando se o campo foi criado</returns>
        //public void AddUserField(string NomeTabela, string NomeCampo, string DescCampo, SAPbobsCOM.BoFieldTypes Tipo, SAPbobsCOM.BoFldSubTypes SubTipo, Int16 Tamanho, string[,] valoresValidos, string valorDefault)
        public void AddUserField(string NomeTabela, string NomeCampo, string DescCampo, SAPbobsCOM.BoFieldTypes Tipo, SAPbobsCOM.BoFldSubTypes SubTipo, Int16 Tamanho, string[,] valoresValidos, string valorDefault, string linkedTable)
        {
            int lErrCode;
            string sErrMsg = "";

            try
            {
                string sSquery = "SELECT [name] FROM syscolumns WHERE [name] = 'U_" + NomeCampo + " ' and id = (SELECT id FROM sysobjects WHERE type = 'U'AND [NAME] = '" + NomeTabela.Replace("[", "").Replace("]", "") + "')";
                object oResult = B1Connections.ExecuteSqlScalar(sSquery);
                if (oResult != null) return;

                SAPbobsCOM.UserFieldsMD oUserField;
                oUserField = (SAPbobsCOM.UserFieldsMD)B1Connections.diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);
                oUserField.TableName = NomeTabela.Replace("@", "").Replace("[", "").Replace("]", "").Trim();
                oUserField.Name = NomeCampo;
                oUserField.Description = DescCampo;
                oUserField.Type = Tipo;
                oUserField.SubType = SubTipo;
                oUserField.DefaultValue = valorDefault;
                if (!string.IsNullOrEmpty(linkedTable)) oUserField.LinkedTable = linkedTable;
                
                //adicionar valores válidos
                if (valoresValidos != null)
                {
                    Int32 qtd = valoresValidos.GetLength(0);
                    if (qtd > 0)
                    {
                        for (int i = 0; i < qtd; i++)
                        {
                            oUserField.ValidValues.Value = valoresValidos[i, 0];
                            oUserField.ValidValues.Description = valoresValidos[i, 1];
                            oUserField.ValidValues.Add();
                        }
                    }
                }

                if (Tamanho != 0)
                    oUserField.EditSize = Tamanho;

                try
                {
                    oUserField.Add();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserField);
                    oUserField = null;
                    B1Connections.diCompany.GetLastError(out lErrCode, out sErrMsg);
                    if (lErrCode != 0)
                    {
                        throw new Exception(sErrMsg);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                oUserField = null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
            
        /// <summary>
        /// Adiciona tabela de usuário à Base do B1
        /// </summary>
        /// <param name="NomeTB">Nome da tabela a ser criada</param>
        /// <param name="Desc">Descrição da tabela a ser criada</param>
        public void AddUserTable(string NomeTB, string Desc, SAPbobsCOM.BoUTBTableType oTableType)
        {
            int lErrCode;
            string sErrMsg = "";

            
            SAPbobsCOM.UserTablesMD oUserTable;

            oUserTable = (SAPbobsCOM.UserTablesMD)B1Connections.diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);

            try
            {
                oUserTable.TableName = NomeTB.Replace("@", "").Replace("[", "").Replace("]", "").Trim();
                oUserTable.TableDescription = Desc;
                oUserTable.TableType = oTableType;
                try
                {
                    oUserTable.Add();
                    B1Connections.diCompany.GetLastError(out lErrCode, out sErrMsg);
                    if (lErrCode != 0)
                    {
                        throw new Exception(sErrMsg);
                    }

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserTable);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                catch (Exception)
                {
                    throw;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AddUDO(string sUDO, string sTable, string sDescricaoUDO, SAPbobsCOM.BoUDOObjType oBoUDOObjType, string[] childTableName, string[] childObjectName)
        {
            int lRetCode = 0;
            int iTabelasFilhas = 0;
            string sErrMsg = "";
            string sQuery = "";
            bool bUpdate = false;
            bool bExisteColuna = false;
            bool bExisteTabelaFilha = false;

            SAPbobsCOM.UserObjectsMD oUserObjectMD = null;

            oUserObjectMD = (SAPbobsCOM.UserObjectsMD)B1Connections.diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD);

            System.Data.DataTable tb = new System.Data.DataTable();

            try
            {
                if (oUserObjectMD.GetByKey(sUDO))
                {
                    return;
                }

                oUserObjectMD.CanCancel = SAPbobsCOM.BoYesNoEnum.tNO;
                oUserObjectMD.CanClose = SAPbobsCOM.BoYesNoEnum.tNO;
                oUserObjectMD.CanDelete = SAPbobsCOM.BoYesNoEnum.tYES;
                oUserObjectMD.CanFind = SAPbobsCOM.BoYesNoEnum.tYES;
                oUserObjectMD.CanYearTransfer = SAPbobsCOM.BoYesNoEnum.tNO;
                oUserObjectMD.CanCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tNO;
                oUserObjectMD.ManageSeries = SAPbobsCOM.BoYesNoEnum.tNO;
                oUserObjectMD.Code = sUDO;
                oUserObjectMD.Name = sDescricaoUDO;
                oUserObjectMD.ObjectType = oBoUDOObjType;
                oUserObjectMD.TableName = sTable;

                sQuery = String.Format("SELECT  COLUNAS.NAME AS COLUNA " + Environment.NewLine +
                                       " FROM SYSOBJECTS AS TABELAS," + Environment.NewLine +
                                       "      SYSCOLUMNS AS COLUNAS" + Environment.NewLine +
                                       " WHERE " + Environment.NewLine +
                                       "     TABELAS.ID = COLUNAS.ID" + Environment.NewLine +
                                       "     AND TABELAS.NAME = '@{0}' and (left(COLUNAS.NAME,2)='U_' or COLUNAS.NAME IN ('DocEntry'))", sTable);

                tb = B1Connections.ExecuteSqlDataTable(sQuery);

                int count = 0;
                foreach (System.Data.DataRow oRow in tb.Rows)
                {
                    bExisteColuna = false;
                    //verificar se existe coluna
                    for (int g = 0; g < oUserObjectMD.FindColumns.Count; g++)
                    {
                        oUserObjectMD.FindColumns.SetCurrentLine(g);
                        if (oUserObjectMD.FindColumns.ColumnAlias == oRow["COLUNA"].ToString())
                        {
                            bExisteColuna = true;
                            break;
                        }
                    }

                    if (bExisteColuna == true)
                    {
                        oUserObjectMD.FindColumns.ColumnDescription = oRow["COLUNA"].ToString();
                    }
                    else
                    {
                        if (count > 0) oUserObjectMD.FindColumns.Add();
                        oUserObjectMD.FindColumns.ColumnAlias = oRow["COLUNA"].ToString();
                        oUserObjectMD.FindColumns.ColumnDescription = oRow["COLUNA"].ToString();
                    }

                    count++;
                }

                //Adicionar tabelas filhas
                if (childObjectName != null)
                {
                    for (int x = 0; x < childObjectName.Length; x++)
                    {

                        iTabelasFilhas = oUserObjectMD.ChildTables.Count;
                        bExisteTabelaFilha = false;
                        for (int y = 0; y < iTabelasFilhas; y++)
                        {
                            oUserObjectMD.ChildTables.SetCurrentLine(y);
                            if (oUserObjectMD.ChildTables.TableName == childTableName[x])
                            {
                                bExisteTabelaFilha = true;
                                break;
                            }
                        }

                        if (bExisteTabelaFilha == false)
                        {
                            if (x > 0) oUserObjectMD.ChildTables.Add();
                            if (childObjectName[x] != "" && childTableName[x] != "")
                            {
                                oUserObjectMD.ChildTables.TableName = childTableName[x];
                                oUserObjectMD.ChildTables.ObjectName = childObjectName[x];
                            }
                        }

                    }
                }

                if (bUpdate)
                    lRetCode = oUserObjectMD.Update();
                else
                    lRetCode = oUserObjectMD.Add();

                // check for errors in the process
                if (lRetCode != 0)
                {
                    B1Connections.diCompany.GetLastError(out lRetCode, out sErrMsg);
                }

            }
            catch (Exception e)
            { System.Windows.Forms.MessageBox.Show(e.ToString()); }

            tb.Dispose();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserObjectMD);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #endregion
}
}




