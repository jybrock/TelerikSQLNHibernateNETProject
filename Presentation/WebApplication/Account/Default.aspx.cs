using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using BusinessServices;
using DomainObjects;

namespace WebApplication.Account
{
    public partial class Default : BasePage
    {
        #region Constructor

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetUserInfo();
                BindData();

            }
        }

        #endregion

        #region Methods

        private void SetUserInfo()
        {
            User user = UserFacade.GetUserById(UserId);
            LoggedInUserLabel.Text = user.UserName;

            if (UserInRole("Admin"))
            {
            }
            else if (UserInRole("Developer"))
            {
            }
            else if (UserInRole("Client"))
            {
            }
        }

        private void BindData()
        {
             FileUpload1.ControlObjectsVisibility = ControlObjectsVisibility.None;
        }

        #endregion

        #region Events

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "RowClick" && e.Item is GridDataItem)
            {
                int id = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"]);
                Image1.ImageUrl = "~/Account/Handler/DocumentHandler.ashx?ID=" + id;

                Radslidingzone1.ExpandedPaneId = "Radslidingpanel1";
            }
        }

        protected void UploadButton_OnClick(object sender, EventArgs e)
        {
            if (FileUpload1.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile validFile in FileUpload1.UploadedFiles)
                {
                    BatchFile batchFile = new BatchFile();
                    batchFile.ClientId = 0;
                    batchFile.FileName = validFile.FileName;
                    batchFile.FileSize = validFile.ContentLength;
                    batchFile.PageCount = 1;
                    batchFile.Status = 0;

                    byte[] bytes = new byte[validFile.ContentLength];
                    validFile.InputStream.Read(bytes, 0, validFile.ContentLength);

                    batchFile.Image = bytes;

                    int batchFileId = FileFacade.SaveBatchFile(batchFile, UserId);

                    BatchFileDescription batchFileDescription = new BatchFileDescription();
                    batchFileDescription.BatchFile = FileFacade.GetBatchFileById(batchFileId);
                    batchFileDescription.FileExtension = validFile.GetExtension();
                    batchFileDescription.FileSize = validFile.ContentLength;
                    batchFileDescription.PageCount = 1;
                    batchFileDescription.Type = 1;

                    int descriptionId = FileFacade.SaveBatchFileDescription(batchFileDescription, UserId);

                    BatchPage batchPage = new BatchPage();
                    batchPage.BatchDescriptionId = descriptionId;
                    batchPage.PageNum = 1;

                    int pageId = FileFacade.SaveBatchPage(batchPage, UserId);


                    string targetFolder = Server.MapPath("~/Account/FileUpload");
                    validFile.SaveAs(Path.Combine(targetFolder, pageId + validFile.GetExtension()), true);

                }

                RadGrid1.Rebind();
            }
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                //GridDataItem item = e.Item as GridDataItem;

                //try
                //{
                //    if (Convert.ToDateTime(item["Date Estimated"].Text) > Convert.ToDateTime(item["Date Required"].Text))
                //    {
                //        item["Date Estimated"].ForeColor = Color.Red;
                //    }


                //}
                //catch
                //{

                //}
            }

            if (e.Item is GridCommandItem)
            {
                GridCommandItem cmditm = (GridCommandItem)e.Item;
                //to hide AddNewRecord button
                cmditm.FindControl("InitInsertButton").Visible = false;//hide the text
                cmditm.FindControl("AddNewRecordButton").Visible = false;//hide the image

                //to hide Refresh button
                cmditm.FindControl("RefreshButton").Visible = false;//hide the text
                cmditm.FindControl("RebindGridButton").Visible = false;//hide the image
            }

        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID"));
            dt.Columns.Add(new DataColumn("FileName"));
            dt.Columns.Add(new DataColumn("Type"));
            dt.Columns.Add(new DataColumn("Pages"));
            dt.Columns.Add(new DataColumn("FileSize"));
            

            BatchFile[] files = FileFacade.GetAllBatchFile();

            foreach (BatchFile file in files)
            {
                string typeName = string.Empty;

                BatchFileDescription[] fileDescription = FileFacade.GetBatchFileDescriptionByBatchFileId(file.Id);
                if (fileDescription != null && fileDescription.Length > 0)
                {
                    Reference reference = FileFacade.GetReferenceById(fileDescription[0].Type);

                    if (reference != null)
                        typeName = reference.Name;
                }

                dt.Rows.Add(new object[] { file.Id, file.FileName, typeName, file.PageCount, file.FileSize});
            }
            
            RadGrid1.DataSource = dt;

        }

        #endregion
    }
}