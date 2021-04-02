var currentTab = 0; // Current tab is set to be the first tab (0)
var sub = document.getElementById("nextBtn");
showTab(currentTab); // Display the current tab

function showTab(n) {
    // This function will display the specified tab of the form...
    var x = document.getElementsByClassName("tab");
    x[n].style.display = "block";
    //... and fix the Previous/Next buttons:
    if (n == 0) {
        document.getElementById("prevBtn").style.display = "none";
    } else {
        document.getElementById("prevBtn").style.display = "inline";
    }
    if (n == (x.length - 1)) {
        sub.innerHTML = "Submit";
        sub.style.backgroundColor = "#00e600";

    } else {
        sub.innerHTML = "Next"
        sub.style.backgroundColor = "#ff1a1a";

    }
    //... and run a function that will display the correct step indicator:
    fixStepIndicator(n)
}

function nextPrev(n) {
    // This function will figure out which tab to display
    var x = document.getElementsByClassName("tab");
    // Exit the function if any field in the current tab is invalid:
    if (n == 1 && !validateForm()) return false;
    // Hide the current tab:
    x[currentTab].style.display = "none";
    // Increase or decrease the current tab by 1:
    currentTab = currentTab + n;
    // if you have reached the end of the form...
    if (currentTab >= x.length) {
        // ... the form gets submitted:
        document.getElementById("regForm").submit();
        return false;
    }
    // Otherwise, display the correct tab:
    showTab(currentTab);
}

function validateForm() {
    // This function deals with validation of the form fields
    var x, y, i, valid = true;
    x = document.getElementsByClassName("tab");
    y = x[currentTab].getElementsByTagName("input");
    // A loop that checks every input field in the current tab:
    for (i = 0; i < y.length; i++) {
        // If a field is empty...
        if (y[i].value == "") {
            // add an "invalid" class to the field:
            y[i].className += " invalid";
            // and set the current valid status to false
            valid = false;
        }
    }
    // If the valid status is true, mark the step as finished and valid:
    if (valid) {
        document.getElementsByClassName("step")[currentTab].className += " finish";
    }
    return valid; // return the valid status
}

function fixStepIndicator(n) {
    // This function removes the "active" class of all steps...
    var i, x = document.getElementsByClassName("step");
    for (i = 0; i < x.length; i++) {
        x[i].className = x[i].className.replace(" active", "");
    }
    //... and adds the "active" class on the current step:
    x[n].className += " active";
}

//Start of order function dynamic
$(document).ready(function () {
    var max_fields = 10; //maximum input boxes allowed
    var wrapper = $(".input_fields_wrap"); //Fields wrapper
    var add_button = $(".add_field_button"); //Add button ID

    var x = 1; //initlal text box count
    $(add_button).click(function (e) { //on add input button click
        e.preventDefault();
        if (x < max_fields) { //max input box allowed
            x++; //text box increment
            $(wrapper).append('<div class="input_fields_wrap"><div class="row"><div class="col-sm-6" style="margin-top: 15px;"> Product Name: <input type = "text" placeholder = "Enter Product" id = "" class= "form-control" name = "mytext[]" style = "padding: 10px; width: 100%; font-size: 14px; border: 1px solid #aaaaaa;" > </div><div class="col-sm-3" style="margin-top: 15px;">Quantity: <input type="number" placeholder="Quantity" id="" class="form-control" name="mytext[]" style="padding: 10px; width: 100%; font-size: 14px; border: 1px solid #aaaaaa;"> </div><div class="col-sm-3" style="margin-top: 15px;">Miligrams: <input type="number" placeholder="Miligrams" id="" class="form-control" name="mytext[]" style=" padding: 10px; width: 100%; font-size: 14px; border: 1px solid #aaaaaa;"></div></div><button class="remove_field" style="background: linear-gradient(to right, rgb(234, 30, 36) 0%, rgb(234, 30, 36) 100%); border: 0; box-shadow: none; padding: 7px 30px; min-width: 100px; border-radius: 20px; color: var(--white); font-weight: 500; margin-bottom: 20px; margin-top:5px;">Remove Product</button></div>'); //add input box
        }
    });

    $(wrapper).on("click", ".remove_field", function (e) { //user click on remove text
        e.preventDefault();
        $(this).parent('div').remove();
        x--;
    })
});
