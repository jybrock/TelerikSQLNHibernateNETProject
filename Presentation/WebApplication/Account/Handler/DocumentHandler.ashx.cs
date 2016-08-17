using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using DomainObjects;
using BusinessServices;
using System.IO;

namespace WebApplication.Account.Handler
{
    /// <summary>
    /// Summary description for DocumentHandler
    /// </summary>
    public class DocumentHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int batchFileId = Convert.ToInt32(context.Request.QueryString["ID"]);

            BatchFile batchFile = FileFacade.GetBatchFileById(batchFileId);

            MemoryStream stream = new MemoryStream(batchFile.Image);
            Image img = Image.FromStream(stream); 
            img.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

            context.Response.ContentType = "image/Jpeg";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}