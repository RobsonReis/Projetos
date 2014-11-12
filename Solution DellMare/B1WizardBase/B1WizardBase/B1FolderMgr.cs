namespace B1WizardBase
{
    using SAPbouiCOM;
    using System;
    using System.Threading;

    public sealed class B1FolderMgr
    {
        private static ItemEvent pVal;

        private B1FolderMgr()
        {
        }

        public static void AddLine(ItemEvent pVal)
        {
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            if (form.Mode != BoFormMode.fm_FIND_MODE)
            {
                Item item;
                form.Freeze(true);
                int paneLevel = form.PaneLevel;
                try
                {
                    item = form.Items.Item("mtx_" + form.PaneLevel);
                }
                catch (Exception)
                {
                    item = form.Items.Item("mtx_" + (form.PaneLevel - 1));
                    paneLevel = form.PaneLevel - 1;
                }
                Matrix specific = (Matrix) item.Specific;
                form.DataSources.DBDataSources.Item(paneLevel + 1).Clear();
                int rowCount = specific.RowCount;
                specific.AddRow(1, rowCount);
                specific.SelectRow(1 + rowCount, true, false);
                if (form.Mode != BoFormMode.fm_ADD_MODE)
                {
                    form.Mode = BoFormMode.fm_UPDATE_MODE;
                }
                specific.FlushToDataSource();
                form.Freeze(false);
            }
        }

        public static void DelLine(ItemEvent pVal)
        {
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            if (form.Mode != BoFormMode.fm_FIND_MODE)
            {
                Item item;
                form.Freeze(true);
                int paneLevel = form.PaneLevel;
                try
                {
                    item = form.Items.Item("mtx_" + form.PaneLevel);
                }
                catch (Exception)
                {
                    item = form.Items.Item("mtx_" + (form.PaneLevel - 1));
                    int num2 = form.PaneLevel;
                }
                Matrix specific = (Matrix) item.Specific;
                int nextSelectedRow = specific.GetNextSelectedRow(0, BoOrderType.ot_SelectionOrder);
                if (nextSelectedRow != -1)
                {
                    specific.DeleteRow(nextSelectedRow);
                    if (form.Mode != BoFormMode.fm_ADD_MODE)
                    {
                        form.Mode = BoFormMode.fm_UPDATE_MODE;
                    }
                    form.Freeze(false);
                }
            }
        }

        public static void Init(ItemEvent pVal)
        {
            B1FolderMgr.pVal = pVal;
            new Thread(new ThreadStart(B1FolderMgr.select)).Start();
        }

        private static void select()
        {
            try
            {
                B1Connections.theAppl.GetLastBatchResults();
                ((Folder) B1Connections.theAppl.Forms.Item(pVal.FormUID).Items.Item("tab_0").Specific).Select();
            }
            catch
            {
            }
        }

        public static void SetPane(ItemEvent pVal, int pane)
        {
            B1Connections.theAppl.Forms.Item(pVal.FormUID).PaneLevel = pane;
        }
    }
}

