namespace B1WizardBase
{
    using SAPbobsCOM;
    using System;
    using System.CodeDom;
    using System.Runtime.InteropServices;

    public class B1DbKey
    {
        public string[] Elements;
        public bool IsPrimary;
        public bool IsUnique;
        public string Name;
        public string TableName;

        public B1DbKey()
        {
            this.Elements = new string[0];
        }

        public B1DbKey(string table, string name, bool isUnique, string[] elements)
        {
            this.Elements = new string[0];
            this.TableName = table;
            this.Name = name;
            this.IsUnique = isUnique;
            this.Elements = elements;
        }

        public int Add(Company company)
        {
            UserKeysMD o = null;
            int num = -1;
            try
            {
                o = (UserKeysMD) company.GetBusinessObject(BoObjectTypes.oUserKeys);
                o.TableName = this.TableName;
                o.KeyName = this.Name;
                o.Unique = this.IsUnique ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                bool flag = true;
                foreach (string str in this.Elements)
                {
                    if (flag)
                    {
                        flag = false;
                    }
                    else
                    {
                        o.Elements.Add();
                    }
                    o.Elements.ColumnAlias = str;
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
            if (!(obj is B1DbKey))
            {
                return base.Equals(obj);
            }
            B1DbKey key = (B1DbKey) obj;
            return (key.TableName.Equals(this.TableName) && key.Name.Equals(this.Name));
        }

        public CodeExpression GenerateCtor()
        {
            int num = 0;
            CodeExpression[] initializers = new CodeExpression[this.Elements.Length];
            foreach (string str in this.Elements)
            {
                initializers[num++] = new CodePrimitiveExpression(str);
            }
            CodeArrayCreateExpression expression = new CodeArrayCreateExpression("System.String", initializers);
            return new CodeObjectCreateExpression("B1DbKey", new CodeExpression[] { new CodePrimitiveExpression(this.TableName), new CodePrimitiveExpression(this.Name), new CodePrimitiveExpression(this.IsUnique), expression });
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

