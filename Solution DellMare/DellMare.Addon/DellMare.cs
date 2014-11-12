using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;
using System.Threading;

namespace DellMare.Addon
{
    public class DellMare : B1AddOn
    {

        public DellMare()
        {
            // ADD YOUR INITIALIZATION CODE HERE	...
            //Here you setup a productidentifer and if a license-check should occur. For demo purpose so far just ignore them but they would need to be here and the 
            //Identifier needs to be unique
            //LicenseCheck = true;
            //ProductIdentifier = "BOY_TESTPLUGIN";
        }

        public override void OnShutDown()
        {
            // ADD YOUR TERMINATION CODE HERE	...
        }

        public override void OnCompanyChanged()
        {
            // ADD YOUR COMPANY CHANGE CODE HERE	...
        }

        public override void OnLanguageChanged(BoLanguages language)
        {
            InitializeMenus();
            // ADD YOUR LANGUAGE CHANGE CODE HERE	...
        }

        public override void OnStatusBarErrorMessage(string txt)
        {
            // ADD YOUR CODE HERE	...
        }

        public override void OnStatusBarSuccessMessage(string txt)
        {
            // ADD YOUR CODE HERE	...
        }

        public override void OnStatusBarWarningMessage(string txt)
        {
            // ADD YOUR CODE HERE	...
        }

        public override void OnStatusBarNoTypedMessage(string txt)
        {
            // ADD YOUR CODE HERE	...
        }

        public override bool OnBeforeProgressBarCreated()
        {
            // ADD YOUR CODE HERE	...
            return true;
        }

        public override bool OnAfterProgressBarCreated()
        {
            // ADD YOUR CODE HERE	...
            return true;
        }

        public override bool OnBeforeProgressBarStopped(bool success)
        {
            // ADD YOUR CODE HERE	...
            return true;
        }

        public override bool OnAfterProgressBarStopped(bool success)
        {
            // ADD YOUR CODE HERE	...
            return true;
        }

        public override bool OnProgressBarReleased()
        {
            // ADD YOUR CODE HERE	...
            return true;
        }

        public static void Main()
        {
            int retCode = 0;
            string connStr = "";
            B1WizardBase.B1Connections.ConnectionType cnxType = B1WizardBase.B1Connections.ConnectionType.SSO;
            // CHANGE ADDON IDENTIFIER BEFORE RELEASING TO CUSTOMER (Solution Identifier)
            string addOnIdentifierStr = null;
            if ((System.Environment.GetCommandLineArgs().Length == 1))
            {
                connStr = B1Connections.connStr;
            }
            else
            {
                connStr = System.Environment.GetCommandLineArgs().GetValue(1).ToString();
            }
            try
            {
                // INIT CONNECTIONS
                retCode = B1Connections.Init(connStr, addOnIdentifierStr, cnxType);
                // CONNECTION FAILED 
                if ((retCode != 0))
                {
                    System.Windows.Forms.MessageBox.Show("ERROR - Connection failed: " + B1Connections.diCompany.GetLastErrorDescription());
                    return;
                }

                // CREATE DB
                // MANAGE COCKPITS
                if (((cnxType == B1WizardBase.B1Connections.ConnectionType.SSO)
                            || (cnxType == B1WizardBase.B1Connections.ConnectionType.MultipleAddOns)))
                {
                    DellMare_Db addOnDb = new DellMare_Db();
                    addOnDb.Add(B1Connections.diCompany);
                    SPS_Cockpits addOnCockpit = new SPS_Cockpits();
                    addOnCockpit.Manage(B1Connections.theAppl, B1Connections.diCompany);


                }
                // CREATE ADD-ON
                DellMare addOn = new DellMare();
                DB CriaTable = new DB();
                CriaTable.CriaMetaDados();

                
                System.Windows.Forms.Application.Run();
            }
            catch (System.Runtime.InteropServices.COMException com_err)
            {
                // HANDLE ANY COMException HERE
                System.Windows.Forms.MessageBox.Show("ERROR - Connection failed: " + com_err.Message);
            }
            catch (Exception com_err)
            {
                // HANDLE ANY COMException HERE
                System.Windows.Forms.MessageBox.Show("ERROR - Connection failed: " + com_err.Message);
            }
        }

        private static void ThreadMessage()
        {
            while (true)
            {
                //Thread.Sleep(1000 * 60);
                B1Connections.theAppl.RemoveWindowsMessage(SAPbouiCOM.BoWindowsMessageType.bo_WM_TIMER, false);
            }
        }

    }
}
