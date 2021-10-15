function ReloadEvents(id) {
    console.log("in function")
    var url = '/Appointment/ReloadAppointmentDetail/' + id;
    jQuery.ajax({
        url: url,
        success: function (data) {
            document.getElementById('component').innerHTML = data;
        }
    })
}