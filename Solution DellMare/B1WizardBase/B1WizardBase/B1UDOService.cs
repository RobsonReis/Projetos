namespace B1WizardBase
{
    using SAPbobsCOM;
    using System;

    public class B1UDOService
    {
        protected BaseChild[] childObjs;
        protected string[] ChildTableNames;
        protected GeneralData oUdoHeaderGeneralData;
        protected GeneralDataParams oUdoHeaderKey;
        protected GeneralService oUdoService;
        public string sClassName;
        public string sUdoUniqueID;
        protected BoUDOObjType UDOType;

        public void Add()
        {
            this.oUdoService.Add(this.oUdoHeaderGeneralData);
        }

        public void Cancel()
        {
            if (this.UDOType != BoUDOObjType.boud_Document)
            {
                throw new Exception("NotRelevantMethod");
            }
            this.oUdoService.Cancel(this.oUdoHeaderKey);
        }

        public void Close()
        {
            if (this.UDOType != BoUDOObjType.boud_Document)
            {
                throw new Exception("NotRelevantMethod");
            }
            this.oUdoService.Close(this.oUdoHeaderKey);
        }

        public void Delete()
        {
            this.oUdoService.Delete(this.oUdoHeaderKey);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is B1UDOService))
            {
                return base.Equals(obj);
            }
            B1UDOService service = obj as B1UDOService;
            if (!service.sClassName.Equals(this.sClassName))
            {
                return service.sUdoUniqueID.Equals(this.sUdoUniqueID);
            }
            return true;
        }

        public void FromXMLFile(string sFileName)
        {
            this.oUdoHeaderGeneralData.FromXMLFile(sFileName);
        }

        public void FromXMLString(string sXMLSring)
        {
            this.oUdoHeaderGeneralData.FromXMLString(sXMLSring);
        }

        public void GetByKey(string sKeyVal)
        {
            this.oUdoHeaderKey = (GeneralDataParams) this.oUdoService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
            if (this.UDOType == BoUDOObjType.boud_MasterData)
            {
                this.oUdoHeaderKey.SetProperty("Code", sKeyVal);
            }
            else
            {
                this.oUdoHeaderKey.SetProperty("DocEntry", sKeyVal);
            }
            this.oUdoHeaderGeneralData = this.oUdoService.GetByParams(this.oUdoHeaderKey);
            for (int i = 0; i < this.childObjs.Length; i++)
            {
                this.childObjs[i].oUdoLinesCollection = this.oUdoHeaderGeneralData.Child(this.ChildTableNames[i]);
                this.childObjs[i].SetCurrentLine(0);
            }
        }

        public GeneralCollectionParams GetList()
        {
            return this.oUdoService.GetList();
        }

        public string GetXMLSchema()
        {
            return this.oUdoHeaderGeneralData.GetXMLSchema();
        }

        protected void Init()
        {
            this.oUdoService = B1Connections.diCompany.GetCompanyService().GetGeneralService(this.sUdoUniqueID);
            this.oUdoHeaderGeneralData = (GeneralData) this.oUdoService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);
            this.childObjs = new BaseChild[this.ChildTableNames.Length];
        }

        public InvokeParams Invoke(InvokeParams oInvokeInput, GeneralData oGeneralData)
        {
            return this.oUdoService.InvokeMethod(oInvokeInput, oGeneralData);
        }

        public void ToXMLFile(string sFileName)
        {
            this.oUdoHeaderGeneralData.ToXMLFile(sFileName);
        }

        public string ToXMLString()
        {
            return this.oUdoHeaderGeneralData.ToXMLString();
        }

        public void Update()
        {
            this.oUdoService.Update(this.oUdoHeaderGeneralData);
        }

        public string Canceled
        {
            get
            {
                return this.oUdoHeaderGeneralData.GetProperty("Canceled").ToString();
            }
        }

        public string Code
        {
            get
            {
                if (this.UDOType != BoUDOObjType.boud_MasterData)
                {
                    throw new Exception("NotRelevantProperty");
                }
                return this.oUdoHeaderGeneralData.GetProperty("Code").ToString();
            }
            set
            {
                if (this.UDOType != BoUDOObjType.boud_MasterData)
                {
                    throw new Exception("NotRelevantProperty");
                }
                this.oUdoHeaderGeneralData.SetProperty("Code", value.ToString());
            }
        }

        public string CreateDate
        {
            get
            {
                return this.oUdoHeaderGeneralData.GetProperty("CreateDate").ToString();
            }
        }

        public string CreateTime
        {
            get
            {
                return this.oUdoHeaderGeneralData.GetProperty("CreateTime").ToString();
            }
        }

        public string DataSource
        {
            get
            {
                return this.oUdoHeaderGeneralData.GetProperty("DataSource").ToString();
            }
        }

        public string DocEntry
        {
            get
            {
                return this.oUdoHeaderGeneralData.GetProperty("DocEntry").ToString();
            }
        }

        public string DocNum
        {
            get
            {
                if (this.UDOType != BoUDOObjType.boud_Document)
                {
                    throw new Exception("NotRelevantProperty");
                }
                return this.oUdoHeaderGeneralData.GetProperty("DocNum").ToString();
            }
            set
            {
                if (this.UDOType != BoUDOObjType.boud_Document)
                {
                    throw new Exception("NotRelevantProperty");
                }
                this.oUdoHeaderGeneralData.SetProperty("DocNum", value.ToString());
            }
        }

        public string HandWrtten
        {
            get
            {
                if (this.UDOType != BoUDOObjType.boud_Document)
                {
                    throw new Exception("NotRelevantProperty");
                }
                return this.oUdoHeaderGeneralData.GetProperty("HandWrtten").ToString();
            }
            set
            {
                if (this.UDOType != BoUDOObjType.boud_Document)
                {
                    throw new Exception("NotRelevantProperty");
                }
                this.oUdoHeaderGeneralData.SetProperty("HandWrtten", value.ToString());
            }
        }

        public string Instance
        {
            get
            {
                if (this.UDOType != BoUDOObjType.boud_Document)
                {
                    throw new Exception("NotRelevantProperty");
                }
                return this.oUdoHeaderGeneralData.GetProperty("Instance").ToString();
            }
        }

        public string LogInst
        {
            get
            {
                return this.oUdoHeaderGeneralData.GetProperty("LogInst").ToString();
            }
        }

        public string Name
        {
            get
            {
                if (this.UDOType != BoUDOObjType.boud_MasterData)
                {
                    throw new Exception("NotRelevantProperty");
                }
                return this.oUdoHeaderGeneralData.GetProperty("Name").ToString();
            }
            set
            {
                if (this.UDOType != BoUDOObjType.boud_MasterData)
                {
                    throw new Exception("NotRelevantProperty");
                }
                this.oUdoHeaderGeneralData.SetProperty("Name", value.ToString());
            }
        }

        public string Object
        {
            get
            {
                return this.oUdoHeaderGeneralData.GetProperty("Object").ToString();
            }
        }

        public string Period
        {
            get
            {
                if (this.UDOType != BoUDOObjType.boud_Document)
                {
                    throw new Exception("NotRelevantProperty");
                }
                return this.oUdoHeaderGeneralData.GetProperty("Period").ToString();
            }
            set
            {
                if (this.UDOType != BoUDOObjType.boud_Document)
                {
                    throw new Exception("NotRelevantProperty");
                }
                this.oUdoHeaderGeneralData.SetProperty("Period", value.ToString());
            }
        }

        public string Series
        {
            get
            {
                if (this.UDOType != BoUDOObjType.boud_Document)
                {
                    throw new Exception("NotRelevantProperty");
                }
                return this.oUdoHeaderGeneralData.GetProperty("Series").ToString();
            }
            set
            {
                if (this.UDOType != BoUDOObjType.boud_Document)
                {
                    throw new Exception("NotRelevantProperty");
                }
                this.oUdoHeaderGeneralData.SetProperty("Series", value.ToString());
            }
        }

        public string Status
        {
            get
            {
                if (this.UDOType != BoUDOObjType.boud_Document)
                {
                    throw new Exception("NotRelevantProperty");
                }
                return this.oUdoHeaderGeneralData.GetProperty("Status").ToString();
            }
        }

        public string Transferred
        {
            get
            {
                return this.oUdoHeaderGeneralData.GetProperty("Transferred").ToString();
            }
        }

        public string UpdateDate
        {
            get
            {
                return this.oUdoHeaderGeneralData.GetProperty("UpdateDate").ToString();
            }
        }

        public string UpdateTime
        {
            get
            {
                return this.oUdoHeaderGeneralData.GetProperty("UpdateTime").ToString();
            }
        }

        public string UserSign
        {
            get
            {
                return this.oUdoHeaderGeneralData.GetProperty("UserSign").ToString();
            }
        }

        public class BaseChild
        {
            protected GeneralData oUdoLineGeneralData;
            public GeneralDataCollection oUdoLinesCollection;
            protected BoUDOObjType UDOType;

            protected BaseChild(string ChildTableName, B1UDOService udoService)
            {
                this.oUdoLinesCollection = udoService.oUdoHeaderGeneralData.Child(ChildTableName);
                this.UDOType = udoService.UDOType;
            }

            public void AddLine()
            {
                this.oUdoLineGeneralData = this.oUdoLinesCollection.Add();
            }

            public void DeleteLine(int Index)
            {
                this.oUdoLinesCollection.Remove(Index);
            }

            public void SetCurrentLine(int Index)
            {
                this.oUdoLineGeneralData = this.oUdoLinesCollection.Item(Index);
            }

            public string Code
            {
                get
                {
                    if (this.UDOType != BoUDOObjType.boud_MasterData)
                    {
                        throw new Exception("NotRelevantProperty");
                    }
                    return this.oUdoLineGeneralData.GetProperty("Code").ToString();
                }
            }

            public int Count
            {
                get
                {
                    return this.oUdoLinesCollection.Count;
                }
            }

            public string DocEntry
            {
                get
                {
                    if (this.UDOType != BoUDOObjType.boud_Document)
                    {
                        throw new Exception("NotRelevantProperty");
                    }
                    return this.oUdoLineGeneralData.GetProperty("DocEntry").ToString();
                }
            }

            public string LineId
            {
                get
                {
                    return this.oUdoLineGeneralData.GetProperty("LineId").ToString();
                }
            }

            public string LogInst
            {
                get
                {
                    return this.oUdoLineGeneralData.GetProperty("LogInst").ToString();
                }
            }

            public string Object
            {
                get
                {
                    return this.oUdoLineGeneralData.GetProperty("Object").ToString();
                }
            }

            public string VisOrder
            {
                get
                {
                    if (this.UDOType != BoUDOObjType.boud_Document)
                    {
                        throw new Exception("NotRelevantProperty");
                    }
                    return this.oUdoLineGeneralData.GetProperty("VisOrder").ToString();
                }
                set
                {
                    if (this.UDOType != BoUDOObjType.boud_Document)
                    {
                        throw new Exception("NotRelevantProperty");
                    }
                    this.oUdoLineGeneralData.SetProperty("VisOrder", value.ToString());
                }
            }
        }
    }
}

