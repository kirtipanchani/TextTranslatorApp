$(document).ready(function () {
    $(document).on('click', '#btnSubmit', function () {
        var languageCode = $("#languageCode").val();
        var inputText = $("#inputText").val();
        if ($.trim($("#inputText").val()) && languageCode) {
            loadershow();
            $('#inputText').removeClass('border-danger');
            hideToastMessage();
            var dataToSend = { "lngCode": languageCode, "inputText": inputText };
            dataType: "json",
                $.ajax({
                    url: '/Home/TranslationAsync',
                    data: dataToSend,
                    type: 'POST',
                    success: function (response) {
                        loaderhide();
                        if (response) {
                            var result = JSON.parse(response);
                            var translatedText = result[0].translations[0].text;
                            $('#translatedText').text(translatedText);
                        } else {
                            displayToastMessage('Something went wrong! Please contact admin');
                        }
                    },
                    error: function (request, status, errorThrown) {
                        loaderhide();
                        displayToastMessage('Something went wrong! Please contact admin');
                    }
                })
        }
        else {
            $('#inputText').addClass('border-danger');
            displayToastMessage('Please enter text!');
            $('#translatedText').text("");
        }
    });

    $('#notification-close').click(function () {
        hideToastMessage();
    });
});

function displayToastMessage(message) {
    $('#notification-msg').text(message);
    $('#notification-bar').addClass('alert-danger d-block');
}
function hideToastMessage() {
    $('#notification-msg').text('');
    $('#notification-bar').removeClass('alert-danger d-block');
}
function loadershow() {
    $("#preloader").show();
}
function loaderhide() {
    $("#preloader").hide();
}