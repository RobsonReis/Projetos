namespace B1WizardBase
{
    using SAPbouiCOM;
    using System;
    using System.CodeDom;
    using System.Runtime.InteropServices;

    public class B1WidgetType
    {
        public string categoryUID;
        public int height;
        public string imagePath;
        public string widgetName;
        public string widgetType;
        public int width;

        public B1WidgetType(string widgetName, string widgetType, string categoryUID, int width, int height, string imagePath)
        {
            this.widgetName = widgetName;
            this.widgetType = widgetType;
            this.categoryUID = categoryUID;
            this.width = width;
            this.height = height;
            this.imagePath = imagePath;
        }

        public override bool Equals(object obj)
        {
            if (obj is B1WidgetType)
            {
                B1WidgetType type = obj as B1WidgetType;
                return (type.widgetType == this.widgetType);
            }
            return base.Equals(obj);
        }

        public CodeExpression GenerateCtor()
        {
            return new CodeObjectCreateExpression("B1WidgetType", new CodeExpression[] { new CodePrimitiveExpression(this.widgetName), new CodePrimitiveExpression(this.widgetType), new CodePrimitiveExpression(this.categoryUID), new CodePrimitiveExpression(this.width), new CodePrimitiveExpression(this.height), new CodePrimitiveExpression(this.imagePath) });
        }

        public void RegisterWidget(Application uiApp)
        {
            WidgetRegParams wrParams = (WidgetRegParams) uiApp.CreateObject(BoCreatableObjectType.cot_WidgetRegParams);
            try
            {
                wrParams.WidgetName = this.widgetName;
                wrParams.WidgetType = this.widgetType;
                wrParams.CategoryUID = this.categoryUID;
                wrParams.Width = this.width;
                wrParams.Height = this.height;
                wrParams.ImagePath = this.imagePath;
                uiApp.Cockpits.RegisterWidget(wrParams);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (wrParams != null)
                {
                    Marshal.ReleaseComObject(wrParams);
                    wrParams = null;
                }
            }
        }
    }
}

