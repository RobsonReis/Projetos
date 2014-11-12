namespace B1WizardMatrix
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("xsd", "2.0.50727.42"), XmlType(AnonymousType=true), DesignerCategory("code"), DebuggerStepThrough]
    public class MatrixRow
    {
        private MatrixRowColumn[] columnsField;
        private bool visibleField;

        [XmlArrayItem("Column", IsNullable=false)]
        public MatrixRowColumn[] Columns
        {
            get
            {
                return this.columnsField;
            }
            set
            {
                this.columnsField = value;
            }
        }

        public bool Visible
        {
            get
            {
                return this.visibleField;
            }
            set
            {
                this.visibleField = value;
            }
        }
    }
}

