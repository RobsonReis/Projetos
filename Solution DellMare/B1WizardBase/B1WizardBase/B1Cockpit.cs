namespace B1WizardBase
{
    using SAPbobsCOM;
    using SAPbouiCOM;
    using System;
    using System.CodeDom;
    using System.Runtime.InteropServices;

    public class B1Cockpit
    {
        public string Description;
        public bool isTemplate;
        public string Manufacturer;
        public string Name;
        public string Publisher;
        public string TypeID;
        public string UID;

        public B1Cockpit(SAPbobsCOM.Cockpit ckpt) : this(ckpt.Name, ckpt.Description, ckpt.Manufacturer, ckpt.AbsEntry.ToString(), ckpt.Code.ToString(), ckpt.Publisher, ckpt.UserSignature == -1)
        {
        }

        public B1Cockpit(string name, string description, string manufacturer) : this(name, description, manufacturer, "", "", "", true)
        {
        }

        public B1Cockpit(string name, string description, string manufacturer, string uid, string typeID, string publisher, bool isTemplate)
        {
            this.Name = name;
            this.Description = description;
            this.Manufacturer = manufacturer;
            this.UID = uid;
            this.TypeID = typeID;
            this.Publisher = publisher;
            this.isTemplate = isTemplate;
        }

        public int Add(Application uiApp, SAPbobsCOM.Company company)
        {
            CockpitsService o = null;
            SAPbobsCOM.Cockpit dataInterface = null;
            CockpitParams params1 = null;
            CockpitsParams params2 = null;
            int num;
            try
            {
                if (GetCockpit(company, this.Name) == null)
                {
                    o = (CockpitsService) company.GetCompanyService().GetBusinessService((ServiceTypes) 0x481f2280);
                    dataInterface = (SAPbobsCOM.Cockpit) o.GetDataInterface(CockpitsServiceDataInterfaces.csCockpit);
                    dataInterface.Name = this.Name;
                    dataInterface.Description = this.Description;
                    dataInterface.Manufacturer = this.Manufacturer;
                    params1 = o.AddCockpit(dataInterface);
                    this.UID = params1.AbsEntry.ToString();
                    dataInterface = o.GetCockpit(params1);
                    this.TypeID = dataInterface.Code.ToString();
                    uiApp.Cockpits.Refresh();
                    return 0;
                }
                num = 1;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (dataInterface != null)
                {
                    Marshal.ReleaseComObject(dataInterface);
                    dataInterface = null;
                }
                if (params2 != null)
                {
                    Marshal.ReleaseComObject(params2);
                    params2 = null;
                }
                if (o != null)
                {
                    Marshal.ReleaseComObject(o);
                    o = null;
                }
            }
            return num;
        }

        public int Delete(SAPbobsCOM.Company company)
        {
            CockpitsService o = null;
            CockpitParams dataInterface = null;
            int num;
            try
            {
                o = (CockpitsService) company.GetCompanyService().GetBusinessService((ServiceTypes) 0x481f2280);
                dataInterface = (CockpitParams) o.GetDataInterface(CockpitsServiceDataInterfaces.csCockpitParams);
                dataInterface.AbsEntry = int.Parse(this.UID);
                o.DeleteCockpit(dataInterface);
                num = 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (dataInterface != null)
                {
                    Marshal.ReleaseComObject(dataInterface);
                    dataInterface = null;
                }
                if (o != null)
                {
                    Marshal.ReleaseComObject(o);
                    o = null;
                }
            }
            return num;
        }

        public override bool Equals(object obj)
        {
            if (obj is B1Cockpit)
            {
                B1Cockpit cockpit = obj as B1Cockpit;
                return (cockpit.Name == this.Name);
            }
            return base.Equals(obj);
        }

        public CodeExpression GenerateCtor()
        {
            return new CodeObjectCreateExpression("B1Cockpit", new CodeExpression[] { new CodePrimitiveExpression(this.Name), new CodePrimitiveExpression(this.Description), new CodePrimitiveExpression(this.Manufacturer) });
        }

        public static B1Cockpit GetCockpit(SAPbobsCOM.Company company, string ckptName)
        {
            CockpitsService o = null;
            CockpitParams params1 = null;
            CockpitsParams userCockpitList = null;
            SAPbobsCOM.Cockpit ckpt = null;
            B1Cockpit cockpit2 = null;
            B1Cockpit cockpit3;
            try
            {
                o = (CockpitsService) company.GetCompanyService().GetBusinessService((ServiceTypes) 0x481f2280);
                userCockpitList = o.GetUserCockpitList();
                if (userCockpitList.Count > 0)
                {
                    foreach (CockpitParams params3 in userCockpitList)
                    {
                        ckpt = o.GetCockpit(params3);
                        if (ckpt.Name == ckptName)
                        {
                            cockpit2 = new B1Cockpit(ckpt);
                            break;
                        }
                    }
                }
                cockpit3 = cockpit2;
            }
            catch (Exception)
            {
                cockpit3 = null;
            }
            finally
            {
                if (params1 != null)
                {
                    Marshal.ReleaseComObject(params1);
                    params1 = null;
                }
                if (ckpt != null)
                {
                    Marshal.ReleaseComObject(ckpt);
                    ckpt = null;
                }
                if (o != null)
                {
                    Marshal.ReleaseComObject(o);
                    o = null;
                }
            }
            return cockpit3;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void MoveWidget(Application uiApp, B1Widget widget)
        {
            uiApp.Cockpits.SwitchCockpit(this.TypeID);
            uiApp.Cockpits.CurrentCockpit.MoveWidget(widget.widgetUID, widget.lRow, widget.lCol);
        }

        public int Publish(SAPbobsCOM.Company company)
        {
            CockpitsService o = null;
            CockpitParams dataInterface = null;
            SAPbobsCOM.Cockpit cockpit = null;
            int num;
            try
            {
                o = (CockpitsService) company.GetCompanyService().GetBusinessService((ServiceTypes) 0x481f2280);
                dataInterface = (CockpitParams)o.GetDataInterface(CockpitsServiceDataInterfaces.csCockpitParams);
                dataInterface.AbsEntry = int.Parse(this.UID);
                cockpit = o.GetCockpit(dataInterface);
                if ((cockpit.Publisher == null) || (cockpit.Publisher.Length == 0))
                {
                    o.PublishCockpit(cockpit);
                }
                num = 0;
            }
            catch (Exception)
            {
                num = -1;
            }
            finally
            {
                if (dataInterface != null)
                {
                    Marshal.ReleaseComObject(dataInterface);
                    dataInterface = null;
                }
                if (cockpit != null)
                {
                    Marshal.ReleaseComObject(cockpit);
                    cockpit = null;
                }
                if (o != null)
                {
                    Marshal.ReleaseComObject(o);
                    o = null;
                }
            }
            return num;
        }

        public int Update(SAPbobsCOM.Company company)
        {
            CockpitsService o = null;
            CockpitParams dataInterface = null;
            SAPbobsCOM.Cockpit cockpit = null;
            int num;
            try
            {
                o = (CockpitsService) company.GetCompanyService().GetBusinessService((ServiceTypes) 0x481f2280);
                dataInterface = (CockpitParams)o.GetDataInterface(CockpitsServiceDataInterfaces.csCockpitParams);
                dataInterface.AbsEntry = int.Parse(this.UID);
                cockpit = o.GetCockpit(dataInterface);
                cockpit.Name = this.Name;
                cockpit.Description = this.Description;
                cockpit.Manufacturer = this.Manufacturer;
                o.UpdateCockpit(cockpit);
                num = 0;
            }
            catch (Exception)
            {
                num = -1;
            }
            finally
            {
                if (dataInterface != null)
                {
                    Marshal.ReleaseComObject(dataInterface);
                    dataInterface = null;
                }
                if (cockpit != null)
                {
                    Marshal.ReleaseComObject(cockpit);
                    cockpit = null;
                }
                if (o != null)
                {
                    Marshal.ReleaseComObject(o);
                    o = null;
                }
            }
            return num;
        }
    }
}

