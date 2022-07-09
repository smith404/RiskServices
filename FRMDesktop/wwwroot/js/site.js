function hideAlerts() {
    $('#success-alert').hide();
    $('#error-alert').hide();
};

function showSucess() {
    $('#success-alert').show();
    setTimeout(function () {
        $('#success-alert').hide();
    }, 2000);
};

function showFail() {
    $('#success-alert').show();
    setTimeout(function () {
        $('#error-alert').hide();
    }, 2000);
};

function _success(response) {
    showSucess();
}

function _error(response) {
    showFail();
    console.log(response);
}

