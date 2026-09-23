using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;

public partial class Admin_Lagoon_Contact_requests : System.Web.UI.Page
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
            var contact = LagoonContactRequest.GetAllContact(conSQ);
            if (contact != null && contact.Count > 0)
            {
                for (int i = 0; i < contact.Count; i++)
                {
                    var fullName = contact[i].FirstName + " " + contact[i].LastName;
                    var Message = @"<td>
                                    <button data-bs-toggle='modal' data-bs-target='#fadeInRightModal' type='button' id='Viewcust' data-vid='" + contact[i].Id + @"' data-vname='" + fullName + @"' class='btn btn-success btn-label waves-effect right waves-light rounded-pill btn-sm'>
                                        <i class='ri-mail-send-line label-icon align-middle rounded-pill fs-16 ms-2'></i>
                                    View Message
                                    </button></td>";
                    StrContact += @"<tr>
                                        <td>" + (i + 1) + @"</td>
                                        <td>" + fullName + @"</td>
                                        <td>" + contact[i].CountryCode + @"</td>
                                        <td><a href='tel:" + contact[i].CountryCode + contact[i].Mobile + "'>" + contact[i].Mobile + @"</a></td>
                                        <td><a href='mailto:" + contact[i].EmailId + "'>" + contact[i].EmailId + @"</a></td>
                                        <td>" + contact[i].Hours + @"</td>
                                        <td>" + contact[i].Rooms + @"</td>
                                        <td>" + contact[i].EventType + @"</td>
                                        <td>" + contact[i].Guests + @"</td>
                                        <td>" + contact[i].EventDate.ToString("dd/MMM/yyyy") + @"</td>
                                        <td>" + contact[i].SourcePage + @"</td>
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
            LagoonContactRequest BD = new LagoonContactRequest();
            BD.Id = Convert.ToInt32(id);
            BD.AddedOn = TimeStamps.UTCTime();
            BD.AddedIp = CommonModel.IPAddress();
            int exec = LagoonContactRequest.DeleteContact(conGV, BD);
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
            message = LagoonContactRequest.GetMessageById(conSQ, id);
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetContactMessage", ex.Message);
        }
        return message;
    }

}