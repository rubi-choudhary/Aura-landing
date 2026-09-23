<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Contact-requests.aspx.cs" Inherits="Admin_Contact_requests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        a {
            color: #898989;
        }

            a:hover {
                color: #1e3e7b;
            }

        .table-light {
            border-color: #1e3e7b !important;
        }

        .align-middle {
            text-align: center !important
        }

        .btn-success {
            --vz-btn-color: #fff;
            --vz-btn-bg: #1e3e7b;
            --vz-btn-border-color: #1e3e7b;
            --vz-btn-hover-color: #fff;
            --vz-btn-hover-bg: #375a9f;
            --vz-btn-hover-border-color: #375a9f;
            --vz-btn-focus-shadow-rgb: 97,211,151;
            --vz-btn-active-color: #fff;
            --vz-btn-active-bg: #375a9f;
            --vz-btn-active-border-color: #375a9f;
            --vz-btn-active-shadow: inset 0 3px 5px rgba(0, 0, 0, 0.125);
            --vz-btn-disabled-color: #fff;
            --vz-btn-disabled-bg: #1e3e7b;
            --vz-btn-disabled-border-color: #1e3e7b;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">
            <!-- start page title -->
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">Manage Contact Request</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="/Admin/">Dashboard</a></li>
                                <li class="breadcrumb-item active"><a href="javascript: void(0);">Contact Request</a></li>
                            </ol>
                        </div>

                    </div>
                </div>
            </div>
            <!-- end page title -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header align-items-center d-flex">
                            <h4 class="card-title mb-0 flex-grow-1">Manage All Contact Request</h4>
                        </div>
                        <div class="card-body">
                            <table id="alternative-pagination" class="table table-nowrap  align-middle table-striped  myTable" style="width: 100%;">
                                <thead class="table-light">
                                    <tr>
                                        <th>#</th>
                                        <th>Name</th>
                                        <th>Subject</th>
                                        <th>Email id</th>
                                        <th>Mobile No</th>
                                        <th>Description</th>
                                        <th>Added On</th>
                                        <th class='text-center'>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%=StrContact%>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="fadeInRightModal" class="modal zoomIn" tabindex="-1" aria-labelledby="fadeInRightModalLabel" style="display: none;" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="fadeInRightModalLabel">Message Information</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="table-responsive" id="Contactinfosingle">
                            </div>
                        </div>
                        <!--end col-->
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $(document.body).on('click', '.deleteItem', function () {
                var elem = $(this);
                var id = elem.attr('data-id');
                Swal.fire({
                    title: "Are you sure?",
                    text: "You won't be able to revert this!",
                    icon: "warning",
                    showCancelButton: !0,
                    confirmButtonClass: "btn btn-primary w-xs me-2 mt-2",
                    cancelButtonClass: "btn btn-danger w-xs mt-2",
                    confirmButtonText: "Yes, delete it!",
                    buttonsStyling: !1,
                    showCloseButton: !0,
                }).then(function (result) {
                    if (result.value) {
                        $.ajax({
                            type: 'POST',
                            url: "Contact-requests.aspx/Delete",
                            data: "{id: '" + id + "'}",
                            contentType: 'application/json; charset=utf-8',
                            dataType: "json",
                            async: false,
                            success: function (data2) {
                                if (data2.d.toString() == "Success") {
                                    Swal.fire({ title: "Deleted!", text: "Your file has been deleted.", icon: "success", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: false })
                                    elem.parent().parent().remove();
                                }
                                else if (data2.d.toString() == "Permission") {

                                    Swal.fire({ title: "Oops...", text: "Permission denied! Please contact to your administrator.", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                                }
                                else {
                                    Swal.fire({ title: "Oops...", text: "Something went wrong!", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                                }
                            }
                        });

                    }
                })
            });

            $(document.body).on('click', '#Viewcust', function () {
                $("#Contactinfosingle").empty();
                var elem = $(this);
                var id = elem.attr('data-vid');
                var name = elem.attr('data-vname');
                $('.modal-header .modal-title').html('Message Information - ' + name);
                $.ajax({
                    type: 'POST',
                    url: "Contact-requests.aspx/GetContactMessage",
                    data: "{id: '" + id + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: false,
                    success: function (res) {
                        var Message = res.d;
                        if (Message) {
                            var tableinfo = "<table class='table'><tbody>";
                            tableinfo += "<tr><td>" + Message + "</td></tr>";
                            tableinfo += "</tbody></table>";
                            $("#Contactinfosingle").append(tableinfo);
                        } else {
                            $("#Contactinfosingle").html("No message available.");
                        }
                    }
                });
            });
        });
    </script>
</asp:Content>

