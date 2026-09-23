using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default : System.Web.UI.Page
{
    SqlConnection conSQ = new SqlConnection(ConfigurationManager.ConnectionStrings["conSQ"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["nmc_aid"] != null)
        {
            Response.Redirect("dashboard.aspx");
        }

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        lblStatus.Visible = true;
        try
        {
            if (Page.IsValid)
            {
                CreateUser inputs = new CreateUser();
                inputs.UserId = txtUserName.Text.Trim();
                inputs.Password = CommonModel.Encrypt(txtPassword.Text.Trim());
                CreateUser logins = CreateUser.Login(conSQ, inputs);
                if (logins.UserGuid != null)
                {
                    if (logins.Status == "Blocked")
                    {
                        lblStatus.Text = "<strong>Error !</strong><br/>User-Id is temporarily blocked by admin. Please contact to Admin ";
                        lblStatus.Attributes.Add("class", "alert alert-danger d-block");
                    }
                    else if (logins.Status == "Active")
                    {
                        lblStatus.Text = "<strong>Success !</strong> Login success";
                        lblStatus.Attributes.Add("class", "alert alert-success d-block");

                        HttpCookie cookie = new HttpCookie("nmc_aid");
                        cookie.Value = logins.UserGuid;

                        //this cookie is to validate password key
                        HttpCookie cookie_pass_key = new HttpCookie("nmc_apkv");
                        cookie_pass_key.Value = logins.PassKey;
                        if (chkLogKeep.Checked == true)
                        {
                            cookie.Expires = TimeStamps.UTCTime().AddDays(15);
                            cookie_pass_key.Expires = TimeStamps.UTCTime().AddDays(15);
                        }
                        else
                        {
                            cookie.Expires = TimeStamps.UTCTime().AddDays(1);
                            cookie_pass_key.Expires = TimeStamps.UTCTime().AddDays(1);
                        }
                        CreateUser.UpdateLastLoginTime(conSQ, logins.UserGuid);
                        Response.Cookies.Add(cookie);
                        Response.Cookies.Add(cookie_pass_key);

                        switch (logins.UserRole.ToLower())
                        {
                            case "admin":
                                Response.Redirect("dashboard.aspx");
                                break;
                            //case "user":
                            //    Response.Redirect("user-dashboard.aspx");
                            //    break;
                            default:
                                Response.Redirect("dashboard.aspx");
                                break;
                        }
                    }
                    else
                    {
                        lblStatus.Text = "<strong>Error !</strong><br/>User-Id or Password incorrect ";
                        lblStatus.Attributes.Add("class", "alert alert-danger d-block");
                    }
                }
                else
                {
                    lblStatus.Text = "<strong>Error !</strong><br/>User-Id or Password incorrect";
                    lblStatus.Attributes.Add("class", "alert alert-danger d-block");
                }
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = "<strong>Error !</strong><br/>There is some problem now. Please try after some time";
            lblStatus.Attributes.Add("class", "alert alert-danger d-block");
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnLogin_Click", ex.Message);
        }
    }
}