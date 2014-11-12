namespace B1WizardBase
{
    using SAPbobsCOM;
    using System;

    public class B1Db
    {
        protected B1Cockpit[] Cockpits;
        protected B1DbColumn[] Columns;
        protected B1DbKey[] Keys;
        protected int[] PublishCockpits;
        protected B1DbTable[] Tables;
        protected B1Udo[] Udos;

        public void Add(Company company)
        {
            if (this.Tables != null)
            {
                foreach (B1DbTable table in this.Tables)
                {
                    table.Add(company);
                }
            }
            if (this.Columns != null)
            {
                foreach (B1DbColumn column in this.Columns)
                {
                    column.Add(company);
                }
            }
            if (this.Keys != null)
            {
                foreach (B1DbKey key in this.Keys)
                {
                    key.Add(company);
                }
            }
            if (this.Udos != null)
            {
                foreach (B1Udo udo in this.Udos)
                {
                    udo.Add(company);
                }
            }
        }
    }
}

