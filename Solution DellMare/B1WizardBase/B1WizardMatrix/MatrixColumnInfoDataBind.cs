namespace B1WizardMatrix
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, XmlType(AnonymousType=true), DebuggerStepThrough, DesignerCategory("code"), GeneratedCode("xsd", "2.0.50727.42")]
    public class MatrixColumnInfoDataBind
    {
        private string aliasField;
        private bool dataBoundField;
        private string tableNameField;

        public string Alias
        {
            get
            {
                return this.aliasField;
            }
            set
            {
                this.aliasField = value;
            }
        }

        public bool DataBound
        {
            get
            {
                return this.dataBoundField;
            }
            set
            {
                this.dataBoundField = value;
            }
        }

        public string TableName
        {
            get
            {
                return this.tableNameField;
            }
            set
            {
                this.tableNameField = value;
            }
        }
    }
}

