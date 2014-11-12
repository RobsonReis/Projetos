namespace B1WizardMatrix
{
    using B1WizardBase;
    using SAPbouiCOM;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Xml.Serialization;

    [Serializable, DesignerCategory("code"), XmlRoot(Namespace="", IsNullable=false), XmlType(AnonymousType=true), DebuggerStepThrough, GeneratedCode("xsd", "2.0.50727.42")]
    public class Matrix
    {
        private bool affectsFormModeField;
        private long backColorField;
        private MatrixColumnInfo[] columnsInfoField;
        private string descriptionField;
        private bool displayDescField;
        private bool enabledField;
        private long fontSizeField;
        private long foreColorField;
        private long fromPaneField;
        private long heightField;
        private long layoutField;
        private long leftField;
        private string linkToField;
        private bool rightJustifiedField;
        private MatrixRow[] rowsField;
        private long textStyleField;
        private long toPaneField;
        private long topField;
        private long typeField;
        private string uniqueIDField;
        private bool visibleField;
        private long widthField;

        private Matrix()
        {
        }

        public static B1WizardMatrix.Matrix Init(SAPbouiCOM.Matrix oMtx)
        {
            try
            {
                StringReader textReader = new StringReader(oMtx.SerializeAsXML(BoMatrixXmlSelect.mxs_All));
                XmlSerializer serializer = new XmlSerializer(typeof(B1WizardMatrix.Matrix));
                return (B1WizardMatrix.Matrix) serializer.Deserialize(textReader);
            }
            catch (Exception exception)
            {
                B1Connections.theAppl.MessageBox("Error in B1Matrix constructor " + exception.Message, 1, "Ok", "", "");
            }
            return null;
        }

        public bool AffectsFormMode
        {
            get
            {
                return this.affectsFormModeField;
            }
            set
            {
                this.affectsFormModeField = value;
            }
        }

        public long BackColor
        {
            get
            {
                return this.backColorField;
            }
            set
            {
                this.backColorField = value;
            }
        }

        [XmlArrayItem("ColumnInfo", IsNullable=false)]
        public MatrixColumnInfo[] ColumnsInfo
        {
            get
            {
                return this.columnsInfoField;
            }
            set
            {
                this.columnsInfoField = value;
            }
        }

        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        public bool DisplayDesc
        {
            get
            {
                return this.displayDescField;
            }
            set
            {
                this.displayDescField = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return this.enabledField;
            }
            set
            {
                this.enabledField = value;
            }
        }

        public long FontSize
        {
            get
            {
                return this.fontSizeField;
            }
            set
            {
                this.fontSizeField = value;
            }
        }

        public long ForeColor
        {
            get
            {
                return this.foreColorField;
            }
            set
            {
                this.foreColorField = value;
            }
        }

        public long FromPane
        {
            get
            {
                return this.fromPaneField;
            }
            set
            {
                this.fromPaneField = value;
            }
        }

        public long Height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }

        public long Layout
        {
            get
            {
                return this.layoutField;
            }
            set
            {
                this.layoutField = value;
            }
        }

        public long Left
        {
            get
            {
                return this.leftField;
            }
            set
            {
                this.leftField = value;
            }
        }

        public string LinkTo
        {
            get
            {
                return this.linkToField;
            }
            set
            {
                this.linkToField = value;
            }
        }

        public bool RightJustified
        {
            get
            {
                return this.rightJustifiedField;
            }
            set
            {
                this.rightJustifiedField = value;
            }
        }

        [XmlArrayItem("Row", IsNullable=false)]
        public MatrixRow[] Rows
        {
            get
            {
                return this.rowsField;
            }
            set
            {
                this.rowsField = value;
            }
        }

        public long TextStyle
        {
            get
            {
                return this.textStyleField;
            }
            set
            {
                this.textStyleField = value;
            }
        }

        public long Top
        {
            get
            {
                return this.topField;
            }
            set
            {
                this.topField = value;
            }
        }

        public long ToPane
        {
            get
            {
                return this.toPaneField;
            }
            set
            {
                this.toPaneField = value;
            }
        }

        public long Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        public string UniqueID
        {
            get
            {
                return this.uniqueIDField;
            }
            set
            {
                this.uniqueIDField = value;
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

        public long Width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }
    }
}

