<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="add-blog.aspx.cs" Inherits="Admin_add_blog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content">
        <div class="container-fluid">

            <!-- start page title -->
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                        <h4 class="mb-sm-0">Add New Blog</h4>

                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="/Admin/">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0);">Blogs</a></li>
                                <li class="breadcrumb-item active"><%=Request.QueryString["id"] == null ? "Add" : "Update" %> Blog</li>
                            </ol>
                        </div>

                    </div>
                </div>
            </div>
            <!-- end page title -->

            <div class="row">
                <div class="col-lg-8">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-6 mb-3">
                                    <label class="form-label" for="project-title-input">Blog Title<sup style="color: red;">*</sup></label>
                                    <%--<input type="text" class="form-control" id="project-title-input" placeholder="Enter project title">--%>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2 txtName" ID="txtBlogTitle" placeholder="Enter Blog title" />
                                    <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtBlogTitle" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-lg-6 mb-3">
                                    <label class="form-label" for="project-title-input">Blog Url<sup style="color: red;">*</sup></label>
                                    <%--<input type="text" class="form-control" id="project-title-input" placeholder="Enter project title">--%>
                                    <asp:TextBox runat="server" ID="txtUrl" CssClass="form-control txtUrl" PlaceHolder="Blog Url"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtUrl" ForeColor="Red" ErrorMessage="Field can't be empty" ID="RequiredFieldValidator1" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-lg-4 mb-3">
                                    <label for="datepicker-postedOn-input" class="form-label">PostedOn<sup style="color: red">*</sup></label>
                                    <asp:TextBox runat="server" ID="txtPostedOn" CssClass="form-control datepicker" PlaceHolder="Posted On"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtPostedOn" ForeColor="Red" ErrorMessage="Field can't be empty" ID="RequiredFieldValidator2" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-4 mb-3">
                                    <label class="form-label" for="project-Tag-input">Posted By<sup style="color: red;">*</sup></label>
                                    <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2 alphaonly" ID="txtPostedBy" placeholder="Posted By" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPostedBy" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                                  <div class="col-lg-4 mb-3">
      <label class="form-label" for="project-Tag-input">Tag<sup style="color: red;">*</sup></label>
      <asp:TextBox runat="server" MaxLength="100" class="form-control mb-2 mr-sm-2 alphaonly" ID="txtTag" placeholder="Tag" />
      <asp:RequiredFieldValidator ID="RequiredFieldValidato" runat="server" ControlToValidate="txtTag" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
  </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 mb-3">
                                    <label>Short Description<sup style="color: red;">*</sup></label>
                                    <asp:TextBox runat="server" class="form-control mb-2 mr-sm-2" ID="txtShortDesc" TextMode="MultiLine" />
                                    <asp:RequiredFieldValidator ID="req2" runat="server" Style="color: Red;" ValidationGroup="Price" ControlToValidate="txtShortDesc" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12 mb-3">
                                    <label>Full Description<sup style="color: red">*</sup></label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" class="form-control mb-2 mr-sm-2 summernote" ID="Txtfuldesc" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="Txtfuldesc" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Save" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                </div>
                            </div>


                        </div>

                        <!-- end card body -->
                    </div>
                    <div class="row mb-3">
                        <div class="text-start mb-1">
                            <p style="color: red; font-weight: bold;">Note : <sup>*</sup> are required fields</p>
                        </div>
                        <div class="text-start mb-4">
                            <div>
                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" CssClass="btn btn-primary waves-effect waves-light " OnClientClick="tinyMCE.triggerSave(false,true);" OnClick="btnSave_Click" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-outline-primary waves-effect waves-light" OnClick="btnClear_Click" />
                                <asp:Label ID="lblThumb" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblDetailImg" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>

                </div>
                <!-- end col -->
                <div class="col-lg-4">

                    <div class="card">
                        <div class="card-header">
                            <h5 class="card-title mb-0">SEO</h5>
                        </div>
                        <div class="card-body">
                            <div class="mb-3">
                                <label for="choices-PageTitle-input" class="form-label">Page Title</label>
                                <div class="col-lg-12">
                                    <asp:TextBox runat="server" ID="txtPageTitle" data-id="Title" CssClass="form-control textcount1" Placeholder="Page Title"></asp:TextBox>
                                    <span></span>
                                </div>
                            </div>

                            <div class="mb-3">
                                <label for="choices-MetaKey-input" class="form-label">Meta Key</label>
                                <div class="col-lg-12">
                                    <asp:TextBox runat="server" ID="txtMetakey" data-id="MetaKey" CssClass="form-control textcount1" PlaceHolder="Meta Key"></asp:TextBox>
                                    <span></span>
                                </div>
                            </div>

                            <div class="mb-3">
                                <label for="choices-Metadesc-input" class="form-label">Meta Description</label>
                                <div class="col-lg-12">
                                    <asp:TextBox runat="server" ID="txtMetaDesc" data-id="MetaDesc" CssClass="form-control textcount1" TextMode="MultiLine" Placeholder="Meta Description"></asp:TextBox>
                                    <span></span>
                                </div>
                            </div>
                        </div>
                        <!-- end card body -->
                    </div>
                    <!-- end card -->

                    <div class="card">
                        <div class="card-header">
                            <h5 class="card-title mb-0">Upload Images</h5>
                        </div>
                        <div class="card-body">
                            <div class="mb-4">
                                <label class="form-label" for="project-thumbnail-img">Thumbnail Image<sup style="color: red;">*</sup></label>
                                <asp:FileUpload runat="server" class="form-control mb-2 mr-sm-2" ID="FileUpload1" />
                                <small style="color: red;">Image format .png, .jpeg, .jpg, .webp with 600px w X 400px h</small><br />
                                <%=strThumbImage %>
                            </div>
                            <div class="mb-4">
                                <label class="form-label" for="project-thumbnail-img">Detail Image<sup style="color: red;">*</sup></label>
                                <asp:FileUpload runat="server" class="form-control mb-2 mr-sm-2" ID="FileUpload2" /><br />
                                <small style="color: red;">Image format .png, .jpeg, .jpg, .webp with 1200px w X 800px h</small>
                                <%=strDetailImg %>
                            </div>
                        </div>
                        <!-- end card body -->
                    </div>
                    <!-- end card -->
                </div>
                <!-- end col -->
            </div>
            <!-- end row -->
        </div>
    </div>
    <script src="assets/js/jquery-3.6.0.min.js"></script>
    <script src="assets/js/pages/view-categories.js"></script>

    <script>
        $(document).ready(function () {
            $(".txtName").change(function () {
                $(".txtUrl").val($(".txtName").val().toLowerCase().replace(/\./g, '').replace(/\//g, '').replace(/\\/g, '').replace(/\*/g, '').replace(/\?/g, '').replace(/\~/g, '').replace(/\ /g, '-'));
            });

            $('.alphaonly').bind('keyup blur', function () {
                var node = $(this);
                node.val(node.val().replace(/[^A-Za-z_\s]/, ''));
            }   // (/[^a-z]/g,''
            );

            $('.mobileNumberOnly').keypress(function (event) {

                var charCode = (e.which) ? e.which : event.keyCode

                if (String.fromCharCode(charCode).match(/[^0-9 +]/g))

                    return false;
            });

            $(".textcount1").on('keyup', function (event) {
                var elem = $(this);
                var tps = elem.attr("data-id");
                var len = elem.val().length;
                elem.siblings('span').text("Character count : " + len);
                if (tps === "Title") {
                    if (len > 100) {
                        elem.siblings('span').css("color", "red");
                    }
                    else {
                        elem.siblings('span').css("color", "green");
                    }
                }
                else if (tps === "MetaDesc") {
                    if (len > 160) {
                        elem.siblings('span').css("color", "red");
                    }
                    else {
                        elem.siblings('span').css("color", "green");
                    }
                }
                else if (tps === "MetaKey") {
                    if (len > 160) {
                        elem.siblings('span').css("color", "red");
                    }
                    else {
                        elem.siblings('span').css("color", "green");
                    }
                }
                else if (tps === "ShortDesc") {
                    if (len > 250) {
                        elem.siblings('span').css("color", "red");
                    }
                    else {
                        elem.siblings('span').css("color", "green");
                    }
                }
            });
        });
    </script>


</asp:Content>

