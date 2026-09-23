using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_add_blog : System.Web.UI.Page
{
    SqlConnection conSQ = new SqlConnection(ConfigurationManager.ConnectionStrings["conSQ"].ConnectionString);
    public string strThumbImage, strDetailImg = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                GetBlogs();
            }
        }
    }

    private void GetBlogs()
    {
        try
        {
            var blogs = Blogs.GetAllBlogDetailsWithId(conSQ, Convert.ToInt32(Request.QueryString["id"]));
            if (blogs != null)
            {
                btnSave.Text = "Update";
                txtBlogTitle.Text = blogs.Title;
                txtUrl.Text = blogs.Url;
                txtMetaDesc.Text = blogs.MetaDesc;
                txtMetakey.Text = blogs.MetaKey;
                txtPageTitle.Text = blogs.PageTitle;
                txtShortDesc.Text = blogs.ShortDesc;
                Txtfuldesc.Text = blogs.FullDesc;
                txtPostedBy.Text = blogs.PostedBy;
                txtTag.Text = blogs.Tags;
                txtPostedOn.Text = blogs.PostedOn.ToString("dd/MMM/yyyy");
                if (blogs.ImageUrl != "")
                {
                    strThumbImage = "<img src='/" + blogs.ImageUrl + "' style='max-height:60px;' />";
                    lblThumb.Text = blogs.ImageUrl;
                }
                if (blogs.DetailImage != "")
                {
                    strDetailImg = "<img src='/" + blogs.DetailImage + "' style='max-height:60px;' />";
                    lblDetailImg.Text = blogs.DetailImage;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetBlogs", ex.Message);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            var upload = CheckImageFormat();
            if (upload == "Format")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Invalid image format. Please upload .png, .jpeg, .jpg, .webp, .gif',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                return;
            }
            if (upload == "Size")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Image size should be 344px w X 250px h',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                return;
            }

            var upload2 = UploadDetailImg();
            if (upload2 == "Not Found")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Please upload Detail Image.',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                return;
            }

            Blogs blogs = new Blogs();
            blogs.Title = txtBlogTitle.Text.Trim();
            blogs.Url = txtUrl.Text.Trim();
            blogs.ShortDesc = txtShortDesc.Text.Trim();
            blogs.PostedOn = Convert.ToDateTime(txtPostedOn.Text.Trim());
            blogs.PostedBy = txtPostedBy.Text.Trim();
            blogs.FullDesc = Txtfuldesc.Text.Trim();
            blogs.ImageUrl = UploadImage();
            blogs.DetailImage = UploadDetailImg();
            blogs.PageTitle = txtPageTitle.Text.Trim();
            blogs.MetaKey = txtMetakey.Text.Trim();
            blogs.MetaDesc = txtMetaDesc.Text.Trim();
            blogs.Tags = txtTag.Text.Trim();

            string aid = Request.Cookies["nmc_aid"].Value;
            if (btnSave.Text == "Update")
            {
                blogs.Id = Convert.ToInt32(Request.QueryString["id"]);
                blogs.UpdatedBy = aid;
                int result = Blogs.UpdateBlog(conSQ, blogs);
                if (result > 0)
                {
                    GetBlogs();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Blog details Updated successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Something went wrong. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                }
            }
            else
            {
                blogs.AddedBy = aid;
                blogs.DisplayHome = "No";
                blogs.Status = "Active";
                blogs.AddedIp = CommonModel.IPAddress();
                blogs.AddedOn = TimeStamps.UTCTime();
                int result = Blogs.WriteBlog(conSQ, blogs);
                if (result > 0)
                {
                    txtBlogTitle.Text = txtUrl.Text = txtPostedOn.Text = txtPostedBy.Text = txtShortDesc.Text = Txtfuldesc.Text = txtPageTitle.Text = txtMetakey.Text = txtMetaDesc.Text = string.Empty;
                    strThumbImage = strDetailImg = string.Empty;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Blog details added successfully.',actionTextColor: '#fff',backgroundColor: '#008a3d'});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Snackbar.show({pos: 'top-right',text: 'Oops! Something went wrong. Please try after some time',actionTextColor: '#fff',backgroundColor: '#ea1c1c'});", true);
                }
            }
        }
    }

    public string UploadImage()
    {
        #region upload image
        string thumbImage = "";
        if (FileUpload1.HasFile)
        {
            string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName.ToLower()), ImageGuid1 = Guid.NewGuid().ToString() + "-blogThumb".Replace(" ", "-").Replace(".", "");
            string iconPath = Server.MapPath(".") + "\\../UploadImages\\" + ImageGuid1 + "" + fileExtension;
            try
            {
                if (File.Exists(Server.MapPath("~/" + Convert.ToString(lblThumb.Text))))
                {
                    File.Delete(Server.MapPath("~/" + Convert.ToString(lblThumb.Text)));
                }
            }
            catch (Exception eeex)
            {
                ExceptionCapture.CaptureException(Request.Url.PathAndQuery, "UploadImage", eeex.Message);
                return lblThumb.Text;
            }

            if (fileExtension == ".webp")
            {
                FileUpload1.SaveAs(iconPath);

            }
            else if (fileExtension == ".gif")
            {
                FileUpload1.SaveAs(iconPath);
            }
            else
            {
                System.Drawing.Bitmap bmpPostedImageBig = new System.Drawing.Bitmap(FileUpload1.PostedFile.InputStream);
                System.Drawing.Image objImagesmallBig = CommonModel.ScaleImageBig(bmpPostedImageBig, bmpPostedImageBig.Height, bmpPostedImageBig.Width);
                if (fileExtension == ".png")
                {
                    CommonModel.SavePNG(iconPath, objImagesmallBig, 99);
                }
                else { CommonModel.SaveJpeg(iconPath, objImagesmallBig, 99); }
            }
            thumbImage = "UploadImages/" + ImageGuid1 + "" + fileExtension;
        }
        else
        {
            thumbImage = lblThumb.Text;
        }
        #endregion
        return thumbImage;
    }

    private string UploadDetailImg()
    {
        #region upload Detail image
        string DetailImage = "";
        string guid = Guid.NewGuid().ToString();
        if (FileUpload2.HasFile)
        {
            string fileExtension = Path.GetExtension(FileUpload2.PostedFile.FileName.ToLower()), ImageGuid1 = guid + "_Detail".Replace(" ", "-").Replace(".", "");
            string iconPath = Server.MapPath(".") + "\\../UploadImages\\" + ImageGuid1 + "" + fileExtension;
            if ((fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".doc" || fileExtension == ".docx" || fileExtension == ".pdf" || fileExtension == ".webp"))
            {
                try
                {
                    if (File.Exists(Server.MapPath("~/" + Convert.ToString(lblDetailImg.Text))))
                    {
                        File.Delete(Server.MapPath("~/" + Convert.ToString(lblDetailImg.Text)));
                    }
                }
                catch (Exception ex)
                {

                }
                if (fileExtension == ".png")
                {
                    System.Drawing.Bitmap bmpPostedImageBig = new System.Drawing.Bitmap(FileUpload2.PostedFile.InputStream);
                    System.Drawing.Image objImagesmallBig = CommonModel.ScaleImageBig(bmpPostedImageBig, bmpPostedImageBig.Height, bmpPostedImageBig.Width);
                    CommonModel.SavePNG(iconPath, objImagesmallBig, 99);
                }
                else if (fileExtension == ".jpeg")
                {
                    System.Drawing.Bitmap bmpPostedImageBig = new System.Drawing.Bitmap(FileUpload2.PostedFile.InputStream);
                    System.Drawing.Image objImagesmallBig = CommonModel.ScaleImageBig(bmpPostedImageBig, bmpPostedImageBig.Height, bmpPostedImageBig.Width);
                    CommonModel.SaveJpeg(iconPath, objImagesmallBig, 99);
                }
                else
                {
                    FileUpload2.SaveAs(iconPath);
                }
                DetailImage = "UploadImages/" + ImageGuid1 + "" + fileExtension;

            }
            else
            {
                return "Format";
            }
        }
        else
        {
            if (lblDetailImg.Text != "")
            {
                DetailImage = lblDetailImg.Text;

            }
            else
            {
                DetailImage = "Not Found";
            }
        }
        #endregion
        return DetailImage;
    }

    private string CheckImageFormat()
    {
        #region upload image
        string thumbImage = "";
        if (FileUpload1.HasFile)
        {
            string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName.ToLower());

            if ((fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".webp"))
            {
                if (fileExtension == ".webp")
                {
                    return thumbImage;
                }
                else
                {
                    System.Drawing.Bitmap bmpPostedImageBig = new System.Drawing.Bitmap(FileUpload1.PostedFile.InputStream);
                    System.Drawing.Image objImagesmallBig = CommonModel.ScaleImageBig(bmpPostedImageBig, bmpPostedImageBig.Height, bmpPostedImageBig.Width);
                    if (bmpPostedImageBig.Height != 400 && bmpPostedImageBig.Width != 600)
                    {
                        return "Size";
                    }
                }

            }
            else
            {
                return "Format";
            }
        }
        #endregion
        return thumbImage;
    }
}