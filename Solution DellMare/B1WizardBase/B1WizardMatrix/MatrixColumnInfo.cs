namespace B1WizardMatrix
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("xsd", "2.0.50727.42"), DesignerCategory("code"), XmlType(AnonymousType=true), DebuggerStepThrough]
    public class MatrixColumnInfo
    {
        private bool affectsFormModeField;
        private long backColorField;
        private string chooseFromListAliasField;
        private string chooseFromListUIDField;
        private MatrixColumnInfoDataBind dataBindField;
        private string descriptionField;
        private bool displayDescField;
        private bool editableField;
        private long fontSizeField;
        private long foreColorField;
        private bool rightJustifiedField;
        private long textStyleField;
        private string titleField;
        private long typeField;
        private string uniqueIDField;
        private MatrixColumnInfoValidValue[] validValuesField;
        private string valOFFField;
        private string valONField;
        private bool visibleField;
        private long widthField;

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

        public string ChooseFromListAlias
        {
            get
            {
                return this.chooseFromListAliasField;
            }
            set
            {
                this.chooseFromListAliasField = value;
            }
        }

        public string ChooseFromListUID
        {
            get
            {
                return this.chooseFromListUIDField;
            }
            set
            {
                this.chooseFromListUIDField = value;
            }
        }

        public MatrixColumnInfoDataBind DataBind
        {
            get
            {
                return this.dataBindField;
            }
            set
            {
                this.dataBindField = value;
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

        public bool Editable
        {
            get
            {
                return this.editableField;
            }
            set
            {
                this.editableField = value;
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

        public string Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
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

        [XmlArrayItem("ValidValue", IsNullable=false)]
        public MatrixColumnInfoValidValue[] ValidValues
        {
            get
            {
                return this.validValuesField;
            }
            set
            {
                this.validValuesField = value;
            }
        }

        public string ValOFF
        {
            get
            {
                return this.valOFFField;
            }
            set
            {
                this.valOFFField = value;
            }
        }

        public string ValON
        {
            get
            {
                return this.valONField;
            }
            set
            {
                this.valONField = value;
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

