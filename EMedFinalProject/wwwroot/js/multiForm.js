$(document).ready(function () {
    //When Next Button is clicked for the Customer Details


    //1st Step Select Branch
    $('#btn_branch_details').click(function () {
        var error_branch = '';
        
        //Validate prescription Field
        if ($.trim($('#branchSelect').val()).length == 0) {
            error_branch = 'Choosing a branch is required';
            $('#error_branch').text(error_branch);
            $('#customFile').addClass('has-error');
        } else {
            error_branch = '';
            $('#error_branch').text(error_branch);
            $('#customFile').removeClass('has-error');
        }

        if (error_branch != '') {
            return false;
        } else {
            $('#list_branch_details').removeClass('active active_tab1');
            $('#list_branch_details').removeAttr('href data-toggle');
            $('#branch_details').removeClass('active');
            $('#list_branch_details').addClass('inactive_tab1');
            $('#list_login_details').removeClass('inactive_tab1');
            $('#list_login_details').addClass('active_tab1 active');
            $('#list_login_details').attr('href', '#contact_details');
            $('#list_login_details').attr('data-toggle', 'tab');
            $('#login_details').addClass('active in');
        }
    });

    $('#previous_btn_customer_details').click(function () {
        $('#list_login_details').removeClass('active active_tab1');
        $('#list_login_details').removeAttr('href data-toggle');
        $('#login_details').removeClass('active in');
        $('#list_login_details').addClass('inactive_tab1');
        $('#list_branch_details').removeClass('inactive_tab1');
        $('#list_branch_details').addClass('active_tab1 active');
        $('#list_branch_details').attr('href', '#personal_details');
        $('#list_branch_details').attr('data-toggle', 'tab');
        $('#branch_details').addClass('active in');
    });
    $('#btn_customer_details').click(function () {

        var error_lastName = '';
        var error_contactNo = '';
        var error_delivAdd = '';
        var error_email = '';
        var filter = /^[A-Za-z]+$/;
        var filterContactNo = /^(09|\+639)\d{9}$/;
        var filterEmail = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;

        //Validation for LastName
        if ($.trim($('#lName').val()).length == 0) {
            error_lastName = 'Last Name is required';
            $('#error_lastName').text(error_lastName);
            $('#lName').addClass('has-error');
        } else {
            if (!filter.test($('#lName').val())) {
                error_lastName = 'Invalid Last Name';
                $('#error_lastName').text(error_lastName);
                $('#lName').addClass('has-error');
            } else {
                error_lastName = '';
                $('#error_lastName').text(error_lastName);
                $('#lName').removeClass('has-error');
            }
        }

        //Validation for FirstName 
        if ($.trim($('#fName').val()).length == 0) {
            error_firstName = 'First Name is required';
            $('#error_firstName').text(error_firstName);
            $('#fName').addClass('has-error');
        } else {
            if (!filter.test($('#fName').val())) {
                error_firstName = 'Invalid First Name';
                $('#error_firstName').text(error_firstName);
                $('#fName').addClass('has-error');
            } else {
                error_firstName = '';
                $('#error_firstName').text(error_firstName);
                $('#fName').removeClass('has-error');
            }
        }

        //Validation for Contact No
        if ($.trim($('#contactNo').val()).length == 0) {
            error_contactNo = 'Contact No is required';
            $('#error_contactNo').text(error_contactNo);
            $('#contactNo').addClass('has-error');
        } else {
            if (!filterContactNo.test($('#contactNo').val())) {
                error_contactNo = 'Invalid Contact No';
                $('#error_contactNo').text(error_contactNo);
                $('#contactNo').addClass('has-error');
            } else {
                error_contactNo = '';
                $('#error_contactNo').text(error_contactNo);
                $('#contactNo').removeClass('has-error');
            }
        }

        //Validation for Email
        if ($.trim($('#email').val()).length == 0) {
            error_email = 'Email is required';
            $('#error_email').text(error_email);
            $('#email').addClass('has-error');
        } else {
            if (!filterEmail.test($('#email').val())) {
                error_email = 'Invalid Email Address';
                $('#error_email').text(error_email);
                $('#email').addClass('has-error');
            } else {
                error_email = '';
                $('#error_email').text(error_email);
                $('#email').removeClass('has-error');
            }
        }

        //Validation for DeliveryAddress
        if ($.trim($('#delivAdd').val()).length == 0) {
            error_delivAdd = 'Delivery Address is required';
            $('#error_delivAdd').text(error_delivAdd);
            $('#delivAdd').addClass('has-error');
        } else {
            error_delivAdd = '';
            $('#error_delivAdd').text(error_delivAdd);
            $('#delivAdd').removeClass('has-error');
        }


        if (error_lastName != '' || error_firstName != '' || error_contactNo != '' || error_email != '' || error_delivAdd != '') {
            return false;
        } else {
            $('#list_login_details').removeClass('active active_tab1');
            $('#list_login_details').removeAttr('href data-toggle');
            $('#login_details').removeClass('active');
            $('#list_login_details').addClass('inactive_tab1');
            $('#list_personal_details').removeClass('inactive_tab1');
            $('#list_personal_details').addClass('active_tab1 active');
            $('#list_personal_details').attr('href', '#personal_details');
            $('#list_personal_details').attr('data-toggle', 'tab');
            $('#personal_details').addClass('active in');
        }
    });

    $('#previous_btn_order_details').click(function () {
        $('#list_personal_details').removeClass('active active_tab1');
        $('#list_personal_details').removeAttr('href data-toggle');
        $('#personal_details').removeClass('active in');
        $('#list_personal_details').addClass('inactive_tab1');
        $('#list_login_details').removeClass('inactive_tab1');
        $('#list_login_details').addClass('active_tab1 active');
        $('#list_login_details').attr('href', '#login_details');
        $('#list_login_details').attr('data-toggle', 'tab');
        $('#login_details').addClass('active in');
    });

  
    //2nd Step Order Items
    $('#btn_order_details').click(function () {
        var error_ProductName = '';
        var error_quantity = '';
        var error_milligrams = '';
        var error_estPrice = '';

        //validate Product Field
        if ($.trim($('#paramFieldName').val()).length == 0) {
            error_ProductName = 'Product is required';
            $('#error_ProductName').text(error_ProductName);
            $('#paramFieldName').addClass('has-error');
        } else {
            error_ProductName = '';
            $('#error_ProductName').text(error_ProductName);
            $('#paramFieldName').removeClass('has-error');
        }

        //Validate Quantity Field
        if ($.trim($('#paramFieldValue').val()).length == 0) {
            error_quantity = 'Quantity is required';
            $('#error_quantity').text(error_quantity);
            $('#paramFieldValue').addClass('has-error');
        } else {
            if (($('#paramFieldValue').val() > 10)) {
                error_quantity = 'Maximum of 10 items';
                $('#error_quantity').text(error_quantity);
                $('#paramFieldValue').addClass('has-error');
            } else {
                error_quantity = '';
                $('#error_quantity').text(error_quantity);
                $('#paramFieldValue').removeClass('has-error');
            }
           
        }

        //Validate Milligrams Field
        if ($.trim($('#paramMilValue').val()).length == 0) {
            error_milligrams = 'Milligrams is required';
            $('#error_milligrams').text(error_milligrams);
            $('#paramMilValue').addClass('has-error');
        } else {
            error_milligrams = '';
            $('#error_milligrams').text(error_milligrams);
            $('#paramMilValue').removeClass('has-error');
        }

        //Validate Quantity Field
        if ($.trim($('#estPrice').val()).length == 0) {
            error_estPrice = 'Estimated Price is required';
            $('#error_estPrice').text(error_estPrice);
            $('#estPrice').addClass('has-error');
        } else {
            if (($('#estPrice').val() > 2000.00)) {
                error_estPrice = 'Maximum of 2000 Pesos';
                $('#error_estPrice').text(error_estPrice);
                $('#estPrice').addClass('has-error');
            } else {
                error_estPrice = '';
                $('#error_estPrice').text(error_estPrice);
                $('#estPrice').removeClass('has-error');
            }

        }
        if (error_ProductName != '' || error_quantity != '' || error_milligrams != '' || error_estPrice != '') {
            return false;
        } else {
            $('#list_personal_details').removeClass('active active_tab1');
            $('#list_personal_details').removeAttr('href data-toggle');
            $('#personal_details').removeClass('active');
            $('#list_personal_details').addClass('inactive_tab1');
            $('#list_contact_details').removeClass('inactive_tab1');
            $('#list_contact_details').addClass('active_tab1 active');
            $('#list_contact_details').attr('href', '#contact_details');
            $('#list_contact_details').attr('data-toggle', 'tab');
            $('#contact_details').addClass('active in');
        }
    });

    $('#previous_btn_upload_details').click(function () {
        $('#list_contact_details').removeClass('active active_tab1');
        $('#list_contact_details').removeAttr('href data-toggle');
        $('#contact_details').removeClass('active in');
        $('#list_contact_details').addClass('inactive_tab1');
        $('#list_personal_details').removeClass('inactive_tab1');
        $('#list_personal_details').addClass('active_tab1 active');
        $('#list_personal_details').attr('href', '#personal_details');
        $('#list_personal_details').attr('data-toggle', 'tab');
        $('#personal_details').addClass('active in');
    });

    //4th Step Upload Items
    $('#btn_upload_details').click(function () {
        var error_prescription = '';
        var error_validId = '';

        //Validate prescription Field
        if ($.trim($('#customFile').val()).length == 0) {
            error_prescription = 'Uploading of Prescription is required';
            $('#error_prescription').text(error_prescription);
            $('#customFile').addClass('has-error');
        } else {
            error_prescription = '';
            $('#error_prescription').text(error_prescription);
            $('#customFile').removeClass('has-error');
        }

        //Validate valid ID Field
        if ($.trim($('#validIdFile').val()).length == 0) {
            error_validId = 'Uploading of Valid ID is required';
            $('#error_validId').text(error_validId);
            $('#validIdFile').addClass('has-error');
        } else {
            error_validId = '';
            $('#error_validId').text(error_validId);
            $('#validIdFile').removeClass('has-error');
        }


        if (error_prescription != '' || error_validId != '') {
            return false;
        } else {
            $('#list_contact_details').removeClass('active active_tab1');
            $('#list_contact_details').removeAttr('href data-toggle');
            $('#contact_details').removeClass('active');
            $('#list_contact_details').addClass('inactive_tab1');
            $('#list_payment_details').removeClass('inactive_tab1');
            $('#list_payment_details').addClass('active_tab1 active');
            $('#list_payment_details').attr('href', '#contact_details');
            $('#list_payment_details').attr('data-toggle', 'tab');
            $('#payment_details').addClass('active in');
        }
    });

    $('#previous_btn_payment_details').click(function () {
        $('#list_payment_details').removeClass('active active_tab1');
        $('#list_payment_details').removeAttr('href data-toggle');
        $('#payment_details').removeClass('active in');
        $('#list_payment_details').addClass('inactive_tab1');
        $('#list_contact_details').removeClass('inactive_tab1');
        $('#list_contact_details').addClass('active_tab1 active');
        $('#list_contact_details').attr('href', '#personal_details');
        $('#list_contact_details').attr('data-toggle', 'tab');
        $('#contact_details').addClass('active in');
    });

    //5th Step Select Payment Method
    $('#btn_payment_details').click(function () {
        var error_paymentMethod = '';

        //Validate prescription Field
        if ($.trim($('#paymentMethod').val()).length == 0) {
            error_paymentMethod = 'Choosing of Payment Method is Required';
            $('#error_paymentMethod').text(error_paymentMethod);
            $('#paymentMethod').addClass('has-error');
        } else {
            error_paymentMethod = '';
            $('#error_paymentMethod').text(error_paymentMethod);
            $('#paymentMethod').removeClass('has-error');
        }

        if (error_paymentMethod != '') {
            return false;
        } else {
            $('#list_payment_details').removeClass('active active_tab1');
            $('#list_payment_details').removeAttr('href data-toggle');
            $('#payment_details').removeClass('active');
            $('#list_payment_details').addClass('inactive_tab1');
            $('#list_confirmation_details').removeClass('inactive_tab1');
            $('#list_confirmation_details').addClass('active_tab1 active');
            $('#list_confirmation_details').attr('href', '#contact_details');
            $('#list_confirmation_details').attr('data-toggle', 'tab');
            $('#confirm_details').addClass('active in');
        }
    });

    $('#previous_btn_confirm_details').click(function () {
        $('#list_confirmation_details').removeClass('active active_tab1');
        $('#list_confirmation_details').removeAttr('href data-toggle');
        $('#confirm_details').removeClass('active in');
        $('#list_confirmation_details').addClass('inactive_tab1');
        $('#list_payment_details').removeClass('inactive_tab1');
        $('#list_payment_details').addClass('active_tab1 active');
        $('#list_payment_details').attr('href', '#personal_details');
        $('#list_payment_details').attr('data-toggle', 'tab');
        $('#payment_details').addClass('active in');
    });

    ////6th Step Submission
    //$('#btn_confirm_details').click(function () {

    //    $('#btn_confirm_details').attr("disabled", "disabled");
    //    $(document).css('cursor', 'prgress');
    //    $("#order_form").submit();
    //});

});
