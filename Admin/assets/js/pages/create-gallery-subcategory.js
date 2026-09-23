$(document).ready(function () {


    $(document.body).on("change", "#ddlType", function () {
        if ($("#ddlType").val() == "Video") {
            $("#videoURL").css("display", "block");
            const dropzoneInput = document.querySelectorAll('.dz-hidden-input')
            for (const input of dropzoneInput) {
                if (input.getAttribute('multiple')) {
                    input.removeAttribute('multiple')
                }
            }

        } else {
            $("#videoURL").css("display", "none");
            const dropzoneInput = document.querySelectorAll('.dz-hidden-input')
            for (const input of dropzoneInput) {
                if (!input.getAttribute('multiple')) {
                    input.setAttribute('multiple','multiple')
                }
            }
        }
    });





    $(".txtCatName").change(function () {
        if ($(".txtCatName").val() != "" && $(".txtCatName").val() != undefined) {
            $(".txtURL").val($(".txtCatName").val().toLowerCase().replace(/\./g, '').replace(/\//g, '').replace(/\\/g, '').replace(/\*/g, '').replace(/\?/g, '').replace(/\~/g, '').replace(/\ /g, '-'));

        }
    });

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
                    url: "create-gallery-subcategory.aspx/Delete",
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


    $(document.body).on("click", "#AddToGallery", function () {


        var videoUrl = $("#videoURL").val();
        if ($("#ddlType").val() == "Video") {
            if (videoUrl == "") {
                Snackbar.show({ pos: 'top-right', text: 'Video link cannot be empty!', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });
                return;
            }
        } else {
            videoUrl = "";
        }
     

        var elem = $(this);
        elem.empty();
        elem.append("Please wait...");
        var type = $("#ddlType").val();
        var pid = $(this).attr("data-pid");
        var files = dropzone.getAcceptedFiles();
        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }
        data.append("pid", pid);
        data.append("tp", type);
        data.append("videoUrl", videoUrl);
        if (files.length == 0) {
            elem.empty();
            elem.append("Upload");

            Snackbar.show({ pos: 'top-right', text: 'Please select atleast one file to upload', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });

            return false;
        }
        $.ajax({
            url: "product-images.ashx",
            type: "POST",
            data: data,
            contentType: false,
            processData: false,
            success: function (result) {
                if (result.split('|')[0] == "Success") {
                    elem.empty();
                    elem.append("Upload");
                    Snackbar.show({ pos: 'top-right', text: 'Images added successfully.', actionTextColor: '#fff', backgroundColor: '#008a3d' });
                    dropzone.removeAllFiles();
                    $("#videoURL").val("");
                    BindGalleryImages(pid);
                    if ($("#ddlType").val() == "Video") {
                        $("#videoURL").css("display", "block");
                        const dropzoneInput = document.querySelectorAll('.dz-hidden-input')
                        for (const input of dropzoneInput) {
                            if (input.getAttribute('multiple')) {
                                input.removeAttribute('multiple')
                            }
                        }

                    } else {
                        $("#videoURL").css("display", "none");
                        const dropzoneInput = document.querySelectorAll('.dz-hidden-input')
                        for (const input of dropzoneInput) {
                            if (!input.getAttribute('multiple')) {
                                input.setAttribute('multiple', 'multiple')
                            }
                        }
                    }


                }
                else if (result.split('|')[0] == "Permission") {
                    elem.empty();
                    elem.append("Upload");
                    Snackbar.show({ pos: 'top-right', text: 'Oops! Access denied. Contact to your administrator', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });

                }
                else {
                    elem.empty();
                    elem.append("Upload");
                    Snackbar.show({ pos: 'top-right', text: 'Oops! Something went wrong. Please try after some time.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });


                }
            },
            error: function (err) {
                elem.empty();
                elem.append("Upload");
                Snackbar.show({ pos: 'top-right', text: 'Oops! Something went wrong. Please try after some time.', actionTextColor: '#fff', backgroundColor: '#ea1c1c' });

            }
        });

    });
    $(document.body).on('click', '.deleteGalleryItem', function () {
        var elem = $(this);
        var id = $(this).attr('data-id');
        var pid = $(this).attr('data-pid');
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
                    url: "create-gallery-subcategory.aspx/DeletePackageGallery",
                    data: "{id: '" + id + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: false,
                    success: function (data2) {
                        if (data2.d.toString() == "Success") {
                            Swal.fire({ title: "Deleted!", text: "Image has been deleted.", icon: "success", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: false })
                            BindGalleryImages(pid);

                        }
                        else if (data2.d.toString() == "Permission") {
                            Swal.fire({ title: "Oops...", text: "Permission denied! Please contact to your administrator.", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });

                        }
                        else {
                            Swal.fire({ title: "Oops...", text: "Something went wrong!", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });

                        }
                    },
                    error: function (err) {
                        Swal.fire({ title: "Oops...", text: "Something went wrong!", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
                    }
                });
            }
        })
    });

    $(document.body).on("click", ".AddGalleryBtn", function () {
        var id = $(this).attr("data-id");
        if (id != "") {
            $("#AddToGallery").attr("data-pid", id);
            dropzone.removeAllFiles();
            BindGalleryImages(id);
        }
    });
    $(document.body).on('click', "#UpdateImgOrder", function (e) {
        var product = "";
        var elem = $(this);
        elem.empty();
        elem.val("Please wait...")
        e.preventDefault();
        $.each($('#left-defaults').find('li'), function () {
            if (product == "") {
                product = product + $(this).attr("data-id");
            }
            else {
                product = product + "|" + $(this).attr("data-id");
            }
        });
        if (product != "") {
            ArrengeCategory(product);
        }
        else {
            elem.empty();
            elem.val("Update Image Order")
        }
    });
});

