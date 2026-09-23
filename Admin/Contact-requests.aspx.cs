
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;

public partial class Admin_Contact_requests : System.Web.UI.Page
{
    SqlConnection conSQ = new SqlConnection(ConfigurationManager.ConnectionStrings["conSQ"].ConnectionString);
    public string StrContact = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        GetAllContact();
    }
    private void GetAllContact()
    {
        try
        {
            var contact = ContactUs.GetAllContact(conSQ);
            if (contact != null && contact.Count > 0)
            {
                for (int i = 0; i < contact.Count; i++)
                {
                    var Message = @"<td>
                                    <button data-bs-toggle='modal' data-bs-target='#fadeInRightModal' type='button' id='Viewcust' data-vid='" + contact[i].Id + @"' data-vname='" + contact[i].Name + @"' class='btn btn-success btn-label waves-effect right waves-light rounded-pill btn-sm'>
                                        <i class='ri-mail-send-line label-icon align-middle rounded-pill fs-16 ms-2'></i>
                                    View Message
                                    </button></td>";
                    StrContact += @"<tr>
                                        <td>" + (i + 1) + @"</td>
                                        <td>" + contact[i].Name + @"</td>
                                        <td>" + contact[i].EamilSubject + @"</td>
                                        <td><a href='mailto:" + contact[i].EmailId + "'>" + contact[i].EmailId + @"</a></td>
                                        <td><a href='tel:" + contact[i].PhoneNo + "'>" + contact[i].PhoneNo + @"</a></td>
                                         " + Message + @"
                                         <td>" + contact[i].AddedOn.ToString("dd/MMM/yyyy") + @"</td>
                                        <td class='text-center'> 
                                         <a href='javascript:void(0);' class='bs-tooltip deleteItem warning confirm link-danger' data-id='" + contact[i].Id + @"' data-toggle='tooltip' data-placement='top' title='' data-original-title='Delete'>
                                              <i class='mdi mdi-trash-can-outline fs-18'></i></a> </td>
                                            </tr>";
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllContact", ex.Message);

        }
    }

    [WebMethod(EnableSession = true)]
    public static string Delete(string id)
    {
        string x = "";
        try
        {
            SqlConnection conGV = new SqlConnection(ConfigurationManager.ConnectionStrings["conSQ"].ConnectionString);
            ContactUs BD = new ContactUs();
            BD.Id = Convert.ToInt32(id);
            BD.AddedOn = TimeStamps.UTCTime();
            BD.AddedIp = CommonModel.IPAddress();
            int exec = ContactUs.DeleteContact(conGV, BD);
            if (exec > 0)
            {
                x = "Success";
            }
            else
            {
                x = "W";
            }

        }
        catch (Exception ex)
        {
            x = "W";
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Delete", ex.Message);
        }
        return x;
    }
    [WebMethod(EnableSession = true)]
    public static string GetContactMessage(string id)
    {
        var message = "";
        try
        {
            SqlConnection conSQ = new SqlConnection(ConfigurationManager.ConnectionStrings["conSQ"].ConnectionString);
            message = ContactUs.GetMessageById(conSQ, id);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetContactMessage", ex.Message);
        }
        return message;
    }

}