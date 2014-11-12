namespace B1WizardBase
{
    using SAPbobsCOM;
    using System;
    using System.CodeDom;
    using System.Runtime.InteropServices;

    public class B1Udo
    {
        public BoYesNoEnum CanCancel;
        public BoYesNoEnum CanClose;
        public BoYesNoEnum CanDelete;
        public BoYesNoEnum CanFind;
        public BoYesNoEnum CanLog;
        public BoYesNoEnum CanYearTransfer;
        public string[] Children;
        public string Code;
        public string[] FindColumnsAlias;
        public string[] FindColumnsDesc;
        public string LogTableName;
        public BoYesNoEnum ManageSeries;
        public string Name;
        public string Table;
        public BoUDOObjType Type;

        public B1Udo()
        {
            this.Children = new string[0];
            this.FindColumnsAlias = new string[0];
            this.FindColumnsDesc = new string[0];
        }

        public B1Udo(string code, string name, string table, string[] children, BoUDOObjType type, BoYesNoEnum canFind, BoYesNoEnum canDelete, BoYesNoEnum canCancel, BoYesNoEnum canClose, BoYesNoEnum canYearTransfer, BoYesNoEnum canLog, BoYesNoEnum manageSeries, string logTableName, string[] findColumnsAlias, string[] findColumnsDesc)
        {
            this.Children = new string[0];
            this.FindColumnsAlias = new string[0];
            this.FindColumnsDesc = new string[0];
            this.Code = code;
            this.Name = name;
            this.Table = table;
            this.Children = children;
            this.Type = type;
            this.CanFind = canFind;
            this.CanDelete = canDelete;
            this.CanCancel = canCancel;
            this.CanClose = canClose;
            this.ManageSeries = manageSeries;
            this.CanYearTransfer = canYearTransfer;
            this.CanLog = canLog;
            this.LogTableName = logTableName;
            this.FindColumnsAlias = findColumnsAlias;
            this.FindColumnsDesc = findColumnsDesc;
        }

        public int Add(Company company)
        {
            UserObjectsMD businessObject = (UserObjectsMD) company.GetBusinessObject(BoObjectTypes.oUserObjectsMD);
            businessObject.Code = this.Code;
            businessObject.Name = this.Name;
            businessObject.TableName = this.Table;
            businessObject.ObjectType = this.Type;
            foreach (string str in this.Children)
            {
                businessObject.ChildTables.TableName = str;
                businessObject.ChildTables.Add();
            }
            businessObject.CanFind = this.CanFind;
            if (this.CanFind == BoYesNoEnum.tYES)
            {
                for (int i = 0; i < this.FindColumnsAlias.GetLength(0); i++)
                {
                    businessObject.FindColumns.ColumnAlias = this.FindColumnsAlias[i];
                    businessObject.FindColumns.ColumnDescription = this.FindColumnsDesc[i];
                    businessObject.FindColumns.Add();
                }
            }
            businessObject.CanDelete = this.CanDelete;
            businessObject.CanCancel = this.CanCancel;
            businessObject.CanClose = this.CanClose;
            businessObject.ManageSeries = this.ManageSeries;
            businessObject.CanYearTransfer = this.CanYearTransfer;
            businessObject.CanLog = this.CanLog;
            businessObject.LogTableName = this.LogTableName;
            int num2 = businessObject.Add();
            Marshal.ReleaseComObject(businessObject);
            businessObject = null;
            return num2;
        }

        public override bool Equals(object obj)
        {
            if (obj is B1Udo)
            {
                B1Udo udo = (B1Udo) obj;
                return udo.Code.Equals(this.Code);
            }
            return base.Equals(obj);
        }

        public CodeExpression GenerateCtor()
        {
            int num = 0;
            CodeExpression[] initializers = new CodeExpression[this.Children.Length];
            foreach (string str in this.Children)
            {
                initializers[num++] = new CodePrimitiveExpression(str);
            }
            CodeArrayCreateExpression expression = new CodeArrayCreateExpression("System.String", initializers);
            num = 0;
            CodeExpression[] expressionArray2 = new CodeExpression[this.FindColumnsAlias.Length];
            foreach (string str2 in this.FindColumnsAlias)
            {
                expressionArray2[num++] = new CodePrimitiveExpression(str2);
            }
            CodeArrayCreateExpression expression2 = new CodeArrayCreateExpression("System.String", expressionArray2);
            num = 0;
            CodeExpression[] expressionArray3 = new CodeExpression[this.FindColumnsDesc.Length];
            foreach (string str3 in this.FindColumnsDesc)
            {
                expressionArray3[num++] = new CodePrimitiveExpression(str3);
            }
            CodeArrayCreateExpression expression3 = new CodeArrayCreateExpression("System.String", expressionArray3);
            return new CodeObjectCreateExpression("B1Udo", new CodeExpression[] { new CodePrimitiveExpression(this.Code), new CodePrimitiveExpression(this.Name), new CodePrimitiveExpression(this.Table), expression, new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("BoUDOObjType"), this.Type.ToString()), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("BoYesNoEnum"), this.CanFind.ToString()), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("BoYesNoEnum"), this.CanDelete.ToString()), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("BoYesNoEnum"), this.CanCancel.ToString()), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("BoYesNoEnum"), this.CanClose.ToString()), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("BoYesNoEnum"), this.CanYearTransfer.ToString()), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("BoYesNoEnum"), this.CanLog.ToString()), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("BoYesNoEnum"), this.ManageSeries.ToString()), new CodePrimitiveExpression(this.LogTableName), expression2, expression3 });
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

