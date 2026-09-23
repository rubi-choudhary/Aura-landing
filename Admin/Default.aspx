<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Aura | Admin</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta content="Shopify" name="author" />
    <!-- App favicon -->
    <link rel="shortcut icon" href="/assets/img/favicon.png" />

    <!-- Bootstrap Css -->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Icons Css -->
    <link href="assets/css/icons.min.css" rel="stylesheet" type="text/css" />
    <!-- App Css-->
    <link href="assets/css/app.min.css" rel="stylesheet" type="text/css" />
    <!-- custom Css-->
    <link href="assets/css/custom.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="auth-page-wrapper pt-5">
            <!-- auth page bg -->
            <%--    <div class="auth-one-bg-position auth-one-bg" id="auth-particles">
                <div class="bg-overlay"></div>

                <div class="shape">
                    <svg xmlns="http://www.w3.org/2000/svg" version="1.1" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 1440 120">
                        <path d="M 0,36 C 144,53.6 432,123.2 720,124 C 1008,124.8 1296,56.8 1440,40L1440 140L0 140z"></path>
                    </svg>
                </div>
            </div>--%>
            <!-- auth page content -->
            <div class="auth-page-content">
                <div class="container">
                    <!-- end row -->
                    <div class="row justify-content-center">
                        <div class="col-md-8 col-lg-6 col-xl-5">
                            <div class="card mt-4">
                                <div class="card-body p-4">
                                    <div class="text-center mt-2">
                                        <a href="javascript:void();" class="d-inline-block auth-logo">
                                            <img src="/assets/img/auraindia-logo.png" class="rounded" alt="" height="70" />
                                        </a>
                                    </div>
                                    <div class="text-center mt-2">
                                        <h5 class="text-primary">Welcome Back !</h5>
                                        <p class="text-muted">Sign in to continue to Aura Admin</p>
                                        <asp:Label ID="lblStatus" runat="server" Style="width: 100%;" Visible="false"></asp:Label>
                                    </div>
                                    <div class="p-2 mt-4 mb-4">
                                        <div class="mb-3">
                                            <label for="username" class="form-label">User Id</label>
                                            <asp:TextBox runat="server" class="form-control" ID="txtUserName" placeholder="Enter username" />
                                            <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtUserName" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Login" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="mb-3">
                                            <div class="float-end">
                                                <a href="forgot-password.aspx" class="text-muted">Forgot password?</a>
                                            </div>
                                            <label class="form-label" for="password-input">Password</label>
                                            <div class="position-relative auth-pass-inputgroup mb-3">
                                                <asp:TextBox runat="server" TextMode="Password" class="form-control pe-5 password-input" placeholder="Enter password" ID="txtPassword" />
                                                <button class="btn btn-link position-absolute end-0 top-0 text-decoration-none text-muted shadow-none password-addon" type="button" id="password-addon"><i class="mdi mdi-eye align-middle fs-16 vispass"></i></button>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Login" ErrorMessage="Field can't be empty"></asp:RequiredFieldValidator>
                                            </div>

                                        </div>

                                        <div class="form-check">
                                            <input class="form-check-input" type="checkbox" runat="server" value="" id="chkLogKeep" />
                                            <label class="form-check-label" for="<%=chkLogKeep.ClientID %>">Remember me</label>
                                        </div>

                                        <div class="mt-4">
                                            <asp:Button runat="server" ID="btnLogin" OnClick="btnLogin_Click" class="btn btn-primary btn-primary-login w-100" ValidationGroup="Login" Text="Sign In" />
                                        </div>
                                    </div>
                                </div>
                                <!-- end card body -->
                            </div>
                            <!-- end card -->

                            <div class="mt-4 text-center">
                                <p class="mb-0">
                                    &copy; 
                                    <script>document.write(new Date().getFullYear())</script>
                                    Aura
                                </p>
                            </div>

                        </div>
                    </div>
                    <!-- end row -->
                </div>
                <!-- end container -->
            </div>
            <!-- end auth page content -->

            <!-- footer -->
            <footer class="footer">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="text-center">
                                <p class="mb-0 text-muted">
                                    Designed & Developed by Nextwebi
                           
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </footer>
            <!-- end Footer -->
        </div>
        <!-- end auth-page-wrapper -->

        <!-- JAVASCRIPT -->
        <script src="assets/libs/bootstrap/js/bootstrap.bundle.min.js"></script>
        <script src="assets/js/plugins.js"></script>
        <!-- prismjs plugin -->
        <script src="assets/libs/prismjs/prism.js"></script>

        <!-- notifications init -->
        <script src="assets/js/pages/notifications.init.js"></script>
        <!-- password-addon init -->
        <script src="assets/js/pages/password-addon.init.js"></script>
    </form>
</body>
</html>