function BindGalleryImages(id) {
    $.ajax({
        type: 'POST',
        url: "create-gallery-subcategory.aspx/GetGalleryImage",
        data: "{id: '" + id + "'}",
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        async: false,
        success: function (data2) {
            var lnt = data2.d;
            var strL = "";
            var str = "";
            for (var i = 0; i < lnt.length; i++) {
                var del = "";
                var strtype = "";
                var videoIcon = "";
                if (lnt[i].Type=="Video") {
                    videoIcon = "<span class='VideoIcon'><i class='mdi mdi-video-marker-outline'></i></span>";
                }

                strtype = "<img src='/" + lnt[i].Images + "' class='img-responsive' style='max-width:100%;' />";
                del += "<a href='javascript:void(0);' class='bs-tooltip btn btn-danger  deleteGalleryItem mt-3' data-id='" + lnt[i].Id + "' data-pid='" + lnt[i].SubCatId + "' data-toggle='tooltip' data-placement='top' title='' data-original-title='Delete'>";
                del += "Delete <svg xmlns='http://www.w3.org/2000/svg' width='20' height='24' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round' class='feather feather-trash-2'><polyline points='3 6 5 6 21 6'></polyline><path d='M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2'></path><line x1='10' y1='11' x2='10' y2='17'></line><line x1='14' y1='11' x2='14' y2='17'></line></svg></a>";
                str = str + "<li data-id='" + lnt[i].Id + "' class='ui-state-default media  d-md-flex d-block text-sm-left text-end col-md-6'><div class='maindiv'>" + strtype + videoIcon + "<div><span>" + del + "</span></div></div></li>";

                //str = str + "<div class='col-md-6 text-end'><div class='maindiv'>" + strtype + "<div><span>" + del + "</span></div></div></div>";
            }
            $("#left-defaults").html(str);
        }
    });
}

function ArrengeCategory(product) {
    var parameter = { "id": product };

    $.ajax({
        type: 'POST',
        url: "create-gallery-subcategory.aspx/ImageOrderUpdate",
        data: JSON.stringify(parameter),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",


        success: function (data2) {
            if (data2.d.toString() == "Success") {
                $("#UpdateImgOrder").val("Update Image Order");
                Snackbar.show({ pos: 'top-right', text: 'Images arranged successfully.', actionTextColor: '#fff', backgroundColor: '#008a3d' });
                BindGalleryImages($("#AddToGallery").attr("data-pid"));


            }
            else if (data2.d.toString() == "Permission") {
                Swal.fire({ title: "Oops...", text: "Permission denied! Please contact to your administrator.", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });

            }
            else {
                Swal.fire({ title: "Oops...", text: "Something went wrong!", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
            }
        },
        error: function (err) {
            Swal.fire({ title: "Oops...", text: "Something went wrong!", icon: "error", confirmButtonClass: "btn btn-primary w-xs mt-2", buttonsStyling: !1, footer: '', showCloseButton: !0 });
        }

    });
}