using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MasterPage : System.Web.UI.MasterPage
{
    SqlConnection conSQ = new SqlConnection(ConfigurationManager.ConnectionStrings["conSQ"].ToString());

    public string strUserName = "", strUserName2 = "", strMenuLink = "", strDashLink = "", strRole = "", strProfileImage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckPassKeyChanged();
        //check if admin login is valid
        if (Request.Cookies["nmc_aid"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        CheckPassKeyChanged();
        //BindMenuLink();
    }




    public void CheckPassKeyChanged()
    {
        try
        {
            if (Request.Cookies["nmc_apkv"] != null)
            {
                string stored_pass_key = Request.Cookies["nmc_apkv"].Value;
                var udetails = CreateUser.GetUserDetails(conSQ, Request.Cookies["nmc_aid"].Value);
                if (udetails != null)
                {
                    strUserName = udetails.UserName;
                    strUserName2 = udetails.UserName.Split(' ')[0];
                    strRole = udetails.UserRole;
                    strProfileImage = udetails.ProfileImage == "" ? "/assets/images/users/user-dummy-img.jpg" : "/" + udetails.ProfileImage;
                    string current_pass_key = udetails.PassKey;

                    if (current_pass_key != stored_pass_key)
                    {
                        Session.Abandon();
                        Session.Clear();
                        if (Request.Cookies["nmc_aid"] != null)
                        {
                            Response.Cookies["nmc_aid"].Expires = TimeStamps.UTCTime().AddDays(-10);
                        }
                        if (Request.Cookies["nmc_apkv"] != null)
                        {
                            Response.Cookies["nmc_apkv"].Expires = TimeStamps.UTCTime().AddDays(-10);
                        }
                        Response.Redirect("Default.aspx");
                    }
                }
                else
                {
                    Session.Abandon();
                    Session.Clear();
                    if (Request.Cookies["nmc_aid"] != null)
                    {
                        Response.Cookies["nmc_aid"].Expires = TimeStamps.UTCTime().AddDays(-10);
                    }
                    if (Request.Cookies["nmc_apkv"] != null)
                    {
                        Response.Cookies["nmc_apkv"].Expires = TimeStamps.UTCTime().AddDays(-10);
                    }
                    Response.Redirect("Default.aspx");
                }
            }
            else
            {
                Session.Abandon();
                Session.Clear();
                if (Request.Cookies["nmc_aid"] != null)
                {
                    Response.Cookies["nmc_aid"].Expires = TimeStamps.UTCTime().AddDays(-10);
                }
                if (Request.Cookies["nmc_apkv"] != null)
                {
                    Response.Cookies["nmc_apkv"].Expires = TimeStamps.UTCTime().AddDays(-10);
                }
                Response.Redirect("Default.aspx");
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "CheckPassKeyChanged", ex.Message);
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                Session.Abandon();
                Session.Clear();
                if (Request.Cookies["nmc_aid"] != null)
                {
                    Response.Cookies["nmc_aid"].Expires = TimeStamps.UTCTime().AddDays(-10);
                }
                if (Request.Cookies["nmc_apkv"] != null)
                {
                    Response.Cookies["nmc_apkv"].Expires = TimeStamps.UTCTime().AddDays(-10);
                }
                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnLogin_Click", ex.Message);
            }

        }

    }

    protected void btnLock_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["nmc_aid"] != null)
            {
                string uid = Request.Cookies["nmc_aid"].Value;
                Response.Cookies["nmc_aid"].Expires = TimeStamps.UTCTime().AddDays(-10);
                HttpCookie cookie = new HttpCookie("ary_lid");
                cookie.Value = uid;
                cookie.Expires = TimeStamps.UTCTime().AddDays(15);
                Response.Cookies.Add(cookie);
                Response.Redirect("lock-screen.aspx");
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "btnLogin_Click", ex.Message);
        }
    }

    public void BindMenuLink()
    {
        try
        {
            int mNo = 0;
            string pageName = Path.GetFileName(Request.Path);
            if (CreateUser.CheckIfAdmin(conSQ, Request.Cookies["nmc_aid"].Value) == true)
            {
                strDashLink = "dashboard.aspx";
                List<UserAccess> ua = CreateUser.LinkIfAdmin(conSQ).OrderBy(x => x.GroupOrder).ToList();
                foreach (var access in ua.Select(x => x.PageGroupId).Distinct())
                {
                    mNo++;
                    var group = ua.Where(x => x.PageGroupId == access).ToList();
                    if (group.Count == 1 && group[0].ShowInMenu == "1")
                    {
                        string act = pageName == group[0].PageLink ? "active" : "";
                        //string expand = pageName == group[0].PageLink ? "true" : "false";
                        strMenuLink += @"<li class='nav-item'>
                                <a class='nav-link menu-link " + act + @"' href='" + group[0].PageLink + @"'>
                                    " + group[0].GroupIcon + @"<span data-key='" + group[0].GroupName.ToLower().Replace(" ", "-") + @"'>" + group[0].GroupName + @"</span>
                                </a>
                            </li>";
                    }
                    else if (group.Count > 1)
                    {
                        string subMenu = "";
                        int active = 0;

                        foreach (var ug in group)
                        {
                            string act = pageName == ug.PageLink ? "class='active'" : "";
                            if (pageName == ug.PageLink)
                            {
                                active++;
                            }
                            if (ug.ShowInMenu == "1")
                                subMenu += @"<li class='nav-item'>
                                            <a href='" + ug.PageLink + @"' class='nav-link " + act + "' data-key='" + ug.PageName.ToLower().Replace(" ", "-") + @"'>" + ug.PageName + @"</a>
                                        </li>";
                        }
                        string show = "", expand = "false";
                        if (active > 0)
                        {
                            show = "show";
                            expand = "true";
                        }
                        strMenuLink += @"<li class='nav-item'>
                                <a class='nav-link menu-link' href='#menu" + mNo + @"' data-bs-toggle='collapse' role='button' aria-expanded='" + expand + @"' aria-controls='menu" + mNo + @"'>
                                    " + group[0].GroupIcon + @"<span data-key='" + group[0].GroupName.ToLower().Replace(" ", "-") + @"'>" + group[0].GroupName + @"</span>
                                </a>
                                <div class='collapse menu-dropdown " + show + @"' id='menu" + mNo + @"'>
                                    <ul class='nav nav-sm flex-column'>" + subMenu + @"</ul>
                                </div>
                            </li>";
                    }
                }
            }
            else
            {
                List<UserAccess> ua = CreateUser.GetAllUserAccess(conSQ, Request.Cookies["nmc_aid"].Value).OrderBy(x => x.GroupOrder).ToList();
                var ud = ua.Where(x => x.PageName.ToLower() == "dashboard" && x.ViewAccess == "1").FirstOrDefault();
                if (ud != null)
                {
                    strDashLink = ud.PageLink;
                }
                else
                {
                    strDashLink = "#";
                }

                foreach (var access in ua.Select(x => x.PageGroupId).Distinct())
                {
                    var group = ua.Where(x => x.PageGroupId == access && x.ViewAccess == "1").ToList();
                    if (group.Count > 0 && group.Count < 2 && group[0].ShowInMenu == "1" && group[0].ViewAccess == "1")
                    {
                        string act = pageName == group[0].PageLink ? "active" : "";
                        string expand = pageName == group[0].PageLink ? "true" : "false";
                        strMenuLink += @"<li class='nav-item'>
                                <a class='nav-link menu-link " + act + @"' href='" + group[0].PageLink + @"'>
                                    " + group[0].GroupIcon + @"<span data-key='" + group[0].GroupName.ToLower().Replace(" ", "-") + @"'>" + group[0].GroupName + @"</span>
                                </a>
                            </li>";
                    }
                    else if (group.Count > 1)
                    {
                        int active = 0;
                        string subMenu = "";
                        int menuShow = 0;
                        foreach (var ug in group)
                        {
                            string act = pageName == ug.PageLink ? "class='active'" : "";
                            if (pageName == ug.PageLink)
                            {
                                active++;
                            }
                            if (ug.ShowInMenu == "1" && ug.ViewAccess == "1")
                            {
                                subMenu += @"<li class='nav-item'>
                                            <a href='" + ug.PageLink + @"' class='nav-link " + act + "' data-key='" + ug.PageName.ToLower().Replace(" ", "-") + @"'>" + ug.PageName + @"</a>
                                        </li>";
                                menuShow = 1;
                            }
                        }
                        if (menuShow == 1)
                        {
                            string show = "", expand = "false";
                            if (active > 0)
                            {
                                show = "show";
                                expand = "true";
                            }

                            strMenuLink += @"<li class='nav-item'>
                                <a class='nav-link menu-link' href='#menu" + mNo + @"' data-bs-toggle='collapse' role='button' aria-expanded='" + expand + @"' aria-controls='menu" + mNo + @"'>
                                    " + group[0].GroupIcon + @"<span data-key='" + group[0].GroupName.ToLower().Replace(" ", "-") + @"'>" + group[0].GroupName + @"</span>
                                </a>
                                <div class='collapse menu-dropdown " + show + @"' id='menu" + mNo + @"'>
                                    <ul class='nav nav-sm flex-column'>" + subMenu + @"</ul>
                                </div>
                            </li>";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "BindMenuLink", ex.Message);
        }
    }
}
