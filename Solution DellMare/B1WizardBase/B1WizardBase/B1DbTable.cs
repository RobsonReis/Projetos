namespace B1WizardBase
{
    using SAPbobsCOM;
    using System;
    using System.CodeDom;
    using System.Runtime.InteropServices;

    public class B1DbTable
    {
        public string Description;
        public string Name;
        public BoUTBTableType Type;

        public B1DbTable()
        {
        }

        public B1DbTable(string name, string description, BoUTBTableType type)
        {
            this.Name = name;
            this.Description = description;
            this.Type = type;
        }

        public int Add(Company company)
        {
            UserTablesMD o = null;
            int num = -1;
            try
            {
                o = (UserTablesMD) company.GetBusinessObject(BoObjectTypes.oUserTables);
                o.TableName = this.Name.Substring(1);
                o.TableType = this.Type;
                o.TableDescription = this.Description;
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
            if (obj is B1DbTable)
            {
                B1DbTable table = obj as B1DbTable;
                return table.Name.Equals(this.Name);
            }
            return base.Equals(obj);
        }

        public CodeExpression GenerateCtor()
        {
            return new CodeObjectCreateExpression("B1DbTable", new CodeExpression[] { new CodePrimitiveExpression(this.Name), new CodePrimitiveExpression(this.Description), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("BoUTBTableType"), this.Type.ToString()) });
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

