namespace B1WizardBase
{
    using SAPbobsCOM;
    using System;
    using System.CodeDom;
    using System.Runtime.InteropServices;

    public class B1DbColumn
    {
        public int DefaultValue;
        public string Description;
        public bool HasValidValues;
        public bool IsNullable;
        public string LinkedTo;
        public string Name;
        public int Size;
        public BoFldSubTypes SubType;
        public string Table;
        public BoFieldTypes Type;
        public B1DbValidValue[] ValidValues;

        public B1DbColumn()
        {
            this.ValidValues = new B1DbValidValue[0];
            this.DefaultValue = -1;
        }

        public B1DbColumn(Fields fields)
        {
            this.ValidValues = new B1DbValidValue[0];
            this.DefaultValue = -1;
            this.Name = (string) fields.Item(0).Value;
            this.Size = (int) fields.Item(1).Value;
            this.Type = (BoFieldTypes) fields.Item(2).Value;
            this.IsNullable = !((string) fields.Item(3).Value).Equals("0");
            this.HasValidValues = ((int) fields.Item(4).Value) != 0;
            this.LinkedTo = (string) fields.Item(5).Value;
            this.Description = (string) fields.Item(6).Value;
        }

        public B1DbColumn(UserFieldsMD field)
        {
            this.ValidValues = new B1DbValidValue[0];
            this.DefaultValue = -1;
            this.Table = field.TableName;
            this.Name = field.Name;
            this.Size = field.Size;
            this.Type = field.Type;
            this.SubType = field.SubType;
            this.IsNullable = field.Mandatory != BoYesNoEnum.tYES;
            if (field.ValidValues.Count == 0)
            {
                this.HasValidValues = false;
            }
            else
            {
                field.ValidValues.SetCurrentLine(0);
                this.HasValidValues = field.ValidValues.Value != "";
            }
            this.LinkedTo = field.LinkedTable;
            this.Description = field.Description;
        }

        public B1DbColumn(string table, string name, string description, BoFieldTypes type, int size, B1DbValidValue[] validValues, int defaultValue) : this(table, name, description, type, BoFldSubTypes.st_None, size, validValues, defaultValue)
        {
        }

        public B1DbColumn(string table, string name, string description, BoFieldTypes type, BoFldSubTypes subtype, int size, B1DbValidValue[] validValues, int defaultValue) : this(table, name, description, type, subtype, size, false, validValues, defaultValue)
        {
        }

        public B1DbColumn(string table, string name, string description, BoFieldTypes type, BoFldSubTypes subtype, int size, bool mandatory, B1DbValidValue[] validValues, int defaultValue)
        {
            this.ValidValues = new B1DbValidValue[0];
            this.DefaultValue = -1;
            this.Table = table;
            this.Name = name;
            this.Description = description;
            this.Type = type;
            this.SubType = subtype;
            this.Size = size;
            this.IsNullable = mandatory;
            this.ValidValues = validValues;
            this.DefaultValue = defaultValue;
        }

        public int Add(Company company)
        {
            UserFieldsMD o = null;
            int num = -1;
            try
            {
                o = (UserFieldsMD) company.GetBusinessObject(BoObjectTypes.oUserFields);
                o.TableName = this.Table;
                o.Name = this.Name;
                o.Description = this.Description;
                o.Type = this.Type;
                o.SubType = this.SubType;
                if (this.Size != 0)
                {
                    o.EditSize = this.Size;
                }
                o.Mandatory = this.IsNullable ? BoYesNoEnum.tNO : BoYesNoEnum.tYES;
                foreach (B1DbValidValue value2 in this.ValidValues)
                {
                    o.ValidValues.Value = value2.Val;
                    o.ValidValues.Description = value2.Description;
                    o.ValidValues.Add();
                }
                if (this.DefaultValue != -1)
                {
                    o.DefaultValue = this.ValidValues[this.DefaultValue].Val;
                }
                num = o.Add();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                Marshal.ReleaseComObject(o);
                o = null;
            }
            return num;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is B1DbColumn))
            {
                return base.Equals(obj);
            }
            B1DbColumn column = (B1DbColumn) obj;
            return (column.Table.Equals(this.Table) && column.Name.Equals(this.Name));
        }

        public CodeExpression GenerateCtor()
        {
            int num = 0;
            CodeExpression[] initializers = new CodeExpression[this.ValidValues.Length];
            foreach (B1DbValidValue value2 in this.ValidValues)
            {
                initializers[num++] = new CodeObjectCreateExpression("B1WizardBase.B1DbValidValue", new CodeExpression[] { new CodePrimitiveExpression(value2.Val), new CodePrimitiveExpression(value2.Description) });
            }
            CodeArrayCreateExpression expression = new CodeArrayCreateExpression("B1WizardBase.B1DbValidValue", initializers);
            return new CodeObjectCreateExpression("B1DbColumn", new CodeExpression[] { new CodePrimitiveExpression(this.Table), new CodePrimitiveExpression(this.Name), new CodePrimitiveExpression(this.Description), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("BoFieldTypes"), this.Type.ToString()), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("BoFldSubTypes"), this.SubType.ToString()), new CodePrimitiveExpression(this.Size), new CodePrimitiveExpression(this.IsNullable), expression, new CodePrimitiveExpression(this.DefaultValue) });
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

