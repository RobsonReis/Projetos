namespace B1WizardBase
{
    using SAPbobsCOM;
    //using SAPbouiCOM;
    using System;
    using System.CodeDom;
    using System.Collections;

    public class B1Widget
    {
        public string category;
        public string cockpitName;
        public int height;
        public string imagePath;
        public Hashtable keyValues;
        public int lCol;
        public int lRow;
        public string widgetType;
        public string widgetUID;
        public int width;

        public B1Widget(string cockpitName, string widgetType, int row, int col) : this(cockpitName, widgetType, string.Concat(new object[] { "NEW ", widgetType, "(", row, ", ", col, ")" }), "", -1, -1, "", row, col)
        {
        }

        public B1Widget(string cockpitName, string widgetType, string widgetUID, int row, int col) : this(cockpitName, "", widgetUID, "", -1, -1, "", row, col)
        {
        }

        private B1Widget(string cockpitName, string widgetType, string widgetUID, string category, int width, int height, string imagePath, int row, int col)
        {
            this.keyValues = new Hashtable();
            this.cockpitName = cockpitName;
            this.widgetUID = widgetUID;
            this.widgetType = widgetType;
            this.category = category;
            this.width = width;
            this.height = height;
            this.imagePath = imagePath;
            this.lRow = row;
            this.lCol = col;
        }

        public void CreateWidgetInstance(SAPbouiCOM.Application uiApp, SAPbobsCOM.Company company)
        {
            B1Cockpit cockpit = B1Cockpit.GetCockpit(company, this.cockpitName);
            if (cockpit == null)
            {
                throw new Exception("B1Widget.CreateWidgetInstance: Cockpit with name " + this.cockpitName + " not found");
            }
            this.CreateWidgetInstance(uiApp, company, cockpit);
        }

        public void CreateWidgetInstance(SAPbouiCOM.Application uiApp, SAPbobsCOM.Company company, B1Cockpit cockpit)
        {
            SAPbouiCOM.Cockpit cockpit2 = uiApp.Cockpits.Item(cockpit.TypeID);
            uiApp.Cockpits.SwitchCockpit(cockpit.TypeID);
            cockpit2.CreateWidgetInstance(this.widgetType, this.lRow, this.lCol);
        }

        public override bool Equals(object obj)
        {
            if (obj is B1Widget)
            {
                B1Widget widget = obj as B1Widget;
                return ((widget.cockpitName + widget.widgetUID) == (this.cockpitName + this.widgetUID));
            }
            return base.Equals(obj);
        }

        public CodeExpression GenerateCtor()
        {
            return new CodeObjectCreateExpression("B1Widget", new CodeExpression[] { new CodePrimitiveExpression(this.cockpitName), new CodePrimitiveExpression(this.widgetType), new CodePrimitiveExpression(this.lRow), new CodePrimitiveExpression(this.lCol) });
        }

        public void SetKeyValue(SAPbouiCOM.Application uiApp, string key, string value)
        {
            this.keyValues.Add(key, value);
        }
    }
}

