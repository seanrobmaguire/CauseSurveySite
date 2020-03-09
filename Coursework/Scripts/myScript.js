$(function () {
    //Log in/signup modal window
    $("#loginBtn").on('click', function () {
        $("#loginForm")[0].reset();

        $('#signup-tab').removeClass('active');
        $('#login-tab').addClass('active');

       $('#signup-form-pane').removeClass('active show');
        $('#login-form-pane').addClass('active show');
    });
    $('#regBtn').on('click', function () {
        $("#Registration").find('input:text').val();

        $('#login-tab').removeClass('active');
        $('#signup-tab').addClass('active');

        $('#signup-form-pane').addClass('active show');
        $('#login-form-pane').removeClass('active show');

    });
    
   
    
    

    

});