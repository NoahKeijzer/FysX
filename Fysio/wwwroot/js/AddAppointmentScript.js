function dateChangedScript() {
    var date = document.getElementById('datepicker')?.value;
    var patientId = document.getElementById('patient')?.value;
    var treatorId = document.getElementById('treator')?.value;
    var obj = { treatorEmail: treatorId, patientId: patientId, date: date };
    console.log(obj);
    if (date != undefined && patientId != undefined && treatorId != undefined && date != "" && patientId != "" && treatorId != "") {
        AjaxCall('/Appointment/GetPossibleAppointmentTimes', obj, 'POST').done(function (response) {
            if (response.length > 0) {
                $('#timelist').html('');
                var options = '';
                options += '<option value="Select">Selecteer een optie...</option>';
                for (var i = 0; i < response.length; i++) {
                    options += '<option value="' + response[i] + '">' + response[i] + '</option>';
                }
                $('#timelist').append(options);

            } else {
                $('#timelist').html('');
                var options = '';
                options += '<option value="Select">Geen moment beschikbaar...</option>';
                $('#timelist').append(options);
            }
        }).fail(function (error) {
            alert(error.StatusText);
        });
    }
}

function AjaxCall(url, data, type) {
    url = url + '/' + data.treatorEmail + '/' + data.patientId + '/' + data.date;
    console.log(url);

    return jQuery.ajax({ url: url, type: type, contentType: 'application/json' });
}